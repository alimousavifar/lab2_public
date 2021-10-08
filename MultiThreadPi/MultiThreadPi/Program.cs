using System;

namespace MultiThreadPi
{
    class MainClass
    {
        static void Main(string[] args)
        {
            long numberOfSamples = 1000;
            long hits = 3;
            double pi;
            hits = GenerateSamples(numberOfSamples);
            Console.WriteLine(hits);
            pi = EstimatePI(numberOfSamples, ref hits);
            Console.WriteLine("The value of Pi is roughly: " + pi);

            return;
        }
        private static double EstimatePI(long numberOfSamples, ref long hits)
        {
            //implement
            double value = 3.14;
            Console.WriteLine(numberOfSamples);
            Console.WriteLine(hits);

            value = (hits / numberOfSamples) * 4;
            Console.WriteLine(value);
            return value;
        }
        static long GenerateSamples(long numberOfSamples) // returns coordinates
        {
            var Rand = new Random();
            double x, y;
            // Implement  
            double[,] hitPointsList = new double[numberOfSamples, 2];

            for (int i = 0; i < numberOfSamples; i++)
            {
                hitPointsList[i, 0] = Rand.NextDouble() * 2;
                hitPointsList[i, 1] = Rand.NextDouble() * 2;
                //Console.WriteLine( "{0}, {1}", hitPointsList[i, 0], hitPointsList[i, 1]);
            }
            long circle = 0;
            for (int i = 0; i < numberOfSamples; i++)
            {
                x = hitPointsList[i, 0];
                y = hitPointsList[i, 1];

                if (((x - 1) * (x - 1)) + ((y - 1) * (y - 1)) <= 1)
                {
                    circle++;
                }
            }
            return circle;
        }

        static long hitOrMiss(double[,] hitPointsList, long numberOfSamples) 
        {
            long circle = 0;
            double x, y;
            for(int i= 0; i < numberOfSamples; i++)
            {
                x = hitPointsList[i, 0];
                y = hitPointsList[i, 1];

                if (((x - 1) * (x - 1)) + ((y - 1) * (y - 1)) <= 1)
                {
                    circle++;
                }
            }
            return circle;
        }
        /*private static int RNG()
        {
            int PosNeg;
            int value = 69;
            var Rand = new Random();

            PosNeg = Rand.Next(0, 15); // generates either 0 or 1
            
            Console.WriteLine(PosNeg);

            if(PosNeg == 0)
            {
                value = 1;
                return value;
            }
            else
            {
                value =-1;
                return value;
            }
            //  return value;

        }*/
    }
}
