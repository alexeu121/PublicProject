using System;
using System.Collections.Generic;
using System.Linq;
using PacmanEngine.Components.Actors;
using PacmanEngine.Components.Base;
using PacmanEngine.Components.Graphics;
using Program.ManagedObjects.Antagonists;
using Program.ManagedObjects.Protagonists;

namespace Program.UnmanagedSources
{
    class Master : IGameObject
    {
        public string Name => "Master";

        public Animation Animation { get; set; }

        public static Master Instance { get; private set; }

        public bool IsEnabled { get { return true; } set { } }
        public Ghost.GhostState currentGhostState = Ghost.GhostState.Regular;

        Animation mazeWhite = AnimationFactory.CreateAnimation(AnimationType.MazeWhite);
        Animation mazeBlue = AnimationFactory.CreateAnimation(AnimationType.MazeBlue);

        private readonly Pacman pacman;
        private readonly BaseGameObject message;
        private readonly BaseGameObject[] ghosts;
        private readonly BaseGameObject backgrounds;
        private readonly BaseGameObject[] coins;

        public Coordinate PacmanLocation { get { return pacman != null ? pacman.Animation.Location : new Coordinate(0, 0); } }

        public Coordinate BlinkyLocation { get { return ghosts.OfType<BlinkyObject>().Single() != null ? ghosts.OfType<BlinkyObject>().Single().Animation.Location : new Coordinate(0, 0); } }

        public Coordinate PacmanDirection
        {
            get
            {
                if (pacman != null)
                {
                    switch (pacman.Animation.AnimationType)
                    {
                        case AnimationType.PacmanDown:
                            return Coordinate.UnitY;
                        case AnimationType.PacmanUp:
                            return -Coordinate.UnitY;
                        case AnimationType.PacmanLeft:
                            return -Coordinate.UnitX;
                        default:
                            return Coordinate.UnitX;
                    }
                }
                else
                { return -Coordinate.UnitY; }
            }
        }

        public Coordinate BlinkyDirection
        {
            get
            {
                if (ghosts.OfType<BlinkyObject>().Single() != null)
                {
                    switch (ghosts.OfType<BlinkyObject>().Single().Animation.AnimationType)
                    {
                        case AnimationType.BlinkyDown:
                            return Coordinate.UnitY;
                        case AnimationType.BlinkyUp:
                            return -Coordinate.UnitY;
                        case AnimationType.BlinkyLeft:
                            return -Coordinate.UnitX;
                        default:
                            return Coordinate.UnitX;
                    }
                }
                else { return -Coordinate.UnitX; }
            }
        }

        public Master(IEnumerable<BaseGameObject> gameObjects)
        {
            message = gameObjects.Where(x => x.Name == ObjectsNames.Master).First();
            pacman = gameObjects.OfType<Pacman>().Single();
            backgrounds = gameObjects.Where(x => (x.Name == ObjectsNames.Background)).Single();
            ghosts = gameObjects.Where(x => (x.Name == ObjectsNames.Pinky || x.Name == ObjectsNames.Inky || x.Name == ObjectsNames.Blinky || x.Name == ObjectsNames.Clyde)).ToArray();
            coins = gameObjects.Where(x => x.Name == ObjectsNames.BigCoin || x.Name == ObjectsNames.SmallCoin).ToArray();
        }

        public void Initialize(IEnumerable<BaseGameObject> gameObjects)
        {
            Instance = new Master(gameObjects);
            Instance.message.Animation.Location = CoordinateExtension.MessagePosition;
            Instance.message.IsEnabled = false;
        }


        public void isPacmanEatBigCoin(bool CoinTimerOn)
        {
            if (CoinTimerOn)
            {
                foreach (var obj in ghosts)
                {
                    (obj as Ghost).SetBlueState();
                }
                currentGhostState = Ghost.GhostState.BlueGhost;
                Instance.backgrounds.Animation = mazeWhite;
            }
            else
            {
                foreach (var obj in ghosts)
                {
                    (obj as Ghost).SetRegularState();
                }
                Instance.backgrounds.Animation = mazeBlue;

            }
        }

        public void isPacmanEatGhost(string ghostName)
        {
            if ((ghosts.Where(x => x.Name == ghostName).First() as Ghost).currentState != Ghost.GhostState.Regular)
            {
                (Instance.ghosts.Where(x => x.Name == ghostName).First() as Ghost).setGhostToHome();
            }

            
        }

        public void isPacmanDeath()
        {
            Animation deathAnim = CompareDirection();
            Coordinate loc = PacmanLocation;
            Animation = deathAnim;
            Animation.Location = loc;
            Instance.pacman.isMessageTimerOn = true;

            Instance.message.Animation = AnimationFactory.CreateAnimation(AnimationType.MessageLose);
            Instance.message.Animation.Location = CoordinateExtension.MessagePosition;
            Instance.message.IsEnabled = true;

            foreach (var obj in Instance.coins)
            { obj.Reset(); }
            foreach (var obj in Instance.ghosts)
            { obj.Reset(); }
            Instance.pacman.Reset();
            Instance.backgrounds.Animation = mazeBlue;

        }

        public void offMessage(bool isMessageOn)
        {
            if (!isMessageOn)
            {
                Instance.message.IsEnabled = false;
            }
        }

        public bool CheckCoins()
        {
            if (Instance.coins.Where(x => x.IsEnabled == true).Count() == 0)
            {
                Instance.message.Animation = AnimationFactory.CreateAnimation(AnimationType.MessageWin);
                Instance.message.Animation.Location = CoordinateExtension.MessagePosition;
                Instance.message.IsEnabled = true;

                pacman.isWin = true;
            }
            return true;
        }

        public void CheckWinShow(bool isWinner, int winTimer)
        {
            if (isWinner && winTimer == 299)
            {
                foreach (var obj in Instance.coins)
                { obj.Reset(); }
                foreach (var obj in Instance.ghosts)
                { obj.Reset(); }
                Instance.pacman.Reset();
                offMessage(false);

            }

            if (winTimer == 30 || winTimer == 90 || winTimer == 150 || winTimer == 210 || winTimer == 270 )
                Instance.backgrounds.Animation = mazeWhite; 
            else if (winTimer == 60|| winTimer == 120 || winTimer == 180 || winTimer == 240)
                Instance.backgrounds.Animation = mazeBlue;
        }

        private Animation CompareDirection()
        {
            switch (pacman.Animation.AnimationType)
            {
                case AnimationType.PacmanDown:
                    return AnimationFactory.CreateAnimation(AnimationType.PacmanDeathDown);
                case AnimationType.PacmanUp:
                    return AnimationFactory.CreateAnimation(AnimationType.PacmanDeathUp);
                case AnimationType.PacmanLeft:
                    return AnimationFactory.CreateAnimation(AnimationType.PacmanDeathLeft);
                default:
                    return AnimationFactory.CreateAnimation(AnimationType.PacmanDeathRight);
            }
        }
        public void Update()
        {  }

       
    }
}
