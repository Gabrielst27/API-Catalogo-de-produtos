
namespace APICatalogo.Logging
{
    public class CustomLogger : ILogger
    {
        readonly string loggerName;
        public readonly CustomLoggerProviderConfiguration loggerConfig;

        public CustomLogger(string name, CustomLoggerProviderConfiguration config)
        {
            loggerName = name;
            loggerConfig = config;
        }

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel == loggerConfig.LogLevel;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state,
            Exception? exception, Func<TState, Exception?, string> formatter)
        {
            string mensagem = $"{logLevel.ToString()}: {eventId.Id} - {formatter(state, exception)}";

            EscreverTextoNoArquivo(mensagem);
        }

        private void EscreverTextoNoArquivo(string mensagem)
        {
            string caminhoDoArquivo = @"C:\Users\proci\Programação\Testes\Gabriel_Log.txt";

            using(StreamWriter sw = new StreamWriter(caminhoDoArquivo, true))
            {
                try
                {
                    sw.WriteLine(mensagem);
                    sw.Close();
                }
                catch (Exception) 
                {
                    throw;
                }
            }
        }
    }
}
