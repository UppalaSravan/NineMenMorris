using GenericMorris;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace NineMenMorris
{
    public class NineMorrisGameFactory
    {
        private readonly string HUMAN_VS_HUMAN = "Human vs Human";
        private readonly string WHITEPLAYER_VS_COMPUTER = "WhitePlayer vs Computer";
        public NineMenMorrisGame GetNineMenMorrisGameObject(string menuSelection)
        {
            if (menuSelection.Contains(HUMAN_VS_HUMAN))
                return new NineMenMorrisGame();
            else if(menuSelection.Contains(WHITEPLAYER_VS_COMPUTER))
                return  new AutoNineMenMorrisGame(Piece.BlackPiece);
            else
                return new AutoNineMenMorrisGame(Piece.WhitePiece);
        }
    }
}
