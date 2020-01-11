using System.Collections.Generic;
using PacmanEngine.Components.Actors;
using PacmanEngine.Components.Base;
using PacmanEngine.Components.Graphics;

namespace Program.GameObjects
{
    class Pacman : BaseGameObject, IProtagonist
    {
        public DirectionKeys PressedKeys { get; set; }

        private DirectionKeys CurrentDirection = DirectionKeys.None;
        public float Speed { get; set; } = 0.1f;    //1 cell = 16 pix // 21 x 27 cells

        public Pacman()    //constructor
        {
            Name = "Pacman";

            IsEnabled = true;

            Animation = AnimationFactory.CreateAnimation(AnimationType.PacmanRight);

            //Animation.Location = new Coordinate(5, 21);    //start position
        }



        public void Collide(IEnumerable<IGameObject> collisions)
        {
            foreach (var obj in collisions)
                if((obj.Name != "Pinky") && (obj.Name != "Blinky") && (obj.Name != "Inky") && (obj.Name != "Clyde"))
                    obj.IsEnabled = false;

        }

        public virtual void Update()
        {
            DirectionKeys NewDirection = DirectionKeys.None;

            if ((PressedKeys & DirectionKeys.Left) == DirectionKeys.Left)
                NewDirection = DirectionKeys.Left;
            else if ((PressedKeys & DirectionKeys.Right) == DirectionKeys.Right)
                NewDirection = DirectionKeys.Right;
            else if ((PressedKeys & DirectionKeys.Up) == DirectionKeys.Up)
                NewDirection = DirectionKeys.Up;
            else if ((PressedKeys & DirectionKeys.Down) == DirectionKeys.Down)
                NewDirection = DirectionKeys.Down;

            if (CurrentDirection != NewDirection && NewDirection != DirectionKeys.None) //change the direction of watching
            {
                Animation newAnimation;
                switch (NewDirection)
                {
                    case DirectionKeys.Left:
                        newAnimation = AnimationFactory.CreateAnimation(AnimationType.PacmanLeft);
                        break;
                    case DirectionKeys.Right:
                        newAnimation = AnimationFactory.CreateAnimation(AnimationType.PacmanRight);
                        break;
                    case DirectionKeys.Up:
                        newAnimation = AnimationFactory.CreateAnimation(AnimationType.PacmanUp);
                        break;
                    default:
                        newAnimation = AnimationFactory.CreateAnimation(AnimationType.PacmanDown);
                        break;
                }
                newAnimation.Location = Animation.Location;
                Animation = newAnimation;
                CurrentDirection = NewDirection;
            }

            switch (CurrentDirection)   //change the direction of going
            {
                case DirectionKeys.Left:
                    Animation.Location -= new Coordinate(Speed, 0);
                    break;
                case DirectionKeys.Right:
                    Animation.Location += new Coordinate(Speed, 0);
                    break;
                case DirectionKeys.Up:
                    Animation.Location -= new Coordinate(0, Speed);
                    break;
                case DirectionKeys.Down:
                    Animation.Location += new Coordinate(0, Speed);
                    break;
            }

            //if (Animation.Location.X == 0)
            //    Animation.Location = new Coordinate(Animation.Location.X + 21f, Animation.Location.Y);
            //else if (Animation.Location.X == 21)
            //    Animation.Location = new Coordinate(Animation.Location.X - 21f, Animation.Location.Y);

            //if (Animation.Location.Y == 0)
            //    Animation.Location = new Coordinate(Animation.Location.X, Animation.Location.Y + 27f);
            //else if (Animation.Location.Y == 27)
            //    Animation.Location = new Coordinate(Animation.Location.X, Animation.Location.Y - 27f);
        }
    }
}
