using System.Collections.Generic;
using System.Linq;
using Tereshkovich.Study.PaDC.ThirdAssigment.Shared;

namespace Tereshkovich.Study.PaDC.ThirdAssigment
{
    public class SingleThreadedMergeSort : BaseMergeSorter
    {
        public override ICollection<int> Sort(ICollection<int> collection)
        {
            if (collection.Count <= 1)
            {
                return collection;
            }

            var (left, right) = GetParts(collection);
            
            left = Sort(left);
            right = Sort(right);
            
            return MergeParts(left, right);
        }
    }
}