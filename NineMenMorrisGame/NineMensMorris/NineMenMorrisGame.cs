using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenericMorris;
using Newtonsoft.Json;

namespace NineMenMorris
{
    public class NineMenMorrisGame : MorrisGame
    {
        [JsonProperty]
        protected static int MAX_COUNT = 9;
        [JsonProperty]
        protected static int MIN_COUNT = 3;
        [JsonProperty]
        protected static string NINE_MENS_JSON_PATH = "JsonMappings/NineMenMorris.json";
        public NineMenMorrisGame() : base(NINE_MENS_JSON_PATH, MIN_COUNT, MAX_COUNT)
        {

        }
    }
}
