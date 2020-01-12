using System.Collections.Generic;
using PacmanEngine.Components.Actors;
using PacmanEngine.Components.Graphics;

namespace Program.GameObjects
{
    class Ghost : BaseGameObject
    {

        public float Speed { get; set; } = 100000;

        public Ghost(string inpName)
        {
            IsEnabled = true;

            switch (inpName)
                {
                    case "Blinky":
                        Name = "Blinky";
                        Animation = AnimationFactory.CreateAnimation(AnimationType.BlinkyRight);
                        Speed = 70000;
                        break;
                    case "Pinky":
                        Name = "Pinky";
                        Animation = AnimationFactory.CreateAnimation(AnimationType.PinkyRight);
                        break;
                    case "Inky":
                        Name = "Inky";
                        Animation = AnimationFactory.CreateAnimation(AnimationType.InkyRight);
                        Speed = 80000;
                        break;
                    case "Clyde":
                        Name = "Clyde";
                        Animation = AnimationFactory.CreateAnimation(AnimationType.ClydeRight);
                        break;
                }
        }

        public void Collide(IEnumerable<IGameObject> collisions)
        {
            //foreach (var obj in collisions)
            //    obj.IsEnabled = false;

        }

        public virtual void Update()
        {

        }
    }
}
