

using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Battleship.Domain
{
    public class Game
    {
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }

        public Game()
        {
            Player1 = new Player("Player1");
            Player2 = new Player("Computer");

            Player1.PlaceShips();
            Player2.PlaceShips();

            roundCompleted = new Subject<Unit>();
            gameFinished = new Subject<string>();
        }

        private Subject<Unit> roundCompleted;
        private Subject<string> gameFinished;
        public IObservable<Unit> RoundCompleted { get { return roundCompleted.AsObservable(); } }
        public IObservable<string> GameFinished { get { return gameFinished.AsObservable(); } }

        public void ProcessPlayer1Shot(Coordinates coordinates)
        {
            if (Player1.HasLost || Player2.HasLost)
                return;
            var res = Player2.ProcessShot(coordinates);
            Player1.ProcessShotResult(coordinates, res);
            if (Player2.HasLost)
            {
                EndGame("You won!");
            }
            coordinates = Player2.FireShot();
            res = Player1.ProcessShot(coordinates);
            Player2.ProcessShotResult(coordinates, res);
            if (Player1.HasLost)
            {
                EndGame("Computer won!");
            }
            // Notification goes after both movrs since player2 is a computer and doesnt take time for move.
            roundCompleted.OnNext(Unit.Default);
        }

        private void EndGame(string message)
        {
            gameFinished.OnNext(message);
        }
    }

}
