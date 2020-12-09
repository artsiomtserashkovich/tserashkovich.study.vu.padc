using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Tereshkovich.Study.PaDC.ThirdAssigment.Shared
{
    public class FileCollectionFactory
    {
        public ICollection<int> GetRandomCollection()
        {
            var serializedArray = File.ReadAllText("./array.json");
            
            var randomCollection = JsonConvert.DeserializeObject<ICollection<int>>(serializedArray);
            
            return randomCollection;
        }
    }
}