using Common;
using LoadBalancer;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Writer;


namespace WriterTest
{
    [TestFixture]
    public class WriterComponentTest
    {


        




        [Test]
        public void ItemPrazanKonstruktorDobriParametri()
        {
            WriterComponent writer = new WriterComponent();
            Item it = new Item();
            LoadBalancerComponent lb = new LoadBalancerComponent();
            Assert.AreEqual(it.Kontrola, writer.item.Kontrola);
            Assert.AreEqual(it.Value, writer.item.Value);
            Assert.AreEqual(it.Code, writer.item.Code);

            Assert.AreEqual(lb.bufferLB.buffer, writer.loadBalancer.bufferLB.buffer);
            Assert.AreEqual(lb.workeri, writer.loadBalancer.workeri);
            Assert.AreEqual(lb.LDList, writer.loadBalancer.LDList);
            Assert.AreEqual(lb.brojac, writer.loadBalancer.brojac);

        }




        [Test]
        public void ItemTest()
        {
            Assert.DoesNotThrow(() => new WriterComponent().item = new Item());
          
            Assert.AreEqual(new WriterComponent().item.Code, new Item().Code);
            Assert.AreEqual(new WriterComponent().item.Kontrola, new Item().Kontrola);
            Assert.AreEqual(new WriterComponent().item.Value, new Item().Value);

        }



        [Test]

        public void LoadBalancerTest()
        {
            Assert.DoesNotThrow(() => new WriterComponent().loadBalancer = new LoadBalancerComponent());

            Assert.AreEqual(new WriterComponent().loadBalancer.bufferLB.buffer, new LoadBalancerComponent().bufferLB.buffer);
            Assert.AreEqual(new WriterComponent().loadBalancer.LDList, new LoadBalancerComponent().LDList);
            Assert.AreEqual(new WriterComponent().loadBalancer.workeri, new LoadBalancerComponent().workeri);
            Assert.AreEqual(new WriterComponent().loadBalancer.brojac, new LoadBalancerComponent().brojac);
            

        }

        [Test]
        [TestCase(1000, 7, 6)]

        public void SlanjeAutomatskoTest(int i, int i2, int i3)
        {


            Assert.DoesNotThrow(() => new WriterComponent().SlanjeAutomatsko(i, i2, i3));

            Assert.AreEqual(new WriterComponent().SlanjeAutomatsko(i,i2,i3), true);

        }


        [Test]
        [TestCase(1000, 7, -3)]
        [TestCase(1000, -2, 1)]
        [ExcludeFromCodeCoverage]

        public void SlanjeAutomatskoTestLosi(int i, int i2, int i3)
        {


            Assert.Catch(() => new WriterComponent().SlanjeAutomatsko(i, i2, i3));

            

        }


        [Test]
       [TestCase(Enumeracija.CodeEnum.CODE_CONSUMER, 88, 4)]
        public void SlanjeManuelnoTest(Enumeracija.CodeEnum code, double value, int kontrola)
        {

            Assert.DoesNotThrow(() => new WriterComponent().SlanjeManuelno());

            Assert.AreEqual(new WriterComponent().SlanjeManuelno(), true);

        }



    }
}
