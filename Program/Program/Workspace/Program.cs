using System.Collections.Generic;
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
        static bool[,] Grid = new bool[21, 27];        //grid for walls, data from App.Config file

        static void Main(string[] args)
        {
            RunGame();
        }

        public static void RunGame()
        {
            List<IGameObject> Collection = ObjectsBuilder.CreateInitData();

            //refs=====================
            Pacman pacman = Collection.OfType<Pacman>().FirstOrDefault();
            Master master = new Master(Collection);
            if (pacman != null) pacman.master = master;          
                   
            Collection.Add(master);
            master.Initialize(Collection);  
            //=========================

            Engine.Run(Collection);       //when load, transmit collection of objects for show and processing
        }
              
    }
}
