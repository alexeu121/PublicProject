using PacmanEngine.Components.Base;
using PacmanEngine.Components.Graphics;
using Program.UnmanagedSources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program.ManagedObjects.Antagonists
{
    class PinkyObject : Ghost
    {
        public PinkyObject(int x, int y) : base(x, y, ObjectsNames.Pinky, AnimationType.PinkyDown) { }

        protected override Animation GetAnimation()
        {
            AnimationType animationType = AnimationType.BlueGhost;

            if (currentState == GhostState.Regular)
            {
                switch (currentDirection)
                {
                    case Direction.up:
                        animationType = AnimationType.PinkyUp;
                        break;
                    case Direction.down:
                        animationType = AnimationType.PinkyDown;
                        break;
                    case Direction.left:
                        animationType = AnimationType.PinkyLeft;
                        break;
                    case Direction.right:
                        animationType = AnimationType.PinkyRight;
                        break;
                }
            }
            else if (currentState == GhostState.Eyes)
            {
                switch (currentDirection)
                {
                    case Direction.up:
                        animationType = AnimationType.EyesUp;
                        break;
                    case Direction.down:
                        animationType = AnimationType.EyesDown;
                        break;
                    case Direction.left:
                        animationType = AnimationType.EyesRight;
                        break;
                    case Direction.right:
                        animationType = AnimationType.EyesRight;
                        break;
                }
            }

            return AnimationFactory.CreateAnimation(animationType);

        }

        protected override Coordinate GetTargetCoordinate(Coordinate pacmanLocation)
        {
            var pacmanDirection = Master.Instance.PacmanDirection;

            Coordinate target = new Coordinate(
                (Coordinate.WorldWidth + (pacmanLocation.X + pacmanDirection.X * 4)) / Coordinate.WorldWidth,
                ((Coordinate.WorldHeight + (pacmanLocation.Y + pacmanDirection.Y * 4)) / Coordinate.WorldHeight));

          
            if (PathFinder.isSquareEmpty(target))
            {
                return target;
            }
            else
            {
                return pacmanLocation;
            }
        }
        protected override Coordinate GetTargetCoordinate2(Coordinate pacmanLocation, Coordinate blinkyLocation)
        {
            return new Coordinate(0, 0);
        }

    }
}
