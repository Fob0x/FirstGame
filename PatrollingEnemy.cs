using SFML.Graphics;
using SFML.System;
using static FirstGame.CleosMovements;

namespace FirstGame
{
	class PatrollingEnemy : Enemy
	{
		private Vector2f leftBoundary;
		private Vector2f rightBoundary;
		private bool moving;

		// Переменные для фаербола
		public List<Fireball> fireballs = new List<Fireball>();

		// Переменные для контроля интервала между атаками
		public float attackCooldown = 2.0f; // Интервал между атаками в секундах
		public float timeSinceLastAttack = 0.0f;

		public PatrollingEnemy(Vector2f leftBoundary, Vector2f rightBoundary, float speed)
			: base(leftBoundary, speed)
		{
			this.leftBoundary = leftBoundary;
			this.rightBoundary = rightBoundary;
			moving = true;
		}

		public override bool CanSeePlayer(Vector2f playerPosition)
		{
			float detectionRadius = 500;
			return (float)Math.Sqrt(Math.Pow(sprite.Position.X - playerPosition.X, 2) + Math.Pow(sprite.Position.Y - playerPosition.Y, 2)) <= detectionRadius;
		}

		public bool IsVisibleToPlayer(View cameraView)
		{
			FloatRect visibleArea = new FloatRect(cameraView.Center.X - cameraView.Size.X / 2, cameraView.Center.Y - cameraView.Size.Y / 2, cameraView.Size.X, cameraView.Size.Y);
			return visibleArea.Intersects(sprite.GetGlobalBounds());
		}

		public void Attacks()
		{
			Vector2f fireballStartPosition = new Vector2f(sprite.Position.X + (currenEnemytDirection == EnemyDirection.Right ? 240 : -50), sprite.Position.Y + 50);
			Vector2f fireballVelocity = new Vector2f(currenEnemytDirection == EnemyDirection.Right ? 1000 : -1000, 0);

			fireballs.Add(new Fireball(fireballStartPosition, fireballVelocity, (Direction)currenEnemytDirection));
		}

		public override void Update(float deltaTime, Vector2f playerPosition, View cameraView)
		{
			if (CanSeePlayer(playerPosition) && IsVisibleToPlayer(cameraView))
			{
				timeSinceLastAttack += deltaTime;

				if (timeSinceLastAttack >= attackCooldown)
				{
					Attacks();
					timeSinceLastAttack = 0.0f; // Сброс таймера
				}
			}
			else
			{
				timeSinceLastAttack = 0.0f; // Сброс таймера, если игрок вне зоны видимости

				// Логика патрулирования
				if (moving)
				{
					if (currentPosition.X < rightBoundary.X)
					{
						currentPosition.X += speed * deltaTime;
						SetDirection(EnemyDirection.Right); // Обновление направления на "право"
					}
					else
					{
						moving = false;
					}
				}
				else
				{
					if (currentPosition.X > leftBoundary.X)
					{
						currentPosition.X -= speed * deltaTime;
						SetDirection(EnemyDirection.Left); // Обновление направления на "лево"
					}
					else
					{
						moving = true;
					}
				}

				UpdateSpritePosition();
			}
		}
	}
}
