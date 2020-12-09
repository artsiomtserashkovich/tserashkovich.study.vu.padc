using System;
using System.Diagnostics;
using System.Linq;
using Tereshkovich.Study.PaDC.ThirdAssigment.Shared;

namespace Tereshkovich.Study.PaDC.ThirdAssigment.MultiThreaded
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new FileCollectionFactory();
            var singleThreadSorter = new SingleThreadedMergeSort();
            var collection = factory.GetRandomCollection();

            var singleThreadResult = singleThreadSorter.Sort(collection);

            for (var threadCount = 1; threadCount <= 8; threadCount++)
            {
                //Console.WriteLine($"Start multithread with count: {threadCount}");

                var sorter = new BothBranchMultiThreadedMergeSorter(threadCount);

                Stopwatch sw = new Stopwatch();
                sw.Start();
                
                var result = sorter.Sort(collection);
                
                sw.Stop();
                Console.WriteLine("Elapsed={0}",sw.Elapsed);

                //Console.WriteLine($"Is result the same with single: {result.SequenceEqual(singleThreadResult)}");
            }
            
            
        }
    }
}