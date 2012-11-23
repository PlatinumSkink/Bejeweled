using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Bejeweled
{
    class JewelHandler:Position
    {
        List<Jewel> jewelList = new List<Jewel>();
        List<int> directions = new List<int>();

        Vector2 WorldSize;
        int jewelSize = 32;
        Random rand = new Random();

        bool pressedG = false;

        byte numberOfJewels;

        public JewelHandler(Vector2 _WorldSize, int _jewelSize, byte _numberOfJewels, Vector2 _position)
            : base(_position)
        {
            WorldSize = _WorldSize;
            jewelSize = _jewelSize;
            numberOfJewels = _numberOfJewels;
            directions.Add(1);
            directions.Add(-1);
            directions.Add((int)WorldSize.X);
            directions.Add((int)-WorldSize.X);
            NewWorld();
        }
        public void NewWorld()
        {
            for (int y = 0; y < WorldSize.Y; y++)
            {
                for (int x = 0; x < WorldSize.X; x++)
                {
                    jewelList.Add(new Jewel("Circle", new Vector2(X + jewelSize * x, Y + jewelSize * y), rand.Next(0, numberOfJewels), false));
                }
            }
        }
        public void Update(GameTime gameTime) 
        {
            KeyboardState ks = Keyboard.GetState();
            foreach (var jewel in jewelList)
            {
                jewel.Update(gameTime);
            }
            if (ks.IsKeyDown(Keys.G) && pressedG == false)
            {
                CheckCourse();
                pressedG = true;
            }
            else if (ks.IsKeyUp(Keys.G) && pressedG == true)
            {
                pressedG = false;
            }
        }
        public void CheckCourse()
        {
            for (int i = 0; i < jewelList.Count; i++)
            {
                if (jewelList[i].Checked == false)
                {
                    CheckAround(jewelList[i]);
                }
            }
        }
        public void CheckAround(Jewel jewel)
        {
            jewel.Checked = true;
            foreach (int direction in directions)
            {
                int index = jewelList.IndexOf(jewel) + direction;
                if (index >= 0 && index < jewelList.Count)
                {
                    if (jewelList[index].Checked == false && jewelList[index].Name == jewel.Name)
                    {
                        CheckAround(jewelList[index]);
                    }
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Jewel jewel in jewelList)
            {
                if (jewel.Selected == false)
                {
                    
                }
                else
                {

                }
                jewel.Draw(spriteBatch);
            }
        }
    }
}
