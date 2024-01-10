using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace FirstGame
{
    internal class Engine
    {
        CleosMovements Cleo;
        Fireball fireball;
        Image image;
        Sprite BackgroundSprite;
        static Texture BackgroundTexture;

        static public View camera;
        static public RenderWindow window = new RenderWindow(new VideoMode(1920, 1080), "SuperGame");
        
        InputHandler inputHandler;

        public Engine()
        {
            window.Closed += (sender, e) => window.Close();
            window.SetFramerateLimit(60);
            camera = window.GetView();
            image = new Image("D:\\Repos\\FirstGame\\back.jpg");
            BackgroundTexture = new Texture(image);
            BackgroundSprite = new Sprite(BackgroundTexture);
            Cleo = new CleosMovements();
            inputHandler = new InputHandler(Cleo);
        }

        void Draw()
        {
            window.Clear(Color.White);
            window.Draw(BackgroundSprite);
            window.Draw(Cleo.GetSprite());
            foreach (var fireball in Cleo.fireballs)
            {
                window.Draw(fireball.sprite);
            }
            window.SetView(camera);
            window.Display();
        }

        void Update(float dtAsSeconds)
        {
            Cleo.Update(dtAsSeconds);
            // Обновление фаерболов
            foreach (var fireball in Cleo.fireballs)
            {
                fireball.Update(dtAsSeconds);
            }
            camera.Center = Cleo.position;
           
            // Удаление фаерболов, которые пролетели слишком далеко
            Cleo.fireballs.RemoveAll(fireball => fireball.ShouldBeDestroyed());

        }

        public void Start()
        {
            Clock clock = new Clock();

            while (window.IsOpen)
            {
                window.DispatchEvents();
                Time dt = clock.Restart();

                float dtAsSeconds = dt.AsSeconds();

                inputHandler.HandleInput();
                Update(dtAsSeconds);
                Draw();
            }
        }
    }
}
