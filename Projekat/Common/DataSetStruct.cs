using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class DataSetStruct
    {
        public List<Item> Kod1 { get; set; }
        public List<Item> Kod2 { get; set; }


        public DataSetStruct()
        {
            Kod1 = new List<Item>();
            Kod2 = new List<Item>();
        }

    }
}
