using System.Collections.Generic;
using System.Linq;
using PacmanEngine.Components.Base;

namespace Program.UnmanagedSources
{
    class PathFinder
    {
        public static bool[,] Grid = new bool[Coordinate.WorldWidth / Coordinate.Multiplier, Coordinate.WorldHeight / Coordinate.Multiplier];

        public static bool isSquareEmpty(Coordinate coordinate)
        {
            var x = ((Coordinate.WorldWidth + coordinate.X) % Coordinate.WorldWidth) / Coordinate.Multiplier;
            var y = ((Coordinate.WorldWidth + coordinate.Y) % Coordinate.WorldHeight) / Coordinate.Multiplier;

            return Grid[x, y];

        }

        public static bool CanMove(Coordinate position, Coordinate direction)
        {
            var roundX = position.X % Coordinate.Multiplier == 0;
            var roundY = position.Y % Coordinate.Multiplier == 0;
            var goX = direction.X != 0;
            var goY = direction.Y != 0;

            if ((roundY && goX && !goY) || (roundX && goY && !goX))
            {
                if ((!roundX && goX) || (!roundY && goY))
                    return true;

                var x = position.X;
                var y = position.Y;

                if (goX)
                    x = (Coordinate.WorldWidth + x + (direction.X > 0 ? Coordinate.Multiplier : -Coordinate.Multiplier)) % Coordinate.WorldWidth;
                else
                    y += direction.Y > 0 ? Coordinate.Multiplier : -Coordinate.Multiplier;

                return Grid[x / Coordinate.Multiplier, y / Coordinate.Multiplier];
            }
            return false;
        }

        private static List<Square> CreateGrid()
        {
            List<Square> squares = new List<Square>();

            var worldWidth = Coordinate.WorldWidth / Coordinate.Multiplier;
            var worldHeight = Coordinate.WorldHeight / Coordinate.Multiplier;

            for (int x = 0; x < worldWidth; x++)
                for (int y = 0; y < worldHeight; y++)
                    squares.Add(new Square
                    {
                        X = x,
                        Y = y,
                        Passable = Grid[x, y]
                    });

            for (int x = 0; x < worldWidth; x++)
                for (int y = 0; y < worldHeight; y++)
                {
                    var sq = squares.First(s => s.X == x && s.Y == y);

                    var neigh = squares.FirstOrDefault(s => s.X == x - 1 && s.Y == y);
                    if (neigh != null)
                        sq.Neighbours.Add(neigh);

                    neigh = squares.FirstOrDefault(s => s.X == x + 1 && s.Y == y);
                    if (neigh != null)
                        sq.Neighbours.Add(neigh);

                    neigh = squares.FirstOrDefault(s => s.X == x && s.Y == y - 1);
                    if (neigh != null)
                        sq.Neighbours.Add(neigh);

                    neigh = squares.FirstOrDefault(s => s.X == x && s.Y == y + 1);
                    if (neigh != null)
                        sq.Neighbours.Add(neigh);
                }

            #region Make tonnel
            var entryExit = squares.Where(sq => (sq.X == 0 || sq.X == (worldWidth - 1)) && sq.Passable).ToArray();

            if (entryExit.Length != 2)
                throw new System.Exception("Unable to initialize grid.");

            entryExit[0].Neighbours.Add(entryExit[1]);
            entryExit[1].Neighbours.Add(entryExit[0]);

            #endregion

            return squares;
        }

        public static Coordinate[] GetPath(Coordinate currentPosition, Coordinate targetPosition)
        {
            if (currentPosition.X / Coordinate.Multiplier == targetPosition.X / Coordinate.Multiplier &&
               currentPosition.Y / Coordinate.Multiplier == targetPosition.Y / Coordinate.Multiplier)
                return null;


            var squares = CreateGrid();

            if (targetPosition.X > Coordinate.WorldWidth - Coordinate.Multiplier)
                targetPosition.X = Coordinate.WorldWidth - Coordinate.Multiplier;
            else if (targetPosition.X < 0)
                targetPosition.X = 0;

            if (targetPosition.Y > Coordinate.WorldHeight - Coordinate.Multiplier)
                targetPosition.Y = Coordinate.WorldHeight - Coordinate.Multiplier; 
            else if (targetPosition.Y < 0)
                targetPosition.Y = 0;

            if (currentPosition.X > Coordinate.WorldWidth - Coordinate.Multiplier)
                currentPosition.X = Coordinate.WorldWidth - Coordinate.Multiplier;
            else if (currentPosition.X < 0)
                currentPosition.X = 0;

            if (currentPosition.Y > Coordinate.WorldHeight - Coordinate.Multiplier)
                currentPosition.Y = Coordinate.WorldHeight - Coordinate.Multiplier;
            else if (currentPosition.Y < 0)
                currentPosition.Y = 0;


            var finish = squares.Single(sq => sq.X == targetPosition.X / Coordinate.Multiplier && sq.Y == targetPosition.Y / Coordinate.Multiplier);
            var start = squares.Single(sq => sq.X == currentPosition.X / Coordinate.Multiplier && sq.Y == currentPosition.Y / Coordinate.Multiplier);

            finish.Finish = true;
            start.Start = true;

            Square.MakeWave(finish);

            finish.PrewSquare = null;

            var path = start.GetPath();
            path.Add(finish);

            return path.Select(sq => new Coordinate(sq.X * Coordinate.Multiplier, sq.Y * Coordinate.Multiplier)).ToArray();
        }

    }
}
