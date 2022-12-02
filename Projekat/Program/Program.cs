using Common;
using LoadBalancer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Writer;
using static Common.Enumeracija;

namespace Program
{
    class Program
    {
        public static WriterComponent writer = new WriterComponent();

        static void Main(string[] args)
        {

            Thread t1 = new Thread(new ThreadStart(Konzola));
            t1.Start();


            while (true)
            {
                writer.SlanjeAutomatsko(1000, 10, 7);
            }

            Console.ReadLine();
        }

        static Mutex mutex = new Mutex();



        static void Konzola()
        {
            Item item = new Item();

            

            while (true)
            {

                Console.WriteLine("================================================");
                Console.WriteLine("Popunjavanje manuelne poruke:");
                Console.WriteLine("-----------------------------");

                Console.WriteLine("Unesite kod od 1 do 8!");

                if (!Int32.TryParse(Console.ReadLine(), out int i) || i < 1 || i > 8)
                {
                    continue;
                }

                switch (i-1)
                {
                    case 1:
                        item.Code = CodeEnum.CODE_ANALOG;
                        break;
                    case 2:
                        item.Code = CodeEnum.CODE_DIGITAL;
                        break;
                    case 3:
                        item.Code = CodeEnum.CODE_CUSTOM;
                        break;
                    case 4:
                        item.Code = CodeEnum.CODE_LIMITSET;
                        break;
                    case 5:
                        item.Code = CodeEnum.CODE_SINGLENODE;
                        break;
                    case 6:
                        item.Code = CodeEnum.CODE_MULTIPLENODE;
                        break;
                    case 7:
                        item.Code = CodeEnum.CODE_CONSUMER;
                        break;
                    case 8:
                        item.Code = CodeEnum.CODE_SOURCE;
                        break;
                }

                Console.WriteLine("Unesite vrijednost (realan broj) :");

                if (!double.TryParse(Console.ReadLine(), out double v))
                {
                    continue;
                }

                item.Value = v;




                Console.WriteLine("Unesite redni broj Workera kome mjenjate stanje (unesite -1 da ne mjenjate ni jedno stanje):");



                if (!Int32.TryParse(Console.ReadLine(), out int w) || w < -1 || w == 0)
                {
                    continue;
                }

                item.Kontrola = w;

                Console.WriteLine("================================================");



                mutex.WaitOne();
                    writer.SlanjeManuelno();
                    Console.WriteLine("Writer je poslao poruku!\n\n\n\n");
                mutex.ReleaseMutex();
            }
            


        }


    }
}
