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

        public Master()
        {
            WObjCollection = new List<IGameObject>();
        }

       

        public virtual void Update()
        {
            if (WObjCollection[0].Animation.AnimationType == AnimationType.PacmanDown)
            {
                IGameObject pacman = WObjCollection.Find(item => item.Name == "Pacman");
            }
        }


    }
}
