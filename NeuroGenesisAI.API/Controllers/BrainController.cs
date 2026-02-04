using Microsoft.AspNetCore.Mvc;
using NeuroGenesisAI.Core.Entities;
using NeuroGenesisAI.Core.Interfaces;
using NeuroGenesisAI.Core.Services; // <- Importação para usar LoggerService
using NeuroGenesisAI.Core.Snapshots;
using System.Linq;
using System.Text; // <- ADICIONADO para usar Encoding

namespace NeuroGenesisAI.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BrainController : ControllerBase
    {
        private static readonly Brain _brain = new Brain();
        private static readonly Trainer _trainer = new Trainer(_brain);

        /// <summary>
        /// Retorna a instância do cérebro para uso por outros serviços.
        /// </summary>
        public static Brain GetBrainInstance() => _brain;

        /// <summary>
        /// Envia um estímulo externo ao cérebro.
        /// </summary>
        [HttpPost("stimulate")]
        public IActionResult StimulateBrain([FromBody] StimulusRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Stimulus))
                return BadRequest("O estímulo não pode ser vazio.");

            _brain.Stimulate(request.Stimulus);
            LoggerService.Log($"API: Estímulo '{request.Stimulus}' enviado ao cérebro.");

            return Ok(new { message = $"Estímulo '{request.Stimulus}' enviado com sucesso!" });
        }

        /// <summary>
        /// Executa um ciclo manual de simulação do cérebro.
        /// </summary>
        [HttpPost("cycle")]
        public IActionResult ExecuteCycle()
        {
            _brain.Cycle();
            LoggerService.Log("API: Ciclo manual executado.");

            return Ok(new 
            { 
                message = "Ciclo executado com sucesso!",
                neuronCount = _brain.Neurons.Count,
                clusterCount = _brain.Clusters.Count
            });
        }

        /// <summary>
        /// Obtém a configuração do ciclo automático.
        /// </summary>
        [HttpGet("cycle/config")]
        public IActionResult GetCycleConfig()
        {
            return Ok(new
            {
                enabled = Services.BrainSimulationService.IsEnabled(),
                intervalMs = Services.BrainSimulationService.GetCycleInterval()
            });
        }

        /// <summary>
        /// Configura o ciclo automático (habilitar/desabilitar e intervalo).
        /// </summary>
        [HttpPost("cycle/config")]
        public IActionResult SetCycleConfig([FromBody] CycleConfigRequest request)
        {
            if (request.Enabled.HasValue)
            {
                Services.BrainSimulationService.SetEnabled(request.Enabled.Value);
            }

            if (request.IntervalMs.HasValue)
            {
                Services.BrainSimulationService.SetCycleInterval(request.IntervalMs.Value);
            }

            return Ok(new
            {
                message = "Configuração atualizada com sucesso!",
                enabled = Services.BrainSimulationService.IsEnabled(),
                intervalMs = Services.BrainSimulationService.GetCycleInterval()
            });
        }

        /// <summary>
        /// Retorna o status atual do cérebro (neurônios, clusters, emoções).
        /// </summary>
        [HttpGet("status")]
        public IActionResult GetBrainStatus()
        {
            var status = new
            {
                neuronCount = _brain.Neurons.Count,
                clusterCount = _brain.Clusters.Count,
                emotions = _brain.Neurons
                    .GroupBy(n => (n as Neuron)?.CurrentEmotion ?? "Neutro")
                    .ToDictionary(g => g.Key, g => g.Count())
            };

            return Ok(status);
        }

        /// <summary>
        /// Retorna todos os clusters e seus neurônios.
        /// </summary>
        [HttpGet("clusters")]
        public IActionResult GetClusters()
        {
            var clusters = _brain.Clusters.Select(cluster => new
            {
                name = cluster.Name,
                neurons = cluster.Neurons.Select(n => new
                {
                    id = n.Id,
                    maturityLevel = n.MaturityLevel,
                    // Cast to Neuron to access Connections property (INeuron interface doesn't expose it)
                    connections = (n as Neuron)?.Connections.Select(c => c.Target.Id).ToList() ?? new List<string>()
                }).ToList()
            }).ToList();

            return Ok(clusters);
        }

        /// <summary>
        /// Retorna todos os eventos de log registrados no cérebro.
        /// </summary>
        [HttpGet("logs")]
        public IActionResult GetLogs()
        {
            var logs = LoggerService.GetLogs().Select(log => new
            {
                timestamp = log.Timestamp,
                message = log.Message
            }).ToList();

            return Ok(logs);
        }

        /// <summary>
        /// Exporta todos os neurônios em CSV.
        /// </summary>
        [HttpGet("snapshot/neurons")]
        public IActionResult ExportNeuronsCsv()
        {
            var csv = SnapshotService.GenerateNeuronsCsv(_brain.Neurons);
            return File(Encoding.UTF8.GetBytes(csv), "text/csv", "neurons.csv");
        }

        /// <summary>
        /// Exporta todas as conexões em CSV.
        /// </summary>
        [HttpGet("snapshot/connections")]
        public IActionResult ExportConnectionsCsv()
        {
            var csv = SnapshotService.GenerateConnectionsCsv(_brain.Neurons);
            return File(Encoding.UTF8.GetBytes(csv), "text/csv", "connections.csv");
        }

        /// <summary>
        /// Exporta todos os clusters em CSV.
        /// </summary>
        [HttpGet("snapshot/clusters")]
        public IActionResult ExportClustersCsv()
        {
            var csv = SnapshotService.GenerateClustersCsv(_brain.Clusters);
            return File(Encoding.UTF8.GetBytes(csv), "text/csv", "clusters.csv");
        }
    }

    /// <summary>
    /// Modelo para receber o estímulo via API.
    /// </summary>
    public class StimulusRequest
    {
        public required string Stimulus { get; set; }
    }

    /// <summary>
    /// Modelo para configurar o ciclo automático.
    /// </summary>
    public class CycleConfigRequest
    {
        public bool? Enabled { get; set; }
        public int? IntervalMs { get; set; }
    }
}
