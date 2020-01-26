using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using PacmanEngine.Components.Actors;
using PacmanEngine.Components.Base;
using PacmanEngine.Components.Graphics;
using Program.ManagedObjects.Antagonists;
using Program.ManagedObjects.Protagonists;
using Program.UnmanagedSources;

namespace Program.Workspace
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

    public class PointData
    {
        public Coordinate coord { get; set; }

        public InitialData InitData { get; set; }

    }

    class Program
    {
        public static string mazeData;
        static bool[,] Grid = new bool[21, 27];        //grid for walls, data from App.Config file

        static void Main(string[] args)
        {

            mazeData = ConfigurationManager.AppSettings["MazeData"];

            var objects = new List<BaseGameObject>() { new BaseGameObject(0, 0, ObjectsNames.MazeBlue, AnimationType.MazeBlue) };

            //Master master = new Master(objects);

            foreach (var square in mazeData.Split(' ').SelectMany((row, y) => row.Select((ch, x) => new { ch, x, y })))
                InitSquare(square.ch, square.x, square.y, objects);

            //var path = PathFinder.GetPath(new Coordinate(5000000, 13000000), new Coordinate(15000000, 13000000));

            Engine.Run(objects);
        }


        public static void InitSquare(char squareType, int x, int y, List<BaseGameObject> gameObjects)
        {
            //Wall = 0, Empty = 1, SmallCoin = 2, BigCoin = 3, Pacman = 4, Blinky = 5, Pinky = 6, Inky = 7, Clyde = 8
            switch (squareType)
            {
                case '2':
                    gameObjects.Add(new BaseGameObject(x, y, ObjectsNames.SmallCoin, AnimationType.SmallCoin));
                    break;
                case '3':
                    gameObjects.Add(new BaseGameObject(x, y, ObjectsNames.BigCoin, AnimationType.BigCoin));
                    break;
                case '4':
                    gameObjects.Add(new Pacman(x, y));
                    break;
                //case '5':
                //    gameObjects.Add(new BaseGameObject(x, y, ObjectsNames.Blinky, AnimationType.BlinkyRight));
                //    break;
                //case '6':
                //    gameObjects.Add(new BaseGameObject(x, y, ObjectsNames.Pinky, AnimationType.PinkyRight));
                //    break;
                //case '7':
                //    gameObjects.Add(new BaseGameObject(x, y, ObjectsNames.Inky, AnimationType.InkyRight));
                //    break;
                //case '8':
                //    gameObjects.Add(new BaseGameObject(x, y, ObjectsNames.Clyde, AnimationType.ClydeRight));
                //    break;
            }

            if (squareType != '0')
                PathFinder.Grid[x, y] = true;
        }

        //public static void RunGame()
        //{
        //    List<IGameObject> Collection = ObjectsBuilder.CreateInitData();

        //    //refs=====================
        //    Pacman pacman = Collection.OfType<Pacman>().FirstOrDefault();
        //    Master master = new Master(Collection);
        //    if (pacman != null) pacman.master = master;          

        //    Collection.Add(master);
        //    master.Initialize(Collection);
        //    //=========================
        //    //var path = PathFinder.GetPath(new Coordinate(5000000, 13000000), new Coordinate(15000000, 13000000));
        //    Engine.Run(Collection);       //when load, transmit collection of objects for show and processing



        //}

    }
}
