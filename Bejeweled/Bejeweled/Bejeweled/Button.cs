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
        bool holding = false;
        public Button(string text, string _textureName, Vector2 _position)
            : base(_textureName, _position)
        {
            textOn = new TextClass(text, "SegoeUIMono", Color.White, new Vector2(X + 10, Y + 10));
            PlaceText(text);
        }
        public void Update(bool clicked, bool released, Point mousePosition)
        {
            if (holding == true && released == true)
            {
                holding = false;
            }
            if (clicked == false && clickedOn == true && holding == false)
            {
                clickedOn = false;
                Load("Button");
            }
        }
        public void PlaceText(string text)
        {
            textOn = new TextClass(text, "SegoeUIMono", Color.White, new Vector2(X + 10, Y + 10));
        }
        public bool ClickedOn(bool clicked, Point mousePosition)
        {
            if (CollisionRectangle().Contains(mousePosition) == true && clickedOn == false && clicked == true)
            {
                clickedOn = true;
                Load("ButtonClicked");
                return true;
            }
            else if (clicked == false && clickedOn == true)
            {
                Load("Button");
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
