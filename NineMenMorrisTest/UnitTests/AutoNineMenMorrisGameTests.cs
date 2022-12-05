using NUnit.Framework;
using NineMenMorris;
using GenericMorris;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace NineMenMorrisTest.UnitTests
{
    public class  AutoNineMenMorrisGameTests
    {
        private NineMenMorrisGame _nineMenMorrisGame;
        private static int MAX_COUNT = 9;

        [SetUp]
        public void Setup()
        {
            _nineMenMorrisGame = new AutoNineMenMorrisGame(Piece.BlackPiece);
        }

        [Test]
        public void testPlayerTurn()
        {
            Assert.AreEqual(PlayerTurn.White, _nineMenMorrisGame.GetPlayerTurn());
            _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_A1);
            Assert.AreEqual(PlayerTurn.White, _nineMenMorrisGame.GetPlayerTurn());
        }

        [Test]
        public void testPointState()
        {
            Assert.AreEqual(PointState.Empty, _nineMenMorrisGame.GetPointState(NineMensPointList.POINT_A1));
            _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_A1);
            Assert.AreEqual(PointState.WhitePlaced, _nineMenMorrisGame.GetPointState(NineMensPointList.POINT_A1));
        }

    }
}
