using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace ITI_MVC.CustomActionFilters
{
    public class LogFilter : ActionFilterAttribute
    {
        Stopwatch sp = new Stopwatch();
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            sp.Start();
            //Debug.Write("Action Started");
            Console.WriteLine("Action Started");
            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            sp.Stop();  
            //Debug.Write($"Action Finished in: {sp.ElapsedMilliseconds}");
            Console.WriteLine($"Action Finished in: {sp.ElapsedMilliseconds}");
            base.OnActionExecuted(context);
        }
    }
}
