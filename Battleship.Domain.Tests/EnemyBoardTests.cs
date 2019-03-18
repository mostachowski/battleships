using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Domain.Tests
{
    public class EnemyBoardTests
    {
        [Test]
        public void GetNeighbors_should_return_neighbours_of_the_field()
        {
            EnemyBoard board = new EnemyBoard();
            var result = board.GetNeighbors(new Coordinates(5, 5));
            Assert.AreEqual(4, result.Count());
            Assert.AreEqual(1, result.Where(x => x.Coordinates.Column == 5 && x.Coordinates.Row == 6).Count());
            Assert.AreEqual(1, result.Where(x => x.Coordinates.Column == 5 && x.Coordinates.Row == 4).Count());
            Assert.AreEqual(1, result.Where(x => x.Coordinates.Column == 4 && x.Coordinates.Row == 5).Count());
            Assert.AreEqual(1, result.Where(x => x.Coordinates.Column == 6 && x.Coordinates.Row == 5).Count());
        }

        [Test]
        public void GetHitNeighbors_should_return_neighbours_of_hit_field()
        {
            EnemyBoard board = new EnemyBoard();
            board.Fields.Where(x => x.Coordinates.Column == 5 && x.Coordinates.Row == 3).First().FieldType = FieldType.Hit;
            var result = board.GetHitNeighbors();

            Assert.AreEqual(1, result.Where(x => x.Column == 5 && x.Row == 2).Count());
            Assert.AreEqual(1, result.Where(x => x.Column == 5 && x.Row == 4).Count());
            Assert.AreEqual(1, result.Where(x => x.Column == 4 && x.Row == 3).Count());
            Assert.AreEqual(1, result.Where(x => x.Column == 6 && x.Row == 3).Count());

        }
    }
}
