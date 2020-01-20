using System.Collections.Generic;
using System.Linq;
using PacmanEngine.Components.Actors;
using PacmanEngine.Components.Base;
using PacmanEngine.Components.Graphics;
using Program.ManagedObjects.Antagonists;
using Program.ManagedObjects.Protagonists;
using Program.UnmanagedObjects;

namespace Program.WorkSpace
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

        public InitialData InitData { get; set; }   //properties for initial objects

    }

    class Program
    {
        static bool[,] Grid = new bool[21, 27];        //grid for walls, data from App.Config file

        static void Main(string[] args)
        {

            List<IGameObject> Collection = new List<IGameObject>();

            Collection.AddRange(CreateInitCollection());

            Pacman pacman = Collection.OfType<Pacman>().FirstOrDefault();

            Master master = new Master();

            master.WObjCollection.AddRange(Collection.Where(x => ((x.Name == "Pacman") ||
                                                                  (x.Name == "Blinky") || 
                                                                  (x.Name == "Pinky") || 
                                                                  (x.Name == "Inky") || 
                                                                  (x.Name == "Clyde") ||
                                                                  (x.Name == "MazeBlue"))).Select(x=> x));
            
            
            master.Name = "Master";
            if (pacman != null) pacman.MasterObj = master;

            Collection.Add(master);

            Engine.Run(Collection);       //when load, transmit collection of objects for show and processing
        }
        private static IEnumerable<IGameObject> CreateInitCollection()
        {
            //create grid with coord's
            GridWalls.CreateInitData();

            List<IGameObject> objectCol = new List<IGameObject>();      // create list of work objects

            objectCol.Add(BaseGameObject.CreateStaticObject(AnimationType.MazeBlue, 0, 0));         // create object of Maze

            objectCol.AddRange(GridWalls.InitData.Select(CreateObject).Where(x => x != null));          //create and add moving object

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
                    result = BaseGameObject.CreateStaticObject(AnimationType.BigCoin, pt.coord.X, pt.coord.Y);
                    break;
                case InitialData.SmallCoin:
                    result = BaseGameObject.CreateStaticObject(AnimationType.SmallCoin, pt.coord.X, pt.coord.Y);
                    break;
                
            }
            return result;
        }
    }
}
