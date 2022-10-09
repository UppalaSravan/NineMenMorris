using NUnit.Framework;
using NineMenMorris;
using GenericMorris;

namespace NineMenMorrisTest
{
    public class NineMenMorrisGameTest
    {
        private NineMenMorrisGame _nineMenMorrisGame;
        [SetUp]
        public void Setup()
        {
            _nineMenMorrisGame = new NineMenMorrisGame();
        }

        [Test]
        public void testInVaildPoint()
        {
            MoveStatus moveStatus = _nineMenMorrisGame.PlacePiece("e5");
            Assert.AreEqual(moveStatus, MoveStatus.Invalid);
        }
    }
}