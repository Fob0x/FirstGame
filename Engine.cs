
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace FirstGame
{
    internal class Engine
    {
        CleosMovements Cleo;
        Image image; // = Image("D:\\Repos\\FirstGame\\back.jpg");
        Sprite BackgroundSprite;// = new Sprite(BackgroundTexture);
        static Texture BackgroundTexture;// = new Texture(image);


        RenderWindow window = new RenderWindow(new VideoMode(1920, 1080), "SuperGame");//, Styles.Fullscreen);
        public Engine()
        {
            window.Closed += (sender, e) => window.Close();
            
            image = new("D:\\Repos\\FirstGame\\back.jpg");
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

            //if (Keyboard.IsKeyPressed(Keyboard.Key.Space))
            //    Cleo.MoveUp();

        }

        void Draw()
        {
            window.Clear(Color.White);

            window.Draw(BackgroundSprite);
            window.Draw(Cleo.GetSprite());

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
                window.DispatchEvents(); // Обработка событий окна
                Time dt = clock.Restart();

                float dtAsSeconds = dt.AsSeconds();

                Input();
                Update(dtAsSeconds);
                Draw();
            }
        }
    }
}
