using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading.Tasks;

namespace RestFlow.Common.Utilities
{
    public class CustomLoggerProvider : ILoggerProvider
    {
        private readonly ConcurrentQueue<string> _logQueue = new ConcurrentQueue<string>();
        private readonly Task _processingTask;
        private volatile bool _isDisposed = false;

        public CustomLoggerProvider()
        {
            _processingTask = Task.Run(ProcessLogQueue);
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new CustomLogger(_logQueue);
        }

        public void Dispose()
        {
            _isDisposed = true;
            _processingTask.Wait();
        }

        private async Task ProcessLogQueue()
        {
            while (!_isDisposed || !_logQueue.IsEmpty)
            {
                while (_logQueue.TryDequeue(out var log))
                {
                    await File.AppendAllTextAsync("logs.txt", log + Environment.NewLine);
                }

                await Task.Delay(100);
            }
        }
    }

    public class CustomLogger : ILogger
    {
        private readonly ConcurrentQueue<string> _logQueue;

        public CustomLogger(ConcurrentQueue<string> logQueue)
        {
            _logQueue = logQueue;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            if (formatter != null)
            {
                var logMessage = formatter(state, exception);
                _logQueue.Enqueue(logMessage);
            }
        }

        public bool IsEnabled(LogLevel logLevel) => true;

        public IDisposable BeginScope<TState>(TState state) => null;
    }
}
