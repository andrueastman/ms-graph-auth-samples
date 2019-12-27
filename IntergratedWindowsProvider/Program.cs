// ------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

using Microsoft.Graph;
using Microsoft.Identity.Client;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace IntergratedWindowsProvider
{
    public class Program
    {
        public static async Task Main()
        {
            string clientId = "INSERT_CLIENT_ID_HERE";
            string tenantID = "INSERT_TENANT_ID_HERE";

            string[] scopes = { "User.Read" };

            IPublicClientApplication publicClientApplication = PublicClientApplicationBuilder
                .Create(clientId)
                .WithTenantId(tenantID)
                .Build();

            IntegratedWindowsAuthenticationProvider authenticationProvider = new IntegratedWindowsAuthenticationProvider(publicClientApplication, scopes);

            HttpClient httpClient = GraphClientFactory.Create(authenticationProvider);

            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, "me");

            HttpResponseMessage response = await httpClient.SendAsync(requestMessage);
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
