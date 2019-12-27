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

namespace ClientCredentialProviderTest
{
    public class Program
    {
        public static async Task Main()
        {
            string clientId = "8c854dbe-be65-496e-88ae-0844ffb53edb";
            string tenantID = "9cacb64e-358b-418b-967a-3cabc2a0ea95";
            string clientSecret = "tAeIr2Qd_/[3O50WPg_?]NcJx/e:4ptp";

            IConfidentialClientApplication confidentialClientApplication = ConfidentialClientApplicationBuilder
                .Create(clientId)
                .WithTenantId(tenantID)
                .WithClientSecret(clientSecret)
                .Build();

            ClientCredentialProvider authenticationProvider = new ClientCredentialProvider(confidentialClientApplication);

            BaseClient baseClient = new BaseClient("https://graph.microsoft.com/v1.0/", authenticationProvider);

            BaseRequest baseRequest = new BaseRequest("https://graph.microsoft.com/v1.0/users", baseClient);

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
