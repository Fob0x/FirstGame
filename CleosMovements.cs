using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstGame 
{
    internal class CleosMovements : Cleo
    {
        // Установка позиции персонажа на экране
        Vector2f position = new Vector2f(0,840);

        // Флаги для остлеживания нажатия движения
        bool leftPressed;
        bool rightPressed;

        // Переменные для скорости 
        float horizontalSpeed = 200; // Горизонтальная скорость персонажа
        float verticalSpeed = 0; // Вертикальная скорость персонажа
        float gravity = 500; // Наше ускорение из закона Ньютона = ускорению свободного падения
        float jumpSpeed = -400; // Начальная скорость прыжка

        public void MoveLeft()
        {
            leftPressed = true;
            sprite.Texture = textureLeft;
        }

        /// <summary>
        /// Движение персонажа вправо
        /// </summary>
        public void MoveRight()
        {
            rightPressed = true;
            sprite.Texture = textureRight;
        }

        /// <summary>
        /// Остановка движения персонажа в левую сторону
        /// </summary>
        public void StopLeft()
        {
            leftPressed = false;
        }

        /// <summary>
        /// Остановка движения персонажа в правую сторону
        /// </summary>
        public void StopRight()
        {
            rightPressed = false;
        }



        public void Update(float elapsedTime)
        {
            if (leftPressed)
                position.X -= horizontalSpeed * elapsedTime;
            if (rightPressed)
                position.X += horizontalSpeed * elapsedTime;

            sprite.Position = position;
        }
    }
}
