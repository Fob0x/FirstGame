using SFML.Window;
using SFML.System;
using System;

namespace FirstGame
{
	internal class InputHandler
	{
		private CleosMovements cleo;
		private bool previousMousePressed = false;
		private bool previousJoystickButtonPressed = false;
		private readonly uint joystickId = 0; // ID джойстика (обычно 0 для первого подключенного)
		private readonly float joystickThreshold = 0.1f; // Порог немного уменьшен для чувствительности

		public InputHandler(CleosMovements cleo)
		{
			this.cleo = cleo;
		}

		public void HandleInput()
		{
			HandleKeyboardInput();
			HandleMouseInput();
			HandleJoystickInput();
		}
		

		private void HandleKeyboardInput()
		{
			if (Keyboard.IsKeyPressed(Keyboard.Key.D))
				cleo.MoveRight();
			else
				cleo.StopRight();

			if (Keyboard.IsKeyPressed(Keyboard.Key.A))
				cleo.MoveLeft();
			else
				cleo.StopLeft();

			if (Keyboard.IsKeyPressed(Keyboard.Key.Space))
				cleo.Jump();
			else
				cleo.StopJump();
		}

		private void HandleMouseInput()
		{
			bool currentMousePressed = Mouse.IsButtonPressed(Mouse.Button.Left);
			if (currentMousePressed && !previousMousePressed)
				cleo.Attack();
			previousMousePressed = currentMousePressed;
		}

		private void HandleJoystickInput()
		{
			if (Joystick.IsConnected(joystickId))
			{
				Vector2f movement = GetMovement(joystickId);

				if (movement.X > joystickThreshold / 100.0f)
				{
					cleo.MoveRight();
				}
				else if (movement.X < -joystickThreshold / 100.0f)
				{
					cleo.MoveLeft();
				}
				else
				{
					cleo.StopRight();
					cleo.StopLeft();
				}

				if (Joystick.IsButtonPressed(joystickId, 0)) // Предполагаем, что кнопка 0 (A) для прыжка
				{
					cleo.Jump();
				}
				else
				{
					cleo.StopJump();
				}

				bool currentJoystickButtonPressed = Joystick.IsButtonPressed(joystickId, 2); // Предполагаем, что кнопка 1 для атаки
				if (currentJoystickButtonPressed && !previousJoystickButtonPressed)
				{
					cleo.Attack();
				}
				previousJoystickButtonPressed = currentJoystickButtonPressed;
			}
			else
			{
				Console.WriteLine("Joystick not connected");
			}
		}

		public static Vector2f GetMovement(uint joystickId)
		{
			float xAxis = Joystick.GetAxisPosition(joystickId, Joystick.Axis.X);
			float yAxis = Joystick.GetAxisPosition(joystickId, Joystick.Axis.Y);

			// Нормализация значений осей
			Vector2f movement = new Vector2f(xAxis, yAxis);
			movement /= 100.0f; // Приводим значения к диапазону [-1, 1]
			
			return movement;
		}
	}
}
