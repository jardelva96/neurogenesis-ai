namespace NeuroGenesisAI.Core.Interfaces
{
    public interface INeuron
    {
        string Id { get; }
        int MaturityLevel { get; }
        void Think();
        INeuron? GenerateNeuron();
        void Connect(INeuron target);
        void ReceiveStimulus(string stimulus, string? clusterSpecialization);
        void Age();  // Maturidade aumenta
        bool ShouldDie();  // Decide se o neurônio morre
    }
}
