using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Bejeweled
{
    class JewelHandler:Position
    {
        List<Jewel> jewelList = new List<Jewel>();
        List<Point> directions = new List<Point>();

        Vector2 WorldSize;
        int jewelSize = 32;
        Random rand = new Random();

        byte numberOfJewels;

        public JewelHandler(Vector2 _WorldSize, int _jewelSize, byte _numberOfJewels, Vector2 _position)
            : base(_position)
        {
            WorldSize = _WorldSize;
            jewelSize = _jewelSize;
            numberOfJewels = _numberOfJewels;
            directions.Add(new Point(1, 0));
            directions.Add(new Point(-1, 0));
            directions.Add(new Point(0, (int)WorldSize.X));
            directions.Add(new Point(0, (int)-WorldSize.X));
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
            foreach (var jewel in jewelList)
            {
                jewel.Update(gameTime);
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
