using Battleship.Domain;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Battleship
{
    public class ColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Board board = value as Board;
            if (board is null)
                throw new ArgumentException("ColorConverter is expecting board object!");
            Point? point = parameter as Point?;
            if (point is null)
                throw new ArgumentException("ColorConverter is expecting point as a parameter!");

            var field = board.Fields.Where(x => x.Coordinates.Column == point.Value.X && x.Coordinates.Row == point.Value.Y).First();

            if (field.FieldType == FieldType.Miss)
                return "Gray";

            if (field.FieldType == FieldType.Hit)
                return "Red";

            if (!field.IsOccupied)
                return "AliceBlue";

            return "Black";

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
