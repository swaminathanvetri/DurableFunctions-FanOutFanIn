using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;

namespace Bdotnet.Demo
{
    public static class BookFlight
    {

        [FunctionName("BookFlightTicket")]
        public static async Task<double> ActivityBookFlight([ActivityTrigger] VacationInfo vacationInfo, ILogger log)
        {            
             log.LogInformation($"Flight ticket booked for {vacationInfo.cityName} with the airlines {vacationInfo.airlinesName}");        
            return await Task.FromResult(10000);
        }


        // [FunctionName("BookFlightTicket")]
        // public static async Task<double> BookFlightTotheCountry([ActivityTrigger] VacationInfo vacationInfo, ILogger log)
        // {
        //     try
        //     {
        //         await Task.Run(() => throw new Exception("some exception happened"));
        //     }
        //     catch (System.Exception ex)
        //     {
        //         log.LogError($"Error in booking {vacationInfo.airlinesName} {ex}");
               
        //     }

        //     return 0.0;
        // }
    }
}



