using Microsoft.Reactive.Testing;
using NUnit.Framework;
using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;


namespace Battleship.Domain.Tests
{
    [TestFixture]
    public class GameTests : ReactiveTest
    {

        private Game game;
        [SetUp]
        public void SetUp()
        {
            game = new Game();
        }


        [Test]
        public void ProcessPlayer1Shot_should_inform_that_turn_is_over()
        {
            Coordinates coord = new Coordinates(1, 1);
            game.ProcessPlayer1Shot(coord);

            var scheduler = new TestScheduler();
            var observer = scheduler.CreateObserver<Unit>();
            scheduler.Schedule(TimeSpan.FromTicks(10), (s, t) => Processshot(coord));
            game.RoundCompleted.Subscribe(observer);

            Assert.AreEqual(0, observer.Messages.Count);
            scheduler.Start();
            Assert.AreEqual(1, observer.Messages.Count);
        }

        [Test]
        public void ProcessPlayer1Shot_should_result_in_Player2_shot()
        {
            Coordinates coord = new Coordinates(1, 1);

            var scheduler = new TestScheduler();
            var observer = scheduler.CreateObserver<Unit>();
            scheduler.Schedule(TimeSpan.FromTicks(10), (s, t) => Processshot(coord));
            game.RoundCompleted.Subscribe(observer);

            Assert.AreEqual(0, game.Player1.GameBoard.Fields.Where(x => x.FieldType == FieldType.Hit || x.FieldType == FieldType.Miss).Count());
            scheduler.Start();
            Assert.AreEqual(1, game.Player1.GameBoard.Fields.Where(x => x.FieldType == FieldType.Hit || x.FieldType == FieldType.Miss).Count());
        }

        [Test]
        public void Game_should_notify_about_endgame_when_all_ships_are_sunk()
        {
            Coordinates coord = new Coordinates(1, 1);
            var scheduler = new TestScheduler();
            var observer = scheduler.CreateObserver<string>();
            scheduler.Schedule(TimeSpan.FromTicks(10), (s, t) =>

            {
                for (int i = 0; i < 100; i++)
                {
                    game.ProcessPlayer1Shot(coord);
                }
                return null;
            });
            game.GameFinished.Subscribe(observer);
            scheduler.Start();

            Assert.AreEqual(1, observer.Messages.Count);
            Assert.AreEqual(observer.Messages.First().Value.Value, "Computer won!");

        }

        private IDisposable Processshot(Coordinates coord)
        {
            game.ProcessPlayer1Shot(coord);
            return null;
        }
    }
}
