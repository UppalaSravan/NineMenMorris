using NUnit.Framework;
using NineMenMorris;
using GenericMorris;

namespace NineMenMorrisTest.AcceptanceTests
{
    public class PlacingBlackPieceTests
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
        [Test]
        public void testInValidPlacingAfterPlacement()
        {
            //White Turn
            _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_A1);
            //Black Turn
            _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_A4);

            //White Turn
            _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_B2);
            //Black Turn
            _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_C3);

            //White Turn
            _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_B4);
            //Black Turn
            _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_C4);

            //White Turn
            _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_E5);
            //Black Turn
            _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_E3);

            //White Turn
            _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_G1);
            //Black Turn
            _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_G7);

            //White Turn
            _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_G4);
            //Black Turn
            _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_F6);

            //White Turn
            _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_D7);
            //Black Turn
            _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_D2);

            //White Turn
            _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_F2);
            //Black Turn
            _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_D1);

            //White Turn
            _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_A7);
            //Black Turn
            _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_E4);

            //White Turn
            _nineMenMorrisGame.MakeMove(NineMensPointList.POINT_G4, NineMensPointList.POINT_F4);

            // Condition to check invalid move
            Assert.AreEqual(MoveStatus.Invalid, _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_B6));
        }
    }
}
