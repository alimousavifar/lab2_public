using System;

namespace MergeSort
{
    class Program
    {
        static void Main(string[] args)
        {

            ARRAY_SIZE = 1000;
            int[] ArraySingleThread = new int[ARRAY_SIZE];

            // copy array by value.. YOu can also use array.copy()
            int[] ArrayMultiThread = Unsorted.Slice(0,Unsorted.Length);


            // TODO : Use the "Random" class in a for loop to initialize an array



            /*TODO : Use the  "Stopwatch" class to measure the duration of time that
               it takes to sort an array using one-thread merge sort and
               multi-thead merge sort
            */


            //TODO :start the stopwatch
            MergeSort(ArraySingleThread);
            //TODO :Stop the stopwatch



            //TODO: Multi Threading Merge Sort







             /*********************** Methods **********************
              *****************************************************/
             /*
             implement Merge method. This method takes two sorted array and
             and constructs a sorted array in the size of combined arrays
             */

            static int[] Merge(int[] LA, int[] RA, int[] A)
            {

                // TODO :implement

            }


             /*
             implement MergeSort method: takes an integer array by reference
             and makes some recursive calls to intself and then sorts the array
             */
            static int[] MergeSort(int[] A)
            {

              // TODO :implement


            }


            // a helper function to print your array
            static void print_array(int[] myArray)
            {
                Console.Write("[");
                for (int i = 0; i < myArray.Length; i++)
                {
                    Console.Write("{0} ", myArray[i]);

                }
                Console.Write("]");
                Console.WriteLine();

            }

            // a helper function to confirm your array is sorted
            // returns boolean True if the array is sorted
            static bool isSorted(int[] a)
            {
                int j = a.Length - 1;
                if (j < 1) return true;
                int ai = a[0], i = 1;
                while (i <= j && ai <= (ai = a[i])) i++;
                return i > j;
            }


        }


    }
