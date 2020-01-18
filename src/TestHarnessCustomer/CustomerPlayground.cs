using Hubster.Auth;
using Hubster.Direct;
using Hubster.Direct.Enums;
using Hubster.Direct.Models;
using Hubster.Direct.Models.Direct;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace TestHarnessCustomer
{
    public static class CustomerPlayground
    {
        private static EstablishedConversationModel _lastConverstion = null;

        private static EstablishConversationRequestModel GetCustomerConversationRequestModel(string username)
        {
            return new EstablishConversationRequestModel
            {
                IntegrationId = "00000000-0000-0000-0000-000000000020",
                Binding = username,
                Properties = new ConversationPropertiesModel
                {
                    Profile = new Dictionary<string, string>
                    {
                        { "Device", "Web" },
                        { "Full name", username },
                    }
                }
            };
        }

        private static string GetUserName(EstablishedConversationModel converstion)
        {
            var username = (string)null;
            if(converstion != null)
            {
                username = converstion.Properties.Profile["Full name"];
            }

            return username;
        }

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
            switch(activity.Sender.IntegrationType)
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
            if(response.Errors != null)
            {
                var issues = JsonConvert.SerializeObject(response.Errors, Formatting.Indented);
                Display(issues, ConsoleColor.Yellow);
            }
        }

        private static void DisplayList(HubsterAuthorizer authorizer, HubsterDirectClientCustomer client, EstablishedConversationModel conversation)
        {
            var apiResponse = client.Activity.Get(authorizer, conversation, 0);
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

        private static void ConversationLoop(HubsterAuthorizer authorizer, HubsterDirectClientCustomer client, EstablishedConversationModel conversation, string username)
        {
            while(true)
            {
                Console.Write($"[CUSTOMER - '{username}'] Please enter message: ");
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

                apiResponse = client.Activity.Send(authorizer, conversation, new DirectActivityModel
                {
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

        private static bool EstablishConversation(HubsterAuthorizer authorizer, HubsterDirectClientCustomer client)
        {            
            Console.WriteLine($"Enter a username:");
            var username = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(username))
            {
                return true;
            }

            var conversationResponse = client.Conversation.Establish(authorizer, GetCustomerConversationRequestModel(username));
            if (conversationResponse.StatusCode != HttpStatusCode.OK)
            {
                Console.WriteLine();
                Display(conversationResponse);
                Console.WriteLine();
                return false;
            }

            _lastConverstion = conversationResponse.Content;
            return true;
        }

        private static bool Commands(HubsterAuthorizer authorizer, HubsterDirectClientCustomer client)
        {
            Console.Clear();

            while (true)
            {
                var lastUser = GetUserName(_lastConverstion);

                Console.WriteLine($"Select a command:");
                Console.WriteLine($"1. Establish new conversation.");
                Console.Write($"2. Start chatting as ");
                DisplaySimpleText($"'{lastUser ?? "You must first establish conversation!"}'", ConsoleColor.Yellow);
                Console.WriteLine();
                Console.WriteLine($"3. List interactions");
                Console.WriteLine($"4. Quit (q or quit)");

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
                    Console.Clear();
                    if (EstablishConversation(authorizer, client) == true)
                    {
                        Console.Clear();
                    }                    
                }

                if(result == 2)
                {
                    if(_lastConverstion == null)
                    {
                        Console.WriteLine();
                        Display("Use must first select 'Establish new conversation' before you can commence chatting.", ConsoleColor.Yellow);                        
                        Console.WriteLine();
                        continue;
                    }

                    return true;
                }

                if(result == 3)
                {
                    if (_lastConverstion == null)
                    {
                        Console.WriteLine();
                        Display("Use must first select 'Establish new conversation', before you can list interactions", ConsoleColor.Yellow);                        
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

        public static void Run()
        {
            var auth = new HubsterAuthClient(hostUrl: "http://localhost:5000", onAuthRequest: (authClient) =>
            {
                // typically this will be a call to some backend service that will return back the token
                var apiResponse = authClient.GetClientToken("hubster.engine.api.00000000000000000000000000000001", "9c5Vbnd0vZGlqTdBzhz9hb9cQ0M=");
                return apiResponse;
            });

            var client = new HubsterDirectClientCustomer("http://localhost:5002", "http://localhost:5005");
            var authorizer = new HubsterAuthorizer(auth);

            while (true)
            {                
                var startChatting = Commands(authorizer, client);
                if (startChatting == false)
                {
                    return;
                }

                Console.Clear();
                var username = GetUserName(_lastConverstion);

                var eventResponse = client.Events.Start(options =>
                {
                    options.Authorizer = authorizer;
                    options.IntegrationId = _lastConverstion.IntegrationId.Value;
                    options.ConversationId = _lastConverstion?.ConversationId;
                    options.OnActivity = (activity) => Display(activity);
                    options.OnConnected = () => Display("Connected", ConsoleColor.Cyan);
                    options.OnDisconnected = () => Display("Disconnected", ConsoleColor.Yellow);
                    options.OnError = (error) => Display(error.Description, ConsoleColor.Yellow);
                });

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
