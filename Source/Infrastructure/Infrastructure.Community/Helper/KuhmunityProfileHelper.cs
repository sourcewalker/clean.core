using Infrastructure.Community.Models;
using Infrastructure.Community.Status;
using Newtonsoft.Json;
using SWEET.WebProjects.Brands.Milka.Kuhmunity.DTO;
using System;
using System.Net;

namespace Infrastructure.Community.Helper
{
    public static class KuhmunityProfileHelper
    {
        public static KuhmunityResponse GetProfile(
            string apiUrl,
            UserOperationInput userOperationData)
        {
            KuhmunityResponse response = new KuhmunityResponse
            {
                IsSuccessful = false
            };

            using (var client = new WebClient())
            {
                try
                {
                    var data = JsonConvert.SerializeObject(userOperationData);
                    client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                    var apiResponse = client.UploadString(new Uri(apiUrl + "GetUserProfile?output=json"), "POST", data);

                    if (!String.IsNullOrWhiteSpace(apiResponse))
                    {
                        var receivedData = JsonConvert.DeserializeObject<GetUserDetailResultDTO>(apiResponse);
                        if (receivedData.Status.Equals("OK"))
                        {
                            response.IsSuccessful = true;
                            response.Body = receivedData.UserDetail;
                        }
                        else
                        {
                            response.Message = receivedData.Status;
                        }
                    }

                    return response;
                }
                catch (Exception ex)
                {
                    response.Message = ResponseMessages.SERVER_ERROR;
                    response.ErrorMessage = ex.Message;

                    return response;
                }
            }
        }
    }
}
