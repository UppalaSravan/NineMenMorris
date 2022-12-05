using NUnit.Framework;
using NineMenMorris;
using GenericMorris;

namespace NineMenMorrisTest.AcceptanceTests
{
    public class PlacingWhitePieceComputerApponent
    {
        private NineMenMorrisGame _nineMenMorrisGame;

        [SetUp]
        public void Setup()
        {
            _nineMenMorrisGame = new AutoNineMenMorrisGame(Piece.BlackPiece);
        }

        [Test]
        public void testValidPiecePlacement()
        {
            //Human Placing White Piece
            Assert.AreEqual(MoveStatus.Valid, _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_A1));
            Assert.AreEqual(PlayerTurn.White, _nineMenMorrisGame.GetPlayerTurn());
        }

        [Test]
        public void testInValidPieceOccupiedPoint()
        {
            //Human Placing White Piece
            _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_A1);
            //Human Placing White Piece on occupied Point
            Assert.AreEqual(MoveStatus.Invalid, _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_A1));
        }

    }
}
