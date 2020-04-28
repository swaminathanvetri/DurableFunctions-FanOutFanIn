#region "Orchestrator Function - BookVacationWithErrorAndRetry"
//#region "Orchestrator Function - BookVacationWithErrorAndRetry"
        // [FunctionName("BookVacation")]
        // public static async Task<double> RunOrchestrator(
        //     [OrchestrationTrigger] IDurableOrchestrationContext context,
        //     ILogger log)
        // {
        //     //Receive Input
        //     var vacationDetails = context.GetInput<VacationInfo>();
        //     List<double> bookingTasks = new List<double>();    
            
        //     //Call Activity Functions in parallel
        //     var bookHotel =   context.CallActivityAsync<double>("BookHotel", vacationDetails.hotelName);  
        //     var bookTransport =  context.CallActivityAsync<double>("BookFlightTicket", vacationDetails);
           
        //     try
        //     {
        //         //Wait for Acitivity Functions to complete - Fan-In
        //         await Task.WhenAll(bookHotel,bookTransport);
        //         bookingTasks.Add(bookHotel.Result);
        //         bookingTasks.Add(bookTransport.Result);
        //     }
        //     catch (System.Exception)
        //     {
        //         if(bookTransport.IsFaulted)
        //         {
        //             log.LogError("Exception in bookng flight ticker. Please try later");
        //             bookingTasks.Add(bookHotel.Result);
        //         }                
        //     }

        //     //Call another Activity Function
        //     var retryOptions = new RetryOptions(
        //         firstRetryInterval : TimeSpan.FromSeconds(5),
        //         maxNumberOfAttempts: 5);

        //     return await context.CallActivityWithRetryAsync<double>("ComputeTotalExpense",retryOptions ,bookingTasks);
            
        // }
    
 //#endregion
#endregion

 #region "Exception Activity Sample"
//#region "Activity Funciton - Flight Ticket Function that can throw exception"
// [FunctionName("BookFlightTicket")]
//         public static async Task<double> BookFlightTotheCountry([ActivityTrigger] VacationInfo vacationInfo,ILogger log)
//         {            

//             try
//             {
//                 await Task.Run(()=> throw new Exception("somthing happened"));
//             }
//             catch (System.Exception ex)
//             {
//                 log.LogError($"Error in booking {vacationInfo.airlinesName} {ex}");
//                 throw ex;
//             }

//             return 0.0;
//         }
//#endregion
 #endregion
    


