using PacmanEngine.Components.Actors;
using PacmanEngine.Components.Base;
using PacmanEngine.Components.Graphics;

namespace Program.UnmanagedSources
{
    class BaseGameObject : IGameObject
    {
        private int x;
        private int y;
        private string pacman;
        private AnimationType pacmanUp;

        protected readonly Coordinate initialCoordinate;
        private readonly AnimationType? initialAnimationType;

        public BaseGameObject(int x, int y, string name, AnimationType? animationType)
        {
            initialCoordinate = new Coordinate(x * Coordinate.Multiplier, y * Coordinate.Multiplier);
            initialAnimationType = animationType;
            Name = name;
            Reset();
        }

        public string Name { get; set; }
        public bool IsEnabled { get; set; } = true; //turn on when load
        public Animation Animation { get; set; }

        public virtual void Reset()
        {
            if (initialAnimationType.HasValue)
            {
                if (Animation == null || Animation.AnimationType != initialAnimationType.Value)
                    Animation = AnimationFactory.CreateAnimation(initialAnimationType.Value);

                Animation.Location = initialCoordinate;
            }
            else
                Animation = null;

            IsEnabled = true;
        }


        //public static BaseGameObject CreateStaticObject(AnimationType type, int xPos, int yPos)
        //{
        //    BaseGameObject result = null;

        //    switch (type)
        //    {                      //---------------MAZES----------------
        //        case AnimationType.MazeBlue:
        //            result = new BaseGameObject()
        //            {
        //                    Animation = AnimationFactory.CreateAnimation(AnimationType.MazeBlue)
        //            };
        //            break;
        //        case AnimationType.MazeWhite:
        //            result = new BaseGameObject()
        //            {
        //                Animation = AnimationFactory.CreateAnimation(AnimationType.MazeWhite)
        //            };
        //            break;         //---------------COINS----------------
        //        case AnimationType.BigCoin:
        //            result = new BaseGameObject()
        //            {
        //                Animation = AnimationFactory.CreateAnimation(AnimationType.BigCoin)
        //            };
        //            break;
        //        case AnimationType.SmallCoin:
        //            result = new BaseGameObject()
        //            {
        //                Animation = AnimationFactory.CreateAnimation(AnimationType.SmallCoin)
        //            };
        //            break;
        //    }
        //    if (result != null)
        //    {
        //        result.Name = type.ToString();
        //        result.Animation.Location = new Coordinate(xPos, yPos);
        //    }
        //    return result;
        //}
        public virtual void Update()
        { }
    }
}
