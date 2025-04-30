namespace NeuroGenesisAI.InputOutput
{
    public static class InputHandler
    {
        public static string ReadStimulus()
        {
            Console.WriteLine("Digite um estímulo para enviar ao cérebro (ou pressione Enter para ignorar):");
            var input = Console.ReadLine();
            return string.IsNullOrWhiteSpace(input) ? string.Empty : input;
        }
    }
}
