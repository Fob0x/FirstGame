using SFML.Graphics;

namespace FirstGame
{
    internal class Cleo
    {   
        // Объявляем объект Sprite
        public Sprite sprite; //= new Sprite(texture);
        
        // Добавляем объект Текстуры для Клео
        static protected Texture textureLeft;// = new Texture(image);
        static protected Texture textureRight;
        // Сохраняем путь к изображениям
        static Image imageLeft; // image = new Image("D:\\Repos\\FirstGame\\Cleo.png"); //System.Environment.CurrentDirectory);
        static Image imageRight;

        bool upPressed; // Ещё не реализованный прыжок
        public Cleo() // Конструктор Главной Героини Клео
        {
            imageLeft = new Image("D:\\Repos\\FirstGame\\Cleo.png");
            imageRight = new Image("D:\\Repos\\FirstGame\\CleoRight.png");
            textureLeft = new Texture(imageLeft);
            textureRight = new Texture(imageRight);
            
            sprite = new Sprite(textureLeft);
        }
        /// <summary>
        /// Метод для получения спрайта
        /// </summary>
        /// <returns>Возвращает спрайт персонажа</returns>
        public Sprite GetSprite() { return sprite; }
    }
}
