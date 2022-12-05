using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericMorris
{
    public class BoardPointMappings
    {
        [JsonProperty]
        public Dictionary<string, List<List<string>>> MillPointMap { get; set; }
        [JsonProperty]
        public Dictionary<string, List<string>> AdjacentPointMap { get; set; }
    }
}
