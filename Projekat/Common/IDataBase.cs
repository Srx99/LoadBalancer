using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Common.Enumeracija;

namespace Common
{
    public interface IDataBase
    {
        void UpisUBazuPodataka(List<Item> item, int id, int dataSet);



        List<Item> CitanjeIzBaze(int dataSet, int id);



        string IstorijaCoda(CodeEnum code);
    }
}
