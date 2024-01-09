using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstGame
{
    internal class Fireball
    {
        public Sprite sprite;
        public Vector2f Velocity { get; set; }
        Image image;
        Texture texture;
        public Fireball(Vector2f startPosition, Vector2f velocity)
        {
            image = new Image("D:\\Repos\\FirstGame\\Cleo.png");
            texture = new Texture(image);
            sprite = new Sprite(texture) { Position = startPosition };
            Velocity = velocity;
        }

        public void Update(float elapsedTime)
        {
            sprite.Position += Velocity * elapsedTime;
        }
    }
}
