using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PacmanEngine.Components.Base;

namespace Program
{
    public static class CoordinateExtension
    {
        public static bool isRoundX(this Coordinate coord) => coord.X % Coordinate.Multiplier == 0;
        public static bool isRoundY(this Coordinate coord) => coord.Y % Coordinate.Multiplier == 0;
        public static bool isRoundAll(this Coordinate coord) => isRoundX(coord) && isRoundY(coord);
    }
}
