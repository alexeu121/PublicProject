using System.Collections.Generic;
using PacmanEngine.Components.Actors;
using PacmanEngine.Components.Base;
using PacmanEngine.Components.Graphics;
using Program.UnmanagedSources;

namespace Program.ManagedObjects.Antagonists
{
    class Ghost : BaseGameObject
    {
        private enum Direction { up, down, left, right}
        private enum ShostState { Regular, BlueGhost, Eyes }

        public float Speed { get; set; } = Coordinate.Multiplier / 10;
        private const int RegularGhostSpeed = Coordinate.Multiplier / 8;
        private const int BlueGhostSpeed = Coordinate.Multiplier / 10;
        private const int EyesSpeed = Coordinate.Multiplier / 10;

        protected Coordinate step;

        public Coordinate pacmanCoord;


        //protected Animation GetAnimation();

        protected Coordinate GetTargetCoordinate(Coordinate pacmanLocation) { return pacmanCoord;  }   //!!!!



        public Ghost(int x, int y, string name, AnimationType? animationType) :base(x, y, name, animationType)
        {
            pacmanCoord = Master.Instance.PacmanLocation;
        }


        //public Ghost(string inpName)
        //{
        //    IsEnabled = true;

        //    switch (inpName)
        //    {
        //        case ObjectsNames.Blinky:
        //            Name = ObjectsNames.Blinky;
        //            Animation = AnimationFactory.CreateAnimation(AnimationType.BlinkyRight);
        //            Speed = 70000;
        //            break;
        //        case ObjectsNames.Pinky:
        //            Name = ObjectsNames.Pinky;
        //            Animation = AnimationFactory.CreateAnimation(AnimationType.PinkyRight);
        //            break;
        //        case ObjectsNames.Inky:
        //            Name = ObjectsNames.Inky;
        //            Animation = AnimationFactory.CreateAnimation(AnimationType.InkyRight);
        //            Speed = 80000;
        //            break;
        //        case ObjectsNames.Clyde:
        //            Name = ObjectsNames.Clyde;
        //            Animation = AnimationFactory.CreateAnimation(AnimationType.ClydeRight);
        //            break;
        //    }
        //}




        public void Collide(IEnumerable<IGameObject> collisions)
        {
            //foreach (var obj in collisions)
            //    obj.IsEnabled = false;

        }

        public override void Update()
        {
            Animation.Location += step;

            if (Animation.Location.isRoundAll())
            {
                var target = GetTargetCoordinate(Master.Instance.PacmanLocation);

                Coordinate[] path  = PathFinder.GetPath(Animation.Location, target);

                if (path != null && path.Length > 1)
                {
                    var moveVector = path[1] - Animation.Location;

                    Direction newDirection =
                        moveVector.X > 0 ? Direction.right :
                        moveVector.X < 0 ? Direction.left :
                        moveVector.Y > 0 ? Direction.down : Direction.up;

                }

            }
        }
    }
}
