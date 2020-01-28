using System;
using System.Collections.Generic;
using PacmanEngine.Components.Actors;
using PacmanEngine.Components.Base;
using PacmanEngine.Components.Graphics;
using Program.UnmanagedSources;

namespace Program.ManagedObjects.Antagonists
{
    abstract class Ghost : BaseGameObject
    {
        protected enum Direction { up, down, left, right }
        public enum GhostState { Regular, BlueGhost, Eyes }

 
        private const int RegularGhostSpeed = Coordinate.Multiplier / 20;
        private const int BlueGhostSpeed = Coordinate.Multiplier / 20;
        private const int EyesSpeed = Coordinate.Multiplier / 8;

        protected Coordinate step;

        protected Direction currentDirection = Direction.down;

        public GhostState currentState { get; set; } = GhostState.Regular;

        protected List<Coordinate> GhostsHome = new List<Coordinate>();

        protected abstract Animation GetAnimation();

        protected abstract Coordinate GetTargetCoordinate(Coordinate pacmanLocation);
        protected abstract Coordinate GetTargetCoordinateInky(Coordinate pacmanLocation, Coordinate blinkyLocation);

        public Ghost(int x, int y, string name, AnimationType? animationType) : base(x, y, name, animationType)
        {
            GhostsHome.Add(new Coordinate(9 * Coordinate.Multiplier, 12 * Coordinate.Multiplier));
            GhostsHome.Add(new Coordinate(10 * Coordinate.Multiplier, 12 * Coordinate.Multiplier));
            GhostsHome.Add(new Coordinate(11 * Coordinate.Multiplier, 12 * Coordinate.Multiplier));
            GhostsHome.Add(new Coordinate(9 * Coordinate.Multiplier, 13 * Coordinate.Multiplier));
            GhostsHome.Add(new Coordinate(10 * Coordinate.Multiplier, 13 * Coordinate.Multiplier));
            GhostsHome.Add(new Coordinate(11 * Coordinate.Multiplier, 13 * Coordinate.Multiplier));
        }     

        public void Collide(IEnumerable<IGameObject> collisions)
        {
            foreach (var obj in collisions)
                if (obj.Name == ObjectsNames.Pacman)
                { }
        }

        public  void SetBlueState()
        {
            currentState = GhostState.BlueGhost;
        }
        public void SetRegularState()
        {
            currentState = GhostState.Regular;
        }

        public void setGhostToHome()
        {
            currentState = GhostState.Eyes;
        }

        public override void Update()
        {
            if(PathFinder.CanMove(Animation.Location, step))
                Animation.Location += step;

            if (Animation.Location.isRoundAll())
            {
                Coordinate target = new Coordinate(0,0);

                if (currentState == GhostState.Regular)
                {
                    if (Name == ObjectsNames.Inky)
                        target = GetTargetCoordinateInky(Master.Instance.PacmanLocation, Master.Instance.BlinkyLocation);
                    else
                        target = GetTargetCoordinate(Master.Instance.PacmanLocation);

                    if (Name == ObjectsNames.Clyde)
                        target = CheckTargetForClyde(target);
                }
                else
                {
                    if (currentState == GhostState.Eyes)
                    {
                        var Loc = new Coordinate(Animation.Location.X, Animation.Location.Y);
                        if (GhostsHome.Contains(Loc))
                            currentState = GhostState.Regular;

                    }

                    switch(Name)
                    {
                        case ObjectsNames.Inky:
                            target = new Coordinate(10 * Coordinate.Multiplier, 13 * Coordinate.Multiplier);
                            break;
                        case ObjectsNames.Blinky:
                            target = new Coordinate(10 * Coordinate.Multiplier, 12 * Coordinate.Multiplier);
                            break;
                        case ObjectsNames.Pinky:
                            target = new Coordinate(9 * Coordinate.Multiplier, 13 * Coordinate.Multiplier);
                            break;
                        case ObjectsNames.Clyde:
                            target = new Coordinate(11 * Coordinate.Multiplier, 13 * Coordinate.Multiplier);
                            break;

                    }
                }

                var path = PathFinder.GetPath(Animation.Location, target);

                if (path != null && path.Length > 1)
                {
                    var moveVector = path[1] - Animation.Location;

                    Direction newDirection =
                        moveVector.X > 0 ? Direction.right :
                        moveVector.X < 0 ? Direction.left :
                        moveVector.Y > 0 ? Direction.down : Direction.up;

                    if (newDirection != currentDirection)
                    {
                        currentDirection = newDirection;
                        step = GetStep();

                        var currentPosition = Animation.Location;
                        Animation = GetAnimation();
                        Animation.Location = currentPosition;
                    }
                   
                }
            }
        }

        public Coordinate GetStep()
        { 
            int currentSpeed;
            switch (currentState)
            {
                case GhostState.BlueGhost:
                    currentSpeed = BlueGhostSpeed;
                    break;
                case GhostState.Eyes:
                    currentSpeed = EyesSpeed;
                    break;
                case GhostState.Regular:
                    currentSpeed = RegularGhostSpeed;
                    break;
                default:
                    throw new Exception("unknown state");
            }

            switch (currentDirection)
            {
                case Direction.up:
                    return new Coordinate(0, -currentSpeed);
                case Direction.down:
                    return new Coordinate(0, currentSpeed);
                case Direction.left:
                    return new Coordinate(-currentSpeed, 0);
                case Direction.right:
                    return new Coordinate(currentSpeed, 0);
            }
            throw new Exception("");
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

        public Coordinate CheckTargetForClyde(Coordinate target)
        {
            Coordinate result;

            if ((Math.Abs(Animation.Location.X - target.X) < 8 * Coordinate.Multiplier) && (Math.Abs(Animation.Location.Y - target.Y) < 8 * Coordinate.Multiplier))
            {
                result = new Coordinate(Coordinate.Multiplier, Coordinate.Multiplier * 25);
            }
            else
            {
                result = target;
            }
            return result;
        }
    }
}
