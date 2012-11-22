using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Bejeweled
{
    class GameManager
    {
        JewelManager jewelManager;
        public GameManager()
        {
            jewelManager = new JewelManager(new Vector2(0, 0));
        }
        public void Update(GameTime gameTime)
        {
            //jewelManager.Update(gameTime);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            //jewelManager.Draw(spriteBatch);
        }
    }
}
