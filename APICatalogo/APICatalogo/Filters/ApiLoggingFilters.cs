using Microsoft.AspNetCore.Mvc.Filters;

namespace APICatalogo.Filters
{
    public class ApiLoggingFilters : IResultFilter, IActionFilter
    {
        private readonly ILogger<ApiLoggingFilters> _logger;
        
        public ApiLoggingFilters(ILogger<ApiLoggingFilters> logger)
        {
            _logger = logger;
        }
        
        public void OnResultExecuting(ResultExecutingContext context)
        {
            int a = 0;
        }

        //Resposta
        public void OnResultExecuted(ResultExecutedContext context)
        {
            _logger.LogInformation("######## Resposta ########");
            string data = DateTime.Now.ToString();
            string[] vetdata = data.Split(" ");
            _logger.LogInformation($"Data: {vetdata[0]} às {DateTime.Now.ToLongTimeString()}");
            _logger.LogInformation($"StatusCode: {context.HttpContext.Response.StatusCode}");
        }

        //Antes da ação
        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation("######## Início da ação ########");
            string data = DateTime.Now.ToString();
            string[] vetdata = data.Split(" ");
            _logger.LogInformation($"Data: {vetdata[0]} às {DateTime.Now.ToLongTimeString()}");
            _logger.LogInformation($"ModelState: {context.ModelState.IsValid}");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            int a = 0;
        }
    }
}
