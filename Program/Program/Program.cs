using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using PacmanEngine.Components.Base;
using PacmanEngine.Components.Actors;
using PacmanEngine.Components.Graphics;

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {

            Pacman mainPacman = new Pacman();

            MazeBlue mazeBlue = new MazeBlue();

            SmallCoin coin = new SmallCoin();

            Engine.Run(new IGameObject[] { mainPacman, mazeBlue, coin}); //при загрузке передаем обьекты для отображения и обработки
        }

        abstract class BaseGameObject : IGameObject
        {
            public string Name { get; set; }
            public bool IsEnabled { get; set; } //по умолчанию включен
            public Animation Animation { get; set; }
            public abstract void Update();
        }

        class Pacman : BaseGameObject, IProtagonist
        {
            public DirectionKeys PressedKeys { get; set; }
            

            public Pacman()    //конструктор класса
            {
                Name = "Pacman";

                IsEnabled = true;
                
                Animation = AnimationFactory.CreateAnimation(AnimationType.PacmanRight);

                Animation.Location = new Coordinate(10, 10);    //стартовая позиция пакмана
            }

            

            public void Collide(IEnumerable<IGameObject> collisions)
            {
               
            }

            public override void Update()
            {
                bool rightKeyPressed = (PressedKeys & DirectionKeys.Right) == DirectionKeys.Right;
                bool leftKeyPressed = (PressedKeys & DirectionKeys.Left) == DirectionKeys.Left;
                bool upKeyPressed = (PressedKeys & DirectionKeys.Up) == DirectionKeys.Up;
                bool downKeyPressed = (PressedKeys & DirectionKeys.Down) == DirectionKeys.Down;

                var StepRight = new Coordinate(0.09f, 0);      //создадим обьект для смещения вправо на юнит
                var StepLeft = new Coordinate(-0.09f, 0);      
                var StepUp = new Coordinate(0, -0.09f);      
                var StepDown = new Coordinate(0, 0.09f);

                if (rightKeyPressed)
                {
                    Animation.Location += StepRight;     //прибавляем один юнит по x = сдвиг вправо
                    Coordinate anim = Animation.Location;
                    Animation = AnimationFactory.CreateAnimation(AnimationType.PacmanRight);
                    Animation.Location = anim;
                }
                else if (leftKeyPressed)
                {
                    Animation.Location += StepLeft;
                    Coordinate anim = Animation.Location;
                    Animation = AnimationFactory.CreateAnimation(AnimationType.PacmanLeft);
                    Animation.Location = anim;
                }
                else if (upKeyPressed)
                {
                    Animation.Location += StepUp;
                    Coordinate anim = Animation.Location;
                    Animation = AnimationFactory.CreateAnimation(AnimationType.PacmanUp);
                    Animation.Location = anim;
                }
                else if (downKeyPressed)
                {
                    Animation.Location += StepDown;
                    Coordinate anim = Animation.Location;
                    Animation = AnimationFactory.CreateAnimation(AnimationType.PacmanDown);
                    Animation.Location = anim;
                }
            }
        }

        class MazeBlue : BaseGameObject
        {
            public MazeBlue()
            {
                Name = "MazeBlue";

                IsEnabled = true;

                Animation = AnimationFactory.CreateAnimation(AnimationType.MazeBlue);

                Animation.Location = new Coordinate(0, 0);    //стартовая позиция поля
            }

            public override void Update()
            {

            }
        }

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
}
