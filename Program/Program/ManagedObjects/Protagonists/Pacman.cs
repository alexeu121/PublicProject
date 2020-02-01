using System;
using System.Collections.Generic;
using PacmanEngine.Components.Actors;
using PacmanEngine.Components.Base;
using PacmanEngine.Components.Graphics;
using Program.ManagedObjects.Antagonists;
using Program.UnmanagedSources;

namespace Program.ManagedObjects.Protagonists
{
    class Pacman : BaseGameObject, IProtagonist
    {
        private DirectionKeys CurrentDirection = DirectionKeys.Up;

        protected readonly int speed = Coordinate.Multiplier / 10;  //10

        private int MessageTimer;
        public bool isMessageTimerOn;
        private int CoinTimer;
        private bool CoinTimerOn;
        private int WinnerTimer;
        public bool isWin;

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

                if ((obj.Name == ObjectsNames.Pinky ||
                   obj.Name == ObjectsNames.Blinky ||
                   obj.Name == ObjectsNames.Inky ||
                   obj.Name == ObjectsNames.Clyde))
                {
                    if (CoinTimerOn && Master.Instance.currentGhostState == Ghost.GhostState.BlueGhost)
                    {
                        Master.Instance.isPacmanEatGhost(obj.Name);
                    }
                    else if (Master.Instance.currentGhostState == Ghost.GhostState.Regular)
                    {
                        Master.Instance.isPacmanDeath();
                    }
                }
            }
        }

        public override void Update()
        {
            bool isRoad = false;

            CheckTimerAndMessage();

            Master.Instance.CheckWinShow(isWin, WinnerTimer);

            DirectionKeys NewDirection = DirectionKeys.None;

            //find new pressed key of direction
            if ((Animation.Location.X % Coordinate.Multiplier == 0) && (Animation.Location.Y % Coordinate.Multiplier == 0))
            {

                Master.Instance.CheckCoins();

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

        public void CheckTimerAndMessage()
        {
            if (CoinTimerOn)
                CoinTimer += 1;

            if (CoinTimer == 480)   //8 sec
            {
                CoinTimerOn = false;
                CoinTimer = 0;
                Master.Instance.isPacmanEatBigCoin(CoinTimerOn);
            }

            if (isMessageTimerOn)
                MessageTimer += 1;

            if (MessageTimer == 300)   //5 sec
            {
                isMessageTimerOn = false;
                MessageTimer = 0;
                Master.Instance.offMessage(isMessageTimerOn);
            }

            if (isWin)
                WinnerTimer += 1;

            if (WinnerTimer == 300)
            {
                isWin = false;
                WinnerTimer = 0;
            }

        }



    }
}
