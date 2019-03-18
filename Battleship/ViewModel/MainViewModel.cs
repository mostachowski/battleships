using Battleship.Domain;
using GalaSoft.MvvmLight;

namespace Battleship.ViewModel
{

    public class MainViewModel : ViewModelBase
    {

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            var game = new Game();
            game.Player1.PlaceShips();
            Title = "Hello Battleship fan!";
            Game = new GameViewModel(game);

        }

        public string Title { get; set; }
        public GameViewModel Game { get; set; }
    }
}