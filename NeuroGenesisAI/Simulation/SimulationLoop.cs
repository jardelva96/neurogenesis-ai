using NeuroGenesisAI.Core.Entities;
using System;
using System.Threading;

namespace NeuroGenesisAI.Simulation
{
    public class SimulationLoop
    {
        private readonly Brain _brain;
        private readonly int _cycleDelayMilliseconds;
        private bool _isRunning;

        public SimulationLoop(Brain brain, int cycleDelayMilliseconds = 1000)
        {
            _brain = brain;
            _cycleDelayMilliseconds = cycleDelayMilliseconds;
            _isRunning = true;
        }

        public void Start()
        {
            int cycle = 1;

            while (_isRunning)
            {
                Console.WriteLine($"\n🧪 Iniciando Ciclo {cycle}");
                _brain.Cycle();

                // Verificar se há estímulos
                CheckForStimuli();

                Thread.Sleep(_cycleDelayMilliseconds);

                cycle++;

                // Exemplo: Parar a simulação após 50 ciclos
                if (cycle > 50)
                {
                    Console.WriteLine("\n🏁 Número máximo de ciclos alcançado. Finalizando simulação...");
                    _isRunning = false;
                }
            }
        }

        private void CheckForStimuli()
        {
            if (Console.KeyAvailable)
            {
                var keyInfo = Console.ReadKey(intercept: true);
                if (keyInfo.Key == ConsoleKey.S)
                {
                    Console.WriteLine("\n⚡ Estímulo: 'Som' recebido!");
                    _brain.Stimulate("Som");
                }
                else if (keyInfo.Key == ConsoleKey.L)
                {
                    Console.WriteLine("\n⚡ Estímulo: 'Luz' recebido!");
                    _brain.Stimulate("Luz");
                }
                else if (keyInfo.Key == ConsoleKey.Escape)
                {
                    Console.WriteLine("\n🛑 Finalizando simulação manualmente...");
                    _isRunning = false;
                }
            }
        }
    }
}
