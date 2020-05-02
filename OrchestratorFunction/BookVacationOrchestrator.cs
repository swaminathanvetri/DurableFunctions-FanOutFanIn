using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace Bdotnet.Demo
{
    public static class BookVacationOrchestrator
    {

        // #region "Book Vacation Orchestrator"

        [FunctionName("BookVacation")]
        public static async Task<double> RunOrchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context,
            ILogger log)
        {
            //Receive Input
            var vacationDetails = context.GetInput<VacationInfo>();

            //Call Activity Functions in parallel
            var bookHotel = context.CallActivityAsync<double>("BookHotel", vacationDetails.hotelName);
            var bookTransport = context.CallActivityAsync<double>("BookFlightTicket", vacationDetails);

            //Wait for Acitivity Functions to complete - Fan-In
            var bookingTasks = (await Task.WhenAll(bookHotel, bookTransport)).ToList();

            //Call another activity function to compute total
            return await context.CallActivityAsync<double>("ComputeTotalExpense", bookingTasks);
        }

        // #endregion

        #region "Orchestrator Funciton - BookVacationWithErrorAndRetry"

        // [FunctionName("BookVacation")]
        // public static async Task<double> RunOrchestrator(
        //     [OrchestrationTrigger] IDurableOrchestrationContext context,
        //     ILogger log)
        // {
        //     //Receive Input
        //     var vacationDetails = context.GetInput<VacationInfo>();
        //     List<double> bookingTasks = new List<double>();

        //     //Call Activity Functions in parallel
        //     var bookHotel = context.CallActivityAsync<double>("BookHotel", vacationDetails.hotelName);
        //     var bookTransport = context.CallActivityAsync<double>("BookFlightTicket", vacationDetails);

        //     try
        //     {
        //         //Wait for Acitivity Functions to complete - Fan-In
        //         await Task.WhenAll(bookHotel, bookTransport);
                
        //     }
        //     catch (System.Exception)
        //     {
        //         if (bookTransport.IsFaulted)
        //         {
        //             log.LogError("Exception in bookng flight ticker. Please try later");
        //             bookingTasks.Add(bookHotel.Result);
        //         }
        //     }

        //     //Call another Activity Function
        //     var retryOptions = new RetryOptions(
        //         firstRetryInterval: TimeSpan.FromSeconds(5),
        //         maxNumberOfAttempts: 5);

        //     return await context.CallActivityWithRetryAsync<double>("ComputeTotalExpense", retryOptions, bookingTasks);

        // }
        #endregion
    }
}