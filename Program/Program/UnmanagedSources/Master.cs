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
        public static Master Instance { get; private set; }
        public string Name => "Master";
        public bool IsEnabled { get { return true; } set { } }          //re use
        public Animation Animation { get; set; }   //to show win or lose message

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

        private readonly Pacman pacman;
        private readonly IGameObject[] backgrounds;
        private readonly IGameObject[] ghosts;


        public void Initialize(IEnumerable<IGameObject> gameObjects)
        {
            Instance = new Master(gameObjects);
        }

        public Master(IEnumerable<IGameObject> gameObjects)
        {
            pacman = gameObjects.OfType<Pacman>().Single();
            backgrounds = gameObjects.Where(x => (x.Name == ObjectsNames.Background)).ToArray();
            ghosts = gameObjects.Where(x => (x.Name == ObjectsNames.Pinky || x.Name == ObjectsNames.Inky || x.Name == ObjectsNames.Blinky || x.Name == ObjectsNames.Clyde)).ToArray();

        }

        public void Update()
        {

            #region old
            //mazeBlue = _gameObjects.Where(x => x.Name == ObjectsNames.MazeBlue).Select(x => x).FirstOrDefault();
            //mazeWhite = _gameObjects.Where(x => x.Name == ObjectsNames.MazeWhite).Select(x => x).FirstOrDefault();

            //PacmanLocation = pacman.Animation.Location;
            //if (isPacmanEatBigCoin)
            //{
            //    isEatTimerOn = true;


            //    mazeBlue.IsEnabled = false;
            //    mazeWhite.IsEnabled = true;

            //    isPacmanEatBigCoin = false;
            //}

            //if (isEatTimerOn)
            //{
            //    eatTimer += 1;
            //}

            //if (eatTimer == 300)
            //{
            //    isEatTimerOn = false;
            //    eatTimer = 0;
            //    mazeBlue.IsEnabled = true;
            //    mazeWhite.IsEnabled = false;
            //}


            //foreach (var obj in Pacman_collisions)
            //{
            //    if (obj.Name == "BigCoin")
            //    {

            //    }
            //}

            //if (!_gameObjects.Any(x => x.IsEnabled && (x.Name == ObjectsNames.BigCoin || x.Name == ObjectsNames.SmallCoin)))
            //{
            //    Animation.Location = new Coordinate(13, 11);

            //    Animation newAnimation = AnimationFactory.CreateAnimation(AnimationType.MessageWin);
            //    newAnimation.Location = Animation.Location;
            //    Animation = newAnimation;

            //}
            #endregion
        }


    }
}
