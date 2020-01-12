using System;
using System.Collections.Generic;
using PacmanEngine.Components.Actors;
using PacmanEngine.Components.Base;
using PacmanEngine.Components.Graphics;

namespace Program.GameObjects
{
    class Pacman : BaseGameObject, IProtagonist
    {
        public DirectionKeys PressedKeys { get; set; }

        private DirectionKeys CurrentDirection = DirectionKeys.None;
        public float Speed { get; set; } = 0.1f;    //1 cell = 16 pix // 21 x 27 cells

        public Pacman()    //constructor
        {
            Name = "Pacman";

            IsEnabled = true;

            Animation = AnimationFactory.CreateAnimation(AnimationType.PacmanRight);
        }



        public void Collide(IEnumerable<IGameObject> collisions)
        {
            foreach (var obj in collisions)     //pacman can eat only coins
                if((obj.Name == "SmallCoin") || (obj.Name == "BigCoin"))
                    obj.IsEnabled = false;

        }

        public virtual void Update()
        {
            DirectionKeys NewDirection = DirectionKeys.None;

            if (((PressedKeys & DirectionKeys.Left) == DirectionKeys.Left) /*&& (Animation.Location.X % 1 == 0) && (Animation.Location.Y % 1 == 0)*/)           //find new pressed key of direction
                NewDirection = DirectionKeys.Left;
            else if ((PressedKeys & DirectionKeys.Right) == DirectionKeys.Right)
                NewDirection = DirectionKeys.Right;
            else if ((PressedKeys & DirectionKeys.Up) == DirectionKeys.Up)
                NewDirection = DirectionKeys.Up;
            else if ((PressedKeys & DirectionKeys.Down) == DirectionKeys.Down)
                NewDirection = DirectionKeys.Down;

            if (CurrentDirection != NewDirection && NewDirection != DirectionKeys.None) //change the direction of watching
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

            switch (CurrentDirection)   //change the direction of going
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

            /*Пакман уходит влево, его координата становится 26.5, 26.6 и т.д. и он начинает показываться справа, а когда его координата становится
             равно 27, он показывается целиком слева, вот в этот момент и нужно координату обнулить, тоже самое с уходом влево,
             если координата станет равна -27, обнулить*/

            if (Math.Round(Convert.ToDecimal(Animation.Location.X)) == 0)
                Animation.Location = new Coordinate(Animation.Location.X + 21f, Animation.Location.Y);
            else if (Math.Round(Convert.ToDecimal(Animation.Location.X)) == 21)
                Animation.Location = new Coordinate(Animation.Location.X - 21f, Animation.Location.Y);

            if (Math.Round(Convert.ToDecimal(Animation.Location.Y)) == 0)
                Animation.Location = new Coordinate(Animation.Location.X, Animation.Location.Y + 27f);
            else if (Math.Round(Convert.ToDecimal(Animation.Location.Y)) == 27)
                Animation.Location = new Coordinate(Animation.Location.X, Animation.Location.Y - 27f);
        }
    }
}
