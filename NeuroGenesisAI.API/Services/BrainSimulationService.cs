using NeuroGenesisAI.Core.Entities;
using NeuroGenesisAI.Core.Services;
using NeuroGenesisAI.API.Controllers;

namespace NeuroGenesisAI.API.Services
{
    /// <summary>
    /// Serviço em background que executa ciclos automáticos de simulação do cérebro.
    /// </summary>
    public class BrainSimulationService : BackgroundService
    {
        private readonly ILogger<BrainSimulationService> _logger;
        private static int _cycleIntervalMs = 10000; // 10 segundos por padrão
        private static bool _isEnabled = true;
        private static readonly object _lock = new object();
        private const int MinCycleIntervalMs = 1000; // Mínimo de 1 segundo

        public BrainSimulationService(ILogger<BrainSimulationService> logger)
        {
            _logger = logger;
        }

        public static void SetCycleInterval(int milliseconds)
        {
            if (milliseconds >= MinCycleIntervalMs)
            {
                lock (_lock)
                {
                    _cycleIntervalMs = milliseconds;
                }
            }
        }

        public static int GetCycleInterval()
        {
            lock (_lock)
            {
                return _cycleIntervalMs;
            }
        }

        public static void SetEnabled(bool enabled)
        {
            lock (_lock)
            {
                _isEnabled = enabled;
            }
        }

        public static bool IsEnabled()
        {
            lock (_lock)
            {
                return _isEnabled;
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Brain Simulation Service iniciado.");

            while (!stoppingToken.IsCancellationRequested)
            {
                bool enabled;
                int intervalMs;
                
                lock (_lock)
                {
                    enabled = _isEnabled;
                    intervalMs = _cycleIntervalMs;
                }

                if (enabled)
                {
                    try
                    {
                        // Obtém a instância do Brain do BrainController
                        var brain = BrainController.GetBrainInstance();
                        brain.Cycle();
                        
                        _logger.LogInformation("Ciclo automático executado. Neurônios: {Count}", brain.Neurons.Count);
                        LoggerService.Log($"🔄 Ciclo automático executado. Neurônios: {brain.Neurons.Count}");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Erro ao executar ciclo automático.");
                    }
                }

                await Task.Delay(intervalMs, stoppingToken);
            }

            _logger.LogInformation("Brain Simulation Service encerrado.");
        }
    }
}
