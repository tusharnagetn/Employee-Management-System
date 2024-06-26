using System.Text;

namespace Employee_Management_System.Common
{
    public class HTTPClientHelper
    {
        public static async Task<string> MakePostRequest(string baseUrl, string endPoint, string apiRequestData)
        {
            var socketHandler = new SocketsHttpHandler
            {
                PooledConnectionLifetime = TimeSpan.FromMinutes(10),
                PooledConnectionIdleTimeout = TimeSpan.FromMinutes(5),
                MaxConnectionsPerServer = 10
            };

            using(var httpClient = new HttpClient(socketHandler))
            {
                httpClient.Timeout = TimeSpan.FromMinutes(5);
                httpClient.BaseAddress = new Uri(baseUrl);
                StringContent apiRequestContext = new StringContent(apiRequestData, Encoding.UTF8, "application/json");
                
                var httpResponse = httpClient.PostAsync(endPoint, apiRequestContext).Result;

                var responseString = httpResponse.Content.ReadAsStringAsync().Result; 

                if(!httpResponse.IsSuccessStatusCode)
                {
                    throw new Exception(responseString);
                }
                return responseString;
            }
        }

        public static async Task<string> MakeGetRequest(string baseUrl, string endPoint)
        {
            var socketHandler = new SocketsHttpHandler
            {
                PooledConnectionLifetime = TimeSpan.FromMinutes(10),
                PooledConnectionIdleTimeout = TimeSpan.FromMinutes(5),
                MaxConnectionsPerServer = 10
            };

            using (var httpClient = new HttpClient(socketHandler))
            {
                httpClient.Timeout = TimeSpan.FromMinutes(5);
                httpClient.BaseAddress = new Uri(baseUrl);
                
                var httpResponse = await httpClient.GetAsync(endPoint);
                var responseString = await httpResponse.Content.ReadAsStringAsync();

                if (!httpResponse.IsSuccessStatusCode)
                {
                    throw new Exception(responseString);
                }
                return responseString;
            }
        }

    }
}
