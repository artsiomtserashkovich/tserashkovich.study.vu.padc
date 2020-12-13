using Tereshkovich.Study.PaDC.ThirdAssigment.Shared;

namespace Tereshkovich.Study.PaDC.ThirdAssigment.SingleThreaded
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new FileCollectionFactory();
            var sorter = new SingleThreadedMergeSort();
            
            var collection = factory.GetRandomCollection();
            
            var result = sorter.Sort(collection);
        }
    }
}