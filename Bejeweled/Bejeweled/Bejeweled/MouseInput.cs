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
        public MouseInput()
        {

        }
        public Vector2 Movement(int Width, int Height)
        {
            MouseState ms = Mouse.GetState();
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
