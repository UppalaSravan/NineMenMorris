using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using GenericMorris;
using Newtonsoft.Json;

namespace NineMenMorris
{
    public class AutoNineMenMorrisGame : NineMenMorrisGame
    {
        [JsonProperty]
        protected Piece _autoPlayerPiece;
        [JsonProperty]
        protected PointState _pointState;
        [JsonProperty]
        private Random _random;
        public AutoNineMenMorrisGame(Piece AutoPlayerPiece)
        {
            _autoPlayerPiece = AutoPlayerPiece;
            _pointState = (_autoPlayerPiece == Piece.BlackPiece) ? PointState.BlackPlaced : PointState.WhitePlaced;
            _random = new Random();
            if (AutoPlayerPiece == Piece.WhitePiece)
            {
                AutoPiecePlacement();
                _whitePlayerName = "Computer";
            }
            else
                _blackPlayerName = "Computer";
        }

        private int[] RandomNumbers(int count)
        {
            int start = _random.Next() % count;
            int[] ret = new int[count];

            for (int i = 0; i < ret.Length; i++)
            {
                ret[i] = start;
                start = start + 1;
                start = (start % count);
            }
            return ret;
        }

        private MoveStatus AutoPiecePlacement()
        {
            //step1 : search for vacant mill point
            var validPoints = _pointsManager.GetListofValidPoints();
            List<string> vacantPoints = new List<string>();

            int[] randSequence = RandomNumbers(validPoints.Count);
            foreach (int indx in randSequence)
            {
                var point = validPoints[indx];
                if (_board[point] == PointState.Empty)
                {
                    vacantPoints.Add(point);
                    _board[point] = _pointState;
                    bool isMill = IsMillPoint(point);
                    _board[point] = PointState.Empty;
                    if (isMill)
                        return base.PlacePiece(point);        
                }
            }
            //step2 : search adjacent vacant point
            randSequence = RandomNumbers(vacantPoints.Count);
            foreach (int indx in randSequence)
            {
                var point = vacantPoints[indx];
                foreach (var adjacentPoint in _pointsManager.GetAdjacentPoints(point))
                {
                    if(_board[adjacentPoint] == _pointState)
                                return base.PlacePiece(point);
                }
                
            }
            int randomindx = (_random.Next() % vacantPoints.Count);
            return base.PlacePiece(vacantPoints[randomindx]);
        }

        private MoveStatus AutoRemovePiece()
        {
            var validPoints = _pointsManager.GetListofValidPoints();
            List<string> opponentPoints = new List<string>();
            var oponentPieceState = _pointState == PointState.BlackPlaced ? PointState.WhitePlaced : PointState.BlackPlaced;
            MoveStatus moveStatus = MoveStatus.Invalid;

            int[] randSequence = RandomNumbers(validPoints.Count);
            //try to remove from adjacent point
            foreach (int indx in randSequence)
            {
                var point = validPoints[indx];
                if (_board[point] == oponentPieceState)
                {
                    opponentPoints.Add(point);
                    foreach (var adjacentPoint in _pointsManager.GetAdjacentPoints(point))
                    {
                        if (_board[adjacentPoint] == oponentPieceState)
                        {
                            moveStatus = base.RemovePiece(point);
                            if (moveStatus == MoveStatus.Valid || moveStatus == MoveStatus.Won)
                                return moveStatus;
                        }
                    }
                }
            }

            randSequence = RandomNumbers(opponentPoints.Count);
            //Remove any opponent point on board
            foreach (int indx in randSequence)
            {
                var point = opponentPoints[indx];
                moveStatus = base.RemovePiece(point);
                if (moveStatus == MoveStatus.Valid || moveStatus == MoveStatus.Won)
                    return moveStatus;
            }
                return moveStatus;
        }

        private MoveStatus AutoFlyMove()
        {
            var validPoints = _pointsManager.GetListofValidPoints();
            List<string> filledPoints = new List<string>();
            MoveStatus moveStatus = MoveStatus.Invalid;
            int[] randSequence = RandomNumbers(validPoints.Count);

            //Move a piece to vacant point such that it forms a mill
            foreach (int indx in randSequence)
            {
                var point = validPoints[indx];
                if (_board[point] == _pointState)
                {
                    filledPoints.Add(point);
                    foreach (var dstPoint in validPoints)
                    {
                        if (_board[dstPoint] == PointState.Empty)
                        {
                            _board[dstPoint] = _pointState;
                            _board[point] = PointState.Empty;
                            bool isMill = IsMillPoint(dstPoint);
                            _board[dstPoint] = PointState.Empty;
                            _board[point] = _pointState;
                            if (isMill)
                                return base.MakeMove(point, dstPoint);
                        }
                    }
                }
            }

            //Move a Piece from existing Mill
            randSequence = RandomNumbers(filledPoints.Count);
            foreach (int indx in randSequence)
            {
                var point = filledPoints[indx];
                bool isMill = IsMillPoint(point);
                if (isMill)
                {
                    foreach (var dstPoint in validPoints)
                    {
                        if (_board[dstPoint] == PointState.Empty)
                        {
                            return base.MakeMove(point, dstPoint);
                        }
                    }
                }
            }

            //Move to an random empty point
            randSequence = RandomNumbers(filledPoints.Count);
            foreach (int indx in randSequence)
            {
                var point = filledPoints[indx];
                foreach (var dstPoint in validPoints)
                {
                    if (_board[dstPoint] == PointState.Empty)
                    {
                        return base.MakeMove(point, dstPoint);
                    }
                }
            }

            return moveStatus;
        }
        private MoveStatus AutoMovePiece()
        {
            var validPoints = _pointsManager.GetListofValidPoints();
            List<string> filledPoints = new List<string>();
            MoveStatus moveStatus = MoveStatus.Invalid;

            int[] randSequence = RandomNumbers(validPoints.Count);
            //Move a piece to vacant point such that it forms a mill
            foreach (int indx in randSequence)
            {
                var point = validPoints[indx];
                if (_board[point] == _pointState)
                {
                    filledPoints.Add(point);
                    foreach (var adjacentPoint in _pointsManager.GetAdjacentPoints(point))
                    {
                        if (_board[adjacentPoint] == PointState.Empty)
                        {
                            _board[adjacentPoint] = _pointState;
                            _board[point] = PointState.Empty;
                            bool isMill = IsMillPoint(adjacentPoint);
                            _board[adjacentPoint] = PointState.Empty;
                            _board[point] = _pointState;
                            if (isMill)
                                return base.MakeMove(point, adjacentPoint);
                        }
                    }
                }
            }

            //Move a Piece from existing Mill
            randSequence = RandomNumbers(filledPoints.Count);
            foreach (int indx in randSequence)
            {
                var point = filledPoints[indx];
                bool isMill = IsMillPoint(point);
                if (isMill)
                {
                    foreach (var adjacentPoint in _pointsManager.GetAdjacentPoints(point))
                    {
                        if (_board[adjacentPoint] == PointState.Empty)
                        {
                            return base.MakeMove(point, adjacentPoint);
                        }
                    }
                }
            }

            //Move a Piece such that the vacant point is having adjacent filled point
            randSequence = RandomNumbers(filledPoints.Count);
            foreach (int indx in randSequence)
            {
                var point = filledPoints[indx];
                foreach (var adjacentPoint in _pointsManager.GetAdjacentPoints(point))
                {
                    if (_board[adjacentPoint] == PointState.Empty)
                    {
                        foreach (var tempPoint in _pointsManager.GetAdjacentPoints(adjacentPoint))
                        {
                            if(tempPoint != point &&  _board[tempPoint] == _pointState)
                                return base.MakeMove(point, adjacentPoint);
                        }
                    }
                }
            }
            //Move to an random adjacent point
            randSequence = RandomNumbers(filledPoints.Count);
            foreach (int indx in randSequence)
            {
                var point = filledPoints[indx];
                foreach (var adjacentPoint in _pointsManager.GetAdjacentPoints(point))
                {
                    if (_board[adjacentPoint] == PointState.Empty)
                    {
                        return base.MakeMove(point, adjacentPoint);
                    }
                }
            }
            return moveStatus;
        }
        public override MoveStatus PlacePiece(string point)
        {
            MoveStatus moveStatus = base.PlacePiece(point);
            if (moveStatus == MoveStatus.Valid )
            {
                if (_gameState == GameState.PlacingPieces)
                {
                    moveStatus = AutoPiecePlacement();
                    if (moveStatus == MoveStatus.Mill)
                        return AutoRemovePiece();
                }
                else if (_gameState == GameState.PiecesPlaced)
                {
                    moveStatus = AutoMovePiece();
                    if (moveStatus == MoveStatus.Mill)
                        return AutoRemovePiece();
                }
            }
            return moveStatus;
        }

        public override MoveStatus RemovePiece(string point)
        {
            MoveStatus moveStatus = base.RemovePiece(point);
            if (moveStatus == MoveStatus.Valid)
            {
                if (_gameState == GameState.PlacingPieces)
                {
                    moveStatus = AutoPiecePlacement();
                    if (moveStatus == MoveStatus.Mill)
                          return AutoRemovePiece();
                }
                else if (_gameState == GameState.PiecesPlaced)
                {
                    moveStatus = AutoMovePiece();
                    if (moveStatus == MoveStatus.Mill)
                        return AutoRemovePiece();
                }
            }
            return moveStatus;
        }

        public override MoveStatus MakeMove(string start, string end)
        {
            MoveStatus moveStatus = base.MakeMove(start, end);
            if (moveStatus == MoveStatus.Valid)
            {
                if(IsValidFlyMove())
                    moveStatus = AutoFlyMove();
                else
                   moveStatus = AutoMovePiece();
                if (moveStatus == MoveStatus.Mill)
                    return AutoRemovePiece();
            }
            return moveStatus;
        }
    }
}
