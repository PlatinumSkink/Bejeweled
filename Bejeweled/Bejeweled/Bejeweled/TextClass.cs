using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Bejeweled
{
    class TextClass : Position
    {
        public string Text { get; set; }
        public SpriteFont Font { get; set; }
        public Color Color { get; set; }
        public int Number { get; set; }

        //A class that simply includes a bit of text, a font for it and a color to draw it in. As well as position.
        public TextClass(string _text, string _font, Color _color, Vector2 _position)
            : base(_position)
        {
            Text = _text;
            Load(_font);
            Color = _color;
        }

        //Load a font for the text.
        public void Load(string font)
        {
            Font = content.Load<SpriteFont>("SpriteFonts/" + font);
        }

        //Draw the string.
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Font, Text, Pos, Color);
        }
    }
}
