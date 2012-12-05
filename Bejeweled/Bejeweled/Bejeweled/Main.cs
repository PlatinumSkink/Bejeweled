using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Bejeweled
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Main : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        HighScoreClass highScore;

        public static bool exit = false;
        
        GameManager gameManager;
        MenuManager menuManager;
        ScoreManager scoreManager;

        enum GameState
        {
            Menu,
            Game,
            Score,
            Quit
        }
        GameState gameState = GameState.Menu;

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            this.Window.Title = "Bejeweled - Niklas Cullberg";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Position.content = Content;
            Position.ScreenWidth = graphics.GraphicsDevice.Viewport.Width;
            Position.ScreenHeight = graphics.GraphicsDevice.Viewport.Height;

            highScore = new HighScoreClass(Content);

            highScore.LoadScores();
            highScore.SetScores();

            gameManager = new GameManager(graphics.GraphicsDevice.Viewport, new Vector2(50, 50), 10, 100);
            menuManager = new MenuManager();
            scoreManager = new ScoreManager(highScore, Content);
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            if (WorldVariables.newGame == true)
            {
                gameManager = new GameManager(graphics.GraphicsDevice.Viewport, new Vector2(WorldVariables.width, WorldVariables.height), WorldVariables.jewels, WorldVariables.time);
                menuManager = new MenuManager();
                scoreManager.inputName = true;
                WorldVariables.newGame = false;
            }
            if (WorldVariables.transfer == true)
            {
                scoreManager.currentScore = WorldVariables.score;
                WorldVariables.transfer = false;
            }

            if (exit == true)
            {
                this.Exit();
            }

            switch (gameState)
            {
                case GameState.Game:
                    gameManager.Update(gameTime);
                    gameState = (GameState)gameManager.CheckState();
                    if (gameState != GameState.Game)
                    {
                        gameManager.ResetState();
                    }
                    break;
                case GameState.Menu:
                    menuManager.Update(gameTime);
                    gameState = (GameState)menuManager.CheckState();
                    if (gameState != GameState.Menu)
                    {
                        menuManager.ResetState();
                    }
                    break;
                case GameState.Score:
                    scoreManager.Update(gameTime);
                    gameState = (GameState)scoreManager.CheckState();
                    if (gameState != GameState.Score)
                    {
                        scoreManager.ResetState();
                    }
                    break;
                case GameState.Quit:
                    exit = true;
                    break;
                default:
                    break;
            }
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        public void EnterGameData(int jewels, int width, int height, int time)
        {
            gameManager = new GameManager(graphics.GraphicsDevice.Viewport, new Vector2(width, height), jewels, time);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            switch (gameState)
            {
                case GameState.Menu:
                    spriteBatch.Begin();
                    menuManager.Draw(spriteBatch);
                    break;
                case GameState.Game:
                    spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.LinearWrap, null, null);
                    gameManager.ParallaxDraw(spriteBatch);
                    spriteBatch.End();
                    spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, gameManager.camera.GetViewMatrix());
                    gameManager.Draw(spriteBatch);
                    spriteBatch.End();
                    spriteBatch.Begin();
                    gameManager.UIDraw(spriteBatch);
                    break;
                case GameState.Score:
                    spriteBatch.Begin();
                    scoreManager.Draw(spriteBatch);
                    break;
                case GameState.Quit:
                    spriteBatch.Begin();
                    break;
                default:
                    break;
            }

            //spriteBatch.End();

            // TODO: Add your drawing code here
            //spriteBatch.Begin();
            //gameManager.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
