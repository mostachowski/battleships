using Battleship.Domain;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Battleship.ViewModel
{
    public class GameViewModel : ViewModelBase
    {
        public GameViewModel(Game game)
        {
            Game = game;
            PlayerBoard = new BoardViewModel(Game.Player1.GameBoard);
            EnemyBoard = new BoardViewModel(Game.Player1.EnemyBoard);
            Game.Player1.FireShot();
            StartGameCommand = new RelayCommand(StartGame);
            EnemyBoard.FieldClicked.Subscribe(HandlePlayer1Shot);
            Game.RoundCompleted.Subscribe(UpdateBoards);
            Game.GameFinished.Subscribe(GameFinished);

        }

        private void UpdateBoards(Unit unit)
        {
            EnemyBoard.UpdateBoard(Game.Player1.EnemyBoard);
            PlayerBoard.UpdateBoard(Game.Player1.GameBoard);
        }

        private void StartGame()
        {
            Game = new Game();
            PlayerBoard.UpdateBoard(Game.Player1.GameBoard);
            EnemyBoard.UpdateBoard(Game.Player1.EnemyBoard);
            Game.RoundCompleted.Subscribe(UpdateBoards);
            Game.GameFinished.Subscribe(GameFinished);
            EndMessage = string.Empty;
            EnemyBoard.IsActive = true;
        }

        public BoardViewModel PlayerBoard { get; set; }
        public BoardViewModel EnemyBoard { get; set; }

        public Game Game { get; set; }
        private string endMessage;
        public string EndMessage
        {
            get { return endMessage; }
            set
            {
                endMessage = value;
                RaisePropertyChanged(() => EndMessage);
            }
        }

        public ICommand StartGameCommand { get; set; }
        public void GameFinished(string message)
        {
            EndMessage = message;
            RaisePropertyChanged(() => EndMessage);
            EnemyBoard.IsActive = false;
        }

        public void HandlePlayer1Shot(Coordinates coordinates)
        {
            this.Game.ProcessPlayer1Shot(coordinates);
            var res = Game.Player2.ProcessShot(coordinates);
            Game.Player1.ProcessShotResult(coordinates, res);
        }
    }
}
