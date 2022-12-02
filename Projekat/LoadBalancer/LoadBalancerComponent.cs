using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Worker;

namespace LoadBalancer
{
    public class LoadBalancerComponent : ILoadBalancer
    {

        public int brojac { get; set; }

        public List<Description> LDList { get; set; }

        public Dictionary<int, WorkerComponent> workeri { get; set; }

       

        public BufferLB bufferLB { get; set; }



        public LoadBalancerComponent()
        {
            workeri = new Dictionary<int, WorkerComponent>();
           
            bufferLB = new BufferLB();

            LDList = new List<Description>();
            brojac = 0;
        }



        
        public bool SmjestanjeUBafer(Item item)
        {

            bufferLB.buffer.Add(brojac++, item);
            //Klijent klijent = new Klijent();
            //klijent.PosaljiPoruku($"Podatak {item.Code}, {item.Value}, {item.Kontrola} je smjesten u  Buffer pod brojem {brojac++}!\n\n");
            bool b = PaliIGasiWorkere(item);
            return b;
            

        }

        public bool PaliIGasiWorkere(Item item)
        {



            if (item.Kontrola != -1 && !workeri.ContainsKey(item.Kontrola))
            {
                WorkerComponent worker = new WorkerComponent();
                workeri.Add(item.Kontrola, worker);
                //Klijent klijent = new Klijent();
                //klijent.PosaljiPoruku($"Worker sa ID-jem {item.Kontrola} je upaljen!\n\n");
            }
            else if (item.Kontrola != -1 && workeri.ContainsKey(item.Kontrola))
            {
                workeri.Remove(item.Kontrola);
                //Klijent klijent = new Klijent();
                //klijent.PosaljiPoruku($"Worker sa ID-jem {item.Kontrola} je ugasen!\n\n");
            }
            else if (item.Kontrola == -1)
            {

            }
            else
            {
                throw new Exception("Nevalidna komanda Workerima!");
            }
            if (item.Kontrola <= 0 && item.Kontrola != -1)
            {
                throw new Exception("Nepostojeci worker!");
            }

            

            bool b = RaspodelaPosla();
            return b;
        }


        public bool RaspodelaPosla()
        {

            Dictionary<int, Item> ts1 = new Dictionary<int, Item>();
            Dictionary<int, Item> ts2 = new Dictionary<int, Item>();
            Dictionary<int, Item> ts3 = new Dictionary<int, Item>();
            Dictionary<int, Item> ts4 = new Dictionary<int, Item>();

            foreach (var buf in bufferLB.buffer)
            {
                if (buf.Value.Code == Enumeracija.CodeEnum.CODE_ANALOG || buf.Value.Code == Enumeracija.CodeEnum.CODE_DIGITAL)
                {
                    ts1.Add(buf.Key, buf.Value);
                }
                else if (buf.Value.Code == Enumeracija.CodeEnum.CODE_CUSTOM|| buf.Value.Code == Enumeracija.CodeEnum.CODE_LIMITSET)
                {
                    ts2.Add(buf.Key, buf.Value);
                }
                else if (buf.Value.Code == Enumeracija.CodeEnum.CODE_SINGLENODE || buf.Value.Code == Enumeracija.CodeEnum.CODE_MULTIPLENODE)
                {
                    ts3.Add(buf.Key, buf.Value);
                }
                else if (buf.Value.Code == Enumeracija.CodeEnum.CODE_CONSUMER || buf.Value.Code == Enumeracija.CodeEnum.CODE_SOURCE)
                {
                    ts4.Add(buf.Key, buf.Value);
                }
                else
                {
                    throw new Exception("Nepostojeci kod!");
                }
            }
            //Klijent klijent = new Klijent();
            //klijent.PosaljiPoruku($"Podaci su procitani iz Buffera!\n");

            double kolikoitema;
            if (workeri.Count() != 0)
            {

                kolikoitema = bufferLB.buffer.Count() / workeri.Count();
            }
            else
            {
                kolikoitema = 0;
            }

            int kontrolerazads1;
            int kontrolerazads2;
            int kontrolerazads3;
            int kontrolerazads4;

            if (kolikoitema != 0)
            {
                kontrolerazads1 = (int)(ts1.Count() / kolikoitema);
                kontrolerazads2 = (int)(ts2.Count() / kolikoitema);
                kontrolerazads3 = (int)(ts3.Count() / kolikoitema);
                kontrolerazads4 = (int)(ts4.Count() / kolikoitema);
            }
            else
            {
                kontrolerazads1 = 0;
                kontrolerazads2 = 0;
                kontrolerazads3 = 0;
                kontrolerazads4 = 0;
            }
            if (kontrolerazads1 + kontrolerazads2 + kontrolerazads3 + kontrolerazads4 != workeri.Count() && ts1.Count() != 0)
            {
                kontrolerazads1++;
            }
            if (kontrolerazads1 + kontrolerazads2 + kontrolerazads3 + kontrolerazads4 != workeri.Count() && ts2.Count() != 0)
            {
                kontrolerazads2++;
            }
            if (kontrolerazads1 + kontrolerazads2 + kontrolerazads3 + kontrolerazads4 != workeri.Count() && ts3.Count() != 0)
            {
                kontrolerazads3++;
            }
            if (kontrolerazads1 + kontrolerazads2 + kontrolerazads3 + kontrolerazads4 != workeri.Count() && ts4.Count() != 0)
            {
                kontrolerazads4++;
            }

            int kolikoitema1;
            int kolikoitema2;
            int kolikoitema3;
            int kolikoitema4;

            if (kontrolerazads1 != 0) { kolikoitema1 = ts1.Count() / kontrolerazads1; } else { kolikoitema1 = 0; }
            if (kontrolerazads2 != 0) { kolikoitema2 = ts2.Count() / kontrolerazads2; } else { kolikoitema2 = 0; }
            if (kontrolerazads3 != 0) { kolikoitema3 = ts3.Count() / kontrolerazads3; } else { kolikoitema3 = 0; }
            if (kontrolerazads4 != 0) { kolikoitema4 = ts4.Count() / kontrolerazads4; } else { kolikoitema4 = 0; }


            int brojacworkera = 0;
            foreach (var work in workeri)
            {
                work.Value.description.DataSet = 2;
                work.Value.description.listItem = new Dictionary<int, Item>();
                brojacworkera++;
                if (brojacworkera <= kontrolerazads1)
                {

                    
                    int brojacitema = 0;
                    foreach (var itee in ts1)
                    {
                        work.Value.description.ID = work.Key;
                        work.Value.description.DataSet = 1;
                        brojacitema++;
                        if (brojacitema <= kolikoitema1 || (ts1.Count() < kolikoitema1 && ts1.Count() >= 0))
                        {
                            work.Value.description.listItem.Add(itee.Key, itee.Value);

                        }
                    }
                    for (int a = 0; a < brojacitema; a++)
                    {
                        ts1.Remove(a);
                    }
                }

                if (brojacworkera <= kontrolerazads2 + kontrolerazads1)
                {


                    //work.Value.description.DataSet = 2;
                    int brojacitema = 0;
                    foreach (var itee in ts2)
                    {
                        work.Value.description.ID = work.Key;
                        work.Value.description.DataSet = 2;
                        brojacitema++;
                        if (brojacitema <= kolikoitema2 || (ts2.Count() < kolikoitema2 && ts2.Count() >= 0))
                        {
                            work.Value.description.listItem.Add(itee.Key, itee.Value);
                            
                        }
                    }
                    for (int a = 0; a < brojacitema; a++)
                    {
                        ts2.Remove(a);
                    }

                }

                if (brojacworkera <= kontrolerazads2 + kontrolerazads1 + kontrolerazads3)
                {

                    //work.Value.description.DataSet = 3;
                    int brojacitema = 0;
                    foreach (var itee in ts3)
                    {
                        work.Value.description.ID = work.Key;
                        work.Value.description.DataSet = 3;
                        brojacitema++;
                        if (brojacitema <= kolikoitema3 || (ts3.Count() < kolikoitema3 && ts3.Count() >= 0))
                        {
                            work.Value.description.listItem.Add(itee.Key, itee.Value);

                        }
                    }
                    for (int a = 0; a < brojacitema; a++)
                    {
                        ts3.Remove(a);
                    }

                }


                if (brojacworkera <= kontrolerazads2 + kontrolerazads1 + kontrolerazads3 + kontrolerazads4)
                {

                    //work.Value.description.DataSet = 4;
                    int brojacitema = 0;
                    foreach (var itee in ts4)
                    {
                        work.Value.description.ID = work.Key;
                        work.Value.description.DataSet = 4;
                        brojacitema++;
                        if (brojacitema <= kolikoitema4 || (ts4.Count() < kolikoitema4 && ts4.Count() >= 0))
                        {
                            work.Value.description.listItem.Add(itee.Key, itee.Value);

                        }
                    }
                    for (int a = 0; a < brojacitema; a++)
                    {
                        ts4.Remove(a);
                    }

                }




            }







            //int kolikoitema;
            //if (workeri.Count() != 0)
            //{

            //    kolikoitema = bufferLB.buffer.Count() / workeri.Count();
            //}
            //else
            //{
            //    kolikoitema = 0;
            //}
            //int br = 0, ds = 1;
            //foreach (var work in workeri)
            //{

            //    worker = work.Value;
            //    worker.description.ID = work.Key;
            //    for (int i = 0; i < kolikoitema; i++)
            //    {
            //        int y = i + br * kolikoitema;

            //        worker.description.listItem.Add(bufferLB.buffer[i + br * kolikoitema]);
            //        if (ds == 4) { ds = 0; }                            //OVDE DATASETOVE NISAM URADIO KAKO TREBA STAVIO SAM IH ONAKO DA IH POSTAVLJA JER NE ZNAM STA JE TAJ RAUND ROBIN i NZM KAKO DA DELIM POSAO DRUGACIJE
            //        worker.description.DataSet = ds;
            //    }

            //    br++;
            //    ds++;
            //}

            //if (kolikoitema * workeri.Count() != bufferLB.buffer.Count())
            //{
            //    for (int i = kolikoitema * workeri.Count(); i < bufferLB.buffer.Count(); i++)
            //    {
            //        worker.description.listItem.Add(bufferLB.buffer[i]);

            //        worker.description.DataSet = ds - 1;

            //    }


            //}
            
            //-------------------

            //klijent.PosaljiPoruku($"Podaci su rasporedjeni!\n\n");
            bool b = PosaljiWorkerima();
            return b;
        }



        public bool PosaljiWorkerima()
        {

            Thread[] niti = new Thread[workeri.Values.Count];
            int i = 0;
            foreach (var po in workeri)
            {
                

                //Klijent klijent = new Klijent();
                //klijent.PosaljiPoruku($"Podaci su poslati Woru sa ID-jem {po.Key}!\n");
                niti[i] = new Thread(() => po.Value.Obrada(po.Value.description));
                niti[i++].Start();

                
            }
            Thread.Sleep(200);
            i = 0;
            return true;


        }


    }
}
