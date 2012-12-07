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
        //The size of the stage, number of jewels used and time spent playing is all customizable. Contained here.
        int chosenWidth = 30;
        int chosenHeight = 30;
        int chosenJewels = 6;
        int chosenDifficulty = 3;
        int chosenTime = 100;

        int[] chosenMax = new int[5];

        TextClass Title;

        List<int> chosenValues = new List<int>();

        List<TextClass> input = new List<TextClass>();

        public bool inputingData = false;

        List<Button> options = new List<Button>();
        List<Button> arrows = new List<Button>();
        List<Button> recommendations = new List<Button>();
        MouseInput mi = new MouseInput();

        byte changedValue = 0;

        public enum GameState { Menu, Game, Score, Quit }

        GameState buttonPressed = GameState.Menu;

        public MenuManager()
        {
            Construct();
        }
        //The constructor needs to be called multiple times. Hence here is a seperate one.
        //All needed values and texts are assigned and made.
        public void Construct()
        {
            options.Add(new Button("Play Game", "Button", Vector2.Zero));
            options.Add(new Button("Score", "Button", Vector2.Zero));
            options.Add(new Button("Quit", "Button", Vector2.Zero));
            for (int i = 0; i < options.Count; i++)
            {
                options[i].Pos = new Vector2(100, 100 + 60 * i);
            }
            foreach (Button option in options)
            {
                option.PlaceText(option.textOn.Text);
            }
            chosenValues = new List<int>();
            chosenValues.Add(chosenJewels);
            chosenValues.Add(chosenWidth);
            chosenValues.Add(chosenHeight);
            chosenValues.Add(chosenDifficulty);
            chosenValues.Add(chosenTime);
            chosenMax[0] = 10;
            chosenMax[1] = 100;
            chosenMax[2] = 100;
            chosenMax[3] = 10;
            chosenMax[4] = 200000;

            Title = new TextClass("Bejeweled", "Title", Color.White, new Vector2(Position.ScreenWidth - Position.ScreenWidth / 2, Position.ScreenHeight / 2 - Position.ScreenHeight / 4));
        }
        //Update things.
        public void Update(GameTime gameTime)
        {
            bool isMiPressed = mi.Clicked();
            if (inputingData == true)
            {
                //Make the text of the unselected value blue.
                foreach (var text in input)
                {
                    if (input.IndexOf(text) == changedValue)
                    {
                        text.Color = Color.White;
                    }
                    else
                    {
                        text.Color = Color.Blue;
                    }
                }
                for (int i = 0; i < chosenValues.Count; i++)
                {
                    input[i].Text = "< " + chosenValues[i] + " >";
                }
            }
            //Update all buttons.
            foreach (Button option in options)
            {
                option.Update(isMiPressed, mi.IsMouseClicked() == false);
            }
            foreach (Button recommendation in recommendations)
            {
                recommendation.Update(isMiPressed, mi.IsMouseClicked() == false);
            }
            if (isMiPressed == true)
            {
                foreach (Button option in options)
                {
                    if (option.ClickedOn(isMiPressed, mi.Position))
                    {
                        //If a button is pressed, run the appropriate function. 
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
                            //And if pressed a button which simply changes what value to change, change it.
                        else if (option.textOn.Text == "Jewels")
                        {
                            changedValue = 0;
                        }
                        else if (option.textOn.Text == "Lines")
                        {
                            changedValue = 1;
                        }
                        else if (option.textOn.Text == "Rows")
                        {
                            changedValue = 2;
                        }
                        else if (option.textOn.Text == "Difficulty")
                        {
                            changedValue = 3;
                        }
                        else if (option.textOn.Text == "Time")
                        {
                            changedValue = 4;
                        }
                        else if (option.textOn.Text == "Enter Data")
                        {
                            inputingData = false;
                            Enter();
                        }
                        else if (option.textOn.Text == "Back")
                        {
                            Back();
                        }
                        return;
                    }
                }
                //If pressed on a recommendation, change to recommendation.
                foreach (Button recommendation in recommendations)
                {
                    if (recommendation.ClickedOn(isMiPressed, mi.Position))
                    {
                        int[] values = new int[5];
                        if (recommendation.textOn.Text == "Basic")
                        {
                            values[0] = 6;
                            values[1] = 10;
                            values[2] = 10;
                            values[3] = 3;
                            values[4] = 60;
                        }
                        else if (recommendation.textOn.Text == "Large")
                        {
                            values[0] = 8;
                            values[1] = 25;
                            values[2] = 25;
                            values[3] = 3;
                            values[4] = 80;
                        }
                        else if (recommendation.textOn.Text == "Huge")
                        {
                            values[0] = 10;
                            values[1] = 50;
                            values[2] = 50;
                            values[3] = 3;
                            values[4] = 100;
                        }
                        else if (recommendation.textOn.Text == "Tower")
                        {
                            values[0] = 8;
                            values[1] = 10;
                            values[2] = 40;
                            values[3] = 3;
                            values[4] = 80;
                        }
                        Recommendation(values);
                        return;
                    }
                }
                //If pressed anywhere else, change value.
                if (inputingData == true)
                {
                    CheckClick();
                }
            }
        }
        //If left of a certain spot, decrease value. Else, increase. Clamp it within desired range.
        public void CheckClick()
        {
            if (mi.Position.X > input[0].X + 30)
            {
                chosenValues[changedValue]++;
            }
            else
            {
                chosenValues[changedValue]--;
            }
            chosenValues[changedValue] = (int)MathHelper.Clamp(chosenValues[changedValue], 2, chosenMax[changedValue]);
        }
        //If going back, restore everything to original status and run the constructor again.
        public void Back()
        {
            inputingData = false;
            options = new List<Button>();
            input = new List<TextClass>();
            recommendations = new List<Button>();
            Construct();
        }
        //When playing, run all decided parameters into the WorldVariables so the gameManager can pick them up.
        public void Enter()
        {
            buttonPressed = GameState.Game;
            WorldVariables.jewels = chosenValues[0];
            WorldVariables.width = chosenValues[1];
            WorldVariables.height = chosenValues[2];
            WorldVariables.difficulty = chosenValues[3];
            WorldVariables.time = chosenValues[4];
            WorldVariables.newGame = true;
        }
        //Play the game creates a new set of buttons for the player to input their desired game data.
        public void Play_Game()
        {
            options = new List<Button>();
            options.Add(new Button("Jewels", "Button", Vector2.Zero));
            options.Add(new Button("Lines", "Button", Vector2.Zero));
            options.Add(new Button("Rows", "Button", Vector2.Zero));
            options.Add(new Button("Difficulty", "Button", Vector2.Zero));
            options.Add(new Button("Time", "Button", Vector2.Zero));
            options.Add(new Button("Enter Data", "Button", Vector2.Zero));
            options.Add(new Button("Back", "Button", Vector2.Zero));
            recommendations.Add(new Button("Basic", "Button", Vector2.Zero));
            recommendations.Add(new Button("Large", "Button", Vector2.Zero));
            recommendations.Add(new Button("Huge", "Button", Vector2.Zero));
            recommendations.Add(new Button("Tower", "Button", Vector2.Zero));
            for (int i = 0; i < options.Count; i++)
            {
                options[i].Pos = new Vector2(100, 100 + 50 * i);
                options[i].PlaceText(options[i].textOn.Text);
            }
            for (int i = 0; i < recommendations.Count; i++)
            {
                recommendations[i].Pos = new Vector2(100 + 140 * i, 50);
                recommendations[i].PlaceText(recommendations[i].textOn.Text);
            }
            for (int i = 0; i < chosenValues.Count; i++)
            {
                input.Add(new TextClass(chosenValues[i].ToString(), "SegoeUIMono", Color.White, Vector2.Zero));
            }
            for (int i = 0; i < input.Count; i++)
            {
                input[i].Pos = new Vector2(300, 100 + 50 * i);
            }
            inputingData = true;
        }
        //Score pressed, change state to score.
        public void Score()
        {
            buttonPressed = GameState.Score;
        }
        //Pressed quit, quit.
        public void Quit()
        {
            Main.exit = true;
            buttonPressed = GameState.Quit;
        }
        //If pressed on a recommendation, input the data of the recommendation.
        public void Recommendation(int[] _values)
        {
            for (int i = 0; i < _values.Length; i++)
            {
                chosenValues[i] = _values[i];
            }
        }
        //Check the state and from there return what state to go to.
        public GameState CheckState()
        {
            return buttonPressed;
        }
        //When going back, the state in the menu needs to be reset or the menu will be closed immediately upon return.
        public void ResetState()
        {
            buttonPressed = GameState.Menu;
        }
        //Draw all buttons and such.
        public void Draw(SpriteBatch spriteBatch)
        {
            if (inputingData == false)
            {
                Title.Draw(spriteBatch);
            }
            foreach (var option in options)
            {
                option.Draw(spriteBatch);
            }
            foreach (var recommendation in recommendations)
            {
                recommendation.Draw(spriteBatch);
            }
            foreach (var text in input)
            {
                text.Draw(spriteBatch);
            }
        }
    }
}
