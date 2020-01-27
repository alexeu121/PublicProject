using System;
using System.Collections.Generic;
using PacmanEngine.Components.Actors;
using PacmanEngine.Components.Base;
using PacmanEngine.Components.Graphics;
using Program.UnmanagedSources;

namespace Program.ManagedObjects.Antagonists
{
    class Ghost : BaseGameObject
    {
        private enum Direction { up, down, left, right }
        private enum ShostState { Regular, BlueGhost, Eyes }

        public float Speed { get; set; } = Coordinate.Multiplier / 10;
        private const int RegularGhostSpeed = Coordinate.Multiplier / 10;
        private const int BlueGhostSpeed = Coordinate.Multiplier / 10;
        private const int EyesSpeed = Coordinate.Multiplier / 10;
        private readonly int speed = RegularGhostSpeed;


        protected Coordinate step;

        public Coordinate pacmanCoord;
        private bool alive;
        private static List<Animation> changedAnimations;

        //protected Animation GetAnimation() { }

        protected Coordinate GetTargetCoordinate(Coordinate pacmanLocation) { return pacmanCoord; }
       

        public Ghost(int x, int y, string name, AnimationType? animationType) : base(x, y, name, animationType)
        {
            pacmanCoord = new Coordinate(10 * Coordinate.Multiplier, 20 * Coordinate.Multiplier);//Master.Instance.PacmanLocation;
        }


        public void Collide(IEnumerable<IGameObject> collisions)
        {
            foreach (var obj in collisions)
                if (obj.Name == ObjectsNames.Pacman)
                { }
        }

        public override void Update()
        {
            if (Animation.Location.isRoundAll())
            {

                var target = GetTargetCoordinate(Master.Instance.PacmanLocation);

                var path = PathFinder.GetPath(Animation.Location, target);

                if (path != null && path.Length > 1)
                {
                    var moveVector = path[1] - Animation.Location;

                    Direction newDirection =
                        moveVector.X > 0 ? Direction.right :
                        moveVector.X < 0 ? Direction.left :
                        moveVector.Y > 0 ? Direction.down : Direction.up;

                    Coordinate newStep, position = Animation.Location;

                    if (newDirection == Direction.right)
                    {
                        newStep = new Coordinate(speed, 0);
                        if (PathFinder.CanMove(position, newStep))
                        {
                            step = newStep;
                            Animation = AnimationFactory.CreateAnimation(GetAnimation(newDirection, Name));
                            Animation.Location = position;
                        }
                    }
                    else if (newDirection == Direction.left)
                    {
                        newStep = new Coordinate(-speed, 0);
                        if (PathFinder.CanMove(position, newStep))
                        {
                            step = newStep;
                            Animation = AnimationFactory.CreateAnimation(GetAnimation(newDirection, Name));
                            Animation.Location = position;
                        }
                    }
                    else if (newDirection == Direction.down)
                    {
                        newStep = new Coordinate(0, speed);
                        if (PathFinder.CanMove(position, newStep))
                        {
                            step = newStep;
                            Animation = AnimationFactory.CreateAnimation(GetAnimation(newDirection, Name));
                            Animation.Location = position;
                        }
                    }
                    else if (newDirection == Direction.up)
                    {
                        newStep = new Coordinate(0, -speed);
                        if (PathFinder.CanMove(position, newStep))
                        {
                            step = newStep;
                            Animation = AnimationFactory.CreateAnimation(GetAnimation(newDirection, Name));
                            Animation.Location = position;
                        }
                    }

                    Animation.Location += step;

                    if (Animation.Location.X >= Coordinate.WorldWidth)
                        Animation.Location = new Coordinate(0, Animation.Location.Y);
                    else if (Animation.Location.X < 0)
                        Animation.Location = new Coordinate(Coordinate.WorldWidth, Animation.Location.Y);
                }
            }
            else
            {
                
                Animation.Location += step;

            }
        }

        private AnimationType GetAnimation(Direction direction, string Name)
        {
            AnimationType res = AnimationType.PacmanRight;

            switch (direction)
            {
                case Direction.down:
                    if (Name == ObjectsNames.Pinky)
                    { res = AnimationType.PinkyDown; }
                    else if (Name == ObjectsNames.Inky)
                    { res = AnimationType.InkyDown; }
                    else if (Name == ObjectsNames.Blinky)
                    { res = AnimationType.BlinkyDown; }
                    else if (Name == ObjectsNames.Clyde)
                    { res = AnimationType.ClydeDown; }
                    break;
                case Direction.up:
                    if (Name == ObjectsNames.Pinky)
                    { res = AnimationType.PinkyUp; }
                    else if (Name == ObjectsNames.Inky)
                    { res = AnimationType.InkyUp; }
                    else if (Name == ObjectsNames.Blinky)
                    { res = AnimationType.BlinkyUp; }
                    else if (Name == ObjectsNames.Clyde)
                    { res = AnimationType.ClydeUp; }
                    break;
                case Direction.left:
                    if (Name == ObjectsNames.Pinky)
                    { res = AnimationType.PinkyLeft; }
                    else if (Name == ObjectsNames.Inky)
                    { res = AnimationType.InkyLeft; }
                    else if (Name == ObjectsNames.Blinky)
                    { res = AnimationType.BlinkyLeft; }
                    else if (Name == ObjectsNames.Clyde)
                    { res = AnimationType.ClydeLeft; }
                    break;
                case Direction.right:
                    if (Name == ObjectsNames.Pinky)
                    { res = AnimationType.PinkyRight; }
                    else if (Name == ObjectsNames.Inky)
                    { res = AnimationType.InkyRight; }
                    else if (Name == ObjectsNames.Blinky)
                    { res = AnimationType.BlinkyRight; }
                    else if (Name == ObjectsNames.Clyde)
                    { res = AnimationType.ClydeRight; }
                    break;
            }
            return res;
        }
    }
}
