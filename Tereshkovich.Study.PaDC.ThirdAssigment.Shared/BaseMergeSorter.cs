using System;
using System.Collections.Generic;
using System.Linq;

namespace Tereshkovich.Study.PaDC.ThirdAssigment.Shared
{
    public abstract class BaseMergeSorter : IMergeSorter
    {
        public abstract int[] Sort(int[] collection);
        
        protected static (int[] Left, int[] Right) GetHalfParts(int[] collection)
        {
            var middle = collection.Length / 2;
            
            var left = collection.Take(middle).ToArray();
            var right = collection.Skip(middle).ToArray();
            
            return (left, right);
        }
        
        protected static int[] MergeParts(int[] left, int[] right)
        {
            int resultLength = right.Length + left.Length;
            int[] result = new int[resultLength];
            
            int indexLeft = 0, indexRight = 0, indexResult = 0;
            while (indexLeft < left.Length || indexRight < right.Length)
            {
                //if both arrays have elements  
                if (indexLeft < left.Length && indexRight < right.Length)  
                {  
                    //If item on left array is less than item on right array, add that item to the result array 
                    if (left[indexLeft] <= right[indexRight])
                    {
                        result[indexResult] = left[indexLeft];
                        indexLeft++;
                        indexResult++;
                    }
                    // else the item in the right array wll be added to the results array
                    else
                    {
                        result[indexResult] = right[indexRight];
                        indexRight++;
                        indexResult++;
                    }
                }
                //if only the left array still has elements, add all its items to the results array
                else if (indexLeft < left.Length)
                {
                    result[indexResult] = left[indexLeft];
                    indexLeft++;
                    indexResult++;
                }
                //if only the right array still has elements, add all its items to the results array
                else if (indexRight < right.Length)
                {
                    result[indexResult] = right[indexRight];
                    indexRight++;
                    indexResult++;
                }  
            }
            
            return result;
        }
    }
}