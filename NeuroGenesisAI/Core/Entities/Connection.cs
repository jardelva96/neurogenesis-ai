using NeuroGenesisAI.Core.Interfaces;

namespace NeuroGenesisAI.Core.Entities
{
    public class Connection
    {
        public INeuron Source { get; private set; }
        public INeuron Target { get; private set; }
        public double Strength { get; private set; }

        public Connection(INeuron source, INeuron target, double strength = 1.0)
        {
            Source = source;
            Target = target;
            Strength = strength;
        }

        public void Weaken(double amount)
        {
            Strength -= amount;
            if (Strength < 0) Strength = 0;
        }

        public void Strengthen(double amount)
        {
            Strength += amount;
        }
        public void Decay(double decayRate = 0.05)
        {
            Strength -= decayRate;
            if (Strength < 0) Strength = 0;
        }
    }
}
