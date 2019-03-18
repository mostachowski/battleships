using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship.Domain
{
    public enum FieldType
    {
        Empty = 0,
        Ship,
        Hit,
        Miss
    }

    public enum ShotResult
    {
        Miss,
        Hit,
        Sunk
    }
}
