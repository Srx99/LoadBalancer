using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Common.Enumeracija;

namespace Common
{
    public class Item
    {
        public CodeEnum Code { get; set; }

        public double Value { get; set; }

        public int Kontrola { get; set; }

        public Item(CodeEnum code = CodeEnum.CODE_ANALOG, double value = 0, int kontrola = -1)
        {
            if ((int)code >= 0 && (int)code <= 7)
            {
                Code = code;
            }
            else
            {
                throw new ArgumentOutOfRangeException("Kod mora biti veci od 0 i manji od 9!");
            }

            if (kontrola > 0 || kontrola == -1)
            {
                Kontrola = kontrola;
            }
            else
            {
                throw new ArgumentOutOfRangeException("Kontrola ne smije biti nula ili manja od -1!");
            }



            Value = value;
            
        }


    }
}
