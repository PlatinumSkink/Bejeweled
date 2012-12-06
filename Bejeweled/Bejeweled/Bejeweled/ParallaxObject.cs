using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Bejeweled
{
    class ParallaxObject : GraphicalObject
    {
        Vector2 parallaxVector = Vector2.Zero;

        //Returning data for all the positions of the paralax.
        public Vector2 ParPos
        {
            get { return parallaxVector; }
            set { parallaxVector = value; }
        }
        public float ParX
        {
            get { return parallaxVector.X; }
            set { parallaxVector.X = value; }
        }
        public float ParY
        {
            get { return parallaxVector.Y; }
            set { parallaxVector.Y = value; }
        }

        //No real difference to a Graphical Object like this.
        public ParallaxObject(string _textureName, Vector2 _position)
            : base(_textureName, _position)
        {

        }
        public void Update()
        {

        }

        //Only a... slightly larger box.
        public Rectangle box()
        {
            return new Rectangle((int)X, (int)Y, ScreenWidth, ScreenHeight);
        }

        //Special draw function for the parallax.
        public void DrawParllex(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, 
                box(), 
                new Rectangle(
                    (int)(parallaxVector.X * 1.0f),
                    (int)(parallaxVector.Y * 1.0f),
                    ScreenWidth, 
                    ScreenHeight), 
               Color.White);
        }
    }
}
