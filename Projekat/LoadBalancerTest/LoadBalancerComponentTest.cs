using Common;
using LoadBalancer;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Worker;

namespace LoadBalancerTest
{
    [TestFixture]
    public class LoadBalancerComponentTest
    {

        Mock<LoadBalancerComponent> mockLoadBalancer;
        Mock<WorkerComponent> mockWorker;

        [SetUp]
        public void SetUp()
        {
            mockLoadBalancer = new Mock<LoadBalancerComponent>();
            mockWorker = new Mock<WorkerComponent>();
        }


        [Test]
        public void LoadBalancerKonsturktorBezParametara()
        {
                List<Description> LDList = new List<Description>();
                Dictionary<int, WorkerComponent> workeri = new Dictionary<int, WorkerComponent>();
                BufferLB bufferLB = new BufferLB();

            LoadBalancerComponent loadbalancer = new LoadBalancerComponent();

            Assert.AreEqual(loadbalancer.bufferLB.buffer, bufferLB.buffer);
            Assert.AreEqual(loadbalancer.LDList, LDList);
            Assert.AreEqual(loadbalancer.workeri, workeri);
            Assert.AreEqual(loadbalancer.brojac, 0);


        }

        [Test]
        [TestCase(8)]
        public void PaliIGasiWorkereTestDobriParametri(int i)
        {
            Item ite = new Item();
            ite.Kontrola = i;
            LoadBalancerComponent loadbalancer = new LoadBalancerComponent();
            WorkerComponent wor = new WorkerComponent();
            WorkerComponent wor2 = new WorkerComponent();
            wor.description.listItem.Add(4,ite);
            
            loadbalancer.workeri.Add(i, wor);

            Assert.AreEqual(loadbalancer.workeri.ContainsKey(i), true);

            Assert.DoesNotThrow(() => loadbalancer.PaliIGasiWorkere(ite));

            Assert.AreEqual(loadbalancer.workeri.ContainsKey(i), false);

            Assert.DoesNotThrow(() => loadbalancer.PaliIGasiWorkere(ite));

            Assert.AreEqual(loadbalancer.workeri.ContainsKey(i), true);

            Assert.AreEqual(loadbalancer.PaliIGasiWorkere(ite), true);

        }


        [Test]
        [TestCase(-4)]
        public void PaliIGasiWorkereTestLosiParametriParametri(int i)
        {
            Item ite = new Item();
            ite.Kontrola = i;
            LoadBalancerComponent loadbalancer = new LoadBalancerComponent();

            Assert.Catch(() => new LoadBalancerComponent().PaliIGasiWorkere(ite));



        }

        [Test]
        
        public void RaspodelaPoslaTest()
        {
            Item ite = new Item();
            ite.Value = 2;
            ite.Code = Enumeracija.CodeEnum.CODE_LIMITSET;
            Item ite2 = new Item();
            ite2.Value = 8;
            ite2.Code = Enumeracija.CodeEnum.CODE_LIMITSET;

            mockLoadBalancer.Object.workeri.Add(1,mockWorker.Object);
            mockLoadBalancer.Object.bufferLB.buffer.Add(0, ite);
            mockLoadBalancer.Object.bufferLB.buffer.Add(1, ite2);

            mockLoadBalancer.Object.RaspodelaPosla();

            Assert.AreEqual(mockLoadBalancer.Object.workeri[1].description.ID, 1);
            Assert.AreEqual(mockLoadBalancer.Object.workeri[1].description.listItem, mockLoadBalancer.Object.bufferLB.buffer) ;
            Assert.AreEqual(mockLoadBalancer.Object.workeri[1].description.DataSet, 2) ;

            Assert.DoesNotThrow(() => new LoadBalancerComponent().RaspodelaPosla());

            Assert.AreEqual(new LoadBalancerComponent().RaspodelaPosla(), true);

        }




        





        public void PosaljiWorkerimaTest()
        {

            Assert.DoesNotThrow(() => new LoadBalancerComponent().PosaljiWorkerima());

            Assert.AreEqual(new LoadBalancerComponent().PosaljiWorkerima(), true);

        }



    }

    
}
