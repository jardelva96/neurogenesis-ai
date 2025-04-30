using NeuroGenesisAI.Core.Entities;
using NeuroGenesisAI.Core.Interfaces;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NeuroGenesisAI.InputOutput
{
    public static class BrainSerializer
    {
        private static readonly JsonSerializerOptions Options = new()
        {
            WriteIndented = true,
            ReferenceHandler = ReferenceHandler.Preserve
        };

        public static void SaveBrain(Brain brain, string filePath)
        {
            var json = JsonSerializer.Serialize(brain, Options);
            File.WriteAllText(filePath, json);
            Console.WriteLine($"💾 Cérebro salvo em {filePath}");
        }

        public static Brain LoadBrain(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"❌ Arquivo {filePath} não encontrado. Criando novo cérebro...");
                return new Brain();
            }

            var json = File.ReadAllText(filePath);
            var brain = JsonSerializer.Deserialize<Brain>(json, Options);
            Console.WriteLine($"📂 Cérebro carregado de {filePath}");
            return brain ?? new Brain();
        }
    }
}
