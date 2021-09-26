using System;

namespace MultiThreadPi
{
    class MainClass
    {
        static void Main(string[] args)
        {
            long numberOfSamples = 1000;
            long hits;
            double pi = EstimatePI(numberOfSamples, ref long hits);

            static double EstimatePI(long numberOfSamples, ref long hits)
            {
                //implement
            }

            static double[,] GenerateSamples(long numberOfSamples)
            {
                // Implement  

            }
        }
    }
}
