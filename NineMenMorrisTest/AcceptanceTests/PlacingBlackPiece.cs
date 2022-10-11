using NUnit.Framework;
using NineMenMorris;
using GenericMorris;

namespace NineMenMorrisTest.AcceptanceTests
{
    public class PlacingBlackPiece
    {
        private NineMenMorrisGame _nineMenMorrisGame;
        [SetUp]
        public void Setup()
        {
            _nineMenMorrisGame = new NineMenMorrisGame();
        }

        [Test]
        public void testValidPiecePlacement()
        {
            _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_A1);
            //Black Turn
            Assert.AreEqual(MoveStatus.Valid, _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_A4));
        }

        [Test]
        public void testInValidPieceOccupiedPoint()
        {
            //White Turn
            _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_A1);
            //Black Turn
            Assert.AreEqual(MoveStatus.Invalid, _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_A1));
        }

    }
}
