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

        public static Coordinate MessagePosition = new Coordinate(10 * Coordinate.Multiplier, 13 * Coordinate.Multiplier);


        public static Coordinate BlinkyHome = new Coordinate(10 * Coordinate.Multiplier, 12 * Coordinate.Multiplier);
        public static Coordinate PinkyHome = new Coordinate(9 * Coordinate.Multiplier, 13 * Coordinate.Multiplier);
        public static Coordinate InkyHome = new Coordinate(10 * Coordinate.Multiplier, 13 * Coordinate.Multiplier);
        public static Coordinate ClydeHome = new Coordinate(11 * Coordinate.Multiplier, 13 * Coordinate.Multiplier);

    }
}
