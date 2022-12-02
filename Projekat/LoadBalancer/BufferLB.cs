using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadBalancer
{
    public class BufferLB
    {
        public Dictionary<int, Item> buffer { get; set; }


        public BufferLB()
        {
            buffer = new Dictionary<int, Item>();
        }



    }
}
