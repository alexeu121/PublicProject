using System;
using System.Linq;
using System.Configuration;
using PacmanEngine.Components.Actors;
using PacmanEngine.Components.Base;
using PacmanEngine.Components.Graphics;
using Program.WorkSpace;

namespace Program.UnmanagedObjects
{
    public class GridWalls
    {
        public static bool[,] Grid;

        public static string mazeData;

        public static  PointData[] InitData;
        
        public static void CreateInitData()
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
        }

    }
}
