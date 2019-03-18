using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Battleship.Domain
{

    public static class PanelExtensions
    {
        public static Field At(this ICollection<Field> fields, int row, int column)
        {
            return fields.Where(x => x.Coordinates.Row == row && x.Coordinates.Column == column).First();
        }

        public static IList<Field> Range(this IList<Field> fields, int startRow, int startColumn, int endRow, int endColumn)
        {
            return fields.Where(x => x.Coordinates.Row >= startRow
                                     && x.Coordinates.Column >= startColumn
                                     && x.Coordinates.Row <= endRow
                                     && x.Coordinates.Column <= endColumn).ToList();
        }
    }
}
