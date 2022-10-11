using NUnit.Framework;
using NineMenMorris;
using GenericMorris;

namespace NineMenMorrisTest.AcceptanceTests
{
    public class PlacingWhitePieceTests
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
            Assert.AreEqual(MoveStatus.Valid, _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_A1));
        }

        [Test]
        public void testInValidPieceOccupiedPoint()
        {
            //White Turn
            _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_A1);
            //Black Turn
            _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_B2);
            //White Turn
            Assert.AreEqual(MoveStatus.Invalid, _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_A1));
        }
    }
}
