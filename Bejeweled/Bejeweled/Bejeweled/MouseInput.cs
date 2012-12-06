using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Bejeweled
{
    class MouseInput
    {
        bool clicked = false;
        Point mousePosition;
        MouseState ms;

        public Point Position
        {
            //get { return mousePosition; }
            //set { mousePosition = value; }
            get { 
                ms = Mouse.GetState();
                return new Point(ms.X, ms.Y); 
            }
            set { mousePosition = value; }
        }

        public MouseInput()
        {

        }
        //If the mouse is clicked, return true. But prevent returning true more than once unless mousebutton is released.
        public bool Clicked()
        {
            ms = Mouse.GetState();
            if (ms.LeftButton == ButtonState.Pressed && clicked == false)
            {
                clicked = true;
                return true;
            }
            if (clicked == true && ms.LeftButton == ButtonState.Released)
            {
                clicked = false;
            }
            return false;
        }
        //Same as above, except without the preventing and only the check if it is actually pressed.
        public bool IsMouseClicked()
        {
            ms = Mouse.GetState();
            if (ms.LeftButton == ButtonState.Pressed)
            {
                return true;
            }
            return false;
        }
        //Update. Checks where the mouse is, and returns a Vector2 for the camera to move when mouse is close to borders.
        public Vector2 Update(int Width, int Height)
        {
            ms = Mouse.GetState();
            Position = new Point(ms.X, ms.Y);
            Vector2 mousePosition = new Vector2(ms.X, ms.Y);
            Vector2 targetVector = Vector2.Zero;
            if (ms.X < Width / 10) 
            {
                targetVector.X = -4;
            } 
            else if (ms.X > Width - Width / 10) 
            {
                targetVector.X = 4;
            }
            if (ms.Y < Height / 10)
            {
                targetVector.Y = -4;
            }
            else if (ms.Y > Height - Height / 10)
            {
                targetVector.Y = 4;
            }

            return targetVector;
        }
    }
}
