using System;
using System.Collections.Generic;
using System.Linq;
using PacmanEngine.Components.Actors;
using PacmanEngine.Components.Base;
using PacmanEngine.Components.Graphics;
using Program.ManagedObjects.Protagonists;

namespace Program.UnmanagedSources
{
    class Master : BaseGameObject
    {
        //public List<IGameObject> ManagedObjects;
        //public Pacman pacman;
        //public BaseGameObject maze;
        IGameObject mazeBlue;
        IGameObject mazeWhite;

        public int eatTimer;
        public bool isEatTimerOn;

        public bool isPacmanEatBigCoin = false;
        //============================================

        public static Master Instance { get; private set; }

        //public string Name => "Master";

        public bool IsEnabled { get { return true; } set { } }          //re use

        public Animation Animation { get; set; }   //to show win or lose message

        private readonly Pacman pacman;
        private readonly IGameObject[] backgrounds;
        private readonly IGameObject[] ghosts;

        public IEnumerable<IGameObject> _gameObjects;

        public Coordinate PacmanLocation => pacman.Animation.Location;

        public void Initialize(IEnumerable<IGameObject> gameObjects)
        {
            Instance = new Master(gameObjects);
        }

        public Master()
        {
            Name = "Master";
        }

        public Master(IEnumerable<IGameObject> gameObjects)
        {
            _gameObjects = gameObjects;
            Name = "Master";
            pacman = gameObjects.OfType<Pacman>().Single();
            backgrounds = gameObjects.Where(x => (x.Name == "MazeBlue" || x.Name == "MazeWhite")).ToArray();
            ghosts = gameObjects.Where(x => (x.Name == "Pinky" || x.Name == "Inky" || x.Name == "Blinky" || x.Name == "Clyde")).ToArray();

            if (ghosts.Length != 4)
            {
                throw new Exception("Wrong number of ghosts!");
            }
            #region NotUse
            //eatTimer = 0;
            //isEatTimerOn = false;
            //IsEnabled = true;
            //Name = "Master";

            #endregion
        }

        public override void Update()
        {
            mazeBlue = _gameObjects.Where(x => x.Name == "MazeBlue").Select(x => x).FirstOrDefault();
            mazeWhite = _gameObjects.Where(x => x.Name == "MazeWhite").Select(x => x).FirstOrDefault();

            if (isPacmanEatBigCoin)
            {
                isEatTimerOn = true;


                mazeBlue.IsEnabled = false;
                mazeWhite.IsEnabled = true;

                isPacmanEatBigCoin = false;
            }

            if (isEatTimerOn)
            {
                eatTimer += 1;
            }

            if (eatTimer == 300)
            {
                isEatTimerOn = false;
                eatTimer = 0;
                mazeBlue.IsEnabled = true;
                mazeWhite.IsEnabled = false;
            }


            //foreach (var obj in Pacman_collisions)
            //{
            //    if (obj.Name == "BigCoin")
            //    {

            //    }
            //}

            if (!_gameObjects.Any(x => x.IsEnabled && (x.Name == "BigCoin" || x.Name == "SmallCoin")))
            {
                Animation = AnimationFactory.CreateAnimation(AnimationType.MessageWin);
                IsEnabled = true;
                


            }

        }


    }
}
