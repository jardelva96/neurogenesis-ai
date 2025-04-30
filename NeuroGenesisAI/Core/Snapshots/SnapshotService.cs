using System.Text;
using NeuroGenesisAI.Core.Entities;
using NeuroGenesisAI.Core.Interfaces;


namespace NeuroGenesisAI.Core.Snapshots
{
    /// <summary>
    /// Serviço para gerar snapshots (exportações) do cérebro em CSV.
    /// </summary>
    public static class SnapshotService
    {
        /// <summary>
        /// Gera um CSV com todos os neurônios do cérebro.
        /// </summary>
        public static string GenerateNeuronsCsv(List<INeuron> neurons)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Id,MaturityLevel,CurrentEmotion");

            foreach (var neuron in neurons)
            {
                if (neuron is Neuron concreteNeuron)
                {
                    sb.AppendLine($"{concreteNeuron.Id},{concreteNeuron.MaturityLevel},{concreteNeuron.CurrentEmotion}");
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Gera um CSV com todas as conexões entre neurônios.
        /// </summary>
        public static string GenerateConnectionsCsv(List<INeuron> neurons)
        {
            var sb = new StringBuilder();
            sb.AppendLine("SourceId,TargetId,Strength");

            foreach (var neuron in neurons)
            {
                if (neuron is Neuron concreteNeuron)
                {
                    foreach (var conn in concreteNeuron.Connections)
                    {
                        sb.AppendLine($"{conn.Source.Id},{conn.Target.Id},{conn.Strength:F2}");
                    }
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Gera um CSV com os clusters e seus neurônios.
        /// </summary>
        public static string GenerateClustersCsv(List<Cluster> clusters)
        {
            var sb = new StringBuilder();
            sb.AppendLine("ClusterName,NeuronId");

            foreach (var cluster in clusters)
            {
                foreach (var neuron in cluster.Neurons)
                {
                    sb.AppendLine($"{cluster.Name},{neuron.Id}");
                }
            }

            return sb.ToString();
        }
    }
}
