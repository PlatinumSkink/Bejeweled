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

        public Camera camera;

        public MouseInput mi = new MouseInput();

        public GameManager(Viewport viewport)
        {
            jewelManager = new JewelManager(new Vector2(0, 0));
            jewelHandler = new JewelHandler(new Vector2(30, 30), 32, 6, 3, new Vector2(0, 0));
            camera = new Camera(viewport, 
                new Rectangle(
                    (int)jewelHandler.X, 
                    (int)jewelHandler.Y, 
                    (int)jewelHandler.WorldSize.X * jewelHandler.jewelSize, 
                    (int)jewelHandler.WorldSize.Y * jewelHandler.jewelSize));
        }
        public void Update(GameTime gameTime)
        {
            //jewelManager.Update(gameTime);
            jewelHandler.Update(gameTime);
                camera.Position += mi.Movement(camera.viewport.Width, camera.viewport.Height);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            //jewelManager.Draw(spriteBatch);
            jewelHandler.Draw(spriteBatch);
        }
    }
}
