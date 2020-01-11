using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using PacmanEngine.Components.Actors;
using PacmanEngine.Components.Base;
using PacmanEngine.Components.Graphics;
using Program.GameObjects;

namespace Program
{
    public enum InitialData
    {
        Wall,
        Empty,
        SmallCoin,
        BigCoin,
        Pacman,
        Blinky,
        Pinky,
        Inky,
        Clyde
    }

    class PointData
    {
        public Coordinate coord { get; set; }
        public InitialData InitData { get; set; }   //dannye for init objects

    }

    class Program
    {
        static void Main(string[] args)
        {
            Engine.Run(InitCollection()); //при загрузке передаем обьекты для отображения и обработки
        }
        private static IEnumerable<IGameObject> InitCollection()
        {

            var mazeData = ConfigurationManager.AppSettings["MazeData"];

            PointData[] InitData = mazeData.Split(' '). //deserialize stroki
                Select(x => x.Select(y => int.Parse(y.ToString())).ToArray()).
                Select((arr, Y) =>
                arr.Select((Number, X) => new PointData() { coord = new Coordinate(X, Y), InitData = (InitialData)Number }
                )).
                SelectMany(x => x).ToArray();

            //27 strok s 21 simvolov v kazhdoy

            var Grid = new bool[21, 27];

            foreach (var dat in InitData)
            {
                Grid[(int)dat.coord.X, (int)dat.coord.Y] = dat.InitData != InitialData.Wall;
            }

            var col1 = new List<IGameObject>();

           
            col1.Add(BaseGameObject.CreateStaticObject(AnimationType.MazeBlue, 0, 0));

            col1.AddRange(InitData.Select(CreateObject).Where(x => x != null));
            return col1;
        }

        static BaseGameObject CreateObject(PointData pt)
        {

            BaseGameObject result = null;

            switch (pt.InitData)
            {
                case InitialData.Pacman:
                    result = new Pacman();
                    result.Animation.Location = pt.coord;
                    break;
                case InitialData.BigCoin:
                    BaseGameObject.CreateStaticObject(AnimationType.BigCoin, pt.coord.X, pt.coord.Y);

                    break;
                case InitialData.SmallCoin:
                    BaseGameObject.CreateStaticObject(AnimationType.SmallCoin, pt.coord.X, pt.coord.Y);

                    break;


            }
            return result;
        }
    }
}
