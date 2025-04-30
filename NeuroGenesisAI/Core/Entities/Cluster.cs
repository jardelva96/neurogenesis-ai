using System.Collections.Generic;
using NeuroGenesisAI.Core.Interfaces;

namespace NeuroGenesisAI.Core.Entities
{
    public class Cluster
    {
        public string Name { get; private set; }
        public List<INeuron> Neurons { get; private set; }
        public string Specialization { get; private set; }

        public Cluster(string name, string specialization)
        {
            Name = name;
            Specialization = specialization;
            Neurons = new List<INeuron>();
        }

        public void AddNeuron(INeuron neuron)
        {
            Neurons.Add(neuron);
        }

        public void RemoveNeuron(INeuron neuron)
        {
            Neurons.Remove(neuron);
        }
    }
}
