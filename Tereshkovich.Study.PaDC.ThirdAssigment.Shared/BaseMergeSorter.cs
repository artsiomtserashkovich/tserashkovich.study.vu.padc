using System.Collections.Generic;
using System.Linq;

namespace Tereshkovich.Study.PaDC.ThirdAssigment.Shared
{
    public abstract class BaseMergeSorter : IMergeSorter
    {
        public abstract ICollection<int> Sort(ICollection<int> collection);
        
        protected static (ICollection<int> Left, ICollection<int> Right) GetHalfParts(ICollection<int> collection)
        {
            var middle = collection.Count / 2;
            
            ICollection<int> left = collection.Take(middle).ToList();
            ICollection<int> right = collection.Skip(middle).ToList();
            
            return (left, right);
        }
        
        protected static ICollection<int> MergeParts(ICollection<int> left, ICollection<int> right)
        {
            var result = new List<int>();

            while(left.Count > 0 || right.Count>0)
            {
                if (left.Count > 0 && right.Count > 0)
                {
                    if (left.First() <= right.First())
                    {
                        result.Add(left.First());
                        left.Remove(left.First());
                    }
                    else
                    {
                        result.Add(right.First());
                        right.Remove(right.First());
                    }
                }
                else if(left.Count > 0)
                {
                    result.Add(left.First());
                    left.Remove(left.First());
                }
                else if (right.Count > 0)
                {
                    result.Add(right.First());

                    right.Remove(right.First());    
                }    
            }
            return result;
        }
    }
}