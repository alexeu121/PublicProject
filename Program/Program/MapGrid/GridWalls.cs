using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Program.GameObjects;
using System.Collections.Generic;
using PacmanEngine.Components.Actors;
using PacmanEngine.Components.Base;
using PacmanEngine.Components.Graphics;

namespace Program.MapGrid
{
    public static class GridWalls
    {
        public static bool[,] Grid;

        static GridWalls()
        {
            //Grid = new bool[21, 27];        //grid for walls, data from App.Config file

            //foreach (var dat in InitData)       //find where is a walls of maze
            //{
            //    Grid[(int)dat.coord.X / Coordinate.Multiplier, (int)dat.coord.Y / Coordinate.Multiplier] = dat.InitData != InitialData.Wall;    //true - road, false - wall
            //}
        }

        static 
}
}
