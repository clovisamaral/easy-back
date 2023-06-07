using Microsoft.AspNetCore.Mvc.Filters;

namespace EasyInvoice.API.Shared
{
    public class Teste : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            //throw new NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var method = context.HttpContext.Request.Method;
            var urlRequest = context.HttpContext.Request.Path;
        }
    }
}
