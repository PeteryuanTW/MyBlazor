using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlazor.Shared.DataClass
{
    public class GAConfig
    {
        public int population;
        public int iteration;
        public int parentRemainCount;
        public float mutationRate;
        public GAConfig()
        {
            population = 50;
            iteration = 30;
            parentRemainCount = 5;
            mutationRate = 0.75f;//0.5%~1% was suggested
        }

        public GAConfig(int population, int parentRemainCount, int iteration, float mutationRate)
        {
            this.population = population;
            this.parentRemainCount = parentRemainCount;
            this.iteration = iteration;
            this.mutationRate = mutationRate;
        }
    }
}
