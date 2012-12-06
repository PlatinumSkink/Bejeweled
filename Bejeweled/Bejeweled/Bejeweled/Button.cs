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
        //This class is a class for a button that is to be pressed.

        public TextClass textOn { get; set; }
        bool clickedOn;
        bool holding = false;

        //The button has text on it, which is sent in when created. Otherwise texture and position is sent in as other objects.
        public Button(string text, string _textureName, Vector2 _position)
            : base(_textureName, _position)
        {
            textOn = new TextClass(text, "SegoeUIMono", Color.White, new Vector2(X + 10, Y + 10));
            PlaceText(text);
        }
        //In Update, we throw in if the player has clicked or if he has released the mouse button yet.
        public void Update(bool clicked, bool released)
        {
            //If he has released, then we can turn the holding boolean off.
            if (holding == true && released == true)
            {
                holding = false;
            }
            //If he isn't holding anymore and isn't clicking anywhere, 
            //that means we can turn off the clickedOn boolean and he can click stuff again.
            if (clicked == false && clickedOn == true && holding == false)
            {
                clickedOn = false;
                Load("Button");
            }
        }
        //Simple function for changing the text on the button.
        public void PlaceText(string text)
        {
            textOn = new TextClass(text, "SegoeUIMono", Color.White, new Vector2(X + 10, Y + 10));
        }
        //Function that checks if the button is clicked. It takes the position on the mouse and compares.
        //"clicked" is if the left mouse button is pressed. "clickedOn" is if the player has already pressed something.
        //Returns true if something is pressed.
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
        //Draws button.
        public override void Draw(SpriteBatch spriteBatch)
        { 
            base.Draw(spriteBatch);
            textOn.Draw(spriteBatch);
        }
    }
}
