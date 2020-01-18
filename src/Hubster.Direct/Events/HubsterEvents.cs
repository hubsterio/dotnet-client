// Hubster 
// Copyright (c) 2020 Hubster Solutions Inc. All rights reserved.

using Hubster.Direct.Interfaces;
using Hubster.Direct.Models;
using Hubster.Direct.Models.Direct;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hubster.Direct.Events
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class HubsterEventsBase : IHubsterEventsBase
    {
        private readonly string _eventsUrl;

        /// <summary>
        /// Initializes a new instance of the <see cref="HubsterEventsBase" /> class.
        /// </summary>
        /// <param name="eventsUrl">The host URL.</param>
        internal HubsterEventsBase(string eventsUrl)
        {
            _eventsUrl = eventsUrl;
        }

        /// <summary>
        /// Starts the specified connection.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="options">The options.</param>
        /// <param name="delay">if set to <c>true</c> [delay].</param>
        /// <returns></returns>
        private ApiResponse Start(HubConnection connection, StartOptions options = null, bool delay = false)
        {
            var apiResponse = new ApiResponse { StatusCode = HttpStatusCode.OK };

            while (true)
            {
                if (delay)
                {
                    Thread.Sleep(new Random().Next(1, 5) * 1000);
                    delay = false;
                }

                try
                {
                    connection.StartAsync().GetAwaiter().GetResult();
                    options.OnConnected?.Invoke();
                    break;
                }
                catch (Exception ex)
                when (ex?.InnerException is IOException 
                || ex?.InnerException is SocketException
                || ex?.InnerException is WebSocketException)
                {
                    delay = true;
                    options.OnDisconnected?.Invoke();                   
                    continue;
                }
                catch (HttpRequestException ex)
                {
                    apiResponse.StatusCode = HttpStatusCode.Unauthorized;
                    apiResponse.Errors = new List<ErrorCodeModel>
                    {
                        new ErrorCodeModel { Code = (int)HttpStatusCode.Unauthorized, Description = ex.Message }
                    };

                    break;
                }
                catch (Exception ex)
                {
                    apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    apiResponse.Errors = new List<ErrorCodeModel>
                    {
                        new ErrorCodeModel { Code = (int)HttpStatusCode.BadRequest, Description = ex.Message }
                    };

                    break;
                }
            }

            if (apiResponse.StatusCode != HttpStatusCode.OK)
            {
                options.OnError?.Invoke(apiResponse.Errors[0]);
            }

            return apiResponse;
        }

        /// <summary>
        /// Starts the specified authorizer.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <returns></returns>
        public ApiResponse<HubConnection> Start(Action<StartOptions> start)
        {
            var options = new StartOptions();
            start(options);

            var apiResponse = new ApiResponse<HubConnection>();
            options.Authorizer.EnsureLifespan(apiResponse);

            if (apiResponse.StatusCode != HttpStatusCode.OK)
            {
                return apiResponse;
            }

            var eventsUrl = new StringBuilder($"{_eventsUrl}/engine-chat-hub?iId={options.IntegrationId}");

            if (options.ConversationId != null)
            {
                eventsUrl.Append($"&cId={options.ConversationId}");
            }

            var connection = new HubConnectionBuilder()
               .WithUrl(eventsUrl.ToString(), (config) =>
               {
                   config.Headers.Add("Authorization", $"Bearer {options.Authorizer.Token.AccessToken}");
               })
               .Build();

            connection.On<DirectActivityModel>("ReceiveDirectActivities", (message) =>
            {
                options.OnActivity?.Invoke(message);
            });

            connection.Closed += (error) =>
            {
                if (error == null)
                {
                    return Task.CompletedTask;
                }

                if (error?.InnerException is IOException
                || error?.InnerException is SocketException
                || error?.InnerException is WebSocketException)
                {
                    options.OnDisconnected?.Invoke();

                    Start(connection, options, true);
                    return Task.CompletedTask;
                }
                else if (error is HubException)
                {
                    var idx = error.Message.IndexOf("HubException: [");
                    if (idx >= 0)
                    {
                        var errorMessage = error.Message.Substring(idx);
                        var firstBraceIdx = errorMessage.IndexOf("[") + 1;
                        var lastBraceIdx = errorMessage.IndexOf("]: ");
                        var errorCode = errorMessage.Substring(firstBraceIdx, lastBraceIdx - firstBraceIdx);
                        var errorDescription = errorMessage.Substring(lastBraceIdx + 3);

                        options.OnError?.Invoke(new ErrorCodeModel
                        {
                            Code = int.Parse(errorCode),
                            Description = errorDescription
                        });

                        return Task.CompletedTask;
                    }
                }
                
                options.OnError?.Invoke(new ErrorCodeModel
                {
                    Code = (int)HttpStatusCode.BadRequest,
                    Description = error.Message
                });

                return Task.CompletedTask;
            };

            apiResponse.Content = connection;

            var startResponse = Start(connection, options);
            if(startResponse.StatusCode != HttpStatusCode.OK)
            {
                apiResponse.StatusCode = startResponse.StatusCode;
                apiResponse.Errors = startResponse.Errors;
                apiResponse.Content = null;
            }

            return apiResponse;
        }

        /// <summary>
        /// Stops the specified connection.
        /// </summary>
        /// <param name="connection">The connection.</param>
        public void Stop(HubConnection connection)
        {
            if (connection != null)
            {
                try
                {
                    connection.StopAsync().GetAwaiter().GetResult();
                }
                catch (Exception)
                {
                }
            }
        }
    }
}
