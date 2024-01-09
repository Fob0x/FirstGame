using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace FirstGame
{
    internal class Engine
    {
        CleosMovements Cleo;
        Image image;
        Sprite BackgroundSprite;
        static Texture BackgroundTexture;
        public View camera = new View(new Vector2f(0, 0), new Vector2f(1920, 1080));

        RenderWindow window = new RenderWindow(new VideoMode(1920, 1080), "SuperGame");

        public Engine()
        {
            window.Closed += (sender, e) => window.Close();

            image = new Image("D:\\Repos\\FirstGame\\back.jpg");
            BackgroundTexture = new Texture(image);
            BackgroundSprite = new Sprite(BackgroundTexture);
            Cleo = new CleosMovements();
        }

        void Input()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Right))
                Cleo.MoveRight();
            else
                Cleo.StopRight();

            if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
                Cleo.MoveLeft();
            else
                Cleo.StopLeft();

            if (Keyboard.IsKeyPressed(Keyboard.Key.Space))
                Cleo.Jump();
            else
                Cleo.StopJump();
        }

        void Draw()
        {
            window.Clear(Color.White);
            window.Draw(BackgroundSprite);
            window.Draw(Cleo.GetSprite());
            window.SetView(camera);
            window.Display();
        }

        void Update(float dtAsSeconds)
        {
            Cleo.Update(dtAsSeconds);
        }

        public void Start()
        {
            Clock clock = new Clock();

            while (window.IsOpen)
            {
                window.DispatchEvents();
                Time dt = clock.Restart();

                float dtAsSeconds = dt.AsSeconds();

                Input();
                Update(dtAsSeconds);
                Draw();
            }
        }
    }
}
