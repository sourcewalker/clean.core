using Infrastructure.Community.Models;
using Infrastructure.Community.Status;
using Newtonsoft.Json;
using SWEET.WebProjects.Brands.Milka.Kuhmunity.DTO;
using System;
using System.Net;

namespace Infrastructure.Community.Helper
{
    public static class KuhmunityLoginHelper
    {
        public static (KuhmunityResponse response, string sessionTicket, UserDetailDTO userDetail) Login(
            string ApiUrl,
            string campaignID,
            string email,
            string password,
            string isPersistentCookie,
            string sessionTicket,
            UserDetailDTO userDetail)
        {
            KuhmunityResponse response = new KuhmunityResponse
            {
                IsSuccessful = false
            };

            var KuhmunityViewModel = new
            {
                campaignID,
                email,
                password,
                isPersistentCookie
            };

            using (var client = new WebClient())
            {
                try
                {
                    var data = JsonConvert.SerializeObject(KuhmunityViewModel);
                    client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                    var apiResponse = client.UploadString(new Uri(ApiUrl + "Login?output=json"), "POST", data);

                    if (!string.IsNullOrWhiteSpace(apiResponse))
                    {
                        var receivedData = JsonConvert.DeserializeObject<UserLoginResultDTO>(apiResponse);
                        if (receivedData.Status.Equals("OK"))
                        {
                            response.IsSuccessful = true;
                            response.Body = receivedData.UserDetailDTO;
                            userDetail = receivedData.UserDetailDTO;
                            sessionTicket = receivedData.SessionTicket;
                        }
                        else
                        {
                            response.Message = receivedData.Status;
                        }
                    }

                    return (response, sessionTicket, userDetail);
                }
                catch (Exception ex)
                {
                    response.Message = ResponseMessages.SERVER_ERROR;
                    response.ErrorMessage = ex.Message;

                    return (response, sessionTicket, userDetail);
                }
            }
        }

        public static KuhmunityResponse Logout(string apiUrl)
        {
            KuhmunityResponse response = new KuhmunityResponse
            {
                IsSuccessful = false
            };

            using (var client = new WebClient())
            {
                try
                {
                    var logoutData = new UserLogoutInput();
                    var data = JsonConvert.SerializeObject(logoutData);
                    client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                    var apiResponse = client.UploadString(new Uri(apiUrl + "Logout?output=json"), "POST", data);

                    if (!String.IsNullOrWhiteSpace(apiResponse))
                    {
                        var receivedData = JsonConvert.DeserializeObject<UserOperationResultDTO>(apiResponse);
                        if (receivedData.Status.Equals("OK"))
                        {
                            response.IsSuccessful = true;
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
