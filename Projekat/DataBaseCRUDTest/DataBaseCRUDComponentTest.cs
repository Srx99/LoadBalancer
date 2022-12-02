using Common;
using DataBaseCRUD;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Common.Enumeracija;

namespace DataBaseCRUDTest
{
    [TestFixture]
    public class DataBaseCRUDComponentTest
    {

        Mock<DataBaseCRUDComponent> mockDataBase;

        [SetUp]
        public void SetUp()
        {
            mockDataBase = new Mock<DataBaseCRUDComponent>();
        }

        [Test]
        [TestCase(2, 1)]
        [ExpectedException(typeof(TargetInvocationException))]
        public void UpisUBazuPodatakaDobriParametri(int id, int dataSet)
        {

            Item i1 = new Item();
            Item i2 = new Item();
            Item i3 = new Item();
            i1.Code = Enumeracija.CodeEnum.CODE_CONSUMER;
            i2.Code = Enumeracija.CodeEnum.CODE_CONSUMER;
            i3.Code = Enumeracija.CodeEnum.CODE_CONSUMER;



            List<Item> itee = new List<Item>() { i1, i2, i3 };

            mockDataBase.Object.UpisUBazuPodataka(itee, id, dataSet);
        }



        


    }
}
