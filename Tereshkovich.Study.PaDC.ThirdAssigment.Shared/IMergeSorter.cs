using System.Collections.Generic;

namespace Tereshkovich.Study.PaDC.ThirdAssigment.Shared
{
    public interface IMergeSorter
    {
        ICollection<int> Sort(ICollection<int> collection);
    }
}