using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private Dictionary<string, PointState> _board;
        private GameState _gameState;
        private PlayerTurn _currentTurn;
        private int _whitePiecesPlaced;
        private int _blackPiecesPlaced;
        private int _whitePiecesCount;
        private int _blackPiecesCount;
        private readonly int MAX_PIECE_COUNT;
        private readonly int MIN_PIECE_COUNT;
        private bool _isLastMoveMill;
        private PointsManager _pointsManager;
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
            IntializeBoard();
        }

        public PointState GetPointState(string point)
        {
            PointState pointState = PointState.Empty;
            if (IsValidPoint(point))
            {
                pointState = _board[point];
            }
            return pointState;
        }

        public GameState GetGameState()
        {
            return _gameState;
        }

        public PlayerTurn GetPlayerTurn()
        {
            return _currentTurn;
        }

        public MoveStatus PlacePiece(string point)
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

        public MoveStatus MakeMove(string start, string end)
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

        public MoveStatus RemovePiece(string point)
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
        public void ResetBoard()
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
        private bool IsValidPoint(string point)
        {
            return _board.ContainsKey(point);
        }

        private bool IsEmptyPoint(string point)
        {
            return _board[point] == PointState.Empty;
        }

        private bool IsMillPoint(string point)
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

        private void SetGameState()
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

        private bool IsValidPiecetoRemove(string point)
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

        private void IntializeBoard()
        {
            foreach (string point in _pointsManager.GetListofValidPoints())
            {
                _board.Add(point, PointState.Empty);
            };
        }


        private MoveStatus IsValidMove(string start, string end)
        {
            if (IsValidPoint(start) && IsValidPoint(end))
            {
                if (_gameState == GameState.PiecesPlaced && _isLastMoveMill == false && _board[end] == PointState.Empty)
                {
                    if ((_currentTurn == PlayerTurn.White && _board[start] == PointState.WhitePlaced) ||
                        (_currentTurn == PlayerTurn.Black && _board[start] == PointState.BlackPlaced))
                    {
                        if (_pointsManager.GetAdjacentPoints(start).Any(point => point.Equals(end)))
                            return MoveStatus.Valid;
                    }
                }
            }
            return MoveStatus.Invalid;
        }
    }
}
