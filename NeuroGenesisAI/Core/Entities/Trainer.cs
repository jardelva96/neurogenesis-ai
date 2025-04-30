using System;
using System.Threading.Tasks;
using NeuroGenesisAI.Core.Services;

namespace NeuroGenesisAI.Core.Entities
{
    public class Trainer
    {
        private readonly Brain _brain;
        private readonly Random _random = new();
        private readonly InternetStimulusService _internetService = new();

        public Trainer(Brain brain)
        {
            _brain = brain;
        }

        /// <summary>
        /// Executa um treino básico enviando estímulos aleatórios ao cérebro.
        /// </summary>
        public async Task RunBasicTraining(int rounds, int delayMilliseconds = 1000)
        {
            string[] stimuli = new[] { "Luz forte", "Som alto", "Memória de infância", "Movimento rápido" };

            for (int i = 0; i < rounds; i++)
            {
                var stimulus = stimuli[_random.Next(stimuli.Length)];
                Console.WriteLine($"\n🎯 Treinamento Básico - Estímulo: {stimulus}");

                _brain.Stimulate(stimulus);

                await Task.Delay(delayMilliseconds);
            }
        }

        /// <summary>
        /// Executa um treino focado em um tipo específico de estímulo.
        /// Ideal para especializar clusters.
        /// </summary>
        public async Task RunFocusedTraining(string stimulusType, int rounds, int delayMilliseconds = 1000)
        {
            for (int i = 0; i < rounds; i++)
            {
                Console.WriteLine($"\n🎯 Treinamento Focado - Estímulo: {stimulusType}");

                _brain.Stimulate(stimulusType);

                await Task.Delay(delayMilliseconds);
            }
        }

        /// <summary>
        /// Executa desafios cognitivos: sequência de estímulos complexos.
        /// Simula aprendizado real e associações entre clusters.
        /// </summary>
        public async Task RunChallengeTraining(int rounds, int delayMilliseconds = 1500)
        {
            string[][] challenges = new[]
            {
                new[] { "Luz forte", "Movimento rápido" },
                new[] { "Som alto", "Memória de infância" },
                new[] { "Luz", "Som", "Movimento" },
                new[] { "Memória de evento antigo", "Som baixo" }
            };

            for (int i = 0; i < rounds; i++)
            {
                var challenge = challenges[_random.Next(challenges.Length)];

                Console.WriteLine($"\n🏆 Desafio Cognitivo {i + 1}:");

                foreach (var stimulus in challenge)
                {
                    Console.WriteLine($"🔹 Estímulo: {stimulus}");
                    _brain.Stimulate(stimulus);
                    await Task.Delay(delayMilliseconds);
                }

                await Task.Delay(delayMilliseconds * 2);
            }
        }

        /// <summary>
        /// Executa treino contínuo puxando estímulos reais da internet para sempre.
        /// O cérebro cresce, morre, e aprende indefinidamente.
        /// </summary>
        public async Task RunInfiniteInternetTraining(int delayMilliseconds = 5000)
        {
            Console.WriteLine("\n🚀 Iniciando Treinamento Infinito com Estímulos da Internet...");

            while (true)
            {
                try
                {
                    var stimulus = await _internetService.GetStimulusAsync();
                    Console.WriteLine($"\n🌐 Estímulo da Internet recebido: {stimulus}");

                    _brain.Stimulate(stimulus);

                    await Task.Delay(delayMilliseconds);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"⚠️ Erro ao buscar estímulo: {ex.Message}");
                    await Task.Delay(2000); // Aguarda 2 segundos e tenta novamente
                }
            }
        }
    }
}
