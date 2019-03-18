using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Battleship.Domain
{
    public class Field
    {
        public FieldType FieldType { get; set; }
        public Coordinates Coordinates { get; set; }
        public Ship Ship { get; set; }
        public Field(int row, int column)
        {
            Coordinates = new Coordinates(row, column);
            FieldType = FieldType.Empty;
        }

        public bool IsOccupied
        {
            get
            {
                return FieldType == FieldType.Ship
                    || FieldType == FieldType.Hit;
            }
        }
    }
}
