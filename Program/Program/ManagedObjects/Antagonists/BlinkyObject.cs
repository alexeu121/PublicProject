using PacmanEngine.Components.Base;
using PacmanEngine.Components.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program.ManagedObjects.Antagonists
{
    class BlinkyObject : Ghost
    {
        public BlinkyObject(int x, int y) : base(x, y, ObjectsNames.Blinky, AnimationType.BlinkyDown) { }

        protected override Animation GetAnimation()
        {
            AnimationType animationType = AnimationType.BlueGhost;

            if (currentState == GhostState.Regular)
            {
                switch (currentDirection)
                {
                    case Direction.up:
                        animationType = AnimationType.BlinkyUp;
                        break;
                    case Direction.down:
                        animationType = AnimationType.BlinkyDown;
                        break;
                    case Direction.left:
                        animationType = AnimationType.BlinkyLeft;
                        break;
                    case Direction.right:
                        animationType = AnimationType.BlinkyRight;
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
            var x = pacmanLocation.X / Coordinate.Multiplier;
            var y = pacmanLocation.Y / Coordinate.Multiplier;

            return new Coordinate(x * Coordinate.Multiplier, y * Coordinate.Multiplier);
        }

        protected override Coordinate GetTargetCoordinate2(Coordinate pacmanLocation, Coordinate blinkyLocation)
        {
            return new Coordinate(0, 0);
        }
    }
}
