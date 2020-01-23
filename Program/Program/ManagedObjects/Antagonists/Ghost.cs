using System.Collections.Generic;
using PacmanEngine.Components.Actors;
using PacmanEngine.Components.Base;
using PacmanEngine.Components.Graphics;
using Program.UnmanagedSources;
using Program.WorkSpace;

namespace Program.ManagedObjects.Antagonists
{
    class Ghost : BaseGameObject
    {
        private enum Direction { up, down, left, right}
        private enum ShostState { Regular, }

        public float Speed { get; set; } = Coordinate.Multiplier / 10;

        protected Coordinate step;

        private const int RegularGhostSpeed = Coordinate.Multiplier / 8;
        private const int BlueGhostSpeed = Coordinate.Multiplier / 10;
        private const int EyesSpeed = Coordinate.Multiplier / 10;

        //public Ghost(int x, int y, string name, AnimationType? animationType) : base(x, y, name, animationType)
        //{

        //}

        //protected abstract Animation GetAnimation();

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

        public override void Update()
        {

        }
    }
}
