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
             Engine.Run(new IGameObject[] { }); //при загрузке передаем новый обьект для отображения и обработки
        }

        abstract class BaseGameObject : IGameObject
        {
            public string Name { get; set; }
            public bool IsEnabled { get; set; } = true; //по умолчанию включен
            public Animation Animation { get; set; }
            public abstract void Update();
        }

        class Pacman : BaseGameObject, IProtagonist
        {
            public Pacman()    //конструктор класса
            {
                Animation = AnimationFactory.CreateAnimation(AnimationType.PacmanLeft);
            }

            //Animation.Location = new Coordinate(1, 2);

            public DirectionKeys PressedKeys { get; set; }

            public void Collide(IEnumerable<IGameObject> collisions)
            {
                bool rightKeyPressed = (PressedKeys & DirectionKeys.Right) == DirectionKeys.Right;

                var StepRight = new Coordinate(0.001f, 0);      //создадим обьект для смещения вправо на юнит

                if (rightKeyPressed)
                    Animation.Location += StepRight;     //прибавяем один юнит по x = сдвиг вправо

            }


            public override void Update()
            {

            }
        }


        //class Pacman : IProtagonist
        //{
        //    DirectionKeys IProtagonist.PressedKeys { set => throw new NotImplementedException(); }

        //    string IGameObject.Name => throw new NotImplementedException();

        //    bool IGameObject.IsEnabled { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        //    Animation IGameObject.Animation => throw new NotImplementedException();

        //    void IProtagonist.Collide(IEnumerable<IGameObject> collisions)
        //    {
        //        throw new NotImplementedException();
        //    }

        //    void IGameObject.Update()
        //    {
        //        throw new NotImplementedException();
        //    }
        //}

    }
}
