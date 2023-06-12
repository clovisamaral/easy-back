using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace EasyInvoice.API.Shared
{
    public class TesteAsync : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var request = context.HttpContext.Request;
            
            Stopwatch stopwatch = Stopwatch.StartNew();

            await next();

            stopwatch.Stop();   
        }
    }
}
 