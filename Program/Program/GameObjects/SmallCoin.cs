using System;
using PacmanEngine.Components.Base;
using PacmanEngine.Components.Graphics;

namespace Program.GameObjects
{
    class SmallCoin : BaseGameObject
    {
        public SmallCoin()
        {
            Name = "SmallCoin";

            IsEnabled = true;

            Animation = AnimationFactory.CreateAnimation(AnimationType.SmallCoin);

            //рандомизируем появление монеток
            Random rnd = new Random();

            int coinX = rnd.Next(0, 21);
            int coinY = rnd.Next(0, 27);

            Animation.Location = new Coordinate(coinX, coinY);    //стартовая позиция монетки
        }

        public override void Update()
        {

        }
    }
}
