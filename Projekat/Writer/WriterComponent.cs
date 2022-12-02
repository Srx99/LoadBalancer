using Common;
using LoadBalancer;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Common.Enumeracija;


namespace Writer
{
    public class WriterComponent : IWriter
    {

        public Item item { get; set; }

        public LoadBalancerComponent loadBalancer { get; set; }

        Mutex m = new Mutex();

        public WriterComponent()
        {
            loadBalancer = new LoadBalancerComponent();
            item = new Item();
        }




        [ExcludeFromCodeCoverage]
        public bool SlanjeAutomatsko(int i, int i2, int i3)
        {
            Random ran = new Random();


            item = new Item();
            
            if (i2 < -1 || i2 == 0 || i3 < 0 || i3 > 7)
            {
                throw new Exception("Nevalidna kontrola workerima!");
            }
                
                item.Value = ran.Next(i);
                item.Kontrola = ran.Next(1, i2);

                switch (ran.Next(0,i3))
                {
                    case 0 :
                        item.Code = CodeEnum.CODE_ANALOG;
                        break;
                    case 1:
                        item.Code = CodeEnum.CODE_DIGITAL;
                        break;
                    case 2:
                        item.Code = CodeEnum.CODE_CUSTOM;
                        break;
                    case 3:
                        item.Code = CodeEnum.CODE_LIMITSET;
                        break;
                    case 4:
                        item.Code = CodeEnum.CODE_SINGLENODE;
                        break;
                    case 5:
                        item.Code = CodeEnum.CODE_MULTIPLENODE;
                        break;
                    case 6:
                        item.Code = CodeEnum.CODE_CONSUMER;
                        break;
                    case 7:
                        item.Code = CodeEnum.CODE_SOURCE;
                        break;  
                       
                }



            //Klijent klijent = new Klijent();
            //klijent.PosaljiPoruku($"======================================\nAutomatska poruka poslata:\n\tKod : {item.Code},\n\tVrijednost : {item.Value},\n\tKontrol : {item.Kontrola}!\n\n");




                m.WaitOne();
                bool b = loadBalancer.SmjestanjeUBafer(item);
                m.ReleaseMutex();
                
                Thread.Sleep(6000);
                return b;
            


        }



        
        public bool SlanjeManuelno()
        {


            item = new Item();
            //Klijent klijent = new Klijent();
            //klijent.PosaljiPoruku($"======================================\nManuelna poruka poslata:\n\tKod : {item.Code},\n\tVrijednost : {item.Value},\n\tKontrola : {item.Kontrola}!\n\n");
            m.WaitOne();
            bool b = loadBalancer.SmjestanjeUBafer(item);
            m.ReleaseMutex();

            if (b == false)
            {
                throw new Exception("Nije poslato load balanceru ");
            }
            return b;
        }


    }
}
