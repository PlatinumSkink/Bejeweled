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
        Texture2D texture;
        protected Color color = Color.White;
        
        public GraphicalObject(string _textureName, Vector2 _position)
            : base(_position)
        {
            Load(_textureName);
        }
        public void Load(string textureName)
        {
            texture = content.Load<Texture2D>("Graphics/" + textureName);
        }
        public int Width
        {
            get { return texture.Width; }
        }
        public int Height
        {
            get { return texture.Height; }
        }

        public Rectangle CollisionRectangle()
        {
            return new Rectangle((int)X, (int)Y, Width, Height);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, CollisionRectangle(), color);
        }
    }
}
