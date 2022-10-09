using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenericMorris;
using Newtonsoft.Json;

namespace GenericMorris
{
    public class PointsManager 
    {
        BoardPointMappings _boardPointMappings;
        public PointsManager(string mappingFilePath)
        {
            PopulatePointMappings(mappingFilePath);
        }
        public List<string> GetListofValidPoints()
        {
            return _boardPointMappings.AdjacentPointMap.Keys.ToList();
        }
        public List<List<string>> GetAllPossibleMills(string point)
        {
            List<List<string>> millPairPoints;
            if (_boardPointMappings.MillPointMap.TryGetValue(point, out millPairPoints))
            {
                return millPairPoints;
            }
            return null;
        }

        public List<string> GetAdjacentPoints(string point)
        {
            List<string> adjacentPoints;
            if (_boardPointMappings.AdjacentPointMap.TryGetValue(point, out adjacentPoints))
            {
                return adjacentPoints;
            }
            return null;
        }
        private void PopulatePointMappings(string mappingFilePath)
        {
              using (StreamReader jsonReader = new StreamReader(mappingFilePath))
              {
                 string json = jsonReader.ReadToEnd();
                 _boardPointMappings =   JsonConvert.DeserializeObject<BoardPointMappings>(json);
              }
        }
    }
}
