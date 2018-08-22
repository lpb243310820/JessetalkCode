using System;
using System.Net.Http;
using IdentityModel.Client;

namespace ThirdPartyDemo
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

            var tokenClient = new TokenClient(diso.TokenEndpoint, "client", "secret");
            var tokenResponse = tokenClient.RequestClientCredentialsAsync("api").Result;

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



            tokenClient = new TokenClient(diso.TokenEndpoint, "clientHome", "secret");
            tokenResponse = tokenClient.RequestClientCredentialsAsync("Home").Result;
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
