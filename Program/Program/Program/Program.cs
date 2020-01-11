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
        public InitialData InitData { get; set; }   //properties for initial objects

    }

    class Program
    {
        static void Main(string[] args)
        {
            Engine.Run(InitCollection());           //when load, transmit collection of objects for show and processing
        }
        private static IEnumerable<IGameObject> InitCollection()
        {

            var mazeData = ConfigurationManager.AppSettings["MazeData"];

            PointData[] InitData = mazeData.Split(' ').                                 //deserialization
                Select(x => x.Select(y => int.Parse(y.ToString())).ToArray()).
                Select((arr, Y) =>
                arr.Select((Number, X) => new PointData() { coord = new Coordinate(X, Y), InitData = (InitialData)Number }
                )).
                SelectMany(x => x).ToArray();

            var Grid = new bool[21, 27];        //grid for data from App.Config file

            foreach (var dat in InitData)       //find where is a walls of maze
            {
                Grid[(int)dat.coord.X, (int)dat.coord.Y] = dat.InitData != InitialData.Wall;
            }

            List<IGameObject> objectCol = new List<IGameObject>();


            objectCol.Add(BaseGameObject.CreateStaticObject(AnimationType.MazeBlue, 0, 0));
            objectCol.AddRange(InitData.Select(CreateObject).Where(x => x != null));


            return objectCol;
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
                case InitialData.Blinky:
                    result = new Ghost("Blinky");
                    result.Animation.Location = pt.coord;
                    break;
                case InitialData.Pinky:
                    result = new Ghost("Pinky");
                    result.Animation.Location = pt.coord;
                    break;
                case InitialData.Inky:
                    result = new Ghost("Inky");
                    result.Animation.Location = pt.coord;
                    break;
                case InitialData.Clyde:
                    result = new Ghost("Clyde");
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
