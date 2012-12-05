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
        public Texture2D Texture;
        //public Vector2 Position;

        Vector2 parallaxVector = Vector2.Zero;

        public float parallelX = 0;
        public float parallaxY = 0;

        public Rectangle LimitRectangle { get; set; }

        /*public Vector2 ParPos
        {
            get { return parallaxVector; }
            set { 
                parallaxVector = value; 
                if (LimitRectangle != null)
                {
                    parallaxVector.X = MathHelper.Clamp(parallaxVector.X, ((Rectangle)LimitRectangle).X, ((Rectangle)LimitRectangle).Width/* - viewport.Width*//*);
                    parallaxVector.Y = MathHelper.Clamp(parallaxVector.Y, ((Rectangle)LimitRectangle).Y, ((Rectangle)LimitRectangle).Height/* - viewport.Height*//*);
                }
            }
        }*/
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
        public ParallaxObject(string _textureName, Vector2 _position)
            : base(_textureName, _position)
        {

        }
        public void Update()
        {
            //parallaxVector.X++;
        }

        public Rectangle box()
        {
            return new Rectangle((int)X, (int)Y, Width * 13, Height * 8);
        }

        public void DrawParllex(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, 
                box(), 
                new Rectangle(
                    (int)(parallaxVector.X * 1.0f),
                    (int)(parallaxVector.Y * 1.0f),
                    Width * 13, 
                    Height * 8), 
               Color.White);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, box(), Color.White);
        }
    }
}
