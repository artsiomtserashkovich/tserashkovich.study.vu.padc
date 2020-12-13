using System;
using System.Collections.Generic;
using System.Threading;
using Tereshkovich.Study.PaDC.ThirdAssigment.Shared;
using Tereshkovich.Study.PaDC.ThirdAssigment.SingleThreaded;

namespace Tereshkovich.Study.PaDC.ThirdAssigment.BothBranchesMultiThreaded
{
    public class BothBranchMultiThreadedMergeSorter : BaseMergeSorter
    {
        private readonly int _threadCount;

        public BothBranchMultiThreadedMergeSorter(int threadCount)
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
            
            if(_threadCount == 1)
            {
                (left, right) = HandleSingleThread(left, right);
            }
            else
            {
                var (leftThreadsCount, rightThreadsCount) = GetThreadsCountForBranches(_threadCount);
                
                var thread = new Thread(() =>
                {
                    var leftMultiThreadedSorter = new BothBranchMultiThreadedMergeSorter(leftThreadsCount);
                    
                    left = leftMultiThreadedSorter.Sort(left);
                });
                thread.Start();

                var rightThreadedSorter = GetRightSorter(rightThreadsCount);
                right = rightThreadedSorter.Sort(right);
                
                thread.Join();
            }
            
            return MergeParts(left, right);
        }

        private static IMergeSorter GetRightSorter(int rightThreadsCount)
        {
            if (rightThreadsCount == 0)
            {
                return new SingleThreadedMergeSort();
            }

            return new BothBranchMultiThreadedMergeSorter(rightThreadsCount + 1);
        }

        private static (ICollection<int>, ICollection<int>) HandleSingleThread(ICollection<int> left, ICollection<int> right)
        {
            var singleThreadSorter = new SingleThreadedMergeSort();


            left = singleThreadSorter.Sort(left);
            right = singleThreadSorter.Sort(right);
            
            return (left, right);
        }

        private (int Left, int Right) GetThreadsCountForBranches(int threadCount)
        {
            var countThreadLeft = threadCount - 1; // sub current

            var half = (int)Math.Round((double)countThreadLeft / 2, MidpointRounding.AwayFromZero);
            
            return (half, countThreadLeft - half);
        }
    }
}