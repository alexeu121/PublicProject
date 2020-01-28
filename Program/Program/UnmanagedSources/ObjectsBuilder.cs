using System;
using System.Linq;
using System.Configuration;
using PacmanEngine.Components.Actors;
using PacmanEngine.Components.Base;
using PacmanEngine.Components.Graphics;
using System.Collections.Generic;
using Program.ManagedObjects.Protagonists;
using Program.ManagedObjects.Antagonists;
using Program.UnmanagedSources;
using Program.Workspace;

namespace Program
{
    public static class ObjectsBuilder
    {
        public static string mazeData;

        public static List<BaseGameObject> PrepareObjects()
        {
            mazeData = ConfigurationManager.AppSettings["MazeData"];

            var objects = new List<BaseGameObject>() { new BaseGameObject(0, 0, ObjectsNames.Background, AnimationType.MazeBlue) };

            foreach (var square in mazeData.Split(' ').SelectMany((row, y) => row.Select((ch, x) => new { ch, x, y })))
                InitSquare(square.ch, square.x, square.y, objects);

            Master master = new Master(objects);
            master.Initialize(objects);

            return objects;
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
                case '5':
                    gameObjects.Add(new BlinkyObject(x, y));
                    break;
                case '6':
                    gameObjects.Add(new PinkyObject(x, y));
                    break;
                case '7':
                    gameObjects.Add(new InkyObject(x, y));
                    break;
                case '8':
                    gameObjects.Add(new ClydeObject(x, y));
                    break;
            }

            if (squareType != '0')
                PathFinder.Grid[x, y] = true;
        }

        #region old
        //public static List<IGameObject> CreateInitData()
        //{
        //    Grid = new bool[21, 27];        //grid for walls, data from App.Config file

        //    mazeData = ConfigurationManager.AppSettings["MazeData"];

        //    InitData = mazeData.Split(' ').                                 //deserialization
        //    Select(x => x.Select(y => int.Parse(y.ToString())).ToArray()).
        //    Select((arr, Y) =>
        //    arr.Select((Number, X) => new PointData() { coord = new Coordinate(X * Coordinate.Multiplier, Y * Coordinate.Multiplier), InitData = (InitialData)Number }
        //    )).
        //    SelectMany(x => x).ToArray();

        //    foreach (var dat in InitData)       //find where is a walls of maze
        //    {
        //        Grid[(int)dat.coord.X / Coordinate.Multiplier, (int)dat.coord.Y / Coordinate.Multiplier] = dat.InitData != InitialData.Wall;    //true - road, false - wall
        //    }

        //    objectCol = new List<IGameObject>();

        //    objectCol.Add(BaseGameObject.CreateStaticObject(AnimationType.MazeBlue, 0, 0));

        //    BaseGameObject mazeWhite = BaseGameObject.CreateStaticObject(AnimationType.MazeWhite, 0, 0);
        //    mazeWhite.IsEnabled = false;
        //    objectCol.Add(mazeWhite);

        //    objectCol.AddRange(InitData.Select(CreateObject).Where(x => x != null));


        //    return objectCol;
        //}

        //private static BaseGameObject CreateObject(PointData pt)
        //{
        //    BaseGameObject result = null;

        //    switch (pt.InitData)
        //    {
        //        case InitialData.Pacman:
        //            result = new Pacman();
        //            result.Animation.Location = pt.coord;
        //            break;
        //        case InitialData.Blinky:
        //            result = new Ghost(ObjectsNames.Blinky);
        //            result.Animation.Location = pt.coord;
        //            break;
        //        case InitialData.Pinky:
        //            result = new Ghost(ObjectsNames.Pinky);
        //            result.Animation.Location = pt.coord;
        //            break;
        //        case InitialData.Inky:
        //            result = new Ghost(ObjectsNames.Inky);
        //            result.Animation.Location = pt.coord;
        //            break;
        //        case InitialData.Clyde:
        //            result = new Ghost(ObjectsNames.Clyde);
        //            result.Animation.Location = pt.coord;
        //            break;
        //        case InitialData.BigCoin:
        //            result = BaseGameObject.CreateStaticObject(AnimationType.BigCoin, pt.coord.X, pt.coord.Y);
        //            break;
        //        case InitialData.SmallCoin:
        //            result = BaseGameObject.CreateStaticObject(AnimationType.SmallCoin, pt.coord.X, pt.coord.Y);
        //            break;

        //    }
        //    return result;
        //}
        #endregion

    }
}
