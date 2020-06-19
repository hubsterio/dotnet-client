// Hubster 
// Copyright (c) 2020 Hubster Solutions Inc. All rights reserved.

using Hubster.Abstractions.Models;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Net;

namespace Hubster.Direct.RemoteAccess
{
    /// <summary>
    /// 
    /// </summary>
    internal abstract class EngineBaseAccess
    {     
        /// Extracts the response.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="restResponse">The rest response.</param>
        /// <returns></returns>
        protected ApiResponse<T> ExtractResponse<T>(IRestResponse restResponse) where T: class
        {
            var apiResponse = new ApiResponse<T>();

            if (restResponse.StatusCode == HttpStatusCode.OK)
            {
                apiResponse.StatusCode = restResponse.StatusCode;
                apiResponse.Content = JsonConvert.DeserializeObject<T>(restResponse.Content);
            }
            else
            {
                apiResponse.StatusCode = restResponse.StatusCode;

                if (restResponse.StatusCode == 0)
                {
                    switch (restResponse.ResponseStatus)
                    {
                        case ResponseStatus.TimedOut:
                            apiResponse.StatusCode = HttpStatusCode.RequestTimeout;
                            break;

                        default:
                            apiResponse.StatusCode = HttpStatusCode.BadGateway;
                            break;
                    }
                }

                if (string.IsNullOrWhiteSpace(restResponse.Content) == false)
                {
                    var exceptionResponse = JsonConvert.DeserializeObject<ExceptionResponse>(restResponse.Content);
                    apiResponse.Errors = exceptionResponse.Errors;
                }
                else
                {
                    apiResponse.Errors = new List<ErrorCodeModel>
                    {
                        new ErrorCodeModel { Code = (int)apiResponse.StatusCode, Description = apiResponse.StatusCode.ToString() }
                    };
                }
            }

            return apiResponse;
        }
    }
}
