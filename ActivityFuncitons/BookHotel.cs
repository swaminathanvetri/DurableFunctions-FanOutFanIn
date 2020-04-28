using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;

namespace Bdotnet.Demo
{
    public static class BookHotel
    {
        [FunctionName("BookHotel")]
        public static async Task<double> ActivityBookHotel([ActivityTrigger] string hotelName, ILogger log)
        {            
            log.LogInformation($"Hotel {hotelName} booked.");        
            return await Task.FromResult(7000);
        }
    }
}