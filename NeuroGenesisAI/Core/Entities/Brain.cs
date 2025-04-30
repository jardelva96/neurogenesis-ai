using NeuroGenesisAI.Core.Interfaces;
using NeuroGenesisAI.Core.Services; // 👈 IMPORTANTE para usar LoggerService
using System;
using System.Collections.Generic;
using System.Linq; // 👈 Para usar .Where() e .ToList()

namespace NeuroGenesisAI.Core.Entities
{
    public class Brain
    {
        public List<INeuron> Neurons { get; private set; } = new();  // Lista de todos os neurônios do cérebro
        public List<Cluster> Clusters { get; private set; } = new(); // Lista de clusters especializados

        private const int MaxNeurons = 100000; // Limite de segurança para não travar o sistema
        private static readonly Random random = new();

        public Brain()
        {
            var root = new Neuron("root");
            Neurons.Add(root);

            // Inicializa clusters especializados
            Clusters.Add(new Cluster("Visão", "Luz"));
            Clusters.Add(new Cluster("Audição", "Som"));
            Clusters.Add(new Cluster("Memória", "Memória"));
            Clusters.Add(new Cluster("Movimento", "Movimento"));

            // Coloca o neurônio root no cluster Memória
            Clusters[2].AddNeuron(root);
        }

        /// <summary>
        /// Executa um ciclo completo de simulação:
        /// pensamento, crescimento, plasticidade e reorganização neural.
        /// </summary>
        public void Cycle()
        {
            var newNeurons = new List<INeuron>();

            foreach (var neuron in Neurons)
            {
                neuron.Think();

                var generated = neuron.GenerateNeuron();
                if (generated != null && Neurons.Count + newNeurons.Count < MaxNeurons)
                {
                    neuron.Connect(generated);
                    newNeurons.Add(generated);
                }

                neuron.Age();
            }

            // Adiciona novos neurônios
            Neurons.AddRange(newNeurons);

            // Atribui novos neurônios a clusters aleatórios
            foreach (var neuron in newNeurons)
            {
                var cluster = Clusters[random.Next(Clusters.Count)];
                cluster.AddNeuron(neuron);
            }

            // Aplica plasticidade: remove conexões fracas
            foreach (var neuron in Neurons)
            {
                if (neuron is Neuron concreteNeuron)
                {
                    concreteNeuron.ApplyPlasticity();
                }
            }

            // Remoção de neurônios isolados e maduros
            var neuronsToRemove = Neurons.Where(neuron =>
            {
                if (neuron is Neuron concreteNeuron)
                {
                    return concreteNeuron.Connections.Count == 0 && concreteNeuron.MaturityLevel > 5;
                }
                return false;
            }).ToList();

            // Logar cada morte individualmente
            foreach (var deadNeuron in neuronsToRemove)
            {
                LoggerService.Log($"💀 Neurônio {deadNeuron.Id} morreu (isolado e maduro).");
            }

            // Agora remover de fato
            Neurons.RemoveAll(neuron => neuronsToRemove.Contains(neuron));

            // Atualiza clusters para remover neurônios mortos
            foreach (var cluster in Clusters)
            {
                cluster.Neurons.RemoveAll(n => !Neurons.Contains(n));
            }
        }

        /// <summary>
        /// Envia um estímulo externo a todos os neurônios vivos.
        /// </summary>
        public void Stimulate(string stimulus)
        {
            foreach (var neuron in Neurons)
            {
                var cluster = FindClusterOfNeuron(neuron);
                if (cluster != null)
                {
                    neuron.ReceiveStimulus(stimulus, cluster.Specialization);
                }
                else
                {
                    neuron.ReceiveStimulus(stimulus, null);
                }
            }

            LoggerService.Log($"⚡ Estímulo recebido: '{stimulus}' enviado para todos os neurônios.");
        }

        /// <summary>
        /// Encontra o cluster ao qual um neurônio pertence.
        /// </summary>
        private Cluster? FindClusterOfNeuron(INeuron neuron)
        {
            foreach (var cluster in Clusters)
            {
                if (cluster.Neurons.Contains(neuron))
                    return cluster;
            }
            return null;
        }
    }
}
