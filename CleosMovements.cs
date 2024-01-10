using SFML.System;

namespace FirstGame
{
    internal class CleosMovements : Cleo
    {
        // Установка позиции персонажа на экране
        public Vector2f position { get; private set; } = new Vector2f(0, 840); // Изменено начальное положение

        // Флаги для отслеживания нажатия движения
        bool leftPressed; // Нажата ли кнопка влево
        bool rightPressed; // Нажата ли кнопка вправо
        bool upPressed; // Нажата ли кнопка прыжка
        bool isOnGround = true; // Переменная для отслеживания, находится ли персонаж на земле (Изначально да, ахах))

        // Переменные, отвечающие за движение
        float horizontalSpeed = 0; // Горизонтальная скорость персонажа
        float verticalSpeed = 0; // Вертикальная скорость персонажа
        const float gravity = 1500; // Гравитация (по закону Ньютона F=ma, но при прыжке a = g)
        const float jumpSpeed = -600; // Скорость прыжка
        const float acceleration = 500; // Ускорение
        const float deceleration = 500; // Замедление (сопротивление некое)
        const float maxSpeed = 400; // Максимальная скорость

        // Переменные для Фаербола
        public List<Fireball> fireballs = new List<Fireball>();

        public enum Direction
        {
            Left,
            Right
        }

        public Direction currentDirection = Direction.Right;

        /// <summary>
        /// Метод, отвечающий за движение влево
        /// </summary>
        public void MoveLeft()
        {
            leftPressed = true;
            sprite.Texture = textureLeft;
            currentDirection = Direction.Left;
        }
        /// <summary>
        /// Метод, отвечающий за движение вправо
        /// </summary>
        public void MoveRight()
        {
            rightPressed = true;
            sprite.Texture = textureRight;
            currentDirection = Direction.Right;
        }
        /// <summary>
        /// Метод, отвечающий за прыжок
        /// </summary>
        public void Jump()
        {
            if (position.Y >= 840)
                verticalSpeed = jumpSpeed;
        }
        /// <summary>
        /// Метод, отвечающий за остановку движения влево
        /// </summary>
        public void StopLeft()
        {
            leftPressed = false;
        }
        /// <summary>
        /// Метод, отвечающий за остановку движения вправо
        /// </summary>
        public void StopRight()
        {
            rightPressed = false;
        }
        /// <summary>
        /// Метод, отвечающий за остановку прыжка
        /// </summary>
        public void StopJump()
        {
            upPressed = false;
        }

        /// <summary>
        /// Метод выстрела Фаербола
        /// </summary>
        public void Attack()
        {

            Vector2f fireballStartPosition = new Vector2f(position.X + (currentDirection == Direction.Right ? 240 : -50), position.Y + 50);
            Vector2f fireballVelocity = new Vector2f(currentDirection == Direction.Right ? 1000 : -1000, 0);


            fireballs.Add(new Fireball(fireballStartPosition, fireballVelocity, currentDirection));
        }

        /// <summary>
        /// Метод обновления движения
        /// </summary>
        /// <param name="elapsedTime">Передаём прошедшее время для создания движения</param
        public void Update(float elapsedTime)
        {
            // Обработка горизонтального движения
            if (leftPressed)
                horizontalSpeed -= acceleration * elapsedTime;
            if (rightPressed)
                horizontalSpeed += acceleration * elapsedTime;

            /* Замедление, когда клавиши не нажаты.
             * Jбеспечивает плавное замедление до остановки, не допуская смены направления движения из-за замедления.*/
            if (!leftPressed && !rightPressed)
            {
                /*Когда персонаж движется вправоу у него положительная скорость. 
                 * 2-ой аргумент вычисляет новую скорость после применения замедления. 
                 * Max гарантирует, что скорость не станет отрицательной (если замедление достаточно сильное)
                 (Аналогично и для второго)*/
                if (horizontalSpeed > 0)
                    horizontalSpeed = Math.Max(0, horizontalSpeed - deceleration * elapsedTime);
                else if (horizontalSpeed < 0)
                    horizontalSpeed = Math.Min(0, horizontalSpeed + deceleration * elapsedTime);
            }

            // Ограничение максимальной скорости
            horizontalSpeed = Math.Clamp(horizontalSpeed, -maxSpeed, maxSpeed);

            // Перемещение по горизонтали
            //position.X += horizontalSpeed * elapsedTime;
            position = new Vector2f(position.X + horizontalSpeed * elapsedTime, position.Y);
            // Обработка вертикального движения
            if (upPressed && isOnGround)
            {
                verticalSpeed = jumpSpeed;
                isOnGround = false; // Персонаж в воздухе
            }

            // Применение гравитации
            verticalSpeed += gravity * elapsedTime; 

            // Перемещение по вертикали
            //position.Y += verticalSpeed * elapsedTime;
            position = new Vector2f(position.X, position.Y + verticalSpeed * elapsedTime);
            // Проверка на землю и коррекция положения
            if (position.Y >= 840) // Предпологаем, что 840 - это уровень земли (т.к. у этой манды отсчёт пикселей снизу идёт)
            {
                position = new Vector2f(position.X, 840);
                verticalSpeed = 0.0f;
                isOnGround = true; // Персонаж на земле
            }

            sprite.Position = position;
        }
    }
}
