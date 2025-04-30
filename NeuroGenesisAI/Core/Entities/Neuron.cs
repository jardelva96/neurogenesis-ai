using NeuroGenesisAI.Core.Interfaces;
using System;
using System.Collections.Generic;
using NeuroGenesisAI.Core.Services;


namespace NeuroGenesisAI.Core.Entities
{
    public class Neuron : INeuron
    {
        private static readonly Random random = new();

        public string Id { get; private set; }
        public int MaturityLevel { get; private set; }
        public List<Connection> Connections { get; private set; } = new();

        private int inactivityCounter = 0;
        private const int MaxInactivity = 5;

        public string CurrentEmotion { get; private set; } = "Neutro";

        public Neuron(string id, int maturityLevel = 1)
        {
            Id = id;
            MaturityLevel = maturityLevel;
        }

        /// <summary>
        /// Executa o pensamento do neurônio.
        /// </summary>
        public void Think()
        {
            Console.WriteLine($"🧠 Neurônio {Id} (Maturidade {MaturityLevel}) pensando...");
            inactivityCounter++;

            if (random.NextDouble() < 0.2) // 20% chance de insight
            {
                inactivityCounter = 0;
                Console.WriteLine($"💡 Neurônio {Id} teve um insight!");
                LoggerService.Log($"Insight: Neurônio {Id} teve um insight!");

            }
        }

        /// <summary>
        /// Gera um novo neurônio conectado, com alta chance.
        /// </summary>
        public INeuron? GenerateNeuron()
        {
            if (random.NextDouble() < 0.8) // 80% de chance de gerar
            {
                var newId = $"{Id}-{Connections.Count + 1}";
                var newMaturity = MaturityLevel + random.Next(0, 2);
                Console.WriteLine($"🌱 Neurônio {Id} criou novo neurônio {newId}.");
                LoggerService.Log($"Neurônio {Id} gerou novo neurônio {newId}.");

                return new Neuron(newId, newMaturity);
            }
            return null;
        }

        /// <summary>
        /// Conecta este neurônio a outro neurônio.
        /// </summary>
        public void Connect(INeuron target)
        {
            var connection = new Connection(this, target);
            Connections.Add(connection);
            Console.WriteLine($"🔗 {Id} conectado a {target.Id} (força {connection.Strength})");
            LoggerService.Log($"Conexão criada: {Id} conectado a {target.Id} (força {connection.Strength})");

        }

        /// <summary>
        /// Recebe estímulo externo, ajusta emoção e comportamento.
        /// </summary>
        public void ReceiveStimulus(string stimulus, string? clusterSpecialization)
        {
            inactivityCounter = 0;
            CurrentEmotion = InterpretStimulus(stimulus);

            Console.WriteLine($"⚡ Neurônio {Id} recebeu estímulo: {stimulus} (Cluster: {clusterSpecialization}) -> emoção: {CurrentEmotion}");

            bool matchesSpecialization = clusterSpecialization != null && stimulus.ToLower().Contains(clusterSpecialization.ToLower());

            switch (CurrentEmotion)
            {
                case "Alegria":
                    MaturityLevel += matchesSpecialization ? 3 : 2;
                    break;

                case "Medo":
                    foreach (var conn in Connections)
                    {
                        conn.Decay(matchesSpecialization ? 0.3 : 0.2);
                    }
                    break;

                case "Curiosidade":
                    MaturityLevel += matchesSpecialization ? 2 : 1;
                    break;
            }
        }

        /// <summary>
        /// Faz o neurônio envelhecer.
        /// </summary>
        public void Age()
        {
            MaturityLevel++;
        }

        /// <summary>
        /// Verifica se o neurônio deve morrer por inatividade.
        /// </summary>
        public bool ShouldDie()
        {
            return inactivityCounter >= MaxInactivity;
        }

        /// <summary>
        /// Aplica plasticidade: enfraquece conexões e remove as muito fracas.
        /// </summary>
        public void ApplyPlasticity()
        {
            foreach (var connection in Connections)
            {
                connection.Decay();
            }

            Connections.RemoveAll(c => c.Strength <= 0.1);
        }

        /// <summary>
        /// Interpreta estímulo e determina emoção.
        /// </summary>
        private string InterpretStimulus(string stimulus)
        {
            stimulus = stimulus.ToLower();

            if (stimulus.Contains("luz") || stimulus.Contains("comida"))
                return "Alegria";

            if (stimulus.Contains("som") || stimulus.Contains("dor"))
                return "Medo";

            return "Curiosidade"; // Estímulos desconhecidos geram curiosidade
        }
    }
}
