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
        public GameUI()
        {
            Score = new TextClass("0", "SegoeUIMono", Color.Black, new Vector2(10, 10));
        }
        public void UpdateScore(int score)
        {
            Score.Text = "Score: " + score.ToString();
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            Score.Draw(spriteBatch);
        }
    }
}
