using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Battleship.Domain
{
    public class EnemyBoard: Board
    {
        public IList<Coordinates> GetOpenRandomFields()
        {
            return Fields.Where(x => x.FieldType == FieldType.Empty).Select(x => x.Coordinates).ToList();
        }

        public IList<Coordinates> GetHitNeighbors()
        {
            List<Field> fields = new List<Field>();
            var hits = Fields.Where(x => x.FieldType == FieldType.Hit);
            foreach (var hit in hits)
            {
                fields.AddRange(GetNeighbors(hit.Coordinates).ToList());
            }
            return fields.Distinct().Where(x => x.FieldType == FieldType.Empty).Select(x => x.Coordinates).ToList();
        }

        public ICollection<Field> GetNeighbors(Coordinates coordinates)
        {
            int row = coordinates.Row;
            int column = coordinates.Column;
            List<Field> fields = new List<Field>();
            if (column > 1)
            {
                fields.Add(Fields.At(row, column - 1));
            }
            if (row > 1)
            {
                fields.Add(Fields.At(row - 1, column));
            }
            if (row < 10)
            {
                fields.Add(Fields.At(row + 1, column));
            }
            if (column < 10)
            {
                fields.Add(Fields.At(row, column + 1));
            }
            return fields;
        }

    }
}
