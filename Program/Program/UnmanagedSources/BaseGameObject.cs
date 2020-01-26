using PacmanEngine.Components.Actors;
using PacmanEngine.Components.Base;
using PacmanEngine.Components.Graphics;

namespace Program.UnmanagedSources
{
    public class BaseGameObject : IGameObject
    {
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

        public bool IsEnabled { get; set; } = true;

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

        public virtual void Update()
        { }
    }
}
