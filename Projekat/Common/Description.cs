using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Description
    {
        public int ID { get; set; }
        public int DataSet { get; set; }
        public Dictionary<int, Item> listItem { get; set; }

        //public Description()
        //{
        //    ID = 0;
        //    DataSet = 0;
        //    listItem = new Dictionary<int, Item>();
        //}

        public Description(int iD = 0, int dataSet = 0)
        {
            if (iD < 0)
            {
                throw new ArgumentOutOfRangeException("ID ne moze da bude negativan broj");
            }
            if (dataSet < 0 || dataSet > 4)
            {
                throw new ArgumentOutOfRangeException("Dataset mora biti u intervalu od 1 do 4");
            }
            ID = iD;
            DataSet = dataSet;
            listItem = new Dictionary<int, Item>();
        }


    }
}
