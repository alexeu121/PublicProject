using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PacmanEngine.Components.Base;
using PacmanEngine.Components.Graphics;
using Program.UnmanagedSources;

namespace Program.ManagedObjects.Antagonists
{
    class InkyObject : Ghost
    {

        public InkyObject(int x, int y) : base(x, y, ObjectsNames.Inky, AnimationType.InkyDown) { }

        protected override Animation GetAnimation()
        {
            AnimationType animationType = AnimationType.BlueGhost;

            if (currentState == GhostState.Regular)
            {
                switch (currentDirection)
                {
                    case Direction.up:
                        animationType = AnimationType.InkyUp;
                        break;
                    case Direction.down:
                        animationType = AnimationType.InkyDown;
                        break;
                    case Direction.left:
                        animationType = AnimationType.InkyLeft;
                        break;
                    case Direction.right:
                        animationType = AnimationType.InkyRight;
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

        protected override Coordinate GetTargetCoordinate2(Coordinate pacmanLocation, Coordinate blinkyLocation)
        {
            var pacmanDirection = Master.Instance.PacmanDirection;
            var blinkyDirection = Master.Instance.BlinkyDirection;

            Coordinate pacmanLocationPlus = pacmanLocation + pacmanDirection + pacmanDirection;

            var deltaX = Math.Abs(blinkyLocation.X - pacmanLocationPlus.X);
            var deltaY = Math.Abs(blinkyLocation.Y - pacmanLocationPlus.Y);


            Coordinate target = new Coordinate((blinkyLocation.X >= pacmanLocation.X ? (blinkyLocation.X - (deltaX + deltaX)) : (blinkyLocation.X + deltaX + deltaX)),
                                               (blinkyLocation.Y >= pacmanLocation.Y ? (blinkyLocation.Y - (deltaY + deltaY)) : (blinkyLocation.Y + deltaY + deltaY)));


            //Coordinate target = new Coordinate(
            //    (Coordinate.WorldWidth + (blinkyLocation.X >= pacmanLocation.X ? (blinkyLocation.X - (deltaX + deltaX)) : (blinkyLocation.X + deltaX + deltaX))) / Coordinate.WorldWidth,
            //    ((Coordinate.WorldHeight + (blinkyLocation.Y >= pacmanLocation.Y ? (blinkyLocation.Y - (deltaY + deltaY)) : (blinkyLocation.Y + deltaY + deltaY))) / Coordinate.WorldHeight));





            if (PathFinder.isSquareEmpty(target))
            {
                return target;
            }
            else
            {
                return pacmanLocation;
            }
        }

        protected override Coordinate GetTargetCoordinate(Coordinate pacmanLocation)
        { return new Coordinate(0, 0); }
    }
}
