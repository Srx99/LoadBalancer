using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    public class Servis : ISlanje
    {

        public static string primljena = "";

        public void SlanjePoruke(string poruka)
        {
            
            primljena = poruka;
        }
    }
}
