using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Bejeweled
{
    class GameUI
    {
        public TextClass Score { get; set; }
        public TextClass Timer { get; set; }

        //The UI is simply only two pieces of text that informs time remaining on the game and current score.
        public GameUI()
        {
            Score = new TextClass("0", "SegoeUIMono", Color.White, new Vector2(10, 10));
            Timer = new TextClass("", "Font", Color.White, new Vector2(50, Position.ScreenHeight - 50));
        }
        //This function sets the score to show correctly.
        public void UpdateScore(int score)
        {
            Score.Text = "Score: " + score.ToString();
        }
        //This function sets the time to show correctly.
        public void UpdateTimer(int time)
        {
            Timer.Text = "Remaining Time: " + time.ToString();
        }
        //Draw the two texts.
        public void Draw(SpriteBatch spriteBatch)
        {
            Score.Draw(spriteBatch);
            Timer.Draw(spriteBatch);
        }
    }
}
