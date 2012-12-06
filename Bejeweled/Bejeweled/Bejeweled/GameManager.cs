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
        JewelHandler jewelHandler;

        GameUI gameUI = new GameUI();

        public Camera camera;

        public MouseInput mi = new MouseInput();

        public int score = 0;
        public int remainingTime = 100000;

        Timer gameTimer = new Timer(10000, true);

        public enum GameState { Menu, Game, Score, Quit }

        GameState gameState = GameState.Game;

        ParallaxObject parallax;

        //The GameManager includes a JewelHandler, a timer for the game, a parallax background and a camera. All data assigned.
        public GameManager(Viewport viewport, Vector2 WorldSize, int jewels, int timeSeconds)
        {
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
        }
        //Update everything.
        public void Update(GameTime gameTime)
        {
            jewelHandler.Update(gameTime);

            //If the mouse is anywhere near the sides, the camera will move in that direction using the MouseInput Update.
            Vector2 rememberPosition = camera.Position;
            camera.Position += mi.Update(camera.viewport.Width, camera.viewport.Height);

            //If the position has changed, the parallax moves with it, although at half the speed.
            if (camera.Position.X != rememberPosition.X)
            {
                parallax.ParX += (mi.Update(camera.viewport.Width, camera.viewport.Height) / 2).X;
            }
            if (camera.Position.Y != rememberPosition.Y)
            {
                parallax.ParY += (mi.Update(camera.viewport.Width, camera.viewport.Height) / 2).Y;
            }
 
            //Updates text for score and timer.
            gameUI.UpdateScore(jewelHandler.Score);
            gameUI.UpdateTimer(gameTimer.TimeRemaining());
            
            //Loads of things happen in the JewelHandler when you press the left button. If clicked, the position of the mouse is sent.
            if (mi.Clicked())
            {
                jewelHandler.MouseClick(new Point(mi.Position.X + (int)camera.X - (int)camera.Origin.X, mi.Position.Y + (int)camera.Y - (int)camera.Origin.Y));
            }
            //If the game has not yet started, the timer should not be updated. If it has started, go ahead.
            if (jewelHandler.first == false)
            {
                //This function gives a boolean if there is time left or not. If not...
                if (gameTimer.Update(gameTime))
                {
                    //If the time is over, go to score-screen.
                    gameState = GameState.Score;
                }
            }
            parallax.Update();
        }
        //If the status of the game has changed, this function will tell so.
        public GameState CheckState()
        {
            return gameState;
        }
        //If the game ends, let the World Variables know we are transfering the data and set the status back to game for when we return to this class.
        public void ResetState()
        {
            WorldVariables.transfer = true;
            gameState = GameState.Game;
        }
        //UI is drawn in front of the game. It is drawn first.
        public void UIDraw(SpriteBatch spriteBatch)
        {
            gameUI.Draw(spriteBatch);
        }
        //Although the only game-thingie is the jewels, here the game is drawn. Special camera-spriteBatch here.
        public void Draw(SpriteBatch spriteBatch)
        {
            jewelHandler.Draw(spriteBatch);
        }
        //The parallax background is drawn after all other things. Special parallax-spriteBatch here.
        public void ParallaxDraw(SpriteBatch spriteBatch)
        {
            parallax.DrawParllex(spriteBatch);
        }
    }
}
