using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Tereshkovich.Study.PaDC.ThirdAssigment.BothBranchesMultiThreaded;
using Tereshkovich.Study.PaDC.ThirdAssigment.SingleBranchMultiThreaded;
using Tereshkovich.Study.PaDC.ThirdAssigment.SingleThreaded;

namespace Tereshkovich.Study.PaDC.ThirdAssigment.Benchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            var random = new Random();
            var collection = Enumerable
                .Range(0, 50_000_000)
                .Select(_ => random.Next())
                .ToArray();
            
            var singleThreadedSorter = new SingleThreadedMergeSort();
            var prestart = singleThreadedSorter.Sort(collection);
            
            Task.Delay(500).Wait();
            
            var singleThreadedStopwatch = new Stopwatch();
            singleThreadedStopwatch.Start();
            var singleThreadedResult = singleThreadedSorter.Sort(collection);
            singleThreadedStopwatch.Stop();
            Console.WriteLine("Single Threaded Result={0}",singleThreadedStopwatch.Elapsed);
            
            Task.Delay(500).Wait();
            
            for (var threadCount = 1; threadCount <= 8; threadCount++)
            {
                var leftBranchSorter = new LeftBranchMultiThreadedMergeSorter(threadCount);

                var leftBranchStopwatch = new Stopwatch();
                leftBranchStopwatch.Start();
                var result = leftBranchSorter.Sort(collection);
                leftBranchStopwatch.Stop();
                
                Console.WriteLine($"Left Branch Result({threadCount} thread)={leftBranchStopwatch.Elapsed}");
                Console.WriteLine($"Is result the same with single: {result.SequenceEqual(singleThreadedResult)}");

                Task.Delay(500).Wait();
            }
            
            for (var threadCount = 1; threadCount <= 8; threadCount++)
            {
                var bothBranchSorter = new BothBranchMultiThreadedMergeSorter(threadCount);

                var bothBranchStopwatch = new Stopwatch();
                bothBranchStopwatch.Start();
                var result = bothBranchSorter.Sort(collection);
                bothBranchStopwatch.Stop();
                
                Console.WriteLine($"Both Branch Result({threadCount} thread)={bothBranchStopwatch.Elapsed}");
                Console.WriteLine($"Is result the same with single: {result.SequenceEqual(singleThreadedResult)}");

                Task.Delay(500).Wait();
            }
        }
    }
}