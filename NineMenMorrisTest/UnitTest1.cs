using NUnit.Framework;
using NineMenMorris;

namespace NineMenMorrisTest
{
    public class NineMenMorrisGameTest
    {
        private NineMenMorrisGame _nineMenMorrisGame;
        [SetUp]
        public void Setup()
        {
            _nineMenMorrisGame = new NineMenMorrisGame();
        }

        [Test]
        public void testInVaildPoint()
        {
            MoveStatus moveStatus = _nineMenMorrisGame.PlacePiece("e4", Piece.BlackPiece);
            Assert.AreEqual(moveStatus, MoveStatus.Invalid);
        }
    }
}