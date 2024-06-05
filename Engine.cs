using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;

namespace FirstGame
{
	internal class Engine
	{
		CleosMovements Cleo;
		PatrollingEnemy patrollingEnemy;
		Image image;
		Sprite BackgroundSprite;
		static Texture BackgroundTexture;

		static public View camera;
		static public RenderWindow window = new RenderWindow(new VideoMode(1920, 1080), "SuperGame");
		// Создаем список врагов как поле класса
		List<Enemy> enemies = new List<Enemy>();
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
			patrollingEnemy = new PatrollingEnemy(new Vector2f(300, 840), new Vector2f(1000, 840), 500);
			inputHandler = new InputHandler(Cleo);
			// Инициализируем врагов в конструкторе
			enemies.Add(patrollingEnemy);
		}

		void Draw()
		{
			window.Clear(Color.White);
			window.Draw(BackgroundSprite);
			window.Draw(Cleo.GetSprite());
			// Отрисовка врагов
			foreach (var enemy in enemies)
			{
				enemy.Draw(window); // Рисуем NPC на экране
			}
			foreach (var fireball in Cleo.fireballs)
			{
				window.Draw(fireball.sprite);
			}
			// Отрисовываем фаерболы врага
			foreach (var fireball in patrollingEnemy.fireballs)
			{
				window.Draw(fireball.sprite);
			}

			window.SetView(camera);
			window.Display();
		}

		void Update(float dtAsSeconds)
		{
			Cleo.Update(dtAsSeconds);

			// Обновление и отрисовка врагов
			foreach (var enemy in enemies)
			{
				enemy.Update(dtAsSeconds, Cleo.position, camera);
			}

			// Обновление фаерболов
			foreach (var fireball in Cleo.fireballs)
			{
				fireball.Update(dtAsSeconds);
			}
			// Обновляем фаерболы врага
			foreach (var fireball in patrollingEnemy.fireballs)
			{
				fireball.Update(dtAsSeconds);
			}
			camera.Center = Cleo.position;

			// Удаление фаерболов, которые пролетели слишком далеко
			Cleo.fireballs.RemoveAll(fireball => fireball.ShouldBeDestroyed());
			patrollingEnemy.fireballs.RemoveAll(fireball => fireball.ShouldBeDestroyed());
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
				//Joystick.Update();
				Draw();
			}
		}
	}
}
