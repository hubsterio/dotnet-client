using Hubster.Auth;
using Hubster.Direct;
using Hubster.Direct.Enums;
using Hubster.Direct.Models;
using Hubster.Direct.Models.Direct;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;

namespace TestHarnessBusiness
{
    public static class AgentPlayground
    {
        // agent metadata         
        private static readonly string _agentIntegrationId = "00000000-0000-0000-0000-000000000021";
        private static readonly string _agentHubId = "00000000-0000-0000-0000-0000000000a2";
        private static EstablishedConversationModel _lastConverstion = null;

        #region display methods
        private static void DisplaySimpleText(string text, ConsoleColor color = ConsoleColor.White)
        {
            var lastColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ForegroundColor = lastColor;
        }

        private static void Display(string message, ConsoleColor color = ConsoleColor.White)
        {
            var lastColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.WriteLine();
            Console.ForegroundColor = lastColor;
        }
        private static void Display(DirectActivityModel activity)
        {
            var color = ConsoleColor.Yellow;
            switch (activity.Sender.IntegrationType)
            {
                case IntegrationType.Agent:
                    color = ConsoleColor.Cyan;
                    break;

                case IntegrationType.Hubster:
                    color = ConsoleColor.Green;
                    break;

                case IntegrationType.Bot:
                    color = ConsoleColor.White;
                    break;
            }

            var activityMessage = JsonConvert.SerializeObject(activity, Formatting.Indented);

            Display(activityMessage, color);
        }

        private static void Display(ApiResponse response)
        {
            if (response.Errors != null)
            {
                var issues = JsonConvert.SerializeObject(response.Errors, Formatting.Indented);
                Display(issues, ConsoleColor.Yellow);
            }
        }

        private static void DisplayList(HubsterAuthorizer authorizer, HubsterDirectClientBusiness client, EstablishedConversationModel conversation)
        {
            var apiResponse = client.Activity.Get(authorizer, conversation.ConversationId.Value, 0, IntegrationType.Agent);
            if (apiResponse.StatusCode != HttpStatusCode.OK)
            {
                var issues = JsonConvert.SerializeObject(apiResponse.Errors, Formatting.Indented);
                Display(issues, ConsoleColor.Yellow);
                return;
            }

            Console.Clear();

            if (apiResponse.Content.Any())
            {
                foreach (var activity in apiResponse.Content)
                {
                    Display(activity);
                }
            }
            else
            {
                Display("There are no interactions to display.", ConsoleColor.Yellow);
            }
        }
        #endregion display methods

        private static string GetUserName(EstablishedConversationModel converstion)
        {
            var username = (string)null;
            if (converstion != null)
            {
                username = converstion.Properties.Profile["FullName"];
            }

            return username;
        }

        private static bool PickAvailableConversaton(HubsterAuthorizer authorizer, HubsterDirectClientBusiness client)
        {
            var apiResponse = client.Conversation.GetAllEstablishedByHubId(authorizer, Guid.Parse(_agentHubId));
            if(apiResponse.StatusCode != HttpStatusCode.OK)
            {
                Console.WriteLine();
                Display(apiResponse);
                Console.WriteLine();
                return false;
            }

            if (apiResponse.Content.Any())
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Pick one of the following established conversations:");

                    var i = 1;
                    foreach (var conversation in apiResponse.Content)
                    {
                        Console.WriteLine($"{i++}. {conversation.ConversationId} - {GetUserName(conversation)}");
                    }

                    Console.WriteLine($"{i}. Quit (q or quit)");

                    var selection = Console.ReadLine().ToLower();

                    if (string.IsNullOrWhiteSpace(selection))
                    {
                        Console.Clear();
                        continue;
                    }

                    if (selection == i.ToString()
                    || selection == "q"
                    || selection == "quit")
                    {
                        Console.Clear();
                        return false;
                    }

                    if (int.TryParse(selection, out int result) == false)
                    {
                        Console.Clear();
                        continue;
                    }

                    if (result < 0 || result > i)
                    {
                        Console.Clear();
                        continue;
                    }

                    _lastConverstion = apiResponse.Content.ToList()[result - 1];
                    return true;
                }
            }
            else
            {
                Display("There are no established conversations to display.", ConsoleColor.Yellow);
                return false;
            }
        }

        private static bool Commands(HubsterAuthorizer authorizer, HubsterDirectClientBusiness client)
        {
            Console.Clear();

            while (true)
            {
                var lastUser = GetUserName(_lastConverstion);

                Console.WriteLine($"Select a commands:");
                Console.WriteLine($"1. List available conversations.");
                Console.Write($"2. Start chatting with ");
                DisplaySimpleText($"'{lastUser ?? "You must first select a conversation!"}'", ConsoleColor.Yellow);
                Console.WriteLine();
                Console.WriteLine($"3. List interactions.");
                Console.WriteLine($"4. Quit (q or quit).");

                var selection = Console.ReadLine().ToLower();

                if (string.IsNullOrWhiteSpace(selection))
                {
                    Console.Clear();
                    continue;
                }

                if (selection == "4"
                || selection == "q"
                || selection == "quit")
                {
                    return false;
                }

                if (int.TryParse(selection, out int result) == false)
                {
                    Console.Clear();
                    continue;
                }

                if (result == 1)
                {
                    if(PickAvailableConversaton(authorizer, client) == true)
                    {
                        Console.Clear();
                    }
                }

                if (result == 2)
                {
                    if (_lastConverstion == null)
                    {
                        Console.WriteLine();
                        Display("Use must first select an available conversation before you can commence chatting.", ConsoleColor.Yellow);
                        Console.WriteLine();
                        continue;
                    }

                    return true;
                }

                if (result == 3)
                {
                    if (_lastConverstion == null)
                    {
                        Console.WriteLine();
                        Display("Use must first select an available conversation before you can list interactions", ConsoleColor.Yellow);
                        Console.WriteLine();
                        continue;
                    }

                    DisplayList(authorizer, client, _lastConverstion);
                    Console.WriteLine();
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                    continue;
                }
            }
        }

        private static void ConversationLoop(HubsterAuthorizer authorizer, HubsterDirectClientBusiness client, EstablishedConversationModel conversation, string username)
        {
            while (true)
            {
                Console.Write($"Sending message to: [CUSTOMER - '{username}']: ");
                var message = Console.ReadLine();
                if (message.ToLower() == "q" || message.ToLower() == "quit")
                {
                    return;
                }

                if (message.ToLower() == "clear")
                {
                    Console.Clear();
                    continue;
                }

                if (string.IsNullOrWhiteSpace(message))
                {
                    continue;
                }

                var apiResponse = (ApiResponse<DirectResponseModel>)null;

                apiResponse = client.Activity.Send(authorizer, conversation.ConversationId.Value, new DirectActivityModel
                {
                    Sender = new DirectSourceModel
                    {
                        IntegrationId = Guid.Parse(_agentIntegrationId)
                    },
                    Message = new DirectMessageModel
                    {
                        Text = message
                    },
                });

                if (apiResponse.StatusCode != HttpStatusCode.OK)
                {
                    Display(apiResponse);
                }
            }
        }
        public static void Run()
        {
            var auth = new HubsterAuthClient(hostUrl: "http://localhost:5000", onAuthRequest: (authClient) =>
            {
                // typically this will be a call to some backend service that will return back the token
                var apiResponse = authClient.GetClientToken("hubster.engine.api.00000000000000000000000000000001", "9c5Vbnd0vZGlqTdBzhz9hb9cQ0M=");
                return apiResponse;
            });

            var client = new HubsterDirectClientBusiness("http://localhost:8251", "http://localhost:8082");
            var authorizer = new HubsterAuthorizer(auth);

            while(true)
            {
                var startChatting = Commands(authorizer, client);
                if (startChatting == false)
                {
                    return;
                }

                Console.Clear();
                var username = GetUserName(_lastConverstion);

                var eventResponse = client.Events.Start(authorizer, Guid.Parse(_agentIntegrationId),
                    activity => Display(activity),
                    error => Display(error.Description, ConsoleColor.Yellow)
                );

                if (eventResponse.StatusCode != HttpStatusCode.OK)
                {
                    Display(eventResponse);
                    Console.ReadKey();
                    return;
                }

                ConversationLoop(authorizer, client, _lastConverstion, username);

                client.Events.Stop(eventResponse.Content);
            }
        }
    }
}
