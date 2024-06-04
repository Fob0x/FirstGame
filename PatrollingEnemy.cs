using SFML.System;

namespace FirstGame
{
    class PatrollingEnemy : Enemy
    {
        private Vector2f leftBoundary;
        private Vector2f rightBoundary;
        private bool movingRight;
        public PatrollingEnemy(Vector2f leftBoundary, Vector2f rightBoundary, float speed)
        : base(leftBoundary, speed)
        {
            this.leftBoundary = leftBoundary;
            this.rightBoundary = rightBoundary;
            movingRight = true;
        }
        // Переменные для Фаербола
        public List<Fireball> fireballs = new List<Fireball>();

        public override bool CanSeePlayer(Vector2f playerPosition)
        {
            float detectionRadius = 500;
            return (float)Math.Sqrt(Math.Pow(sprite.Position.X - playerPosition.X, 2) + Math.Pow(sprite.Position.Y - playerPosition.Y, 2)) <= detectionRadius;
        }
        Direction direction;
        //void Attack()
        //{

        //    Vector2f fireballStartPosition = new Vector2f(sprite.Position.X + (currentDirection == Direction.Right ? 240 : -50), sprite.Position.Y + 50);
        //    Vector2f fireballVelocity = new Vector2f(currentDirection == Direction.Right ? 1000 : -1000, 0);


        //    fireballs.Add(new Fireball(fireballStartPosition, fireballVelocity, currentDirection));
        //}
        public override void Update(float deltaTime, Vector2f playerPosition)
        {
            if (CanSeePlayer(playerPosition))
            {
                // Реализуем логику атаки
                //Attack();
            }
            else
            {
            // Логика патрулирования
                if (movingRight)
                {
                    if (currentPosition.X < rightBoundary.X)
                    {
                        currentPosition.X += speed * deltaTime;
                        SetDirection(Direction.Right); // Обновление направления на "право"
                    }
                    else
                    {
                        movingRight = false;
                    }
                }
                else
                {
                    if (currentPosition.X > leftBoundary.X)
                    {
                        currentPosition.X -= speed * deltaTime;
                        SetDirection(Direction.Left); // Обновление направления на "лево"
                    }
                    else
                    {
                        movingRight = true;
                    }
                }

                UpdateSpritePosition();
            }
        }
    }
}
