using System.Collections.Generic;
using PacmanEngine.Components.Actors;
using PacmanEngine.Components.Base;
using PacmanEngine.Components.Graphics;

namespace Program.GameObjects
{
    class Pacman : BaseGameObject//BaseGameObject, IProtagonist
    {
        public DirectionKeys PressedKeys { get; set; }

        private DirectionKeys CurrentDirection = DirectionKeys.None;
        public float Speed { get; set; } = 0.1f;    //1 kletka = 16 pix // 21 na 27 kletok

        public Pacman()    //конструктор класса
        {
            Name = "Pacman";

            IsEnabled = true;

            Animation = AnimationFactory.CreateAnimation(AnimationType.PacmanRight);

            Animation.Location = new Coordinate(10, 10);    //стартовая позиция пакмана
        }



        public void Collide(IEnumerable<IGameObject> collisions)
        {
            foreach (var obj in collisions)
                obj.IsEnabled = false;

        }

        public virtual void Update()
        {
            DirectionKeys NewDirection = DirectionKeys.None;

            if ((PressedKeys & DirectionKeys.Left) == DirectionKeys.Left)
                NewDirection = DirectionKeys.Left;
            else if ((PressedKeys & DirectionKeys.Right) == DirectionKeys.Right)
                NewDirection = DirectionKeys.Right;
            else if ((PressedKeys & DirectionKeys.Up) == DirectionKeys.Up)
                NewDirection = DirectionKeys.Up;
            else if ((PressedKeys & DirectionKeys.Down) == DirectionKeys.Down)
                NewDirection = DirectionKeys.Down;

            if (CurrentDirection != NewDirection && NewDirection != DirectionKeys.None)
            {
                Animation newAnimation;
                switch (NewDirection)
                {
                    case DirectionKeys.Left:
                        newAnimation = AnimationFactory.CreateAnimation(AnimationType.PacmanLeft);
                        break;
                    case DirectionKeys.Right:
                        newAnimation = AnimationFactory.CreateAnimation(AnimationType.PacmanRight);
                        break;
                    case DirectionKeys.Up:
                        newAnimation = AnimationFactory.CreateAnimation(AnimationType.PacmanUp);
                        break;
                    default:
                        newAnimation = AnimationFactory.CreateAnimation(AnimationType.PacmanDown);
                        break;
                }
                newAnimation.Location = Animation.Location;
                Animation = newAnimation;
                CurrentDirection = NewDirection;
            }

            switch (CurrentDirection)
            {
                case DirectionKeys.Left:
                    Animation.Location -= new Coordinate(Speed, 0);
                    break;
                case DirectionKeys.Right:
                    Animation.Location += new Coordinate(Speed, 0);
                    break;
                case DirectionKeys.Up:
                    Animation.Location -= new Coordinate(0, Speed);
                    break;
                case DirectionKeys.Down:
                    Animation.Location += new Coordinate(0, Speed);
                    break;
            }
        }
    }
}
