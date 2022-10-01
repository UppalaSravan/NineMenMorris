using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineMenMorris
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
    public class NineMenMorrisGame
    {
        private Dictionary<string, PointState> _board;
        private GameState _gameState;
        private PlayerTurn _currentTurn;
        private int _whitePiecesPlaced;
        private int _blackPiecesPlaced;
        private int _whitePiecesCount;
        private int _blackPiecesCount;
        public const int MAX_PIECE_COUNT = 9;
        public const int MIN_PIECE_COUNT = 3;
        private bool _isLastMoveMill;
        public NineMenMorrisGame()
        {
            _board = new Dictionary<string, PointState>();
            _gameState = GameState.PlacingPieces;
            _currentTurn = PlayerTurn.White;
            _whitePiecesPlaced = 0;
            _blackPiecesPlaced = 0;
            _whitePiecesCount = 0;
            _blackPiecesCount = 0;
            _isLastMoveMill = false;
            IntializeBoard();
        }

        private bool IsValidPoint(string point)
        {
            return _board.ContainsKey(point);
        }
        private bool IsEmptyPoint(string point)
        {
            return _board[point] == PointState.Empty;
        }
        private bool IsMillMove(string point)
        {
            //TODO
            return false;
        }
        private void SetGameState()
        {
            if (_gameState == GameState.PlacingPieces && _blackPiecesPlaced == MAX_PIECE_COUNT && _whitePiecesPlaced == MAX_PIECE_COUNT)
                _gameState = GameState.PiecesPlaced;
            else if(_gameState == GameState.PiecesPlaced)
            {
                if (_blackPiecesCount < MIN_PIECE_COUNT)
                    _gameState = GameState.WhiteWon;
                else if (_whitePiecesCount < MIN_PIECE_COUNT)
                    _gameState = GameState.BlackWon;
            }   
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
                    if (!IsMillMove(point))
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
                else if(_currentTurn == PlayerTurn.White)
                {
                    _board[point] = PointState.WhitePlaced;
                    _whitePiecesPlaced++;
                    if (!IsMillMove(point))
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

            return moveStatus;
        }

        public MoveStatus RemovePiece(string Point)
        {
            MoveStatus moveStatus = MoveStatus.Invalid;
            if ((_gameState == GameState.PiecesPlaced || _gameState == GameState.PlacingPieces) &&
                    _isLastMoveMill && IsValidPoint(Point))
            {
                if(_currentTurn == PlayerTurn.Black && _board[Point] == PointState.WhitePlaced)
                {
                    _board[Point] = PointState.Empty;
                    _whitePiecesCount--;
                    SetGameState();
                    if (_gameState == GameState.BlackWon)
                        moveStatus = MoveStatus.Won;
                    else
                        moveStatus = MoveStatus.Valid;
                }
                else if (_currentTurn == PlayerTurn.White && _board[Point] == PointState.BlackPlaced)
                {
                    _board[Point] = PointState.Empty;
                    _blackPiecesCount--;
                    SetGameState();
                    if (_gameState == GameState.WhiteWon)
                        moveStatus = MoveStatus.Won;
                    else
                        moveStatus = MoveStatus.Valid;
                }
            }
            return moveStatus;
        }
        private void IntializeBoard()
        {
            _board.Add("e4", PointState.Empty);
            _board.Add("d5", PointState.Empty);
            _board.Add("d6", PointState.Empty);
        }

        private MoveStatus IsValidMove(string start, string end)
        {
            return MoveStatus.Invalid;
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
    }
}
