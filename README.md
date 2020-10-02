---
layout: lab
title:  Lab 2 - Multi-Threading
date:   2020-10-30 12:00:00
authors: [C. Antonio Sánchez, Ali Mousavifar]
categories: [labs, threads, multithread, Mergesort]
usemath: true

---

# Lab 2 -- Multi-Threading
{:.no_toc}

In this lab, we will get some practice with creating threads and applying concurrency effectively in C\#.  

To help get you started, some of the code is posted on GitHub [here (https://github.com/alimousavifar/lab2_public/)](https://github.com/alimousavifar/lab2_public/).

Feel free to discuss approaches and solutions with your classmates, but labs are to be completed individually. Each student is expected to be able to answer questions about the content, describe their work, and reproduce their code (or parts thereof).

{% include toc.html %}

## Part 1: MergeSort

The MergeSort algorithm is one of the most efficient single-threaded sorting algorithms out there, with an expected computational complexity of $$O(n\log n)$$.  We will try to speed this algorithm up slightly by introducing concurrency.

Download the template provided on [GitHub](https://github.com/alimousavifar/lab2_public/).  We will create two versions of the MergeSort algorithm for sorting a set of random integers:
- `MergeSort Single Thread`: regular single-threaded version
- `MergeSort Multiple Threads`: a version which can break the initial array into multiple sub-arrays and sort them in separate threads and then sort the sub-arrays

We will then measure the computation time of both, and compute the *speed-up factor* to see how much we have gained.

The basic layout of your code should look as follows:
```
static void Main(string[] args)
{

    ARRAY_SIZE = 1000;
    int[] arraySingleThread = new int[ARRAY_SIZE];




    // TODO : Use the "Random" class in a for loop to initialize an array

    // copy array by value.. You can also use array.copy()
    int[] arrayMultiThread = arraySingleThread.Slice(0,arraySingleThread.Length);

    /*TODO : Use the  "Stopwatch" class to measure the duration of time that
       it takes to sort an array using one-thread merge sort and
       multi-thead merge sort
    */


    //TODO :start the stopwatch
    MergeSort(arraySingleThread);
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
    static void PrintArray(int[] myArray)
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
    static bool IsSorted(int[] a)
    {
        int j = a.Length - 1;
        if (j < 1) return true;
        int ai = a[0], i = 1;
        while (i <= j && ai <= (ai = a[i])) i++;
        return i > j;
    }


}

```
In the above implementation we have used arrays. Note you can use lists or vectors if you prefer. Like many other Programming languages, arrays are passed by refrenece and therefore, any changes that a method makes to an array will change the array after outside of the scope of the function. If you need a reminder of the implementation details of the Merge Sort algorithm, feel free to have a look at the page on [Wikipedia](https://en.wikipedia.org/wiki/Merge_sort). Additionally there is a very informative video by mycodeschool channel on [YouTube](https://www.youtube.com/watch?v=TzeBrDU-JaY)


### Use the helper functions to print and to test your algorithm
If the helpers functions are not provided to you, please create helper functions to do the following:
- Write a method to check that the outputs of both of your implementations are indeed sorted.  It's good to get into the habit of writing tests as you go along.
- Write a method to print arrays outputs of both of your implementations.

If they are provided to you, you may modify or enhance them for your use.

### Measuring computation time

To measure computation time, we will use the `Stopwatch` class from `System.Diagnostics` namespace.  The `Stopwatch` class contains a set of functions for measuring times in different units, and for computing and converting durations.  See the [documentation for durations](https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.stopwatch?view=netcore-3.1) for more details.  And that's it!  With these few lines of code, and a duration cast to a desired time resolution, you should be able to measure computation times in mili-seconds.

### Speed-up Factor

Recall that the speed-up factor is computed by

$$S_p = T_1/T_p$$

where $$p$$ is the number of processors or cores, and $$T_p$$ is the time taken for the code to run on $$p$$ processors.

### Multi Threading Merge Sort:
- Start by first manually subdividing the unsorted array into subarrays and sort them separately in a dedicated thread. And then merge them together. And test your algorithm.
- Then use a method which can subdividing unsorted array into multiple sub-arrays based on a variable integer (number of threads), e.g. if the thread# is 10 and the array size is 100, the process can create 10 similar size arrays and sort them in their own dedicated threads.
- Note that threads can take input arguments (Page 23 Lecture 3 via Lambda functions or Start() methods or [MS Docs](https://docs.microsoft.com/en-us/dotnet/standard/threading/creating-threads-and-passing-data-at-start-time))  
- Note that you can pass the unsorted array (as an argument of a method) into a thread, and sort it in the thread. Once the thread is done, the array in the main thread is sorted because arrays are passed into function by reference.
- Note that you must synchronize all the threads to join before you start merging the sorted sub-array from each thread


### Questions

1. In a table summarize the duration of time for your single thread and multi-thread merge sort algorithms for the following unsorted array sizes of {10, 10^2, 10^3, 10^4, 10^5, 10^6, 10^7}
2. How much speed-up were you expecting based on the number of processors/cores on your machine?
3. Did you achieve the speed-up you expected?  If not, what do you think might be interfering with this?
4. In your parallel implementation, how many threads were created (approximately) when sorting one million elements? Is there a way to reduce the number of new threads by about half without sacrificing parallelism?
