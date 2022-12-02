using Common;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Common.Enumeracija;

namespace CommonTest
{
    [TestFixture]
    public class ItemTest
    {


        [Test]
        [TestCase(CodeEnum.CODE_ANALOG)]
        [TestCase(CodeEnum.CODE_CONSUMER)]
        [TestCase(CodeEnum.CODE_CUSTOM)]
        [TestCase(CodeEnum.CODE_DIGITAL)]
        [TestCase(CodeEnum.CODE_LIMITSET)]
        [TestCase(CodeEnum.CODE_MULTIPLENODE)]
        [TestCase(CodeEnum.CODE_SINGLENODE)]
        [TestCase(CodeEnum.CODE_SOURCE)]
        public void CodeTest(CodeEnum kod)
        {
            Assert.DoesNotThrow(() => new Item().Code = kod);
            Assert.AreEqual(new Item().Code, new Item().Code);

        }

        [Test]
        public void KontrolaTest()
        {
            Assert.DoesNotThrow(() => new Item().Kontrola = 5);
            Assert.AreEqual(new Item().Kontrola, new Item().Kontrola);

        }

        [Test]
        public void ValueTest()
        {
            Assert.DoesNotThrow(() => new Item().Value = 88);
            Assert.AreEqual(new Item().Value, new Item().Value);

        }





        [Test]
        [TestCase(-2, 1, 8)]
        [TestCase(9, 2, 3)]
        [TestCase(CodeEnum.CODE_CUSTOM, 3, -2)]
        [TestCase(CodeEnum.CODE_DIGITAL, 4, 0)]
        [ExcludeFromCodeCoverage]

        public void ItemKonstruktorLosiParametri(CodeEnum code, double value, int kontrola)
        {

            Assert.Throws<ArgumentOutOfRangeException>(
                () =>
                {
                    Item item = new Item(code, value, kontrola);


                });


        }



        [Test]
        [TestCase(CodeEnum.CODE_ANALOG, 1, 8)]
        [TestCase(CodeEnum.CODE_CONSUMER, 2, 3)]
        [TestCase(CodeEnum.CODE_CUSTOM, 3, -1)]
        [TestCase(CodeEnum.CODE_DIGITAL, 4, 8)]
        [TestCase(CodeEnum.CODE_LIMITSET, 5, 8)]
        [TestCase(CodeEnum.CODE_MULTIPLENODE, 6, 8)]
        [TestCase(CodeEnum.CODE_SINGLENODE, 7, 8)]
        [TestCase(CodeEnum.CODE_SOURCE, 8, 8)]


        public void ItemKonstruktorDobriParametri(CodeEnum code, double value, int kontrola)
        {
            Item item = new Item(code, value, kontrola);


            Assert.AreEqual(item.Code, code);
            Assert.AreEqual(item.Value, value);
            Assert.AreEqual(item.Kontrola, kontrola);


        }







        [Test]
        public void ItemPrazanKonstruktorDobriParametri()
        {
            Item item = new Item();


            Assert.AreEqual(item.Code, CodeEnum.CODE_ANALOG);
            Assert.AreEqual(item.Value, 0);
            Assert.AreEqual(item.Kontrola, -1);


        }


    }







}
