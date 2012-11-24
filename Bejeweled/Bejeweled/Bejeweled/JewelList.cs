using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Bejeweled
{
    class JewelList : Position
    {
        public List<Jewel> jewelList = new List<Jewel>();
        Vector2 WorldSize;
        int jewelSize;
        byte numberOfJewels;
        Random rand = new Random();

        public JewelList(Vector2 _WorldSize, int _jewelSize, byte _numberOfJewels, Vector2 _position)
            : base(_position)
        {
            WorldSize = _WorldSize;
            jewelSize = _jewelSize;
            numberOfJewels = _numberOfJewels;
            //NewWorld();
        }
        public void NewWorld()
        {
            for (int y = (int)WorldSize.Y - 1; y >= 0; y--)
            {
                jewelList.Add(new Jewel("Circle", new Vector2(X, Y + jewelSize * y), rand.Next(0, numberOfJewels), false));
            }
        }
        public void NewJewel(int y, int randomJewel)
        {
            jewelList.Add(new Jewel("Circle", new Vector2(X, Y + jewelSize * y), randomJewel, false));
        }
        public void Update(GameTime gameTime)
        {
            foreach (var jewel in jewelList)
            {
                jewel.Update(gameTime);
            }
        }
        public void RemoveJewel(Jewel jewel, int index)
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
