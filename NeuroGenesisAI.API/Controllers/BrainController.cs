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
                    maturityLevel = n.MaturityLevel
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
        public string Stimulus { get; set; }
    }
}
