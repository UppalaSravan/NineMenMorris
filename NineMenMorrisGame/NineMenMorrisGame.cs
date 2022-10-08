using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenericMorris;

namespace NineMenMorris
{
    public class NineMenMorrisGame : MorrisGame
    {
        private static int MAX_COUNT = 9;
        private static int MIN_COUNT = 3;
        public NineMenMorrisGame() : base(new NineMensPointsManager(), MIN_COUNT, MAX_COUNT)
        {

        }
    }
}
