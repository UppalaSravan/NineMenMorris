using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GenericMorris
{
    public enum PointState
    {
        Empty,
        WhitePlaced,
        BlackPlaced
    }
    public enum Piece
    {
        BlackPiece,
        WhitePiece
    }
    public enum PlayerTurn
    {
        White,
        Black
    }
    public enum GameState
    {
        PlacingPieces,
        PiecesPlaced,
        WhiteWon,
        BlackWon
    }
    public enum MoveStatus
    {
        Valid,
        Invalid,
        Mill,
        Won
    }
    public class MorrisGame
    {
        [JsonProperty]
        protected Dictionary<string, PointState> _board;
        [JsonProperty]
        protected GameState _gameState;
        [JsonProperty]
        protected PlayerTurn _currentTurn;
        [JsonProperty]
        protected int _whitePiecesPlaced;
        [JsonProperty]
        protected int _blackPiecesPlaced;
        [JsonProperty]
        protected int _whitePiecesCount;
        [JsonProperty]
        protected int _blackPiecesCount;
        [JsonProperty]
        protected int MAX_PIECE_COUNT;
        [JsonProperty]
        protected int MIN_PIECE_COUNT;
        [JsonProperty]
        protected bool _isLastMoveMill;
        [JsonProperty]
        protected PointsManager _pointsManager;
        [JsonProperty]
        protected string _whitePlayerName;
        [JsonProperty]
        protected string _blackPlayerName;
        public MorrisGame(string mappingFilePath, int minPieceCount, int maxPieceCount)
        {
            _board = new Dictionary<string, PointState>();
            _pointsManager = new PointsManager(mappingFilePath);
            MAX_PIECE_COUNT = maxPieceCount;
            MIN_PIECE_COUNT = minPieceCount;
            _gameState = GameState.PlacingPieces;
            _currentTurn = PlayerTurn.White;
            _whitePiecesPlaced = 0;
            _blackPiecesPlaced = 0;
            _whitePiecesCount = 0;
            _blackPiecesCount = 0;
            _isLastMoveMill = false;
            _whitePlayerName = "White Player";
            _blackPlayerName = "Black Player";
            IntializeBoard();
        }

        public virtual PointState GetPointState(string point)
        {
            PointState pointState = PointState.Empty;
            if (IsValidPoint(point))
            {
                pointState = _board[point];
            }
            return pointState;
        }

        public virtual GameState GetGameState()
        {
            return _gameState;
        }

        public virtual PlayerTurn GetPlayerTurn()
        {
            return _currentTurn;
        }

        public virtual int GetPiecesleftforPlacement(PlayerTurn playerTurn)
        {
            if (playerTurn == PlayerTurn.Black)
                return MAX_PIECE_COUNT - _blackPiecesPlaced;
            else
                return MAX_PIECE_COUNT - _whitePiecesPlaced;
        }

        public virtual MoveStatus PlacePiece(string point)
        {
            MoveStatus moveStatus = MoveStatus.Invalid;
            if (_gameState == GameState.PlacingPieces && IsValidPoint(point) && IsEmptyPoint(point))
            {
                if (_currentTurn == PlayerTurn.Black)
                {
                    _board[point] = PointState.BlackPlaced;
                    _blackPiecesPlaced++;
                    _blackPiecesCount++;
                    if (!IsMillPoint(point))
                    {
                        _currentTurn = PlayerTurn.White;
                        moveStatus = MoveStatus.Valid;
                    }
                    else
                    {
                        moveStatus = MoveStatus.Mill;
                        _isLastMoveMill = true;
                    }

                }
                else if (_currentTurn == PlayerTurn.White)
                {
                    _board[point] = PointState.WhitePlaced;
                    _whitePiecesPlaced++;
                    _whitePiecesCount++;
                    if (!IsMillPoint(point))
                    {
                        _currentTurn = PlayerTurn.Black;
                        moveStatus = MoveStatus.Valid;
                    }
                    else
                    {
                        moveStatus = MoveStatus.Mill;
                        _isLastMoveMill = true;
                    }
                }
                SetGameState();
            }
            return moveStatus;
        }

        public virtual MoveStatus MakeMove(string start, string end)
        {
            MoveStatus moveStatus = MoveStatus.Invalid;
            if (MoveStatus.Valid ==  IsValidMove(start,end))
            {
                if (_currentTurn == PlayerTurn.Black && _board[start] == PointState.BlackPlaced)
                {
                    _board[start] = PointState.Empty;
                    _board[end] = PointState.BlackPlaced;
                    if (!IsMillPoint(end))
                    {
                        _currentTurn = PlayerTurn.White;
                        moveStatus = MoveStatus.Valid;
                    }
                    else
                    {
                        moveStatus = MoveStatus.Mill;
                        _isLastMoveMill = true;
                    }
                }
                else if (_currentTurn == PlayerTurn.White && _board[start] == PointState.WhitePlaced )
                {
                    _board[start] = PointState.Empty;
                    _board[end] = PointState.WhitePlaced;
                    if (!IsMillPoint(end))
                    {
                        _currentTurn = PlayerTurn.Black;
                        moveStatus = MoveStatus.Valid;
                    }
                    else
                    {
                        moveStatus = MoveStatus.Mill;
                        _isLastMoveMill = true;
                    }
                }
                SetGameState();
            }
            return moveStatus;
        }

        public virtual MoveStatus RemovePiece(string point)
        {
            MoveStatus moveStatus = MoveStatus.Invalid;
            if ((_gameState == GameState.PiecesPlaced || _gameState == GameState.PlacingPieces) &&
                    _isLastMoveMill && IsValidPiecetoRemove(point))
            {
                if (_currentTurn == PlayerTurn.Black && _board[point] == PointState.WhitePlaced)
                {
                    _board[point] = PointState.Empty;
                    _whitePiecesCount--;
                    _isLastMoveMill = false;
                    SetGameState();
                    _currentTurn = PlayerTurn.White;
                    if (_gameState == GameState.BlackWon)
                        moveStatus = MoveStatus.Won;
                    else
                        moveStatus = MoveStatus.Valid;
                }
                else if (_currentTurn == PlayerTurn.White && _board[point] == PointState.BlackPlaced)
                {
                    _board[point] = PointState.Empty;
                    _blackPiecesCount--;
                    _isLastMoveMill = false;
                    SetGameState();
                    _currentTurn = PlayerTurn.Black;
                    if (_gameState == GameState.WhiteWon)
                        moveStatus = MoveStatus.Won;
                    else
                        moveStatus = MoveStatus.Valid;
                }
            }
            return moveStatus;
        }
        public virtual void ResetBoard()
        {
            _gameState = GameState.PlacingPieces;
            _currentTurn = PlayerTurn.White;
            _whitePiecesPlaced = 0;
            _blackPiecesPlaced = 0;
            _whitePiecesCount = 0;
            _blackPiecesCount = 0;
            _isLastMoveMill = false;
            foreach (string point in _pointsManager.GetListofValidPoints())
            {
                _board[point] = PointState.Empty;
            };
        }
        protected virtual bool IsValidPoint(string point)
        {
            return _board.ContainsKey(point);
        }

        protected virtual bool IsEmptyPoint(string point)
        {
            return _board[point] == PointState.Empty;
        }

        protected virtual bool IsMillPoint(string point)
        {
            bool isMillMove = false;
            if (IsValidPoint(point) && _board[point] != PointState.Empty)
            {
                PointState currPointState = _board[point];
                int millPointCounter;
                foreach (var millPointSet in _pointsManager.GetAllPossibleMills(point))
                {
                    millPointCounter = 1;
                    foreach (string mPoint in millPointSet)
                    {
                        if (_board[mPoint] == currPointState)
                            millPointCounter++;
                    }
                    if (millPointCounter == MIN_PIECE_COUNT)
                    {
                        isMillMove = true;
                        break;
                    }
                }
            }
            return isMillMove;
        }

        protected virtual void SetGameState()
        {
            if (_gameState == GameState.PlacingPieces && _blackPiecesPlaced == MAX_PIECE_COUNT && _whitePiecesPlaced == MAX_PIECE_COUNT)
                _gameState = GameState.PiecesPlaced;
            else if (_gameState == GameState.PiecesPlaced)
            {
                if (_blackPiecesCount < MIN_PIECE_COUNT)
                    _gameState = GameState.WhiteWon;
                else if (_whitePiecesCount < MIN_PIECE_COUNT)
                    _gameState = GameState.BlackWon;
            }
        }

        protected virtual bool IsValidPiecetoRemove(string point)
        {
            bool retVal = false;
            if (!IsValidPoint(point))
                retVal = false;
            if (IsEmptyPoint(point))
                retVal = false;
            else if (!IsMillPoint(point))
                retVal = true;
            else
            {
                int countNonMillPoints = 0;
                foreach (var validPoint in _pointsManager.GetListofValidPoints())
                {
                    if (_board[validPoint] == _board[point])
                    {
                        if (!IsMillPoint(validPoint))
                        {
                            countNonMillPoints++;
                        }
                    }
                }
                if (countNonMillPoints == 0)
                    retVal = true;
            }
            return retVal;
        }

        protected virtual void IntializeBoard()
        {
            foreach (string point in _pointsManager.GetListofValidPoints())
            {
                _board.Add(point, PointState.Empty);
            };
        }

        protected virtual bool IsValidFlyMove()
        {
            if (_currentTurn == PlayerTurn.Black && _blackPiecesCount == MIN_PIECE_COUNT)
                return true;
            if (_currentTurn == PlayerTurn.White && _whitePiecesCount == MIN_PIECE_COUNT)
                return true;
            return false;
        }

        protected virtual MoveStatus IsValidMove(string start, string end)
        {
            if (IsValidPoint(start) && IsValidPoint(end))
            {
                if (_gameState == GameState.PiecesPlaced && _isLastMoveMill == false && _board[end] == PointState.Empty)
                {
                    if ((_currentTurn == PlayerTurn.White && _board[start] == PointState.WhitePlaced) ||
                        (_currentTurn == PlayerTurn.Black && _board[start] == PointState.BlackPlaced))
                    {
                        if (_pointsManager.GetAdjacentPoints(start).Any(point => point.Equals(end)) || IsValidFlyMove())
                            return MoveStatus.Valid;
                    }
                }
            }
            return MoveStatus.Invalid;
        }

        public string GetWiningPlayerName()
        {
            if (_gameState == GameState.BlackWon)
                return _blackPlayerName;
            if (_gameState == GameState.WhiteWon)
                return _whitePlayerName;
            return string.Empty;
        }
    }
}
