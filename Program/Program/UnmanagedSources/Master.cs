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
        //public bool PacmanCoinTimerOn;

        private readonly Pacman pacman;
        private readonly BaseGameObject[] ghosts;
        private readonly BaseGameObject backgrounds;

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

        public void isPacmanEatBigCoin(bool CoinTimerOn)
        {
            var mazeWhite = AnimationFactory.CreateAnimation(AnimationType.MazeWhite);
            var mazeBlue = AnimationFactory.CreateAnimation(AnimationType.MazeBlue);

            if (CoinTimerOn)
            {
                foreach (var obj in Instance.ghosts)
                { (obj as Ghost).SetBlueState();  }

                Instance.backgrounds.Animation = mazeWhite;
            }
            else
            {
                foreach (var obj in Instance.ghosts)
                { (obj as Ghost).SetRegularState(); }
                Instance.backgrounds.Animation = mazeBlue;

            }
        }


        public void Initialize(IEnumerable<BaseGameObject> gameObjects)
        {
            Instance = new Master(gameObjects);
        }

        public Master(IEnumerable<BaseGameObject> gameObjects)
        {
            pacman = gameObjects.OfType<Pacman>().Single();
            backgrounds = gameObjects.Where(x => (x.Name == ObjectsNames.Background)).Single();
            ghosts = gameObjects.Where(x => (x.Name == ObjectsNames.Pinky || x.Name == ObjectsNames.Inky || x.Name == ObjectsNames.Blinky || x.Name == ObjectsNames.Clyde)).ToArray();

        }

        public void Update()
        { }
    }
}
