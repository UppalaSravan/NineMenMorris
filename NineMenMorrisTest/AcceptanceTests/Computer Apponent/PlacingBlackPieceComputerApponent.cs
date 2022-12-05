using NUnit.Framework;
using NineMenMorris;
using GenericMorris;

namespace NineMenMorrisTest.AcceptanceTests
{
    public class PlacingBlackPieceComputerApponent
    {
        private NineMenMorrisGame _nineMenMorrisGame;

        [SetUp]
        public void Setup()
        {
            _nineMenMorrisGame = new AutoNineMenMorrisGame(Piece.WhitePiece);
        }
        [Test]
        public void testValidPiecePlacement()
        {
            //Human Placing Black Piece
            Assert.AreEqual(MoveStatus.Valid, _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_D1));
            Assert.AreEqual(PlayerTurn.Black, _nineMenMorrisGame.GetPlayerTurn());
        }

        [Test]
        public void testInValidPieceOccupiedPoint()
        {
            //Human Placing Black Piece
            _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_D1);
            //Human Placing Black Piece on occupied Point
            Assert.AreEqual(MoveStatus.Invalid, _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_D1));
        }
    }
}
