using System;
using System.Diagnostics;
using System.Linq;
using Tereshkovich.Study.PaDC.ThirdAssigment.Shared;

namespace Tereshkovich.Study.PaDC.ThirdAssigment.SingleThreaded
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new FileCollectionFactory();
            var sorter = new SingleThreadedMergeSort();
            
            var collection = factory.GetRandomCollection().ToArray();
            
            Console.WriteLine("Sort started");
            Stopwatch sw = new Stopwatch();
            sw.Start();

            var result = sorter.Sort(collection);
            
            sw.Stop();
            Console.WriteLine("Elapsed={0}",sw.Elapsed);
        }
    }
}