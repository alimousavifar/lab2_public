using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics;

namespace MultiThreadPi
{
    class MainClass
    {
        private static readonly Random locked = new Random();
        static readonly object lockCompleted = new object();
        static void Main(string[] args)
        {
            long numberOfSamples = 1000;

            // Single Thread
            long hits = 0;
            double pi;
            Stopwatch stopwatch = new Stopwatch();

            Console.WriteLine("Single Thread Estimation!");
            stopwatch.Start();

            // Estimating Pi with Single Thread
            hits = GenerateSamples(numberOfSamples);
            pi = EstimatePI(numberOfSamples, ref hits);
            Console.WriteLine("The value of Pi is roughly: " + pi);

            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;

            string timeElapsed = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            Console.WriteLine("Time Elapsed for Single Thread: " + timeElapsed);
            Console.WriteLine(" ");
            Console.WriteLine(" ");
            Console.WriteLine(" ");

            ////////////////////////////////////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////// Multi Thread ////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////////////////////////////////////

            int threadNum = 10; // number of threads I want to make
            long[] multiThreadHits = new long[1];
            multiThreadHits[0] = 0;
            double multiThreadPi;
            Stopwatch multiThreadstopwatch = new Stopwatch();

            Console.WriteLine("Multi Thread Estimation!");
            multiThreadstopwatch.Start();



            // Estimating Pi with Multi Thread

            // Using Lecture Example
            List<Thread> myThreads = new List<Thread>();
            for (int i = 0; i < threadNum; i++)
            {
                var t = new Thread(() => multiThreadGenerateSamples(numberOfSamples, multiThreadHits, threadNum));
                t.Name = string.Format("Thread {0}", i + 1);
                t.Start();
                myThreads.Add(t);
            }
            foreach (Thread t in myThreads)
            {
                t.Join();
            }
            lock (lockCompleted)
            {
                multiThreadHits[0] /= 10;
                multiThreadPi = EstimatePI((numberOfSamples * threadNum), ref multiThreadHits[0]);
                Console.WriteLine("The value of Pi is roughly: " + multiThreadPi);
            }

            multiThreadstopwatch.Stop();
            TimeSpan multiThreadts = multiThreadstopwatch.Elapsed;

            string multiThreadTimeElapsed = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                multiThreadts.Hours, multiThreadts.Minutes, multiThreadts.Seconds, multiThreadts.Milliseconds / 10);
            Console.WriteLine("Time Elapsed for Multi Thread: " + timeElapsed);

            return;
        }
        private static double EstimatePI(long numberOfSamples, ref long hits)
        {
            //implement
            double value;
            Console.WriteLine(numberOfSamples);
            Console.WriteLine(hits);

            value = (Convert.ToDouble(hits) / Convert.ToDouble(numberOfSamples)) * 4.0;
            Console.WriteLine(value);
            return value;
        }
        static void multiThreadGenerateSamples(long numSamples, long[] hitsArray, int threadNum)
        {
            long hits = 0;
            for (int i = 0; i < threadNum; i++)
            {
                hits += GenerateSamples(numSamples);
                //Console.WriteLine("Total Hits: " + hits);
            }
            Console.WriteLine("hits " + hits);
            lock (lockCompleted)
            {
                hitsArray[0] += hits;
            }
        }
        static long GenerateSamples(long numberOfSamples) // returns coordinates
        {
            int root = locked.Next();
            var Rand = new Random(root);
            double x, y;
            // Implement  
            double[,] hitPointsList = new double[numberOfSamples, 2];

            for (int i = 0; i < numberOfSamples; i++)
            {
                hitPointsList[i, 0] = Rand.NextDouble() * 2;
                hitPointsList[i, 1] = Rand.NextDouble() * 2;
                //Console.WriteLine( "{0}, {1}", hitPointsList[i, 0], hitPointsList[i, 1]);
            }
            //Console.WriteLine("{0}, {1}", hitPointsList[69, 0], hitPointsList[69, 1]);
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
    }
}
