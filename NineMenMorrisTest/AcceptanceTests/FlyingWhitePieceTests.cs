using NUnit.Framework;
using NineMenMorris;
using GenericMorris;

namespace NineMenMorrisTest.AcceptanceTests
{
    public class FlyingWhitePieceTests
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
            _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_G7);
            //Black Turn
            _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_A1);

            //White Turn
            _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_D7);
            //Black Turn
            _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_A4);

            //White Turn
            _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_D6);
            //Black Turn and Mill
            _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_A7);
            _nineMenMorrisGame.RemovePiece(NineMensPointList.POINT_D7);

            //White Turn
            _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_D7);
            //Black Turn
            _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_B4);

            //White Turn
            _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_E5);
            //Black Turn and Mill
            _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_C4);
            _nineMenMorrisGame.RemovePiece(NineMensPointList.POINT_D7);

            //White Turn
            _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_D7);
            //Black Turn
            _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_C5);

            //White Turn 
            _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_E3);
            //Black Turn and Mill
            _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_C3);
            _nineMenMorrisGame.RemovePiece(NineMensPointList.POINT_D7);

            //White Turn
            _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_D7);
            //Black Turn
            _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_B2);

            //White Turn
            _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_F2);
            //Black Turn and Mill
            _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_B6);
            _nineMenMorrisGame.RemovePiece(NineMensPointList.POINT_D7);

            //White Turn
            _nineMenMorrisGame.MakeMove(NineMensPointList.POINT_F2, NineMensPointList.POINT_F4);
            //Black Turn
            _nineMenMorrisGame.MakeMove(NineMensPointList.POINT_B2, NineMensPointList.POINT_D2);

            //White Turn
            _nineMenMorrisGame.MakeMove(NineMensPointList.POINT_F4, NineMensPointList.POINT_F2);
            //Black Turn and Mill
            _nineMenMorrisGame.MakeMove(NineMensPointList.POINT_D2, NineMensPointList.POINT_B2);
            _nineMenMorrisGame.RemovePiece(NineMensPointList.POINT_D6);

            //White Turn
            _nineMenMorrisGame.MakeMove(NineMensPointList.POINT_F2, NineMensPointList.POINT_F4);
            //Black Turn
            _nineMenMorrisGame.MakeMove(NineMensPointList.POINT_B2, NineMensPointList.POINT_D2);
        }


        [Test]
        public void testValidFlyMove()
        {
            DoPiecePlacementAndMove();
            //White Turn
            _nineMenMorrisGame.MakeMove(NineMensPointList.POINT_F4, NineMensPointList.POINT_F2);
            //Black Turn and Mill
            _nineMenMorrisGame.MakeMove(NineMensPointList.POINT_D2, NineMensPointList.POINT_B2);
            _nineMenMorrisGame.RemovePiece(NineMensPointList.POINT_E3);

            Assert.AreEqual(PlayerTurn.White, _nineMenMorrisGame.GetPlayerTurn());
            Assert.AreEqual(MoveStatus.Valid, _nineMenMorrisGame.MakeMove(NineMensPointList.POINT_G7, NineMensPointList.POINT_D1));
        }

        [Test]
        public void testInValidFlyOccupiedPoint()
        {
            DoPiecePlacementAndMove();
            //White Turn
            _nineMenMorrisGame.MakeMove(NineMensPointList.POINT_F4, NineMensPointList.POINT_F2);
            //Black Turn and Mill
            _nineMenMorrisGame.MakeMove(NineMensPointList.POINT_D2, NineMensPointList.POINT_B2);
            _nineMenMorrisGame.RemovePiece(NineMensPointList.POINT_E3);

            Assert.AreEqual(PlayerTurn.White, _nineMenMorrisGame.GetPlayerTurn());
            Assert.AreEqual(MoveStatus.Invalid, _nineMenMorrisGame.MakeMove(NineMensPointList.POINT_G7, NineMensPointList.POINT_E5));
        }

        [Test]
        public void testInValidFlyWithMorePieces()
        {
            DoPiecePlacementAndMove();
            Assert.AreEqual(PlayerTurn.White, _nineMenMorrisGame.GetPlayerTurn());
            Assert.AreEqual(MoveStatus.Invalid, _nineMenMorrisGame.MakeMove(NineMensPointList.POINT_G7, NineMensPointList.POINT_D1));
        }

        [Test]
        public void testInValidFlyBlackPieceSelected()
        {
            DoPiecePlacementAndMove();
            //White Turn
            _nineMenMorrisGame.MakeMove(NineMensPointList.POINT_F4, NineMensPointList.POINT_F2);
            //Black Turn and Mill
            _nineMenMorrisGame.MakeMove(NineMensPointList.POINT_D2, NineMensPointList.POINT_B2);
            _nineMenMorrisGame.RemovePiece(NineMensPointList.POINT_E3);

            Assert.AreEqual(PlayerTurn.White, _nineMenMorrisGame.GetPlayerTurn());
            Assert.AreEqual(MoveStatus.Invalid, _nineMenMorrisGame.MakeMove(NineMensPointList.POINT_A1, NineMensPointList.POINT_D1));
        }
    }
}
