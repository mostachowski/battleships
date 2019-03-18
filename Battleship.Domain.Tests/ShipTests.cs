

using NUnit.Framework;

namespace Battleship.Domain.Tests
{
    public class ShipTests
    {
        [Test]
        public void Ship_should_be_sunk_when_all_parts_hit()
        {
            Battleship ship = new Battleship();
            ship.Hits = 5;
            Assert.IsTrue(ship.IsSunk);
        }
    }

}
