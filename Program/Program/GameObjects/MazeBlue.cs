using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PacmanEngine.Components.Actors;
using PacmanEngine.Components.Base;
using PacmanEngine.Components.Graphics;

namespace Program.GameObjects
{
    class MazeBlue : BaseGameObject
    {
        public MazeBlue()
        {
            Name = "MazeBlue";

            IsEnabled = true;

            Animation = AnimationFactory.CreateAnimation(AnimationType.MazeBlue);

            Animation.Location = new Coordinate(0, 0);    //стартовая позиция поля
        }

        public override void Update()
        {

        }
    }
}
