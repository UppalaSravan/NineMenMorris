using NUnit.Framework;
using NineMenMorris;
using GenericMorris;


namespace NineMenMorrisTest.AcceptanceTests
{
    public class FlyingBlackPieceTests
    {
        private NineMenMorrisGame _nineMenMorrisGame;

        [SetUp]
        public void Setup()
        {
            _nineMenMorrisGame = new NineMenMorrisGame();
        }
        private void DoPiecePlacementAndMove()
        {
            //White Turn
            _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_A1);
            //Black Turn
            _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_G7);

            //White Turn
            _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_A4);
            //Black Turn
            _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_D7);

            //White Turn and Mill
            _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_A7);
            _nineMenMorrisGame.RemovePiece(NineMensPointList.POINT_D7);
            //Black Turn
            _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_D7);

            //White Turn
            _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_B4);
            //Black Turn
            _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_D6);

            //White Turn and Mill
            _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_C4);
            _nineMenMorrisGame.RemovePiece(NineMensPointList.POINT_D7);
            //Black Turn
            _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_D7);

            //White Turn
            _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_C5);
            //Black Turn
            _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_D3);

            //White Turn and Mill
            _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_C3);
            _nineMenMorrisGame.RemovePiece(NineMensPointList.POINT_D7);
            //Black Turn
            _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_D7);

            //White Turn
            _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_B2);
            //Black Turn
            _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_E3);

            //White Turn and Mill
            _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_B6);
            _nineMenMorrisGame.RemovePiece(NineMensPointList.POINT_D7);
            //Black Turn
            _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_D7);

            //White Turn
            _nineMenMorrisGame.MakeMove(NineMensPointList.POINT_B2, NineMensPointList.POINT_D2);
            //Black Turn
            _nineMenMorrisGame.MakeMove(NineMensPointList.POINT_E3, NineMensPointList.POINT_E4);

            //White Turn and Mill
            _nineMenMorrisGame.MakeMove(NineMensPointList.POINT_D2, NineMensPointList.POINT_B2);
            _nineMenMorrisGame.RemovePiece(NineMensPointList.POINT_D7);
            //Black Turn
            _nineMenMorrisGame.MakeMove(NineMensPointList.POINT_E4, NineMensPointList.POINT_E3);

            //White Turn
            _nineMenMorrisGame.MakeMove(NineMensPointList.POINT_B2, NineMensPointList.POINT_D2);
        }

        [Test]
        public void testValidFlyMove()
        {
            DoPiecePlacementAndMove();
            //Black Turn
            _nineMenMorrisGame.MakeMove(NineMensPointList.POINT_E3, NineMensPointList.POINT_E4);
            //White Turn and Mill
            _nineMenMorrisGame.MakeMove(NineMensPointList.POINT_D2, NineMensPointList.POINT_B2);
            _nineMenMorrisGame.RemovePiece(NineMensPointList.POINT_D6);

            Assert.AreEqual(PlayerTurn.Black, _nineMenMorrisGame.GetPlayerTurn());
            Assert.AreEqual(MoveStatus.Valid, _nineMenMorrisGame.MakeMove(NineMensPointList.POINT_G7, NineMensPointList.POINT_D1));
        }

        [Test]
        public void testInValidFlyOccupiedPoint()
        {
            DoPiecePlacementAndMove();
            //Black Turn
            _nineMenMorrisGame.MakeMove(NineMensPointList.POINT_E3, NineMensPointList.POINT_E4);
            //White Turn and Mill
            _nineMenMorrisGame.MakeMove(NineMensPointList.POINT_D2, NineMensPointList.POINT_B2);
            _nineMenMorrisGame.RemovePiece(NineMensPointList.POINT_D6);

            Assert.AreEqual(PlayerTurn.Black, _nineMenMorrisGame.GetPlayerTurn());
            Assert.AreEqual(MoveStatus.Invalid, _nineMenMorrisGame.MakeMove(NineMensPointList.POINT_G7, NineMensPointList.POINT_E4));
        }

        [Test]
        public void testInValidFlyWithMorePieces()
        {
            DoPiecePlacementAndMove();
            Assert.AreEqual(PlayerTurn.Black, _nineMenMorrisGame.GetPlayerTurn());
            Assert.AreEqual(MoveStatus.Invalid, _nineMenMorrisGame.MakeMove(NineMensPointList.POINT_G7, NineMensPointList.POINT_D1));
        }

        [Test]
        public void testInValidFlyWhitePieceSelected()
        {
            DoPiecePlacementAndMove();
            //Black Turn
            _nineMenMorrisGame.MakeMove(NineMensPointList.POINT_E3, NineMensPointList.POINT_E4);
            //White Turn and Mill
            _nineMenMorrisGame.MakeMove(NineMensPointList.POINT_D2, NineMensPointList.POINT_B2);
            _nineMenMorrisGame.RemovePiece(NineMensPointList.POINT_D6);

            Assert.AreEqual(PlayerTurn.Black, _nineMenMorrisGame.GetPlayerTurn());
            Assert.AreEqual(MoveStatus.Invalid, _nineMenMorrisGame.MakeMove(NineMensPointList.POINT_A1, NineMensPointList.POINT_D1));
        }
    }
}
