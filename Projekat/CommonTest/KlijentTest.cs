using Common;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTest
{
    [TestFixture]
    class KlijentTest
    {


        [Test]
        public void KlijentKonstruktor()
        {
            Assert.DoesNotThrow(() => new Klijent());

        }

        [Test]
        public void ProxyTest()
        {
            
            Assert.DoesNotThrow(() => new Klijent().proxy = null);
            
        }


        

        //    [Test]
        
        //public void PosaljiPorukuTest()
        //{

        //    Assert.DoesNotThrow(() => new Klijent().PosaljiPoruku("poruka"));

        //}

    }
}
