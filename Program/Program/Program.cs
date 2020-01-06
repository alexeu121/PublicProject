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
            Engine.Run(new IGameObject[] { new Pacman { } }); //при загрузке передаем новый обьект для отображения и обработки
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
            public Pacman()    //конструктор класса
            {
                Name = "User";

                IsEnabled = true;
                
                Animation = AnimationFactory.CreateAnimation(AnimationType.PacmanRight);

            }

            public DirectionKeys PressedKeys { get; set; }

            public void Collide(IEnumerable<IGameObject> collisions)
            {
               
            }

            public override void Update()
            {
                Animation.Location = new Coordinate(10, 10);    //стартовая позиция юзера

                bool rightKeyPressed = (PressedKeys & DirectionKeys.Right) == DirectionKeys.Right;

                var StepRight = new Coordinate(0.09f, 0);      //создадим обьект для смещения вправо на юнит

                if (rightKeyPressed)
                    Animation.Location += StepRight;     //прибавяем один юнит по x = сдвиг вправо
            }
        }
    }
}
