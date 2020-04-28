using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;

namespace Bdotnet.Demo
{
    public static class ComputeExpense
    {
        [FunctionName("ComputeTotalExpense")]
        public static async Task<double> ComputeTotalExpesne([ActivityTrigger] List<double> expenses, ILogger log)
        {    
            double totalCost = 0;        
            foreach (var cost in expenses)
            {
                totalCost += cost;
            }
            return await Task.FromResult(totalCost);
        }
    }
}
