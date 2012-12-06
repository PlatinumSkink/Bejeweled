using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Bejeweled
{
    class GraphicalObject:Position
    {
        protected Texture2D texture;
        protected Color color = Color.White;
        
        //Graphical object class is simply and only a texture-class. It takes a texture and has a position.
        public GraphicalObject(string _textureName, Vector2 _position)
            : base(_position)
        {
            Load(_textureName);
        }
        //A string is used to find the name of the texture, which is then loaded from the Graphics folder.
        public void Load(string textureName)
        {
            texture = content.Load<Texture2D>("Graphics/" + textureName);
        }

        //These two functions simply gets the width and height of the texture and therefore the object.
        public int Width
        {
            get { return texture.Width; }
        }
        public int Height
        {
            get { return texture.Height; }
        }

        //A rectangle to be used for drawing and check if a pressable object contains the mouse.
        public Rectangle CollisionRectangle()
        {
            return new Rectangle((int)X, (int)Y, Width, Height);
        }

        //Draw the thing.
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, CollisionRectangle(), color);
        }
    }
}
