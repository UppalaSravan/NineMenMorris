using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericMorris
{
    public class BoardPointMappings
    {
        public Dictionary<string, List<List<string>>> MillPointMap { get; set; }
        public Dictionary<string, List<string>> AdjacentPointMap { get; set; }
    }
}
