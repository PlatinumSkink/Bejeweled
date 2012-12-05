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
        public int remainingTime = 100000;

        Timer gameTimer = new Timer(10000/*0*/, true);

        public enum GameState { Menu, Game, Score, Quit }

        GameState gameState = GameState.Game;

        ParallaxObject parallax;

        public GameManager(Viewport viewport, Vector2 WorldSize, int jewels, int timeSeconds)
        {
            jewelManager = new JewelManager(new Vector2(0, 0));
            jewelHandler = new JewelHandler(WorldSize, 32, (byte)jewels, 3, new Vector2(0, 0));
            remainingTime = timeSeconds * 1000;
            gameTimer = new Timer(remainingTime, true);
            camera = new Camera(viewport, 
                new Rectangle(
                    (int)jewelHandler.X, 
                    (int)jewelHandler.Y, 
                    (int)jewelHandler.WorldSize.X * jewelHandler.jewelSize, 
                    (int)jewelHandler.WorldSize.Y * jewelHandler.jewelSize));
            parallax = new ParallaxObject("LoopFlower", Vector2.Zero);
            parallax.LimitRectangle = new Rectangle(
                    (int)jewelHandler.X, 
                    (int)jewelHandler.Y, 
                    (int)jewelHandler.WorldSize.X * jewelHandler.jewelSize, 
                    (int)jewelHandler.WorldSize.Y * jewelHandler.jewelSize);
        }
        public void Update(GameTime gameTime)
        {
            //jewelManager.Update(gameTime);
            jewelHandler.Update(gameTime);
            Vector2 rememberPosition = camera.Position;
            camera.Position += mi.Update(camera.viewport.Width, camera.viewport.Height);
            if (camera.Position.X != rememberPosition.X)
            {
                parallax.ParX += (mi.Update(camera.viewport.Width, camera.viewport.Height) / 2).X;
            }
            if (camera.Position.Y != rememberPosition.Y)
            {
                parallax.ParY += (mi.Update(camera.viewport.Width, camera.viewport.Height) / 2).Y;
            }
            
            //parallax.ParPos += mi.Update(camera.viewport.Width, camera.viewport.Height) / 2;

            gameUI.UpdateScore(jewelHandler.Score);
            gameUI.UpdateTimer(gameTimer.TimeRemaining());
            if (mi.Clicked())
            {
                jewelHandler.MouseClick(new Point(mi.Position.X + (int)camera.X - (int)camera.Origin.X, mi.Position.Y + (int)camera.Y - (int)camera.Origin.Y));
            }
            if (gameTimer.Update(gameTime)) 
            {
                gameState = GameState.Score;
            }
            parallax.Update();
        }
        public GameState CheckState()
        {
            return gameState;
        }
        public void ResetState()
        {
            WorldVariables.transfer = true;
            gameState = GameState.Game;
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
        public void ParallaxDraw(SpriteBatch spriteBatch)
        {
            parallax.DrawParllex(spriteBatch);
        }
    }
}
