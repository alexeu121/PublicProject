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
        private DirectionKeys CurrentDirection = DirectionKeys.Up;

        protected readonly int speed = Coordinate.Multiplier / 10;
        private bool alive = true;
        private Coordinate step;

        private int CoinTimer;
        private bool CoinTimerOn;

        public Master master;

        private bool[,] Grid = new bool[Coordinate.WorldWidth / Coordinate.Multiplier, Coordinate.WorldHeight / Coordinate.Multiplier];

        public Pacman(int x, int y) : base(x, y, ObjectsNames.Pacman, AnimationType.PacmanUp)
        {
            Grid = PathFinder.Grid;
            CoinTimer = 0; CoinTimerOn = false;
        }

        public DirectionKeys PressedKeys { get; set; }
        
        public void Collide(IEnumerable<IGameObject> collisions)
        {
            foreach (var obj in collisions)
            {
                if (obj.Name == ObjectsNames.SmallCoin || obj.Name == ObjectsNames.BigCoin)
                    obj.IsEnabled = false;

                if (obj.Name == ObjectsNames.BigCoin)
                {
                    CoinTimerOn = true;
                    Master.Instance.isPacmanEatBigCoin(CoinTimerOn);
                }

                if (obj.Name == ObjectsNames.Pinky ||
                   obj.Name == ObjectsNames.Blinky ||
                   obj.Name == ObjectsNames.Inky ||
                   obj.Name == ObjectsNames.Clyde)
                { }

            }
            
        }


        public override void Update()
        {
            CheckTimer();


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
            if (Animation.Location.X > 0 &&
                Animation.Location.X < Coordinate.WorldWidth - Coordinate.Multiplier &&
                Animation.Location.Y > 0 &&
                Animation.Location.Y < Coordinate.WorldHeight - Coordinate.Multiplier)
            {
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

            
                switch (CurrentDirection)   //change the direction of going
                {
                    case DirectionKeys.Left:

                        isRoad = Grid[((int)Math.Ceiling((decimal)Animation.Location.X / (decimal)Coordinate.Multiplier) - 1), Animation.Location.Y / Coordinate.Multiplier];
                        if (isRoad)
                            Animation.Location -= new Coordinate(speed, 0);
                        break;
                    case DirectionKeys.Right:
                        isRoad = Grid[((int)((decimal)Animation.Location.X / (decimal)Coordinate.Multiplier) + 1), Animation.Location.Y / Coordinate.Multiplier];
                        if (isRoad)
                            Animation.Location += new Coordinate(speed, 0);
                        break;
                    case DirectionKeys.Up:
                        isRoad = Grid[Animation.Location.X / Coordinate.Multiplier, ((int)Math.Ceiling((decimal)Animation.Location.Y / (decimal)Coordinate.Multiplier) - 1)];
                        if (isRoad)
                            Animation.Location -= new Coordinate(0, speed);
                        break;
                    case DirectionKeys.Down:
                        isRoad = Grid[Animation.Location.X / Coordinate.Multiplier, ((int)((decimal)Animation.Location.Y / (decimal)Coordinate.Multiplier) + 1)];
                        if (isRoad)
                            Animation.Location += new Coordinate(0, speed);
                        break;
                }
            }
            else
            {
                //cross the walls on x coordinates
                if ((Animation.Location.X == 0) && (CurrentDirection == DirectionKeys.Left))
                {
                    Animation.Location = new Coordinate(Coordinate.WorldWidth, Animation.Location.Y);
                }
                else if ((Animation.Location.X == Coordinate.WorldWidth) && (CurrentDirection == DirectionKeys.Right))
                {
                    Animation.Location = new Coordinate(0, Animation.Location.Y);
                }
                else if (Animation.AnimationType == AnimationType.PacmanRight)
                {
                    Animation.Location += new Coordinate(speed, 0);
                }
                else if (Animation.AnimationType == AnimationType.PacmanLeft)
                {
                    Animation.Location -= new Coordinate(speed, 0);
                }
            }
        }

        public void CheckTimer()
        {
            if (CoinTimerOn)
                CoinTimer += 1;

            if (CoinTimer == 480)
            {
                CoinTimerOn = false;
                CoinTimer = 0;
                Master.Instance.isPacmanEatBigCoin(CoinTimerOn);
            }
        }

    }
}
