using DataBaseCRUD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Common.Enumeracija;

namespace HistoryCode
{
    class Program
    {
        static void Main(string[] args)
        {
            DataBaseCRUDComponent dataBase = new DataBaseCRUDComponent();
            bool izlaz = false;

            while (true)
            {
                int opcija = Ispis();

                switch (opcija)
                {
                    case 0:
                        izlaz = true;
                        break;
                    case 1:
                        Console.WriteLine(dataBase.IstorijaCoda(CodeEnum.CODE_ANALOG));
                        break;
                    case 2:
                        Console.WriteLine(dataBase.IstorijaCoda(CodeEnum.CODE_DIGITAL));
                        break;
                    case 3:
                        Console.WriteLine(dataBase.IstorijaCoda(CodeEnum.CODE_CUSTOM));
                        break;
                    case 4:
                        Console.WriteLine(dataBase.IstorijaCoda(CodeEnum.CODE_LIMITSET));
                        break;
                    case 5:
                        Console.WriteLine(dataBase.IstorijaCoda(CodeEnum.CODE_SINGLENODE));
                        break;
                    case 6:
                        Console.WriteLine(dataBase.IstorijaCoda(CodeEnum.CODE_MULTIPLENODE));
                        break;
                    case 7:
                        Console.WriteLine(dataBase.IstorijaCoda(CodeEnum.CODE_CONSUMER));
                        break;
                    case 8:
                        Console.WriteLine(dataBase.IstorijaCoda(CodeEnum.CODE_SOURCE));
                        break;
                    default:
                        break;
                }
                if (izlaz == true)
                    break;

            }

            Console.ReadLine();
        }

        static int Ispis()
        {
            Console.WriteLine("-----------Istorija koda-----------");
            Console.WriteLine("\tOdabarite jedan kod(unseite broj): ");
            Console.WriteLine("\t1. CODE_ANALOG");
            Console.WriteLine("\t2. CODE_DIGITAL");
            Console.WriteLine("\t3. CODE_CUSTOM");
            Console.WriteLine("\t4. CODE_LIMITSET");
            Console.WriteLine("\t5. CODE_SINGLENODE");
            Console.WriteLine("\t6. CODE_MULTINODE");
            Console.WriteLine("\t7. CODE_CONSUMER");
            Console.WriteLine("\t8. CODE_SOURCE");
            Console.WriteLine("\tZa izlaz 0");
            int opcija;
            if (!Int32.TryParse(Console.ReadLine(), out opcija))
            {
                Console.WriteLine("Niste poslali odgovarajuci broj");
            }

            return opcija;
        }
    }
}
