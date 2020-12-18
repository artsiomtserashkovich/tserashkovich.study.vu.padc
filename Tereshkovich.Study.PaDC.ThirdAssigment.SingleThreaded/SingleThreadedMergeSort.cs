using System.Collections.Generic;
using Tereshkovich.Study.PaDC.ThirdAssigment.Shared;

namespace Tereshkovich.Study.PaDC.ThirdAssigment.SingleThreaded
{
    public class SingleThreadedMergeSort : BaseMergeSorter
    {
        public override int[] Sort(int[] collection)
        {
            if (collection.Length <= 1)
            {
                return collection;
            }

            var (left, right) = GetHalfParts(collection);
            
            left = Sort(left);
            right = Sort(right);
            
            return MergeParts(left, right);
        }
    }
}