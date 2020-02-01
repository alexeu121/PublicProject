using System;
using PacmanEngine.Components.Base;
using PacmanEngine.Components.Graphics;
using Program.UnmanagedSources;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program.ManagedObjects.Antagonists
{
    class ClydeObject : Ghost
    {
        public ClydeObject(int x, int y) : base(x, y, ObjectsNames.Clyde, AnimationType.ClydeDown) { }
     

       
        protected override Animation GetAnimation()
        {
            AnimationType animationType = AnimationType.BlueGhost;

            if (currentState == GhostState.Regular)
            {
                switch (currentDirection)
                {
                    case Direction.up:
                        animationType = AnimationType.ClydeUp;
                        break;
                    case Direction.down:
                        animationType = AnimationType.ClydeDown;
                        break;
                    case Direction.left:
                        animationType = AnimationType.ClydeLeft;
                        break;
                    case Direction.right:
                        animationType = AnimationType.ClydeRight;
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
            return new Coordinate(pacmanLocation.X, pacmanLocation.Y);
        }

        public Coordinate CheckTargetForClyde(Coordinate pacmanLocation)
        {
            Coordinate result;

            if ((Math.Abs(Animation.Location.X - pacmanLocation.X) < 8 * Coordinate.Multiplier) && (Math.Abs(Animation.Location.Y - pacmanLocation.Y) < 8 * Coordinate.Multiplier))
            {
                result = new Coordinate(Coordinate.Multiplier, Coordinate.Multiplier * 25);
            }
            else
            {
                result = pacmanLocation;
            }
            return result;
        }
    }
}
