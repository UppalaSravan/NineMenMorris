using NUnit.Framework;
using NineMenMorris;
using GenericMorris;

namespace NineMenMorrisTest.AcceptanceTests
{
    public class RemovingWhitePieceTests
    {
        private NineMenMorrisGame _nineMenMorrisGame;
        [SetUp]
        public void Setup()
        {
            _nineMenMorrisGame = new NineMenMorrisGame();
        }

        private void MakeMillforBlack()
        {
            //{A1 D1 G1} Mill Set
            _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_A4);
            //Black turn
            _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_A1);
            _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_G7);
            //Black turn
            _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_D1);
            _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_B4);
            //Black turn
            _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_G1);
        }

        [Test]
        public void testValidRemoval()
        {
            MakeMillforBlack();
            Assert.AreEqual(MoveStatus.Valid, _nineMenMorrisGame.RemovePiece(NineMensPointList.POINT_A4));
            Assert.AreEqual(PlayerTurn.White, _nineMenMorrisGame.GetPlayerTurn());
        }

        [Test]
        public void testInValidRemovalBlackSelected()
        {
            MakeMillforBlack();
            Assert.AreEqual(MoveStatus.Invalid, _nineMenMorrisGame.RemovePiece(NineMensPointList.POINT_A1));
            Assert.AreEqual(PlayerTurn.Black, _nineMenMorrisGame.GetPlayerTurn()); ;
        }
        [Test]
        public void testInvalidRemovalFromMill()
        {
            //White Turn
            _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_B2);
            //Black Turn
            _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_F2);

            //White Turn
            _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_B4);
            //Black Turn
            _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_F4);

            //White Turn
            _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_B6);

            Assert.AreEqual(MoveStatus.Valid, _nineMenMorrisGame.RemovePiece(NineMensPointList.POINT_F2));

            //Black Turn
            _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_F2);
            //White Turn
            _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_A7);

            //Black Turn
            _nineMenMorrisGame.PlacePiece(NineMensPointList.POINT_F6);

            Assert.AreEqual(MoveStatus.Invalid, _nineMenMorrisGame.RemovePiece(NineMensPointList.POINT_B6));


        }
    }
}
