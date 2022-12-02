using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Worker;
using Assert = NUnit.Framework.Assert;

namespace WorkerComponentTest
{
    [TestFixture]
    public class WorkerComponentTest
    {
        
      




        [Test]
        public void WorkerPrazanKonstruktor()
        {
            WorkerComponent worker = new WorkerComponent();
            Description description = new Description();
            DataSetStruct dataSetStruct = new DataSetStruct();
            Assert.AreEqual(description.DataSet, worker.description.DataSet);
            Assert.AreEqual(description.ID, worker.description.ID);
            Assert.AreEqual(description.listItem, worker.description.listItem);
            Assert.AreEqual(dataSetStruct.Kod1, worker.datasetWO.Kod1);
            Assert.AreEqual(dataSetStruct.Kod2, worker.datasetWO.Kod2);
           

        }

        [Test]
        

        public void WorkerKonstruktorSaParametrima()
        {
            Description description = new Description();
            description.DataSet = 3;
            description.ID = 8;
            DataSetStruct dataSetStruct = new DataSetStruct();

            WorkerComponent worker = new WorkerComponent(description);

            Assert.AreEqual(description.DataSet, worker.description.DataSet);
            Assert.AreEqual(description.ID, worker.description.ID);
            Assert.AreEqual(description.listItem, worker.description.listItem);
            Assert.AreEqual(dataSetStruct.Kod1, worker.datasetWO.Kod1);
            Assert.AreEqual(dataSetStruct.Kod2, worker.datasetWO.Kod2);


        }





        [Test]
        
        public void DeadbandTest()
        {
            Item i1 = new Item();
            Item i2 = new Item();
            Item i3 = new Item();
            i1.Code = Enumeracija.CodeEnum.CODE_CONSUMER;
            i2.Code = Enumeracija.CodeEnum.CODE_CONSUMER;
            i3.Code = Enumeracija.CodeEnum.CODE_CONSUMER;



            List<Item> itee = new List<Item>() { i1, i2, i3 };
            WorkerComponent worker = new WorkerComponent();
            worker.description.DataSet = 1;
            worker.description.ID = 3;

            Assert.DoesNotThrow(() => worker.Deadband(itee));


        }
        [Test]
        public void UpisUBazu()
        {
            Item i1 = new Item();
            Item i2 = new Item();
            Item i3 = new Item();
            i1.Code = Enumeracija.CodeEnum.CODE_CONSUMER;
            i2.Code = Enumeracija.CodeEnum.CODE_CONSUMER;
            i3.Code = Enumeracija.CodeEnum.CODE_CONSUMER;



            List<Item> itee = new List<Item>() { i1, i2, i3 };

            Assert.DoesNotThrow(() => new WorkerComponent().UpisUBazu(itee, 8, 1));

            

        }




        [Test]
        public void ObradaTest()
        {
            Description des = new Description();
            des.DataSet = 1;
            des.ID = 5;

            Item i1 = new Item();
            Item i2 = new Item();
            Item i3 = new Item();
            i1.Code = Enumeracija.CodeEnum.CODE_CONSUMER;
            i2.Code = Enumeracija.CodeEnum.CODE_CONSUMER;
            i3.Code = Enumeracija.CodeEnum.CODE_CONSUMER;



            Dictionary<int, Item> itee = new Dictionary<int, Item>();

            itee.Add(1, i1);
            itee.Add(2, i2);
            itee.Add(3, i3);

            des.listItem = itee;

            Assert.DoesNotThrow(() => new WorkerComponent().Obrada(des));

            

        }


    }
}
