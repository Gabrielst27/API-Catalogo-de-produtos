using Microsoft.AspNetCore.Mvc.Filters;

namespace APICatalogo.Filters
{
    public class ApiLoggingFilters : IActionFilter
    {
        private readonly ILogger<ApiLoggingFilters> _logger;
        
        public ApiLoggingFilters(ILogger<ApiLoggingFilters> logger)
        {
            _logger = logger;
        }
        
        //Antes do método Action
        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation("### Executando --> OnActionExecuting");
            _logger.LogInformation("#########################################");
            string data = DateTime.Now.ToString();
            string[] vetdata = data.Split(" ");
            _logger.LogInformation($"Data: {vetdata[0]} às {DateTime.Now.ToLongTimeString()}");
            _logger.LogInformation($"ModelState: {context.ModelState.IsValid}");
            _logger.LogInformation("#########################################");
        }

        //Depois do método Action
        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation("### Executado --> OnActionExecuted");
            _logger.LogInformation("#########################################");
            string data = DateTime.Now.ToString();
            string[] vetdata = data.Split(" ");
            _logger.LogInformation($"Data: {vetdata[0]} às {DateTime.Now.ToLongTimeString()}");
            _logger.LogInformation($"StatusCode: {context.HttpContext.Response.StatusCode}");
            _logger.LogInformation("#########################################");
        }

    }
}
