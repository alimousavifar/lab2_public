using System;
using System.Threading;
using System.Diagnostics;

namespace MergeSort
{
    class Program
    {
        static void Main(string[] args)
        {

            int ARRAY_SIZE = 10000;
            int divider = 17;
            int[] arraySingleThread = new int[ARRAY_SIZE];
            var Rand = new Random();
            Stopwatch stopWatch = new Stopwatch();



            // TODO : Use the "Random" class in a for loop to initialize an array
            for (int i = 0; i < arraySingleThread.Length; i++)
            {
                arraySingleThread[i] = Rand.Next(1, 9999999);
                //Console.WriteLine(arraySingleThread[i]);
            }

            // copy array by value.. You can also use array.copy()
            int[] arrayMultiThread = new int [arraySingleThread.Length];
            arraySingleThread.CopyTo(arrayMultiThread, 0);

            /*TODO : Use the  "Stopwatch" class to measure the duration of time that
               it takes to sort an array using one-thread merge sort and
               multi-thead merge sort
            */


            //TODO :start the stopwatch
            stopWatch.Start();

            MergeSort(arraySingleThread, 0, arraySingleThread.Length-1);
            //StartTheThread(arraySingleThread, 0, arraySingleThread.Length - 1);
            //SubdivideArray(arraySingleThread, divider);

            //TODO :Stop the stopwatch
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;

            string timeElapsed = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                                ts.Hours, ts.Minutes, ts.Seconds,
                                                ts.Milliseconds / 10);
            Console.WriteLine("Single Thread time Elapsed: " + timeElapsed);
            //Console.WriteLine("Final Thread");
            //PrintArray(arraySingleThread);

            //TODO: Multi Threading Merge Sort

            void SubdivideArray(int[] array, int divider)
            {
                int[][] newArray = new int[divider][];
                int[] replacementArray = new int[array.Length];
                int[] replacementArray2 = new int[array.Length];
                if (divider > array.Length)
                {
                    //Console.WriteLine("Divider too big");
                    return;
                }
                else
                {
                    int newArraySize = array.Length / divider; // 2
                    int leftover = array.Length % divider; // 1
                    int size;
                    int counter = 0;
                    for (int i = 0; i < divider; i++)
                    {
                        if (leftover > 0)
                        {
                            size = newArraySize + 1;
                            leftover--;
                        }
                        else
                        {
                            size = newArraySize;
                        }

                        //int[] newArray = new int[size];
                        newArray[i] = new int[size];
                        Array.Copy(array, counter, newArray[i], 0, size);
                        StartTheThread(newArray[i], 0, size - 1);
                        
                        counter += size;
                    }
                    Array.Copy(newArray[0], 0, replacementArray2, 0, newArray[0].Length);
                    Array.Copy(newArray[0], 0, replacementArray, 0, newArray[0].Length);
                    for(int i = 1; i < divider; i++)
                    {
                        //Console.WriteLine("Counter" + i);
                        MergeArrays(replacementArray2, newArray[i], replacementArray, i);
                        Array.Copy(replacementArray, 0, replacementArray2, 0, array.Length);
                        //Console.WriteLine("Replacement Array 2 " + i);
                        //PrintArray(replacementArray2);
                    }
                    //Console.WriteLine("FInal Replacement");
                    Array.Copy(replacementArray, 0, array, 0, array.Length);
                }
            }
            
            Thread StartTheThread(int[] array, int left, int right)
            {
                var t = new Thread(() => MergeSort(array, left, right));
                t.Start();
                t.Join();
                //PrintArray(array);
                return t;
            }

            /*********************** Methods **********************
             *****************************************************/
            /*
            implement Merge method. This method takes two sorted array and
            and constructs a sorted array in the size of combined arrays
            */

            void MergeArrays(int[] array1, int[] array2, int[] copiedArray, int iteration)
            {
                int i = 0;
                int j = 0;
                int k = 0;

                while (i <= (array2.Length * iteration) && j < array2.Length && k < copiedArray.Length)
                {
                    if (array1[i] != 0)
                    {
                        if (array1[i] < array2[j])
                        {
                            //Console.WriteLine("Top Function i " + k);
                            //Console.WriteLine("k and i ");
                            //Console.WriteLine(k);
                            //Console.WriteLine(i);
                            copiedArray[k] = array1[i];
                            i++;
                            k++;
                        }
                        else
                        {
                            //Console.WriteLine("Top Function j " + k);
                            //Console.WriteLine("k and j ");
                            //Console.WriteLine(k);
                            //Console.WriteLine(j);
                            copiedArray[k] = array2[j];
                            j++;
                            k++;
                        }
                    }
                    else
                    {
                        i++;
                    }
                }
                // copy the rest of the elements in Left[]
                while (j < array2.Length && k < copiedArray.Length)
                {
                    //Console.WriteLine("Bottom Function j");

                    //Console.WriteLine("k is" + k);
                    copiedArray[k] = array2[j];
                    j++;
                    k++;
                }
                while (i < ((array2.Length +1) * iteration) && k < copiedArray.Length)
                {
                    //Console.WriteLine("Bottom Function i");

                    //Console.WriteLine("k is" + k);
                    copiedArray[k] = array1[i];
                    i++;
                    k++;
                }
                
            }

            void Merge(int[] array, int left, int mid, int right)
            {

                // TODO :implement
                // variables for looping
                int i, j, k = 0;
                int l = mid - left + 1;
                int m = right - mid;

                // create temporary arrays
                int[] Left = new int[l]; 
                int[] Right = new int[m];

                // Copy left half to left array and
                // right half to right array
                for (i = 0; i < l; i++) Left[i] = array[left + i];
                for (j = 0; j < m; j++) Right[j] = array[mid + 1 + j];

                // Merge the temporary Arrays Together
                i = 0;
                j = 0;
                k = left;// initial index of merged subarray array

                while(i < l && j < m)
                {
                    if(Left[i] <= Right[j])
                    {
                        array[k] = Left[i];
                        i++;
                    }
                    else
                    {
                        array[k] = Right[j];
                        j++;
                    }
                    k++;
                }
                
                // copy the rest of the elements in Left[]
                while (i < l)
                {
                    array[k] = Left[i];
                    i++;
                    k++;
                }

                while (j < m)
                {
                    array[k] = Right[j];
                    j++;
                    k++;
                }
            }


             /*
             implement MergeSort method: takes an integer array by reference
             and makes some recursive calls to intself and then sorts the array
             */
            void MergeSort(int[] array, int left, int right)
            {
                // TODO :implement
                if(left < right)
                {
                    int mid = left + (right - left) / 2;

                    MergeSort(array, left, mid);
                    MergeSort(array, mid + 1, right);

                    Merge(array, left, mid, right);                    
                }

            }


            // a helper function to print your array
            static void PrintArray(int[] myArray)
            {
                Console.Write("[");
                for (int i = 0; i < myArray.Length; i++)
                {
                    Console.WriteLine("Number {0} is {1}", i, myArray[i]);

                }
                Console.Write("]");
                Console.WriteLine();

            }

            // a helper function to confirm your array is sorted
            // returns boolean True if the array is sorted
            static bool IsSorted(int[] a)
            {
                int j = a.Length - 1;
                if (j < 1) return true;
                int ai = a[0], i = 1;
                while (i <= j && ai <= (ai = a[i])) i++;
                return i > j;
            }


        }
    }
}
