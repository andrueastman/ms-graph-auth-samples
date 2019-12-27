// ------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

using Microsoft.Graph;
using Microsoft.Identity.Client;
using System;
using System.Net;
using System.Net.Http;
using System.Security;
using System.Threading.Tasks;

namespace IntergratedWindowsProvider
{
    public class Program
    {
        public static async Task Main()
        {
            string clientId = "INSERT_CLIENT_ID_HERE";
            string tenantID = "INSERT_TENANT_ID_HERE";
            string userName = "XXXXX";
            string password = "XXXXX";

            string[] scopes = { "User.Read" };

            IPublicClientApplication publicClientApplication = PublicClientApplicationBuilder
                .Create(clientId)
                .WithTenantId(tenantID)
                .Build();

            UsernamePasswordProvider authenticationProvider = new UsernamePasswordProvider(publicClientApplication, scopes);

            BaseClient baseClient = new BaseClient("https://graph.microsoft.com/v1.0/", authenticationProvider);

            BaseRequest baseRequest = new BaseRequest("https://graph.microsoft.com/v1.0/me", baseClient);

            SecureString theSecureString = new NetworkCredential("", password).SecurePassword;
            baseRequest = baseRequest.WithUsernamePassword(userName, theSecureString);

            HttpRequestMessage requestMessage = baseRequest.GetHttpRequestMessage();

            HttpResponseMessage response = await baseClient.HttpProvider.SendAsync(requestMessage);
            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                Console.WriteLine(jsonResponse);
            }
            else
            {
                Console.WriteLine("Error when making graph call");
            }
        }
    }
}
