using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Domain.Tests
{
    public class PlayerTests
    {
        [Test]
        public void PlaceShips_should_put_2_destroyers_and_1_battleship_on_board()
        {
            Player player = new Player("Test");
            player.PlaceShips();
            var ships = player.GameBoard.Fields.Select(x => x.Ship).Distinct().ToList();
            Assert.AreEqual(ships.Where(x => x is Battleship).Count(), 1);
            Assert.AreEqual(ships.Where(x => x is Destroyer).Count(), 2);

        }
        [Test]
        public void ProcessShot_should_return_hit_when_it_hits_the_ship()
        {
            Player player = new Player("Test");
            player.PlaceShips();
            var coord = player.GameBoard.Fields.First(x => x.Ship != null).Coordinates;
            var result = player.ProcessShot(coord);
            Assert.AreEqual(result, ShotResult.Hit);
        }

        [Test]
        public void ProcessShot_should_return_sunk_when_it_sinks_the_ship()
        {
            Player player = new Player("Test");
            player.PlaceShips();
            var ship = player.GameBoard.Fields.First(x => x.Ship != null).Ship;
            var coordinates = player.GameBoard.Fields.Where(x => x.Ship == ship).Select(x => x.Coordinates).ToList();
            var results = new List<ShotResult>();
            foreach (var coord in coordinates)
            {
                results.Add(player.ProcessShot(coord));
            }

            Assert.AreEqual(1, results.Where(x => x == ShotResult.Sunk).Count());
        }
    }
}
