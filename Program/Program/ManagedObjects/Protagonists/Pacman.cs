using System;
using System.Collections.Generic;
using PacmanEngine.Components.Actors;
using PacmanEngine.Components.Base;
using PacmanEngine.Components.Graphics;
using Program.UnmanagedSources;

namespace Program.ManagedObjects.Protagonists
{
    class Pacman : BaseGameObject, IProtagonist
    {
        public DirectionKeys PressedKeys { get; set; }

        private DirectionKeys CurrentDirection = DirectionKeys.None;

        public int Speed { get; set; } = 500000;    //1 cell = 16 pix // 21 x 27 cells

        public Master master;

        private bool[,] Grid;


        public Pacman()    //constructor
        {
            Name = "Pacman";

            IsEnabled = true;

            Grid = ObjectsBuilder.Grid;

            Animation = AnimationFactory.CreateAnimation(AnimationType.PacmanRight);

            

            //MasterObj = new Master();
        }



        public void Collide(IEnumerable<IGameObject> collisions)
        {
            
            foreach (var obj in collisions)     //pacman can eat only coins
            {
                obj.IsEnabled = false;

                if (obj.Name == "BigCoin")
                {
                    
                    master.isPacmanEatBigCoin = true;

                }

            }
            //if((obj.Name == "SmallCoin") || (obj.Name == "BigCoin"))

        }

        public override void Update()
        {
            //if (EatTimerOn)
            //    EatTimer += 1;

            DirectionKeys NewDirection = DirectionKeys.None;
            bool isRoad = false;

            //find new pressed key of direction
            if ((Animation.Location.X % Coordinate.Multiplier == 0) && (Animation.Location.Y % Coordinate.Multiplier == 0))
            {
                if ((PressedKeys & DirectionKeys.Left) == DirectionKeys.Left)
                    NewDirection = DirectionKeys.Left;
                else if ((PressedKeys & DirectionKeys.Right) == DirectionKeys.Right)
                    NewDirection = DirectionKeys.Right;
                else if ((PressedKeys & DirectionKeys.Up) == DirectionKeys.Up)
                    NewDirection = DirectionKeys.Up;
                else if ((PressedKeys & DirectionKeys.Down) == DirectionKeys.Down)
                    NewDirection = DirectionKeys.Down;
            }

            if (CurrentDirection != NewDirection && NewDirection != DirectionKeys.None) //change the direction of watching
            {
                Animation newAnimation = AnimationFactory.CreateAnimation(AnimationType.PacmanRight);   //default
                switch (NewDirection)
                {
                    case DirectionKeys.Left:
                        isRoad = Grid[((int)Math.Ceiling((decimal)Animation.Location.X / (decimal)Coordinate.Multiplier) - 1), Animation.Location.Y / Coordinate.Multiplier];
                        if (isRoad)
                            newAnimation = AnimationFactory.CreateAnimation(AnimationType.PacmanLeft);
                        break;
                    case DirectionKeys.Right:
                        isRoad = Grid[((int)((decimal)Animation.Location.X / (decimal)Coordinate.Multiplier) + 1), Animation.Location.Y / Coordinate.Multiplier];
                        if (isRoad)
                            newAnimation = AnimationFactory.CreateAnimation(AnimationType.PacmanRight);
                        break;
                    case DirectionKeys.Up:
                        isRoad = Grid[Animation.Location.X / Coordinate.Multiplier, ((int)Math.Ceiling((decimal)Animation.Location.Y / (decimal)Coordinate.Multiplier) - 1)];
                        if (isRoad)
                            newAnimation = AnimationFactory.CreateAnimation(AnimationType.PacmanUp);
                        break;
                    default:
                        isRoad = Grid[Animation.Location.X / Coordinate.Multiplier, ((int)((decimal)Animation.Location.Y / (decimal)Coordinate.Multiplier) + 1)];
                        if (isRoad)
                            newAnimation = AnimationFactory.CreateAnimation(AnimationType.PacmanDown);
                        break;
                }
                if (isRoad)
                {
                    newAnimation.Location = Animation.Location;
                    Animation = newAnimation;
                    CurrentDirection = NewDirection;
                }
            }

            if (Animation.Location.X / Coordinate.Multiplier > 0 &&
                Animation.Location.X / Coordinate.Multiplier < 20 &&
                Animation.Location.Y / Coordinate.Multiplier > 0 &&
                Animation.Location.Y / Coordinate.Multiplier < 26)
            {
                switch (CurrentDirection)   //change the direction of going
                {
                    case DirectionKeys.Left:

                        isRoad = Grid[((int)Math.Ceiling((decimal)Animation.Location.X / (decimal)Coordinate.Multiplier) - 1), Animation.Location.Y / Coordinate.Multiplier];
                        if (isRoad)
                            Animation.Location -= new Coordinate(Speed, 0);
                        break;
                    case DirectionKeys.Right:
                        isRoad = Grid[((int)((decimal)Animation.Location.X / (decimal)Coordinate.Multiplier) + 1), Animation.Location.Y / Coordinate.Multiplier];
                        if (isRoad)
                            Animation.Location += new Coordinate(Speed, 0);
                        break;
                    case DirectionKeys.Up:
                        isRoad = Grid[Animation.Location.X / Coordinate.Multiplier, ((int)Math.Ceiling((decimal)Animation.Location.Y / (decimal)Coordinate.Multiplier) - 1)];
                        if (isRoad)
                            Animation.Location -= new Coordinate(0, Speed);
                        break;
                    case DirectionKeys.Down:
                        isRoad = Grid[Animation.Location.X / Coordinate.Multiplier, ((int)((decimal)Animation.Location.Y / (decimal)Coordinate.Multiplier) + 1)];
                        if (isRoad)
                            Animation.Location += new Coordinate(0, Speed);
                        break;
                }
            }
            else
            {
                //cross the walls on x coordinates
                if ((Animation.Location.X == 0) && (CurrentDirection == DirectionKeys.Left))
                    Animation.Location = new Coordinate(21 * Coordinate.Multiplier, Animation.Location.Y);
                else if ((Animation.Location.X == 21 * Coordinate.Multiplier) && (CurrentDirection == DirectionKeys.Right))
                    Animation.Location = new Coordinate(0, Animation.Location.Y);
                else if (Animation.AnimationType == AnimationType.PacmanRight) Animation.Location += new Coordinate(Speed, 0);
                else if (Animation.AnimationType == AnimationType.PacmanLeft) Animation.Location -= new Coordinate(Speed, 0);
            }
        }
    }
}
