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
        public Master()
        { }

        public List<IGameObject> WorkObjectsCollection = new List<IGameObject>();

        public virtual void Update()
        {

        }


    }
}
