using PacmanEngine.Components.Actors;
using PacmanEngine.Components.Base;
using PacmanEngine.Components.Graphics;

namespace Program.GameObjects
{
    class BaseGameObject : IGameObject
    {
        public string Name { get; set; }
        public bool IsEnabled { get; set; } = true;//по умолчанию включен
        public Animation Animation { get; set; }

        public static BaseGameObject CreateStaticObject(AnimationType type, float xPos, float yPos)
        {
            BaseGameObject result = null;

            switch (type)
            {                      //---------------MAZES----------------
                case AnimationType.MazeBlue:
                    result = new BaseGameObject()
                    {
                        Animation = AnimationFactory.CreateAnimation(AnimationType.MazeBlue)
                    };
                    break;
                case AnimationType.MazeWhite:
                    result = new BaseGameObject()
                    {
                        Animation = AnimationFactory.CreateAnimation(AnimationType.MazeWhite)
                    };
                    break;         //---------------COINS----------------
                case AnimationType.BigCoin:
                    result = new BaseGameObject()
                    {
                        Animation = AnimationFactory.CreateAnimation(AnimationType.BigCoin)
                    };
                    break;
                case AnimationType.SmallCoin:
                    result = new BaseGameObject()
                    {
                        Animation = AnimationFactory.CreateAnimation(AnimationType.SmallCoin)
                    };
                    break;         //---------------PACMAN----------------
                case AnimationType.PacmanRight:
                    result = new BaseGameObject()
                    {
                        Animation = AnimationFactory.CreateAnimation(AnimationType.PacmanRight)
                    };
                    break;
                case AnimationType.PacmanDown:
                    result = new BaseGameObject()
                    {
                        Animation = AnimationFactory.CreateAnimation(AnimationType.PacmanDown)
                    };
                    break;
                case AnimationType.PacmanLeft:
                    result = new BaseGameObject()
                    {
                        Animation = AnimationFactory.CreateAnimation(AnimationType.PacmanLeft)
                    };
                    break;
                case AnimationType.PacmanUp:
                    result = new BaseGameObject()
                    {
                        Animation = AnimationFactory.CreateAnimation(AnimationType.PacmanUp)
                    };
                    break;
                case AnimationType.PacmanDeathDown:
                    result = new BaseGameObject()
                    {
                        Animation = AnimationFactory.CreateAnimation(AnimationType.PacmanDeathDown)
                    };
                    break;
                case AnimationType.PacmanDeathLeft:
                    result = new BaseGameObject()
                    {
                        Animation = AnimationFactory.CreateAnimation(AnimationType.PacmanDeathLeft)
                    };
                    break;
                case AnimationType.PacmanDeathRight:
                    result = new BaseGameObject()
                    {
                        Animation = AnimationFactory.CreateAnimation(AnimationType.PacmanDeathRight)
                    };
                    break;
                case AnimationType.PacmanDeathUp:
                    result = new BaseGameObject()
                    {
                        Animation = AnimationFactory.CreateAnimation(AnimationType.PacmanDeathUp)
                    };
                    break;
                //case AnimationType.:
                //    result = new BaseGameObject()
                //    {
                //        Animation = AnimationFactory.CreateAnimation(AnimationType.PacmanUp)
                //    };
                //    break;
                //case AnimationType.PacmanUp:
                //    result = new BaseGameObject()
                //    {
                //        Animation = AnimationFactory.CreateAnimation(AnimationType.PacmanUp)
                //    };
                //    break;
                //case AnimationType.PacmanUp:
                //    result = new BaseGameObject()
                //    {
                //        Animation = AnimationFactory.CreateAnimation(AnimationType.PacmanUp)
                //    };
                //    break;
            }
            if (result != null)
            {
                result.Name = type.ToString();
                result.Animation.Location = new Coordinate(xPos, yPos);
            }
            return result;
        }
        public virtual void Update() { }
    }
}
