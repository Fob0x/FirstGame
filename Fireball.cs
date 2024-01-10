using SFML.Graphics;
using SFML.System;
using static FirstGame.CleosMovements;


namespace FirstGame
{
    internal class Fireball
    {
        public Sprite sprite;
        public Vector2f Velocity { get; set; }
        Image imageLeft;
        Image imageRight;

        Texture textureLeft;
        Texture textureRight;

        private Vector2f startPosition;
        private const float MaxDistance = 1000; // Максимальное расстояние, которое может пролететь фаербол
        
        public Fireball(Vector2f startPosition, Vector2f velocity, Direction direction)
        {
            imageLeft = new Image("D:\\Repos\\FirstGame\\FireballLeft.png");
            textureLeft = new Texture(imageLeft);
            imageRight = new Image("D:\\Repos\\FirstGame\\FireballRight.png");
            textureRight = new Texture(imageRight);
            Texture selectedTexture = direction == Direction.Right ? textureRight : textureLeft;
            sprite = new Sprite(selectedTexture) { Position = startPosition };
            Velocity = velocity;
            this.startPosition = startPosition;
        }
        public bool ShouldBeDestroyed() // По теореме Пифагора
        {
            float distance = (float)Math.Sqrt(Math.Pow(sprite.Position.X - startPosition.X, 2) + Math.Pow(sprite.Position.Y - startPosition.Y, 2));
            return distance > MaxDistance;
        }
        //public void Shooting() { }

        public void Update(float elapsedTime)
        {
            sprite.Position += Velocity * elapsedTime;
        }
    }
}
