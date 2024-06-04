using SFML.Graphics;
using SFML.System;

namespace FirstGame
{
    abstract class Enemy
    {
        protected Vector2f currentPosition;
        protected float speed;
        protected Sprite sprite;
        protected Texture textureLeft;
        protected Texture textureRight;
        protected Image imageLeft;
        protected Image imageRight;
        public enum EnemyDirection
		{
            Left,
            Right
        }
        public Enemy(Vector2f startPosition, float speed) 
        {
            currentPosition = startPosition;
            this.speed = speed;
            LoadTexture(currenEnemytDirection);
            SetDirection(currenEnemytDirection);
            
        }
        public EnemyDirection currenEnemytDirection = EnemyDirection.Right;
        private void LoadTexture(EnemyDirection direction)
        {
            imageLeft = new Image("D:\\Repos\\FirstGame\\EnemyLeft.png");
            textureLeft = new Texture(imageLeft);
            imageRight = new Image("D:\\Repos\\FirstGame\\EnemyRight.png");
            textureRight = new Texture(imageRight);

            // Инициализация спрайта здесь, чтобы избежать NullReferenceException
            sprite = new Sprite();
            SetDirection(direction); // Обновление текстуры спрайта
        }

        public void SetDirection(EnemyDirection direction)
        {
            if (currenEnemytDirection != direction)
            {
				currenEnemytDirection = direction;
                sprite.Texture = direction == EnemyDirection.Right ? textureRight : textureLeft;
                sprite.Position = currentPosition;
            }
        }
        public abstract bool CanSeePlayer(Vector2f playerPosition);
        public abstract void Update(float deltaTime, Vector2f playerPosition);
        public void Draw(RenderWindow window)
        {
            window.Draw(sprite);
        }
        protected void UpdateSpritePosition()
        {
            sprite.Position = currentPosition;
        }
    }
}
