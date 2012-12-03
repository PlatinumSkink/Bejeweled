﻿using System;
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

        List<TextClass> input = new List<TextClass>();

        bool PlacedText = false;

        bool inputingData = false;

        List<Button> options = new List<Button>();
        List<Button> arrows = new List<Button>();
        List<Button> recommendations = new List<Button>();
        MouseInput mi = new MouseInput();

        enum ValueChanging { Jewels, Lines, Rows }

        ValueChanging valueToChange = ValueChanging.Jewels;

        public enum GameState { Menu, Play, Score, Quit }

        GameState buttonPressed = GameState.Menu;

        public MenuManager()
        {
            Construct();
        }
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
            PlacedText = true;
        }
        public void Update(GameTime gameTime)
        {
            if (inputingData == true)
            {
                foreach (var text in input)
                {
                    
                }
                input[0].Text = "< " + chosenJewels + " >";
                input[1].Text = "< " + chosenWidth + " >";
                input[2].Text = "< " + chosenHeight + " >";
            }
            if (mi.Clicked() == true)
            {
                foreach (Button option in options)
                {
                    if (option.ClickedOn(true, mi.Position))
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
                        else if (option.textOn.Text == "Jewels")
                        {

                        }
                        else if (option.textOn.Text == "Lines")
                        {

                        }
                        else if (option.textOn.Text == "Rows")
                        {

                        }
                        else if (option.textOn.Text == "Enter_Data")
                        {

                        }
                        else if (option.textOn.Text == "Back")
                        {
                            Back();
                        }
                        else if (option.textOn.Text == "Basic")
                        {
                            Recommendation(6, 10, 10);
                        }
                        else if (option.textOn.Text == "Large")
                        {
                            Recommendation(8, 25, 25);
                        }
                        else if (option.textOn.Text == "Huge")
                        {
                            Recommendation(10, 50, 50);
                        }
                        else if (option.textOn.Text == "Tower")
                        {
                            Recommendation(8, 10, 40);
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
        }
        public void Back()
        {
            inputingData = false;
            options = new List<Button>();
            input = new List<TextClass>();
            Construct();
        }
        public void Enter()
        {
            buttonPressed = GameState.Play;
        }
        public void Play_Game()
        {
            options = new List<Button>();
            options.Add(new Button("Jewels", "Button", Vector2.Zero));
            options.Add(new Button("Lines", "Button", Vector2.Zero));
            options.Add(new Button("Rows", "Button", Vector2.Zero));
            options.Add(new Button("Enter_Data", "Button", Vector2.Zero));
            options.Add(new Button("Back", "Button", Vector2.Zero));
            recommendations.Add(new Button("Basic", "Button", Vector2.Zero));
            recommendations.Add(new Button("Large", "Button", Vector2.Zero));
            recommendations.Add(new Button("Huge", "Button", Vector2.Zero));
            recommendations.Add(new Button("Tower", "Button", Vector2.Zero));
            for (int i = 0; i < options.Count; i++)
            {
                options[i].Pos = new Vector2(100, 100 + 60 * i);
                options[i].PlaceText(options[i].textOn.Text);
            }
            for (int i = 0; i < recommendations.Count; i++)
            {
                recommendations[i].Pos = new Vector2(100 + 100 * i, 50);
                recommendations[i].PlaceText(recommendations[i].textOn.Text);
            }
            PlacedText = true;
            input.Add(new TextClass(chosenJewels.ToString(), "SegoeUIMono", Color.White, Vector2.Zero));
            input.Add(new TextClass(chosenWidth.ToString(), "SegoeUIMono", Color.White, Vector2.Zero));
            input.Add(new TextClass(chosenHeight.ToString(), "SegoeUIMono", Color.White, Vector2.Zero));
            for (int i = 0; i < input.Count; i++)
            {
                input[i].Pos = new Vector2(300, 100 + 60 * i);
            }
            inputingData = true;
        }
        public void Score()
        {
            buttonPressed = GameState.Score;
        }
        public void Quit()
        {
            Main.exit = true;
            buttonPressed = GameState.Quit;
        }
        public void Recommendation(int _jewels, int _width, int _height)
        {
            chosenJewels = _jewels;
            chosenWidth = _width;
            chosenHeight = _height;
        }
        public GameState CheckState()
        {
            return buttonPressed;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
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
