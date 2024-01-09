using SFML.Graphics;
using SFML.System;

namespace FirstGame
{
    internal class Fireball
    {
        public Sprite sprite;
        public Vector2f Position { get; set; }
        Image image;
        Texture texture;
        public bool mouseWasPressed;
        public Fireball(Vector2f startPosition, Vector2f position)
        {
            image = new Image("D:\\Repos\\FirstGame\\Fireball.png");
            texture = new Texture(image);
            sprite = new Sprite(texture) { Position = startPosition };
            Position = position;
        }

        public void Update(float elapsedTime)
        {
            sprite.Position += Position * elapsedTime;
        }
    }
}
