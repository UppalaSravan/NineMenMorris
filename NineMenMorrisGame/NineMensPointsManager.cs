using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenericMorris;

namespace NineMenMorris
{
    public class NineMensPointsManager : IPointManager
    {
        Dictionary<string,List<List<string>>> _millPointMap;
        public NineMensPointsManager()
        {
            _millPointMap = new Dictionary<string, List<List<string>>>();
            PopulateMillPointsMap();
        }
        public List<string> GetListofValidPoints()
        {
            return _millPointMap.Keys.ToList();
        }
        public List<List<string>> GetAllPossibleMills(string point)
        {
            List<List<string>> millPairPoints = null;
            if (_millPointMap.TryGetValue(point, out millPairPoints))
            {
                return millPairPoints;
            }
            return null;
        }

        public List<string> GetAdjacentPoints(string point)
        {
            return null;
        }
        private void PopulateMillPointsMap()
        {
            foreach (string point in NineMensPointList.GetAllValidPoints())
            {
                _millPointMap.Add(point, new List<List<string>>());
            }

            _millPointMap[NineMensPointList.POINT_A1].Add((NineMensPointList.POINT_A4 + "," +
                                                          NineMensPointList.POINT_A7).Split(',').ToList<String>());
            _millPointMap[NineMensPointList.POINT_A1].Add((NineMensPointList.POINT_D1 + "," +
                                                          NineMensPointList.POINT_G1).Split(',').ToList<String>());

            _millPointMap[NineMensPointList.POINT_A4].Add((NineMensPointList.POINT_A1 + "," +
                                              NineMensPointList.POINT_A7).Split(',').ToList<String>());
            _millPointMap[NineMensPointList.POINT_A4].Add((NineMensPointList.POINT_B4 + "," +
                                                          NineMensPointList.POINT_C4).Split(',').ToList<String>());

            _millPointMap[NineMensPointList.POINT_A7].Add((NineMensPointList.POINT_A4 + "," +
                                              NineMensPointList.POINT_A1).Split(',').ToList<String>());
            _millPointMap[NineMensPointList.POINT_A7].Add((NineMensPointList.POINT_D7 + "," +
                                                          NineMensPointList.POINT_G7).Split(',').ToList<String>());

        }
    }
}
