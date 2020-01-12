using System;
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

        public int Speed { get; set; } = 100000;    //1 cell = 16 pix // 21 x 27 cells

        public Pacman()    //constructor
        {
            Name = "Pacman";

            IsEnabled = true;

            Animation = AnimationFactory.CreateAnimation(AnimationType.PacmanRight);
        }



        public void Collide(IEnumerable<IGameObject> collisions)
        {
            foreach (var obj in collisions)     //pacman can eat only coins
                if((obj.Name == "SmallCoin") || (obj.Name == "BigCoin"))
                    obj.IsEnabled = false;
        }

        public virtual void Update()
        {
            DirectionKeys NewDirection = DirectionKeys.None;

            //find new pressed key of direction
            if (((PressedKeys & DirectionKeys.Left) == DirectionKeys.Left) && (Animation.Location.X % 1 == 0) && (Animation.Location.Y % 1 == 0))           
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

            //cross the walls on x coordinates
            if ((Animation.Location.X <= 0.3f) && (Animation.Location.X >= -0.3f) && (CurrentDirection == DirectionKeys.Left))
                Animation.Location = new Coordinate(Animation.Location.X + 21000000, Animation.Location.Y);
            else if ((Animation.Location.X <= 21.3f) && (Animation.Location.X >= 20.7f) && (CurrentDirection == DirectionKeys.Right))
                Animation.Location = new Coordinate(Animation.Location.X - 21000000, Animation.Location.Y);

            if ((Animation.Location.Y <= 0.3f) && (Animation.Location.Y >= -0.3f) && (CurrentDirection == DirectionKeys.Up))
                Animation.Location = new Coordinate(Animation.Location.X, Animation.Location.Y + 27000000);
            else if ((Animation.Location.Y <= 27.3f) && (Animation.Location.Y >= 26.7f) && (CurrentDirection == DirectionKeys.Down))
                Animation.Location = new Coordinate(Animation.Location.X , Animation.Location.Y - 27000000);
        }
    }
}
