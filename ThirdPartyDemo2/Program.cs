using IdentityModel.Client;
using System;
using System.Net.Http;

namespace ThirdPartyDemo2
{
    class Program
    {
        static void Main(string[] args)
        {
            var diso = DiscoveryClient.GetAsync("http://localhost:5000").Result;
            if (diso.IsError)
            {
                Console.WriteLine(diso.Error);
            }

            var tokenClient = new TokenClient(diso.TokenEndpoint, "pwdclient", "secret");
            var tokenResponse = tokenClient.RequestResourceOwnerPasswordAsync("jesse", "123456", "api").Result;

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
            }
            else
            {
                Console.WriteLine(tokenResponse.Json);
            }

            HttpClient httpClient = new HttpClient();
            httpClient.SetBearerToken(tokenResponse.AccessToken);
            var response = httpClient.GetAsync("http://localhost:5001/api/values").Result;
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            }



            tokenClient = new TokenClient(diso.TokenEndpoint, "pwdclientHome", "secret");
            tokenResponse = tokenClient.RequestResourceOwnerPasswordAsync("jesse", "123456", "Home").Result;
            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
            }
            else
            {
                Console.WriteLine(tokenResponse.Json);
            }

            httpClient.SetBearerToken(tokenResponse.AccessToken);
            response = httpClient.GetAsync("http://localhost:5001/Home").Result;
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            }

            Console.ReadLine();
        }
    }
}
