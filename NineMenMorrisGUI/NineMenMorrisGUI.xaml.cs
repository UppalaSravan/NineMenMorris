using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GenericMorris;

namespace NineMenMorris
{
    enum GUIPointState
    {
        White,
        Black,
        WhiteSelected,
        BlackSelected,
        Empty
    }
    class GameStatusMessage
    {
        public const string GAME_START = "White Player Starts";
        public const string WHITE_TURN = "White Player to ";
        public const string BLACK_TURN = "Black Player to ";
        public const string PLACE_PIECE = "Place Piece";
        public const string MOVE_PIECE =  "Move Piece";
        public const string REMOVE_BLACK_PIECE = "Remove Black Piece";
        public const string REMOVE_WHITE_PIECE = "Remove White Piece";
        public const string WHITE_WON =  "White Won";
        public const string BLACK_WON = "Black Won";
    }
    class Point : Button, INotifyPropertyChanged
    {
        private GUIPointState _currentState;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public GUIPointState CurrentState
        {
            get
            {
                return this._currentState;
            }


            set
            {
                if (value != this.CurrentState)
                {
                    this._currentState = value;
                    NotifyPropertyChanged("CurrentState");
                }
            }
        }
        public Point()
        {

            CurrentState = GUIPointState.Empty;
            this.DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    /// <summary>
    /// Interaction logic for NineMenMorris.xaml
    /// </summary>
    public partial class NineMenMorrisGUI : Window,INotifyPropertyChanged
    {
        private NineMenMorrisGame _nineMenMorrisGame;
        private Dictionary<string, Point> _uiPointList;
        private GameState _gamestate;
        private bool _isLastMillMove;
        private string _statusMsg;
        private string _selection;

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public string StatusMessage {
            get
            {
                return _statusMsg;
            }
            set
            {
                if (value != _statusMsg)
                {
                    _statusMsg = value;
                    NotifyPropertyChanged("StatusMessage");
                }
            }
       
        }
        public NineMenMorrisGUI()
        {
            this.DataContext = this;
            _nineMenMorrisGame = new NineMenMorrisGame();
            _uiPointList = new Dictionary<string, Point>();
            _isLastMillMove = false;
            _selection = string.Empty;
            StatusMessage = GameStatusMessage.GAME_START;
            InitializeComponent();
            IntializeGameGUI();
        }
        private void IntializeGameGUI()
        {
            _uiPointList.Add(NineMensPointList.POINT_E4, e4);
            _uiPointList.Add(NineMensPointList.POINT_E3, e3);
            _uiPointList.Add(NineMensPointList.POINT_E5, e5);
            _uiPointList.Add(NineMensPointList.POINT_C3, c3);
            _uiPointList.Add(NineMensPointList.POINT_C4, c4);
            _uiPointList.Add(NineMensPointList.POINT_C5, c5);
            _uiPointList.Add(NineMensPointList.POINT_D5, d5);
            _uiPointList.Add(NineMensPointList.POINT_D6, d6);
            _uiPointList.Add(NineMensPointList.POINT_D1, d1);
            _uiPointList.Add(NineMensPointList.POINT_A1, a1);
            _uiPointList.Add(NineMensPointList.POINT_A4, a4);
            _uiPointList.Add(NineMensPointList.POINT_A7, a7);
            _uiPointList.Add(NineMensPointList.POINT_B2, b2);
            _uiPointList.Add(NineMensPointList.POINT_B4, b4);
            _uiPointList.Add(NineMensPointList.POINT_B6, b6);
            _uiPointList.Add(NineMensPointList.POINT_F2, f2);
            _uiPointList.Add(NineMensPointList.POINT_F4, f4);
            _uiPointList.Add(NineMensPointList.POINT_F6, f6);
            _uiPointList.Add(NineMensPointList.POINT_G1, g1);
            _uiPointList.Add(NineMensPointList.POINT_G4, g4);
            _uiPointList.Add(NineMensPointList.POINT_G7, g7);
            _uiPointList.Add(NineMensPointList.POINT_D2, d2);
            _uiPointList.Add(NineMensPointList.POINT_D3, d3);
            _uiPointList.Add(NineMensPointList.POINT_D7, d7);
            RefreshUIPointsState();
            _gamestate = _nineMenMorrisGame.GetGameState();
        }
        private void RefreshUIPointsState()
        {
            PointState currPointState;
            foreach (var uiPoint in _uiPointList)
            {
                currPointState = _nineMenMorrisGame.GetPointState(uiPoint.Key);
                uiPoint.Value.CurrentState = ConvertBoardPointtoUIPoint(currPointState);
            }
        }
        private void RefreshUIPointState(string uiPoint)
        {
            if (uiPoint != _selection)
            {
                PointState currPointState;
                currPointState = _nineMenMorrisGame.GetPointState(uiPoint);
                _uiPointList[uiPoint].CurrentState = ConvertBoardPointtoUIPoint(currPointState);
            }
            else
            {
                if (_uiPointList[uiPoint].CurrentState == GUIPointState.Black)
                    _uiPointList[uiPoint].CurrentState = GUIPointState.BlackSelected;
                else if(_uiPointList[uiPoint].CurrentState == GUIPointState.White)
                    _uiPointList[uiPoint].CurrentState = GUIPointState.WhiteSelected;
            }
        }
        private GUIPointState ConvertBoardPointtoUIPoint(PointState pointState)
        {
            switch (pointState)
            {
                case PointState.BlackPlaced:
                    return GUIPointState.Black;
                case PointState.WhitePlaced:
                    return GUIPointState.White;
                case PointState.Empty:
                    return GUIPointState.Empty;
            }
            return GUIPointState.Empty;
        }

        private void HandlePiecePlacement(String point)
        {
            MoveStatus moveStatus =  _nineMenMorrisGame.PlacePiece(point);
            if (moveStatus == MoveStatus.Valid || moveStatus == MoveStatus.Mill)
            {
                RefreshUIPointState(point);
                _gamestate = _nineMenMorrisGame.GetGameState();
                if (moveStatus == MoveStatus.Mill)
                    _isLastMillMove = true;
            }
        }

        private void HandleMill(String point)
        {
            MoveStatus moveStatus = _nineMenMorrisGame.RemovePiece(point);
            if (moveStatus == MoveStatus.Valid || moveStatus == MoveStatus.Won)
            {
                RefreshUIPointState(point);
                _gamestate = _nineMenMorrisGame.GetGameState();
                _isLastMillMove = false;
            }
        }

        private void HandleMove(String point)
        {
            if (_selection == string.Empty)
            {
                _selection = point;
            }
            else
            {
                MoveStatus moveStatus = _nineMenMorrisGame.MakeMove(_selection,point);
                if (moveStatus == MoveStatus.Valid || moveStatus == MoveStatus.Mill)
                {
                    _gamestate = _nineMenMorrisGame.GetGameState();
                    if (moveStatus == MoveStatus.Mill)
                        _isLastMillMove = true;
                }
                string startpoint = _selection;
                _selection = string.Empty;
                RefreshUIPointState(startpoint);
            }
            RefreshUIPointState(point);
        }
        private void PlayerAction(object sender, RoutedEventArgs e)
        {
            Point currentPoint = sender as Point;
            if (_isLastMillMove)
                HandleMill(currentPoint.Name);
            else if (_gamestate == GameState.PlacingPieces)
                HandlePiecePlacement(currentPoint.Name);
            else if (_gamestate == GameState.PiecesPlaced)
            {
                HandleMove(currentPoint.Name);
            }
            ShowStatustoUI();
        }

        private void ShowStatustoUI()
        {
            if (_gamestate == GameState.BlackWon || _gamestate == GameState.WhiteWon)
            {
                if (_gamestate == GameState.BlackWon)
                    StatusMessage = GameStatusMessage.BLACK_WON;
                else
                    StatusMessage = GameStatusMessage.WHITE_WON;
            }
            else if (_isLastMillMove)
            {
                if (_nineMenMorrisGame.GetPlayerTurn() == PlayerTurn.Black)
                    StatusMessage = GameStatusMessage.BLACK_TURN + GameStatusMessage.REMOVE_WHITE_PIECE;
                else
                    StatusMessage = GameStatusMessage.WHITE_TURN + GameStatusMessage.REMOVE_BLACK_PIECE;
            }
            else if (_gamestate == GameState.PlacingPieces)
            {
                if (_nineMenMorrisGame.GetPlayerTurn() == PlayerTurn.Black)
                    StatusMessage = GameStatusMessage.BLACK_TURN + GameStatusMessage.PLACE_PIECE;
                else
                    StatusMessage = GameStatusMessage.WHITE_TURN + GameStatusMessage.PLACE_PIECE;
            }
            else if (_gamestate == GameState.PiecesPlaced)
            {
                if (_nineMenMorrisGame.GetPlayerTurn() == PlayerTurn.Black)
                    StatusMessage = GameStatusMessage.BLACK_TURN + GameStatusMessage.MOVE_PIECE;
                else
                    StatusMessage = GameStatusMessage.WHITE_TURN + GameStatusMessage.MOVE_PIECE;
            }
        }

        private void ResetGameUI()
        {
            RefreshUIPointsState();
            _gamestate = _nineMenMorrisGame.GetGameState();
        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            if (menuItem.Header.ToString().ToLower().Contains("exit"))
                Application.Current.Shutdown();
            else if (menuItem.Header.ToString().Contains("Human vs Human"))
            {
                _nineMenMorrisGame.ResetBoard();
                ResetGameUI();
                StatusMessage = GameStatusMessage.GAME_START;
            }
            else
            {
                //New Game Human vs Computer
            }
        }
    }
}
