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
```c#
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
4. In your parallel implementation, try different number of threads for an array size of million elements. Observe the speed up factor as a function of thread size, i.e. speed-up factor (Y-axis) for #threads increasing (X-axis). Summarize your results in a table in your README file in the repo.



## Part 2: Monte Carlo Methods

The Monte-Carlo method is a general purpose technique for estimating quantities traditionally difficult to compute analytically. It involves estimating a quantity using random sampling over and over and over and over again, and averaging the results to come up with a `best guess' of the value.  In general, each sample is independent of all other samples, making this ideal for concurrent implementations.

For example, consider a game of monopoly.  What is the expected number of times of being sent to jail in a game with four players and 100 turns?  Not only do you have to consider the random rolls of the die, but also the community chest and chance cards.  Rather than try to go through all possible games with every possible dice roll for every possible turn, we can *simulate* a whole bunch of games -- say 100,000 -- count how many times each player ended up in jail, and average them.  This should give us a decent estimate of the true value.

In this part of the lab, we are going to use the Monte-Carlo method to estimate the value of $$\pi$$, and to integrate some functions.

### Generating random numbers


We need to create *random* numbers which are *distributted* uniformly [-1,1]. We need to create a random seed so that every time our code runs, we get a different random sequence. This is particularly important if we plean to use multi-threading as we need each thread to generate a distinct seed and consequently, a different random sequence. View [MS Docs](https://docs.microsoft.com/en-us/dotnet/api/system.random.nextdouble?view=netcore-3.1) for the details of implementation.


### Estimating PI

In order to estimate the value of PI using random sampling, we need to define a random function where we expect the answer to be PI.  The most common approach is to consider a circle with radius 1 inside a square with side-lengths 2.


The area of the unit circle is simply $$\pi$$.  Surround this with a bounding square.  The square has side-lenghts 2 and an area of 4.  If we were to randomly generate samples inside the square region [-1,1]x[-1,1], we would expect that the points would fall inside or on the unit circle with a probability of $$p=\pi/4$$, the ratio of the two areas.  If a random sample falls within the circle, we call that a *hit*.  If it falls outside the circle, we call that a *miss*.  All we need to do now is generate samples within the square region, count the fraction of hits, and multiply by four to recover the estimate of $$\pi$$. You can also watch this [video](https://www.youtube.com/watch?v=VJTFfIqO4TU) for further illustration.

Create a new solution in lab2  called `pi`, or download the template provided on [GitHub](https://github.com/alimousavifar/lab2_public/).  First, create a single-threaded method that estimates the value of PI.  The layout of the file should look something like the following:

```c#
using System;

namespace pi
{
    class Program
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

```

How accurate is your PI estimate with only 1000 random samples?  How many samples do you need to be accurate to 3 decimal places?

#### Speeding up the computation

One way to try to speed things up is to generate and check each random sample in a separate thread (**WARNING:** *use a very small number of samples, such as 1000).  Each sample is independent of each other, so we should be able to perform our sampling in parallel. Note that the number of *hits* between threads is the critical section of the code and needs to be protected during the parallel processing. Feel free to use MutEx or other locking mechanisms you learned in the lecture.


Fill in the function that does the random sampling and circle test. And use equal number of theads as samples 1000.  If you create too many threads at once you will get a system error.*  Is this method any faster than the single-threaded version?  Why not?

We want to limit the number of threads created in order to reduce overhead, but also try to maximize concurrency.  How many threads should we allow?  You can determine the number of cores on your machine using `Environment.ProcessorCount`.

A better way of splitting up the work is to create a small number of threads, and let each thread handle a certain number of samples. Create threads dynamically (in a function or like the [lecture example](https://bit.ly/2SrcAKn)) based on a thread# variable and sub-divide the sample amongst the threads. Try a few thread# including the number of cores on your machine ([Environment.ProcessorCount](https://bit.ly/3njGVZl)).

Note, that your GenerateSamples function should generate samples based on a random seed. See [this example](https://bit.ly/2GDASOq) for reference. Also,  

Now are you seeing a speed-up?  With so few threads, you can increase the number of samples again until you get an accuracy of 3 decimal places.  For how many samples does it become worth it to use multiple threads? 

In a README file in your repository, summarize the speed up factor of a single-thread and a multi-thread (use thread# = number of cores on your machine) as a funtion of the sample sizes {10^3, 10^4,..,10^7,10^8}.

#### Questions

1. What have you learned in terms of splitting up work between threads?
2. What implications does this have when designing concurrent code?
3. How many samples do you *think* you will need for an accuracy of 7 decimal places? Is the Monte Carlo simulation an efficient method to estimate Pi with high accuracy (feel free to research in the internet)?

