using NeuroGenesisAI.Core.Entities;
using NeuroGenesisAI.Core.Services;
using NeuroGenesisAI.InputOutput;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NeuroGenesisAI
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            Console.WriteLine("🌱 Iniciando NeuroGenesisAI Cérebro Infinito...");

            const string BrainFilePath = "brain.json";

            // Tenta carregar o cérebro salvo ou cria um novo
            var brain = BrainSerializer.LoadBrain(BrainFilePath);

            var trainer = new Trainer(brain);

            // Task para rodar a evolução dos neurônios (ciclos)
            var brainTask = Task.Run(async () =>
            {
                while (true)
                {
                    brain.Cycle();
                    await Task.Delay(500); // Meio segundo entre ciclos
                }
            });

            // Task para rodar o treinamento infinito (estímulos da internet)
            var trainingTask = trainer.RunInfiniteInternetTraining(5000); // A cada 5 segundos

            // Task para salvar o cérebro a cada 2 minutos
            var saveTask = Task.Run(async () =>
            {
                while (true)
                {
                    await Task.Delay(120000); // 2 minutos
                    BrainSerializer.SaveBrain(brain, BrainFilePath);
                    Console.WriteLine("💾 Cérebro salvo automaticamente.");
                }
            });

            Console.WriteLine("\n🚀 Cérebro está vivo! Pressione Ctrl+C para encerrar manualmente.");
            
            // Mantém a aplicação rodando até ser encerrada manualmente
            await Task.WhenAll(brainTask, trainingTask, saveTask);
        }
    }
}
