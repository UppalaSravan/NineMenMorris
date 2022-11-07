using NUnit.Framework;
using NineMenMorris;
using GenericMorris;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace NineMenMorrisTest.UnitTests
{
    public class PointsManagerTests
    {
        private PointsManager _pointsManager;

        [SetUp]
        public void Setup()
        {
            _pointsManager = new PointsManager("JsonMappings/NineMenMorris.json");
        }

        [Test]
        public void testGetListofValidPoints()
        {
            List<string> points = _pointsManager.GetListofValidPoints();
            Assert.AreEqual(points.Count, 24);
            Assert.IsTrue(points.Contains(NineMensPointList.POINT_A1));
            Assert.IsTrue(points.Contains(NineMensPointList.POINT_A4));
            Assert.IsTrue(points.Contains(NineMensPointList.POINT_F6));
        }

        [Test]
        public void testGetListofInValidPoints()
        {
            List<string> points = _pointsManager.GetListofValidPoints();
            Assert.AreEqual(points.Count, 24);
            Assert.IsFalse(points.Contains("a3"));
            Assert.IsFalse(points.Contains("a5"));
            Assert.IsFalse(points.Contains("c1"));
        }

        [Test]
        public void testListValidAdjacentPoints()
        {
           List<string> points =  _pointsManager.GetAdjacentPoints(NineMensPointList.POINT_A1);
           Assert.IsTrue(points.Contains(NineMensPointList.POINT_A4));
           Assert.IsTrue(points.Contains(NineMensPointList.POINT_D1));

           points = _pointsManager.GetAdjacentPoints(NineMensPointList.POINT_D1);
           Assert.IsTrue(points.Contains(NineMensPointList.POINT_A1));
           Assert.IsTrue(points.Contains(NineMensPointList.POINT_G1));
        }

        [Test]
        public void testListInValidAdjacentPoints()
        {
            List<string> points = _pointsManager.GetAdjacentPoints(NineMensPointList.POINT_A1);
            Assert.IsFalse(points.Contains(NineMensPointList.POINT_B2));
            Assert.IsFalse(points.Contains(NineMensPointList.POINT_B6));

            points = _pointsManager.GetAdjacentPoints(NineMensPointList.POINT_D1);
            Assert.IsFalse(points.Contains(NineMensPointList.POINT_A7));
            Assert.IsFalse(points.Contains(NineMensPointList.POINT_D7));
        }

        [Test]
        public void testValidMillPoints()
        {
            List<List<string>> millSets = _pointsManager.GetAllPossibleMills(NineMensPointList.POINT_A1);
            List<string> millSet1 = new List<string> { NineMensPointList.POINT_A1, 
                                                       NineMensPointList.POINT_A4,NineMensPointList.POINT_A7 };
            List<string> millSet2 = new List<string> { NineMensPointList.POINT_A1,
                                                       NineMensPointList.POINT_D1,NineMensPointList.POINT_G1 };
            foreach(List<string> millSet in millSets)
            {
                Assert.IsTrue(millSet.All(value => millSet1.Contains(value)) ||
                              millSet.All(value => millSet2.Contains(value)));
            }

        }

        [Test]
        public void testInValidMillPoints()
        {
            List<List<string>> millSets = _pointsManager.GetAllPossibleMills(NineMensPointList.POINT_A1);
            List<string> millSet1 = new List<string> { NineMensPointList.POINT_A1,
                                                       NineMensPointList.POINT_A4,NineMensPointList.POINT_B2 };
            List<string> millSet2 = new List<string> { NineMensPointList.POINT_A1,
                                                       NineMensPointList.POINT_D1,NineMensPointList.POINT_G7 };
            foreach (List<string> millSet in millSets)
            {
                Assert.IsFalse(millSet.All(value => millSet1.Contains(value)) ||
                              millSet.All(value => millSet2.Contains(value)));
            }

        }

    }
}
