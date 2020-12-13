using System;
using System.Collections.Generic;
using System.Threading;
using Tereshkovich.Study.PaDC.ThirdAssigment.Shared;
using Tereshkovich.Study.PaDC.ThirdAssigment.SingleThreaded;

namespace Tereshkovich.Study.PaDC.ThirdAssigment.SingleBranchMultiThreaded
{
     public class LeftBranchMultiThreadedMergeSorter : BaseMergeSorter
    {
        private readonly int _threadCount;

        public LeftBranchMultiThreadedMergeSorter(int threadCount)
        {
            if(threadCount <= 0)
            {
                throw new ArgumentException("Thread Count can't be zero or less.", nameof(threadCount));
            }
            
            _threadCount = threadCount;
        }
        
        public override ICollection<int> Sort(ICollection<int> collection)
        {
            if (collection.Count <= 1)
            {
                return collection;
            }
            
            var (left, right) = GetHalfParts(collection);
            
            var singleThreadSorter = new SingleThreadedMergeSort();

            if (_threadCount == 1)
            {
                left = singleThreadSorter.Sort(left);
                right = singleThreadSorter.Sort(right);
            }
            else
            {
                var leftThread = new Thread(() =>
                {
                    var leftMultiThreadedSorter = new LeftBranchMultiThreadedMergeSorter(_threadCount - 1);
                    
                    left = leftMultiThreadedSorter.Sort(left);
                });
                leftThread.Start();
                
                right = singleThreadSorter.Sort(right);

                leftThread.Join();
            }

            return MergeParts(left, right);
        }
    }
}