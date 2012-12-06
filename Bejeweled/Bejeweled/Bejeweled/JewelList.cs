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
        //Class for the lines of jewels. Jewels are stacked on top of each other in individual jewel-lists.
        //The first jewels in each jewellist is in the bottom, so newly added jewels always end up on top.

        public List<Jewel> jewelList = new List<Jewel>();
        Vector2 WorldSize;
        int jewelSize;
        byte numberOfJewels;
        Random rand = new Random();

        //Jewellist needs a position, the size of the jewels and the size of the world.
        public JewelList(Vector2 _WorldSize, int _jewelSize, byte _numberOfJewels, Vector2 _position)
            : base(_position)
        {
            WorldSize = _WorldSize;
            jewelSize = _jewelSize;
            numberOfJewels = _numberOfJewels;
            //NewWorld();
        }
        //In the length of the world in height, add jewels "backwards", as in first lowest and last on top.
        public void NewWorld()
        {
            for (int y = (int)WorldSize.Y - 1; y >= 0; y--)
            {
                jewelList.Add(new Jewel("Circle", new Vector2(X, Y + jewelSize * y), rand.Next(0, numberOfJewels), false));
            }
        }
        //New jewels. Add here.
        public void NewJewel(int y, int randomJewel)
        {
            jewelList.Add(new Jewel("Circle", new Vector2(X, Y + jewelSize * y), randomJewel, false));
        }
        //Update all jewels
        public void Update(GameTime gameTime)
        {
            foreach (var jewel in jewelList)
            {
                jewel.Update(gameTime);
            }
        }
        //Remove a jewel.
        public void RemoveJewel(Jewel jewel, int index)
        {
            jewelList.Remove(jewel);
        }
        //Draw all jewels
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Jewel jewel in jewelList)
            {
                jewel.Draw(spriteBatch);
            }
        }
    }
}
