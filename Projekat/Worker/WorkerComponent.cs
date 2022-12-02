using Common;
using DataBaseCRUD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Common.Enumeracija;

namespace Worker
{
    public class WorkerComponent
    {

        public Description description { get; set; }

        public DataSetStruct datasetWO { get; set; }

        Mutex m = new Mutex();


        public WorkerComponent()
        {
            description = new Description();
            datasetWO = new DataSetStruct();
        }


        public WorkerComponent(Description des)
        {
            description = des;
            datasetWO = new DataSetStruct();
        }



        public void Deadband(List<Item> itee)
        {

            List<Item> procitaniItemi = new List<Item>();

            foreach(var it in itee)
            {
                if (it.Code == CodeEnum.CODE_DIGITAL)
                {
                    throw new Exception("Ovaj kod ne treba da dodje u Deadband");
                }
                
            }

            if (description.DataSet == 1)
            {
               m.WaitOne();
               procitaniItemi = CitanjeIzBaze(1,description.ID);
               m.ReleaseMutex();
            }
            else if (description.DataSet == 2)
            {
                m.WaitOne();
                procitaniItemi = CitanjeIzBaze(2, description.ID);
                m.ReleaseMutex();
            }
            else if (description.DataSet == 3)
            {
                m.WaitOne();
                procitaniItemi = CitanjeIzBaze(3, description.ID);
                m.ReleaseMutex();
            }
            else if (description.DataSet == 4)
            {
                m.WaitOne();
                procitaniItemi = CitanjeIzBaze(4, description.ID);
                m.ReleaseMutex();
            }
            else
            {
                throw new Exception("Nepostojeci DataSet");
            }



            if (procitaniItemi == null)
            {
                m.WaitOne();
                UpisUBazu(itee, description.ID, description.DataSet);
                m.ReleaseMutex();
            }
            else
            {
                double ukupnavrijednoststara = 0;
                double ukupnavrijednosnova = 0;

                foreach (var itemm in procitaniItemi)
                {
                    CodeEnum pomoc = new CodeEnum();
                    foreach (var it in itee)
                    {
                        pomoc = it.Code; 
                    }
                    if (pomoc == itemm.Code) {
                        ukupnavrijednoststara = ukupnavrijednoststara + itemm.Value;
                    }
                }

                foreach(var itemm in itee)
                {

                    ukupnavrijednosnova = ukupnavrijednosnova + itemm.Value;

                }

                if (ukupnavrijednosnova <= (ukupnavrijednoststara + (ukupnavrijednoststara/100 * 2)) && ukupnavrijednosnova >= (ukupnavrijednoststara - (ukupnavrijednoststara / 100 * 2)))
                {


                }
                else
                {
                    m.WaitOne();
                    UpisUBazu(itee, description.ID, description.DataSet);
                    m.ReleaseMutex();
                }


            }




        }


        public void Obrada(Description des)
        {
            description = des;

            //Klijent klijent = new Klijent();
            //klijent.PosaljiPoruku($"Worker sa ID-jem {des.ID} je primio poruku!\n");


            List<Item> Kod6 = new List<Item>();
            List<Item> Kod7 = new List<Item>();
            List<Item> Kod8 = new List<Item>();
            List<Item> Kod1 = new List<Item>();
            List<Item> Kod2 = new List<Item>();
            List<Item> Kod3 = new List<Item>();
            List<Item> Kod4 = new List<Item>();
            List<Item> Kod5 = new List<Item>();



            if (description.DataSet == 1)
            {
                foreach (var ite in description.listItem)
                {
                    if (ite.Value.Code == Enumeracija.CodeEnum.CODE_ANALOG)
                    {
                        Kod1.Add(ite.Value);
                    }
                    else
                    {
                        Kod2.Add(ite.Value);
                    }

                }

                if (Kod1.Count() != 0 && Kod2.Count() != 0)
                {

                    datasetWO.Kod1 = Kod1;
                    datasetWO.Kod2 = Kod2;

                    m.WaitOne();
                    UpisUBazu(Kod1, description.ID, description.DataSet);
                    m.ReleaseMutex();



                    Deadband(Kod2);

                    datasetWO.Kod1 = null;
                    datasetWO.Kod2 = null;

                }

            }
            else if (description.DataSet == 2)
            {
                foreach (var ite in description.listItem)
                {
                    if (ite.Value.Code == Enumeracija.CodeEnum.CODE_CUSTOM)
                    {
                        Kod3.Add(ite.Value);
                    }
                    else
                    {
                        Kod4.Add(ite.Value);
                    }
                }


                if (Kod3.Count() != 0 && Kod4.Count() != 0)
                {
                    datasetWO.Kod1 = Kod3;
                    datasetWO.Kod2 = Kod4;

                    Deadband(Kod3);
                    Deadband(Kod4);

                    datasetWO.Kod1 = null;
                    datasetWO.Kod2 = null;

                }

            }
            else if (description.DataSet == 3)
            {
                foreach (var ite in description.listItem)
                {
                    if (ite.Value.Code == Enumeracija.CodeEnum.CODE_SINGLENODE)
                    {
                        Kod5.Add(ite.Value);
                    }
                    else
                    {
                        Kod6.Add(ite.Value);
                    }

                }

                if (Kod5.Count() != 0 && Kod6.Count() != 0)
                {
                    datasetWO.Kod1 = Kod5;
                    datasetWO.Kod2 = Kod6;

                    Deadband(Kod5);
                    Deadband(Kod6);

                    datasetWO.Kod1 = null;
                    datasetWO.Kod2 = null;

                }


            }
            else if (description.DataSet == 4)
            {
                foreach (var ite in description.listItem)
                {
                    if (ite.Value.Code == Enumeracija.CodeEnum.CODE_CONSUMER)
                    {
                        Kod7.Add(ite.Value);
                    }
                    else
                    {
                        Kod8.Add(ite.Value);
                    }

                }

                if (Kod7.Count() != 0 && Kod8.Count() != 0)
                {
                    datasetWO.Kod1 = Kod7;
                    datasetWO.Kod2 = Kod8;

                    Deadband(Kod7);
                    Deadband(Kod8);


                    datasetWO.Kod1 = null;
                    datasetWO.Kod2 = null;

                }


            }
            else
            {
                throw new Exception("Nemoguc DataSet!");
            }

        }

        public void UpisUBazu(List<Item> ite, int ID, int datasett)
        {

            //Klijent klijent = new Klijent();
            //klijent.PosaljiPoruku($"Worker inicira upis u bazu!\n\n");


            IDataBase dataBase = new DataBaseCRUDComponent();
            //dataBase.UpisUBazuPodataka(ite, ID, datasett);    OVO TREBA OTKOMENTARISATI KAO I KLIJENTE SVUGDJE
        }

        //dataset
        public List<Item> CitanjeIzBaze(int datasett, int ID)
        {

            //Klijent klijent = new Klijent();
            //klijent.PosaljiPoruku($"Worker inicira citanje iz baze!\n\n");

            List<Item> temp = new List<Item>();
            IDataBase dataBase = new DataBaseCRUDComponent();
            //temp = dataBase.CitanjeIzBaze(datasett, ID);  OVO TREBA OTKOMENTARISATI KAO I KLIJENTE SVUGDJE

            return temp;
        }

    }
}
