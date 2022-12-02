using Common;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTest
{
    [TestFixture]
    public class DescriptionTest
    {

        [Test]
        [TestCase(1, 2)]
        [TestCase(6, 1)]
        [TestCase(23, 3)]
        [TestCase(2, 4)]
        public void DescriptionKonstruktorDobriParametri(int id, int dataSet)
        {
            Description description = new Description(id, dataSet);


            Assert.AreEqual(description.ID, id);
            Assert.AreEqual(description.DataSet, dataSet);


        }


        [Test]
        public void DescriptionPrazanKonstruktorDobriParametri()
        {
            Description description = new Description();


            Assert.AreEqual(description.ID, 0);
            Assert.AreEqual(description.DataSet, 0);

        }



        [Test]
        [TestCase(-5, 2)]
        [TestCase(-1, 51)]
        [TestCase(23, 6)]
        [TestCase(2, -4)]
        [ExcludeFromCodeCoverage]
        public void DescriptionKonstruktorLosiParametri(int id, int dataSet)
        {

            Assert.Throws<ArgumentOutOfRangeException>(

                () =>
                {

                    Description description = new Description(id, dataSet);

                });
                ;
            
        }

    }
}
