using SFML.Window;

namespace FirstGame
{
    internal class InputHandler
    {
        private CleosMovements cleo;
        private Fireball fireball;

        public InputHandler(CleosMovements cleo, Fireball fireball) 
        {  
            this.cleo = cleo;
            //this.fireball = fireball;
            //fireball.mouseWasPressed = false;
        }

        //// Управление джойстиком
        //uint joystickId = 0; // ID джойстика (обычно 0 для первого подключенного)
        //float joystickThreshold = 15.0f; // Порог срабатывания оси

        public void HandleInput()
        {
           
            if (Keyboard.IsKeyPressed(Keyboard.Key.Right))
                cleo.MoveRight();
            else
                cleo.StopRight();

            if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
                cleo.MoveLeft();
            else
                cleo.StopLeft();

            if (Keyboard.IsKeyPressed(Keyboard.Key.Space))
                cleo.Jump();
            else
                cleo.StopJump();

            //if (Mouse.IsButtonPressed(Mouse.Button.Left))
            //{
            //    if (!fireball.mouseWasPressed)
            //    {
            //        cleo.Attack();
            //        fireball.mouseWasPressed = true;
            //    }
            //}
            //else
            //{
            //    fireball.mouseWasPressed = false;
            //}
            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                cleo.Attack();
            }


            //if (Joystick.IsConnected(joystickId))
            //{
            //    // Проверка горизонтального движения
            //    float xAxis = Joystick.GetAxisPosition(joystickId, Joystick.Axis.X);

            //    if (xAxis > joystickThreshold)
            //        cleo.MoveRight();
            //    else if (xAxis < -joystickThreshold)
            //        cleo.MoveLeft();
            //    else
            //    {
            //        cleo.StopRight();
            //        cleo.StopLeft();
            //    }

            //    // Проверка кнопки для прыжка (например, кнопка 0)
            //    if (Joystick.IsButtonPressed(joystickId, 0))
            //        cleo.Jump();
            //    else
            //        cleo.StopJump();
            //}
        }


    }
}
