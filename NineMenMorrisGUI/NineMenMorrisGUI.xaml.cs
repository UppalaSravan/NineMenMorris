using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Principal;
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
using Microsoft.Win32;
using Newtonsoft.Json;

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
    enum GameMode
    {
        Human,
        Computer
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
        public const string WON =  " Won";
        public const string PLACED_COUNT = "Placed {0} ";
        public const string REMAIN_COUNT = "        Remaining Pieces - {0}";
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
        private NineMorrisGameFactory _gameFactory;
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly string JSON_FILE_EXTENSION = "Json files (*.json)|*.json";
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
            _gameFactory = new NineMorrisGameFactory();
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
                RefreshUIPointsState();
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
                RefreshUIPointsState();
                _gamestate = _nineMenMorrisGame.GetGameState();
                _isLastMillMove = false;
            }
        }

        private void HandleMove(String point)
        {
            if (_selection == string.Empty)
            {
                _selection = point;
                RefreshUIPointState(point);
            }
            else
            {
                MoveStatus moveStatus = _nineMenMorrisGame.MakeMove(_selection,point);
                if (moveStatus != MoveStatus.Invalid)
                {
                    _gamestate = _nineMenMorrisGame.GetGameState();
                    if (moveStatus == MoveStatus.Mill)
                        _isLastMillMove = true;
                }
                string startpoint = _selection;
                _selection = string.Empty;
                RefreshUIPointsState();
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
                HandleMove(currentPoint.Name);
            }
            ShowStatustoUI();
        }

        private void ShowStatustoUI()
        {
            if (_gamestate == GameState.BlackWon || _gamestate == GameState.WhiteWon)
            {
                StatusMessage = _nineMenMorrisGame.GetWiningPlayerName() + GameStatusMessage.WON;
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
                    StatusMessage = GameStatusMessage.BLACK_TURN + GameStatusMessage.PLACE_PIECE +
                                    string.Format(GameStatusMessage.REMAIN_COUNT, _nineMenMorrisGame.GetPiecesleftforPlacement(PlayerTurn.Black));
                else
                    StatusMessage = GameStatusMessage.WHITE_TURN + GameStatusMessage.PLACE_PIECE +
                                    string.Format(GameStatusMessage.REMAIN_COUNT, _nineMenMorrisGame.GetPiecesleftforPlacement(PlayerTurn.White));
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
        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            if (menuItem.Header.ToString().ToLower().Contains("exit"))
                Application.Current.Shutdown();
            else
            {
                _nineMenMorrisGame = _gameFactory.GetNineMenMorrisGameObject(menuItem.Header.ToString());
                ResetGameUI();
                ShowStatustoUI();
            }
        }

        private void SaveAndRestore(object sender, RoutedEventArgs e)
        {
            try
            {
                MenuItem menuItem = sender as MenuItem;
                if (menuItem.Header.ToString().ToLower().Contains("restore"))
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.Filter = JSON_FILE_EXTENSION;
                    string json_string = "";
                    if (openFileDialog.ShowDialog() == true)
                        json_string = File.ReadAllText(openFileDialog.FileName);
                    NineMenMorrisGame gameObject = JsonConvert.DeserializeObject<NineMenMorrisGame>(json_string, new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.Auto
                    });
                    _nineMenMorrisGame = gameObject;
                    ResetGameUI();
                    ShowStatustoUI();
                }
                else
                {
                    string json_string = JsonConvert.SerializeObject(_nineMenMorrisGame, Formatting.Indented, new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.Objects

                    });
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = JSON_FILE_EXTENSION;
                    if (saveFileDialog.ShowDialog() == true)
                        File.WriteAllText(saveFileDialog.FileName, json_string);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to save or restore file", "Save/Restore error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
