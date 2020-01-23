using System.Collections.Generic;
using System.Linq;

namespace Program.UnmanagedSources
{
    class Square
    {
        public int X, Y;
        public Square PrewSquare;
        public bool Start = false, Finish = false, Passable = true;
        public List<Square> Neighbours = new List<Square>();

        public Square[] GetAndMark()
        {
            return
                 Neighbours.
                 Where(x => x.Passable && x.PrewSquare == null).
                 Select(x => { x.PrewSquare = this; return x; }).
                 ToArray();
        }

        public void Clear()
        {
            PrewSquare = null;

            Neighbours.
                Where(x => x.PrewSquare != null).
                ToList().
                ForEach(x => x.Clear());
        }

        public static void MakeWave(params Square[] currentWave)
        {

            var nextWave = currentWave.SelectMany(x => x.GetAndMark()).ToArray();

            if (!nextWave.Any() || nextWave.Any(x => x.Start))
                return;
            else
                MakeWave(nextWave);
        }

        public List<Square> GetPath(List<Square> path = null)
        {
            if (path == null)
                path = new List<Square>();

            if (PrewSquare != null)
            {
                path.Add(this);
                return PrewSquare.GetPath(path);
            }

            return path;
        }

    }
}
