using Hubster.Auth;
using Hubster.Direct;
using Hubster.Direct.Enums;
using Hubster.Direct.Models;
using Hubster.Direct.Models.Direct;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;

namespace TestHarness.Playground
{
    //public static class AgentPlayground
    //{
    //    // agent metadata 
    //    private static readonly string _agentConversationId = "e597acca-85fc-4243-a615-c106c4be1e7a";
    //    private static readonly string _agentIntegrationId = "00000000-0000-0000-0000-000000000021";

    //    private static EstablishConversationRequestModel GetCustomerConversationRequestModel()
    //    {
    //        return new EstablishConversationRequestModel
    //        {
    //            IntegrationId = "00000000-0000-0000-0000-000000000020",
    //            Binding = "my unique data",
    //            Properties = new ConversationPropertiesModel
    //            {
    //                Profile = new Dictionary<string, string>
    //                {
    //                    { "Device", "Web" },
    //                    { "FullName", "Ross Pellegrino-2" },
    //                }
    //            }
    //        };
    //    }

    //    private static void Display(string message, ConsoleColor color = ConsoleColor.White)
    //    {
    //        var lastColor = Console.ForegroundColor;
    //        Console.ForegroundColor = color;
    //        Console.WriteLine(message);
    //        Console.WriteLine();
    //        Console.ForegroundColor = lastColor;
    //    }

    //    private static void Display(DirectActivityModel activity)
    //    {
    //        var color = activity.Sender.IntegrationType == IntegrationType.Customer ? ConsoleColor.Yellow : ConsoleColor.Cyan;
    //        var activityMessage = JsonConvert.SerializeObject(activity, Formatting.Indented);

    //        Display(activityMessage, color);
    //    }

    //    private static void Display(ApiResponse response)
    //    {
    //        if(response.Errors != null)
    //        {
    //            var issues = JsonConvert.SerializeObject(response.Errors, Formatting.Indented);
    //            Display(issues, ConsoleColor.Yellow);
    //        }
    //    }

    //    private static void DisplayList(HubsterAuthorizer authorizer, HubsterDirectClient client, string mode, EstablishedConversationModel conversation)
    //    {
    //        var apiResponse = client.Activity.Get(authorizer, conversation, 0, mode == "customer" ? IntegrationType.Customer : IntegrationType.Agent);
    //        if (apiResponse.StatusCode != HttpStatusCode.OK)
    //        {
    //            var issues = JsonConvert.SerializeObject(apiResponse.Errors, Formatting.Indented);
    //            Display(issues, ConsoleColor.Yellow);
    //        }

    //        Console.Clear();

    //        foreach (var activity in apiResponse.Content)
    //        {
    //            Display(activity);
    //        }
    //    }

    //    private static void ConversationLoop(HubsterAuthorizer authorizer, HubsterDirectClient client, string mode, EstablishedConversationModel conversation)
    //    {
    //        while(true)
    //        {
    //            Console.Write($"[{mode.ToUpper()}] Please enter message: ");
    //            var message = Console.ReadLine();
    //            if (message.ToLower() == "q" || message.ToLower() == "quit")
    //            {
    //                return;
    //            }

    //            if (message.ToLower() == "clear")
    //            {
    //                Console.Clear();
    //                continue;
    //            }

    //            if (string.IsNullOrWhiteSpace(message))
    //            {                    
    //                continue;
    //            }

    //            if(message.ToLower() == "list")
    //            {
    //                DisplayList(authorizer, client, mode, conversation);
    //                continue;
    //            }                

    //            var apiResponse = (ApiResponse<DirectResponseModel>)null;

    //            if (mode == "customer")
    //            {
    //                apiResponse = client.Activity.SendToAgent(authorizer, conversation, new DirectActivityModel
    //                {
    //                    Message = new DirectMessageModel
    //                    {
    //                        Text = message
    //                    },
    //                });

    //                continue;
    //            }

    //            // agent 
    //            apiResponse = client.Activity.SendToCustomer(authorizer, conversation, new DirectActivityModel
    //            {
    //                Sender = new DirectSourceModel
    //                {
    //                    IntegrationId = Guid.Parse(_agentIntegrationId),
    //                },
    //                Message = new DirectMessageModel
    //                {
    //                    Text = message
    //                },
    //            });

    //            if (apiResponse.StatusCode != HttpStatusCode.OK)
    //            {
    //                Display(apiResponse);
    //            }
    //        }
    //    }

    //    private static string GetMode()
    //    {
    //        while (true)
    //        {
    //            Console.Write("Please enter mode (customer or agent): ");
    //            var mode = Console.ReadLine().ToLower();
    //            if (mode == "q" || mode == "quit")
    //            {
    //                return null;
    //            }

    //            if (string.IsNullOrWhiteSpace(mode))
    //            {
    //                Console.Clear();
    //                continue;
    //            }

    //            if (mode != "customer" && mode != "agent")
    //            {
    //                Console.WriteLine("Incorrect mode. Must be 'customer' or 'agent'");
    //                Console.WriteLine();
    //                continue;
    //            }

    //            return mode;
    //        }
    //    }

    //    private static ApiResponse<EstablishedConversationModel> GetConversation(string mode, HubsterAuthorizer authorizer, HubsterDirectClient client)
    //    {
    //        var conversationResponse = (ApiResponse<EstablishedConversationModel>)null;
    //        if (mode == "customer")
    //        {
    //            conversationResponse = client.Conversation.Establish(authorizer, GetCustomerConversationRequestModel());
    //        }
    //        else // agent
    //        {
    //            conversationResponse = client.Conversation.GetEstablished(authorizer, Guid.Parse(_agentConversationId));
    //        }

    //        return conversationResponse;
    //    }

    //    public static void Run()
    //    {
    //        var mode = GetMode();
    //        if(mode == null)
    //        {
    //            return;
    //        }

    //        Console.Clear();
            
    //        var auth = new HubsterAuthClient(hostUrl: "http://localhost:5000", onAuthRequest: (authClient) =>
    //        {
    //            // typically this will be a call to some backend service that will return back the token
    //            var apiResponse = authClient.GetClientToken("hubster.engine.api.00000000000000000000000000000001", "9c5Vbnd0vZGlqTdBzhz9hb9cQ0M=");
    //            return apiResponse;
    //        });

    //        var client = new HubsterDirectClient("http://localhost:8251", "http://localhost:8082");
    //        var authorizer = new HubsterAuthorizer(auth);

    //        var conversationResponse = GetConversation(mode, authorizer, client);
    //        if(conversationResponse.StatusCode != HttpStatusCode.OK)
    //        {
    //            Display(conversationResponse);
    //            Display("Application terminating", ConsoleColor.Yellow);
    //            return;
    //        }

    //        var conversation = conversationResponse.Content;

    //        var eventResponse = client.Events.Start(authorizer, conversation.IntegrationId.Value, conversation, 
    //            activity => 
    //            {
    //                Display(activity);
    //            },
    //            error => 
    //            {
    //                Display(error);
    //            }
    //        );

    //        if(eventResponse.StatusCode != HttpStatusCode.OK)
    //        {
    //            Display(eventResponse);
    //            return;
    //        }

    //        ConversationLoop(authorizer, client, mode, conversationResponse.Content);

    //        client.Events.Stop(conversation);
    //    }
    //}
}
