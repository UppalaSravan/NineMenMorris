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
        public const string WHITE_TURN = "White Turn";
        public const string BLACK_TURN = "Black Turn";
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
            StatusMessage = GameStatusMessage.GAME_START;
            InitializeComponent();
            IntializeGameGUI();
        }
        private void IntializeGameGUI()
        {
            _uiPointList.Add("e4", e4);
            _uiPointList.Add("d5", d5);
            _uiPointList.Add("d6", d6);
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
            PointState currPointState;
            currPointState = _nineMenMorrisGame.GetPointState(uiPoint);
            _uiPointList[uiPoint].CurrentState = ConvertBoardPointtoUIPoint(currPointState);
            
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
            if (moveStatus == MoveStatus.Valid)
            {
                RefreshUIPointState(point);
                _gamestate = _nineMenMorrisGame.GetGameState();
                _isLastMillMove = false;
            }
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
            else
            {
                if (_nineMenMorrisGame.GetPlayerTurn() == PlayerTurn.Black)
                    StatusMessage = GameStatusMessage.BLACK_TURN;
                else
                    StatusMessage = GameStatusMessage.WHITE_TURN;
            }
        }
    }
}
