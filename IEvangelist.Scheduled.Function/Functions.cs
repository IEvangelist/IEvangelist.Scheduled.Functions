using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IEvangelist.Scheduled.Function
{
    public static class DoSomething {
        [FunctionName("DoSomething")]
        public static void Run(
            [TimerTrigger("0 */5 * * * *")]
            TimerInfo timer,
            TraceWriter log) {

            log.Info($"Doing something on a schedule, at {DateTime.Now}.");
        }
    }

    public static class TellJoke {
        private static readonly HttpClient _httpClient = new HttpClient();

        [FunctionName("TellJoke")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]
            HttpRequestMessage req,
            TraceWriter log) {
            log.Info("C# HTTP trigger function processed a request.");

            // Parse query parameter
            string name =
                req.GetQueryNameValuePairs()
                   .FirstOrDefault(q => string.Compare(q.Key, "name", true) == 0)
                   .Value
                   ?? ((dynamic)(await req.Content.ReadAsAsync<object>()))?.name;

            if (name is null) {
                return req.CreateResponse(HttpStatusCode.BadRequest, "Please pass a name on the query string or in the request body");
            }
            else {
                var response = await _httpClient.GetStringAsync("http://api.icndb.com/jokes/random?limitTo=[nerdy]");
                var joke = JsonConvert.DeserializeObject<dynamic>(response)?.value?.joke;
                return new HttpResponseMessage(HttpStatusCode.OK) {
                    Content = new StringContent($"Hey, {name}... I have a joke for you. {joke}", Encoding.UTF8, "application/json")
                };
            }
        }
    }
}