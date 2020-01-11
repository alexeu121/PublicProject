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
            {
                case AnimationType.MazeBlue:
                    result = new BaseGameObject()
                    {
                        Animation = AnimationFactory.CreateAnimation(AnimationType.MazeBlue)
                    };
                    break;
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
                    break;


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
