using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PacmanEngine.Components.Actors;
using PacmanEngine.Components.Base;
using PacmanEngine.Components.Graphics;
using Program.GameObjects;
using Program.MapGrid;

namespace Program
{
    class Master : BaseGameObject
    {
        public List<IGameObject> WObjCollection;
        public Pacman pacman;
        public BaseGameObject maze;
        
        //public bool isSmallCoinOff;

        public IEnumerable<IGameObject> Pacman_collisions;
       

        public Master()
        {
            WObjCollection = new List<IGameObject>();

            IsEnabled = true;
            
            //pacman = new List<IGameObject>();

        }

    
     

        public void GetAllObjects()
        {
            pacman = WObjCollection.OfType<Pacman>().FirstOrDefault();
        }

        public virtual void Update()
        {
            if (pacman.EatTimerOn && pacman.EatTimer == 600)
            {
                pacman.EatTimerOn = false;
                pacman.EatTimer = 0;

                maze = WObjCollection.OfType<BaseGameObject>().Where(x=>x.Name == "MazeBlue").FirstOrDefault();
                maze.Animation = AnimationFactory.CreateAnimation(AnimationType.MazeWhite);
                
            }




            //foreach (var obj in Pacman_collisions)
            //{
            //    if (obj.Name == "BigCoin")
            //    {
            //        BaseGameObject.CreateStaticObject(AnimationType.MazeWhite, 0, 0);
            //    }
            //}
        }


    }
}
