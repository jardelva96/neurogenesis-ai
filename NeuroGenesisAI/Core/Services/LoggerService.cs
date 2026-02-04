using System;
using System.Collections.Generic;

namespace NeuroGenesisAI.Core.Services
{
    /// <summary>
    /// Serviço de registro de eventos do cérebro.
    /// </summary>
    public static class LoggerService
    {
        private static readonly List<LogEntry> _logs = new();

        /// <summary>
        /// Registra um novo evento no log.
        /// </summary>
        public static void Log(string message)
        {
            var logEntry = new LogEntry
            {
                Timestamp = DateTime.Now,
                Message = message
            };

            _logs.Add(logEntry);

            // Opcional: manter só os últimos 1000 logs para não consumir muita memória
            if (_logs.Count > 1000)
                _logs.RemoveAt(0);
        }

        /// <summary>
        /// Retorna todos os logs registrados.
        /// </summary>
        public static List<LogEntry> GetLogs()
        {
            return new List<LogEntry>(_logs); // Retorna cópia
        }
    }

    public class LogEntry
    {
        public DateTime Timestamp { get; set; }
        public required string Message { get; set; }
    }
}
