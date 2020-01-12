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

        public bool[,] Grid;

        public Pacman(bool[,] GridOfWalls)    //constructor
        {
            Name = "Pacman";

            IsEnabled = true;

            Animation = AnimationFactory.CreateAnimation(AnimationType.PacmanRight);

            Grid = GridOfWalls;
        }



        public void Collide(IEnumerable<IGameObject> collisions)
        {
            foreach (var obj in collisions)     //pacman can eat only coins
                obj.IsEnabled = false;
            //if((obj.Name == "SmallCoin") || (obj.Name == "BigCoin"))
            
        }

        public virtual void Update()
        {
            DirectionKeys NewDirection = DirectionKeys.None;

            //find new pressed key of direction
            if (((PressedKeys & DirectionKeys.Left) == DirectionKeys.Left) /*&& (Animation.Location.X % 1 == 0) && (Animation.Location.Y % 1 == 0)*/)           
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
                    //bool isWall = Grid[((Animation.Location.X / Coordinate.Multiplier) - 1), Animation.Location.Y / Coordinate.Multiplier];
                   
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
            if ((Animation.Location.X == 0) && (CurrentDirection == DirectionKeys.Left))
                Animation.Location = new Coordinate(21*Coordinate.Multiplier, Animation.Location.Y);
            else if ((Animation.Location.X == 21*Coordinate.Multiplier) && (CurrentDirection == DirectionKeys.Right))
                Animation.Location = new Coordinate(0, Animation.Location.Y);

            if ((Animation.Location.Y == 0) && (CurrentDirection == DirectionKeys.Up))
                Animation.Location = new Coordinate(Animation.Location.X, 27*Coordinate.Multiplier);
            else if ((Animation.Location.Y == 27*Coordinate.Multiplier) && (CurrentDirection == DirectionKeys.Down))
                Animation.Location = new Coordinate(Animation.Location.X , 0);
        }
    }
}
