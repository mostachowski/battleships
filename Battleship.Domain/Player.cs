using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Battleship.Domain
{
    public class Player
    {
        public string Name { get; set; }
        public Board GameBoard { get; set; }
        public EnemyBoard EnemyBoard { get; set; }
        public List<Ship> Ships { get; set; }
        public bool HasLost
        {
            get
            {
                return Ships.All(x => x.IsSunk);
            }
        }

        public Player(string name)
        {
            Name = name;
            Ships = new List<Ship>()
        {
            new Destroyer(),
            new Destroyer(),
            new Battleship(),
        };
            GameBoard = new Board();
            EnemyBoard = new EnemyBoard();
        }

        public void PlaceShips()
        {
            GameBoard = new Board();
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            foreach (var ship in Ships)
            {

                bool isOpen = true;
                while (isOpen)
                {
                    var startcolumn = rand.Next(1, 11);
                    var startrow = rand.Next(1, 11);
                    int endrow = startrow, endcolumn = startcolumn;
                    var orientation = rand.Next(1, 101) % 2;

                    List<int> panelNumbers = new List<int>();
                    if (orientation == 0)
                    {
                        for (int i = 1; i < ship.Width; i++)
                        {
                            endrow++;
                        }
                    }
                    else
                    {
                        for (int i = 1; i < ship.Width; i++)
                        {
                            endcolumn++;
                        }
                    }

                    //end of the board. Start again
                    if (endrow > 10 || endcolumn > 10)
                    {
                        isOpen = true;
                        continue;
                    }


                    var affectedFields = GameBoard.Fields.Range(startrow, startcolumn, endrow, endcolumn);
                    if (affectedFields.Any(x => x.IsOccupied))
                    {
                        isOpen = true;
                        continue;
                    }

                    foreach (var field in affectedFields)
                    {
                        field.Ship = ship;
                        field.FieldType = FieldType.Ship;
                    }
                    isOpen = false;
                }
            }
        }

        public Coordinates FireShot()
        {
            var hitNeighbors = EnemyBoard.GetHitNeighbors();
            Coordinates coords;
            if (hitNeighbors.Any())
            {
                coords = DamagedShipShot();
            }
            else
            {
                coords = RandomShot();
            }
            return coords;
        }

        private Coordinates RandomShot()
        {
            var availableFields = EnemyBoard.GetOpenRandomFields();
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            var fieldID = rand.Next(availableFields.Count);
            return availableFields[fieldID];
        }

        private Coordinates DamagedShipShot()
        {
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            var hitNeighbors = EnemyBoard.GetHitNeighbors();
            var neighborID = rand.Next(hitNeighbors.Count);
            return hitNeighbors[neighborID];
        }

        public ShotResult ProcessShot(Coordinates coords)
        {
            var field = GameBoard.Fields.At(coords.Row, coords.Column);

            if (!field.IsOccupied)
            {
                field.FieldType = FieldType.Miss;
                return ShotResult.Miss;
            }

            var ship = field.Ship;

            if (field.FieldType == FieldType.Ship) // so we dont count twice
                ship.Hits++;
            field.FieldType = FieldType.Hit;
            if (ship.IsSunk)
            {
                return ShotResult.Sunk;
            }

            return ShotResult.Hit;
        }

        public void ProcessShotResult(Coordinates coords, ShotResult result)
        {
            var field = EnemyBoard.Fields.At(coords.Row, coords.Column);
            switch (result)
            {
                case ShotResult.Hit:
                    field.FieldType = FieldType.Hit;
                    break;
                case ShotResult.Sunk:
                    field.FieldType = FieldType.Hit;
                    break;
                default:
                    field.FieldType = FieldType.Miss;
                    break;
            }
        }
    }
}
