using NUnit.Framework;
using NineMenMorris;
using GenericMorris;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace NineMenMorrisTest.UnitTests
{
    public class NineMenMorrisGameTests
    {
        private NineMenMorrisGame _nineMenMorrisGame;
        private static int MAX_COUNT = 9;

        [SetUp]
        public void Setup()
        {
            _nineMenMorrisGame = new NineMenMorrisGame();
        }

        [Test]
        public void testPointState()
        {
            Assert.AreEqual(PointState.Empty,_nineMenMorrisGame.GetPointState(NineMensPointList.POINT_A1));
            _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_A1);
            Assert.AreEqual(PointState.WhitePlaced, _nineMenMorrisGame.GetPointState(NineMensPointList.POINT_A1));
            _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_A4);
            Assert.AreEqual(PointState.BlackPlaced, _nineMenMorrisGame.GetPointState(NineMensPointList.POINT_A4));
        }

        [Test]
        public void testPlayerTurn()
        {
            Assert.AreEqual(PlayerTurn.White, _nineMenMorrisGame.GetPlayerTurn());
            _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_A1);
            Assert.AreEqual(PlayerTurn.Black, _nineMenMorrisGame.GetPlayerTurn());
        }

        private void CompletePiecePlacement()
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
        }
        [Test]
        public void testGameState()
        {
            Assert.AreEqual(GameState.PlacingPieces, _nineMenMorrisGame.GetGameState());
            CompletePiecePlacement();
            Assert.AreEqual(GameState.PiecesPlaced, _nineMenMorrisGame.GetGameState());
        }
        [Test]
        public void testInValidMakeMoves()
        {
            CompletePiecePlacement();
            //Occupied Point
            Assert.AreEqual(MoveStatus.Invalid, _nineMenMorrisGame.MakeMove(NineMensPointList.POINT_A1, NineMensPointList.POINT_A4));
            //NonAdjacent Point
            Assert.AreEqual(MoveStatus.Invalid, _nineMenMorrisGame.MakeMove(NineMensPointList.POINT_A1, NineMensPointList.POINT_D3));
            //InValid Turn
            Assert.AreEqual(MoveStatus.Invalid, _nineMenMorrisGame.MakeMove(NineMensPointList.POINT_D2, NineMensPointList.POINT_D3));
            //Empty Point
            Assert.AreEqual(MoveStatus.Invalid, _nineMenMorrisGame.MakeMove(NineMensPointList.POINT_D3, NineMensPointList.POINT_C3));
        }

        [Test]
        public void testValidMakeMoves()
        {
            CompletePiecePlacement();
            //Empty Adjacent Point
            Assert.AreEqual(MoveStatus.Valid, _nineMenMorrisGame.MakeMove(NineMensPointList.POINT_B4, NineMensPointList.POINT_B6));

        }

        [Test]
        public void testRemainingWhitePieces()
        {
            Assert.AreEqual(MAX_COUNT, _nineMenMorrisGame.GetPiecesleftforPlacement(PlayerTurn.White));

            //White Turn
            _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_A1);
            Assert.AreEqual(MAX_COUNT-1, _nineMenMorrisGame.GetPiecesleftforPlacement(PlayerTurn.White));
            //Black Turn
            _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_A4);

            //White Turn
            _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_B2);
            Assert.AreEqual(MAX_COUNT - 2, _nineMenMorrisGame.GetPiecesleftforPlacement(PlayerTurn.White));
        }

        [Test]
        public void testRemainingBlackPieces()
        {
            Assert.AreEqual(MAX_COUNT, _nineMenMorrisGame.GetPiecesleftforPlacement(PlayerTurn.Black));

            //White Turn
            _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_A1);
            //Black Turn
            _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_A4);
            Assert.AreEqual(MAX_COUNT - 1, _nineMenMorrisGame.GetPiecesleftforPlacement(PlayerTurn.Black));

            //White Turn
            _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_B2);
            //Black Turn
            _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_C3);
            Assert.AreEqual(MAX_COUNT - 2, _nineMenMorrisGame.GetPiecesleftforPlacement(PlayerTurn.Black));
        }
    }
}
