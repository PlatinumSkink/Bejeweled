using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Bejeweled
{
    class Button : GraphicalObject
    {
        public TextClass textOn { get; set; }
        bool clickedOn;
        public Button(string text, string _textureName, Vector2 _position)
            : base(_textureName, _position)
        {
            textOn = new TextClass(text, "SegoeUIMono", Color.White, new Vector2(Pos.X + 10, Pos.Y + 10));
        }
        public bool ClickedOn(bool clicked, Point mousePosition)
        {
            if (CollisionRectangle().Contains(mousePosition) == true && clickedOn == false && clicked == true)
            {
                clickedOn = true;
                return true;
            }
            else if (clicked == false && clickedOn == true)
            {
                clickedOn = false;
            }
            return false;
        }
        public override void Draw(SpriteBatch spriteBatch)
        { 
            base.Draw(spriteBatch);
            textOn.Draw(spriteBatch);
        }
    }
}
