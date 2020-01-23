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
        //public List<IGameObject> ManagedObjects;
        //public Pacman pacman;
        //public BaseGameObject maze;
        public List<IGameObject> CollectionOfAllObjects;

        IGameObject mazeBlue;
        IGameObject mazeWhite;

        public int eatTimer;
        public bool isEatTimerOn;

        public bool isPacmanEatBigCoin = false;
       

        public Master()
        {
            eatTimer = 0;

            isEatTimerOn = false;

            IsEnabled = true;

            Name = "Master";

            CollectionOfAllObjects = new List<IGameObject>();

            InitAllObjects();           //init all objects from collecion

            

        }

        public void InitAllObjects()
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
            mazeBlue = CollectionOfAllObjects.Where(x => x.Name == "MazeBlue").Select(x => x).FirstOrDefault();
            mazeWhite = CollectionOfAllObjects.Where(x => x.Name == "MazeWhite").Select(x => x).FirstOrDefault();

            if (isPacmanEatBigCoin)
            {
                isEatTimerOn = true;


                mazeBlue.IsEnabled = false;
                mazeWhite.IsEnabled = true;

                isPacmanEatBigCoin = false;
            }

            if (isEatTimerOn)
            {
                eatTimer += 1;
            }

            if (eatTimer == 300)
            {
                isEatTimerOn = false;
                eatTimer = 0;
                mazeBlue.IsEnabled = true;
                mazeWhite.IsEnabled = false;
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
