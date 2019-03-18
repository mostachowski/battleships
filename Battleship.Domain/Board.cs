using System.Collections.Generic;

namespace Battleship.Domain
{
    public class Board
    {
        public List<Field> Fields { get; set; }

        public Board()
        {
            Fields = new List<Field>();
            for (int i = 1; i <= 10; i++)
            {
                for (int j = 1; j <= 10; j++)
                {
                    Fields.Add(new Field(i, j));
                }
            }
        }
    }
}
