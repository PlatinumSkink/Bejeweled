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
        JewelHandler jewelHandler;
        public GameManager()
        {
            jewelManager = new JewelManager(new Vector2(0, 0));
            jewelHandler = new JewelHandler(new Vector2(10, 10), 32, 6, new Vector2(100, 100));
        }
        public void Update(GameTime gameTime)
        {
            //jewelManager.Update(gameTime);
            jewelHandler.Update(gameTime);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            //jewelManager.Draw(spriteBatch);
            jewelHandler.Draw(spriteBatch);
        }
    }
}
