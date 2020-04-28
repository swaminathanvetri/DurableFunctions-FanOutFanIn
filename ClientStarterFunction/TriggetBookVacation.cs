
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace Bdotnet.Demo
{
    public static class TriggerBookVacation
    {     

        [FunctionName("TriggerBookVacation")]
        public static async Task<HttpResponseMessage> HttpStart(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestMessage req,
            [DurableClient] IDurableOrchestrationClient starter,
            ILogger log)
        {
            var vacationInfo = await req.Content.ReadAsAsync<VacationInfo>();

            // Function input comes from the request content.
            string instanceId = await starter.StartNewAsync("BookVacation", vacationInfo);

            await starter.StartNewAsync("BookVacation", vacationInfo);

            log.LogInformation($"Started orchestration with ID = '{instanceId}'.");

            return starter.CreateCheckStatusResponse(req, instanceId);           
        }
    }
}
