using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Bejeweled
{
    class MenuManager
    {
        int chosenWidth = 30;
        int chosenHeight = 30;
        int chosenJewels = 6;

        List<Button> options = new List<Button>();
        MouseInput mi = new MouseInput();
        public MenuManager()
        {
            options.Add(new Button("Play Game", "Button", Vector2.Zero));
            options.Add(new Button("Score", "Button", Vector2.Zero));
            options.Add(new Button("Quit", "Button", Vector2.Zero));
            for (int i = 0; i < options.Count; i++)
            {
                options[i].Pos = new Vector2(100, 100 + 60 * i);
            }
        }
        public void Update(GameTime gameTime)
        {
            foreach (Button option in options)
            {
                if (option.ClickedOn(mi.Clicked(), mi.Position))
                {
                    if (option.textOn.Text == "Play Game")
                    {
                        Play_Game();
                    }
                    else if (option.textOn.Text == "Score")
                    {
                        Score();
                    }
                    else if (option.textOn.Text == "Quit")
                    {
                        Quit();
                    }
                    else if (option.textOn.Text == "Play Game")
                    {

                    }
                    else if (option.textOn.Text == "Play Game")
                    {

                    }
                    else if (option.textOn.Text == "Play Game")
                    {

                    }
                    else if (option.textOn.Text == "Play Game")
                    {

                    }
                    else if (option.textOn.Text == "Play Game")
                    {

                    }
                    else if (option.textOn.Text == "Play Game")
                    {

                    }
                    else if (option.textOn.Text == "Play Game")
                    {

                    }
                }
            }
        }
        public void Play_Game()
        {

        }
        public void Score()
        {

        }
        public void Quit()
        {
            Main.exit = true;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var option in options)
            {
                option.Draw(spriteBatch);
            }
        }
    }
}
