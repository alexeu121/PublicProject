using System;
using System.Linq;
using System.Configuration;
using PacmanEngine.Components.Actors;
using PacmanEngine.Components.Base;
using PacmanEngine.Components.Graphics;
using Program.WorkSpace;
using System.Collections.Generic;
using Program.ManagedObjects.Protagonists;
using Program.ManagedObjects.Antagonists;

namespace Program.UnmanagedSources
{
    public static class ObjectsBuilder
    {
        public static bool[,] Grid;

        public static string mazeData;

        public static  PointData[] InitData;

        public static List<IGameObject> objectCol;      // create list of work objects

        public static List<IGameObject> CreateInitData()
        {
            Grid = new bool[21, 27];        //grid for walls, data from App.Config file

            mazeData = ConfigurationManager.AppSettings["MazeData"];

            InitData = mazeData.Split(' ').                                 //deserialization
            Select(x => x.Select(y => int.Parse(y.ToString())).ToArray()).
            Select((arr, Y) =>
            arr.Select((Number, X) => new PointData() { coord = new Coordinate(X * Coordinate.Multiplier, Y * Coordinate.Multiplier), InitData = (InitialData)Number }
            )).
            SelectMany(x => x).ToArray();

            foreach (var dat in InitData)       //find where is a walls of maze
            {
                Grid[(int)dat.coord.X / Coordinate.Multiplier, (int)dat.coord.Y / Coordinate.Multiplier] = dat.InitData != InitialData.Wall;    //true - road, false - wall
            }

            objectCol = new List<IGameObject>();

            objectCol.Add(BaseGameObject.CreateStaticObject(AnimationType.MazeBlue, 0, 0));

            BaseGameObject mazeWhite = BaseGameObject.CreateStaticObject(AnimationType.MazeWhite, 0, 0);
            mazeWhite.IsEnabled = false;
            objectCol.Add(mazeWhite);

            objectCol.AddRange(InitData.Select(CreateObject).Where(x => x != null));


            return objectCol;
        }

        private static BaseGameObject CreateObject(PointData pt)
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
                    result = BaseGameObject.CreateStaticObject(AnimationType.BigCoin, pt.coord.X, pt.coord.Y);
                    break;
                case InitialData.SmallCoin:
                    result = BaseGameObject.CreateStaticObject(AnimationType.SmallCoin, pt.coord.X, pt.coord.Y);
                    break;

            }
            return result;
        }

        private static void InitSquare(char squareType, int x, int y, List<BaseGameObject> gameObjects)
        {
            // Wall = 0, Empty = 1, SmallCoin = 2, BigCoin = 3, Pacman = 4, Blinky = 5, Pinky = 6, Inky = 7, Clyde = 8.
            switch (squareType)
            {
                //case '2':
                //    gameObjects.Add(new BaseGameObject(x, y, ObjectNames.Coin, AnimationType.SmallCoin));
                //    break;
                //case '3':
                //    gameObjects.Add(new BaseGameObject(x, y, ObjectNames.BigCoin, AnimationType.BigCoin));
                //    break;
                //case '4':
                //    gameObjects.Add(new BaseGameObjects.Pacman(x, y));
                //    break;
            }

            if (squareType != '0')
                PathFinder.Grid[x, y] = true;
        }

    }
}
