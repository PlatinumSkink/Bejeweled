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

        GameUI gameUI = new GameUI();

        public Camera camera;

        public MouseInput mi = new MouseInput();

        public int score = 0;

        public GameManager(Viewport viewport, Vector2 WorldSize, int jewels)
        {
            jewelManager = new JewelManager(new Vector2(0, 0));
            jewelHandler = new JewelHandler(WorldSize, 32, (byte)jewels, 3, new Vector2(0, 0));
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
            camera.Position += mi.Update(camera.viewport.Width, camera.viewport.Height);

            gameUI.UpdateScore(jewelHandler.Score);
            if (mi.Clicked())
            {
                jewelHandler.MouseClick(new Point(mi.Position.X + (int)camera.X - (int)camera.Origin.X, mi.Position.Y + (int)camera.Y - (int)camera.Origin.Y));
            }
        }
        public void UIDraw(SpriteBatch spriteBatch)
        {
            gameUI.Draw(spriteBatch);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            //jewelManager.Draw(spriteBatch);
            jewelHandler.Draw(spriteBatch);
        }
    }
}
