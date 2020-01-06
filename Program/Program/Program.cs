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
            MazeBlue maze = new MazeBlue();

            Engine.Run(new IGameObject[] { mainPacman, maze }); //при загрузке передаем новый обьект для отображения и обработки
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
                Name = "Pacman";

                IsEnabled = true;
                
                Animation = AnimationFactory.CreateAnimation(AnimationType.PacmanRight);
                Animation.Location = new Coordinate(10, 10);    //стартовая позиция юзера
            }

            public DirectionKeys PressedKeys { get; set; }

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
                    //Animation = AnimationFactory.CreateAnimation(AnimationType.PacmanRight);
                }
                else if (leftKeyPressed)
                {
                    Animation.Location += StepLeft;
                    //Animation = AnimationFactory.CreateAnimation(AnimationType.PacmanLeft);
                }
                else if (upKeyPressed)
                {
                    Animation.Location += StepUp;
                   // Animation = AnimationFactory.CreateAnimation(AnimationType.PacmanUp);
                }
                else if (downKeyPressed)
                {
                    Animation.Location += StepDown;
                    //Animation = AnimationFactory.CreateAnimation(AnimationType.PacmanDown);
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
            }

            public override void Update()
            {
                Animation.Location = new Coordinate(0, 0);    //стартовая позиция поля
            }
        }
    }
}
