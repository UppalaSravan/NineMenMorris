using NUnit.Framework;
using NineMenMorris;
using GenericMorris;

namespace NineMenMorrisTest.AcceptanceTests
{
    public class GameStatusTests
    {
        private NineMenMorrisGame _nineMenMorrisGame;

        [SetUp]
        public void Setup()
        {
            _nineMenMorrisGame = new NineMenMorrisGame();
        }

        private void PlacementAndMovesForWhitePlayer()
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
            //Black Turn
            _nineMenMorrisGame.MakeMove(NineMensPointList.POINT_E3, NineMensPointList.POINT_E4);

            //White Turn and Mill
            _nineMenMorrisGame.MakeMove(NineMensPointList.POINT_D2, NineMensPointList.POINT_B2);
            _nineMenMorrisGame.RemovePiece(NineMensPointList.POINT_D6);
            //Black Turn
            _nineMenMorrisGame.MakeMove(NineMensPointList.POINT_G7, NineMensPointList.POINT_D1);

            //White Turn
            _nineMenMorrisGame.MakeMove(NineMensPointList.POINT_B2, NineMensPointList.POINT_D2);
            //Black Turn
            _nineMenMorrisGame.MakeMove(NineMensPointList.POINT_D1, NineMensPointList.POINT_G7);
        }

        private void PlacementAndMovesForBlackPlayer()
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

            //White Turn
            _nineMenMorrisGame.MakeMove(NineMensPointList.POINT_F4, NineMensPointList.POINT_F2);
            //Black Turn and Mill
            _nineMenMorrisGame.MakeMove(NineMensPointList.POINT_D2, NineMensPointList.POINT_B2);
            _nineMenMorrisGame.RemovePiece(NineMensPointList.POINT_E3);

            //White Turn
            _nineMenMorrisGame.MakeMove(NineMensPointList.POINT_G7, NineMensPointList.POINT_D1);
            //Black Turn
            _nineMenMorrisGame.MakeMove(NineMensPointList.POINT_B2, NineMensPointList.POINT_D2);

            //White Turn
            _nineMenMorrisGame.MakeMove(NineMensPointList.POINT_D1, NineMensPointList.POINT_G7);
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
        public void testWinByWhitePlayer()
        {
            PlacementAndMovesForWhitePlayer();
            Assert.AreEqual(PlayerTurn.White, _nineMenMorrisGame.GetPlayerTurn());
            Assert.AreEqual(GameState.PiecesPlaced, _nineMenMorrisGame.GetGameState());

            //White Turn
            Assert.AreEqual(MoveStatus.Mill,_nineMenMorrisGame.MakeMove(NineMensPointList.POINT_D2, NineMensPointList.POINT_B2));
            Assert.AreEqual(MoveStatus.Won, _nineMenMorrisGame.RemovePiece(NineMensPointList.POINT_G7));
            //White Won
            Assert.AreEqual(GameState.WhiteWon, _nineMenMorrisGame.GetGameState());
        }

        [Test]
        public void testWinByBlackPlayer()
        {
            PlacementAndMovesForBlackPlayer();
            Assert.AreEqual(PlayerTurn.Black, _nineMenMorrisGame.GetPlayerTurn());
            Assert.AreEqual(GameState.PiecesPlaced, _nineMenMorrisGame.GetGameState());

            //Black Turn
            Assert.AreEqual(MoveStatus.Mill, _nineMenMorrisGame.MakeMove(NineMensPointList.POINT_D2, NineMensPointList.POINT_B2));
            Assert.AreEqual(MoveStatus.Won, _nineMenMorrisGame.RemovePiece(NineMensPointList.POINT_G7));
            //Black Won
            Assert.AreEqual(GameState.BlackWon, _nineMenMorrisGame.GetGameState());
        }

        [Test]
        public void testContinuingGameAfterWhitePlayerMove()
        {
            CompletePiecePlacement();
            //White Player Move
            Assert.AreEqual(PlayerTurn.White, _nineMenMorrisGame.GetPlayerTurn());
            Assert.AreEqual(MoveStatus.Valid, _nineMenMorrisGame.MakeMove(NineMensPointList.POINT_G4, NineMensPointList.POINT_F4));
            //Game Continuing after WhitePlayer Move
            Assert.AreEqual(GameState.PiecesPlaced, _nineMenMorrisGame.GetGameState());
        }

        [Test]
        public void testContinuingGameAfterBlackPlayerMove()
        {
            CompletePiecePlacement();
            //White Player Move
            _nineMenMorrisGame.MakeMove(NineMensPointList.POINT_G4, NineMensPointList.POINT_F4);

            //Black Player Move
            Assert.AreEqual(PlayerTurn.Black, _nineMenMorrisGame.GetPlayerTurn());
            Assert.AreEqual(MoveStatus.Valid, _nineMenMorrisGame.MakeMove(NineMensPointList.POINT_F6, NineMensPointList.POINT_D6));
            //Game Continuing after BlackPlayer Move
            Assert.AreEqual(GameState.PiecesPlaced, _nineMenMorrisGame.GetGameState());
        }
    }
}
