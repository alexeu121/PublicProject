using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PacmanEngine.Components.Actors;
using PacmanEngine.Components.Base;
using PacmanEngine.Components.Graphics;
using Program.ManagedObjects.Antagonists;
using Program.ManagedObjects.Protagonists;
using Program.WorkSpace;

namespace Program.UnmanagedSources
{
    class Master : BaseGameObject
    {
        public List<IGameObject> ManagedObjects;
        public Pacman pacman;
        public BaseGameObject maze;
        public List<IGameObject> CollectionOfAllObjects;
        

        public bool isPacmanEatBigCoin = false;
       

        public Master()
        {

            IsEnabled = true;

            Name = "Master";

            CollectionOfAllObjects = new List<IGameObject>();

            InitAllObjects();           //init all objects from collecion

        }

        private void InitAllObjects()
        {


            //master.ManagedObjects.AddRange(CollectionOfAllObjects.Where(x => ((x.Name == "Pacman") ||
            //                                                      (x.Name == "Blinky") ||
            //                                                      (x.Name == "Pinky") ||
            //                                                      (x.Name == "Inky") ||
            //                                                      (x.Name == "Clyde") ||
            //                                                      (x.Name == "MazeBlue") ||
            //                                                      (x.Name == "MazeWhite"))).Select(x => x));

        }

        public override void Update()
        {


            //if (pacman.EatTimerOn && pacman.EatTimer == 600)
            //{
            //    pacman.EatTimerOn = false;
            //    pacman.EatTimer = 0;

            //}


            if (isPacmanEatBigCoin)
            {
                IGameObject mazeBlue = CollectionOfAllObjects.Where(x => x.Name == "MazeBlue").Select(x => x).FirstOrDefault();
                IGameObject mazeWhite = CollectionOfAllObjects.Where(x => x.Name == "MazeWhite").Select(x => x).FirstOrDefault();

                mazeBlue.IsEnabled = false;
                mazeWhite.IsEnabled = true;
                
                isPacmanEatBigCoin = false;
            }

            //foreach (var obj in Pacman_collisions)
            //{
            //    if (obj.Name == "BigCoin")
            //    {
                   
            //    }
            //}
        }


    }
}
