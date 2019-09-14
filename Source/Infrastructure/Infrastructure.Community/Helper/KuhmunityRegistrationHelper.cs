using Infrastructure.Community.Models;
using Infrastructure.Community.Status;
using Newtonsoft.Json;
using SWEET.WebProjects.Brands.Milka.Kuhmunity.DTO;
using System;
using System.Net;

namespace Infrastructure.Community.Helper
{
    public static class KuhmunityRegistrationHelper
    {
        public static KuhmunityResponse Register(
            string apiUrl,
            UserRegisterInput kuhmunityProfileData)
        {
            KuhmunityResponse response = new KuhmunityResponse
            {
                IsSuccessful = false
            };

            using (var client = new WebClient())
            {
                try
                {
                    var data = JsonConvert.SerializeObject(kuhmunityProfileData);
                    client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                    var apiResponse = client.UploadString(new Uri(apiUrl + "Register?output=json"), "POST", data);

                    if (!string.IsNullOrWhiteSpace(apiResponse))
                    {
                        var receivedData = JsonConvert.DeserializeObject<UserResultDTO>(apiResponse);
                        if (receivedData.Status.Equals("OK"))
                        {
                            response.IsSuccessful = true;
                            response.Body = receivedData.UserId;
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
