using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Bejeweled
{
    class Jewel:MovingObject
    {
        public string Name;
        public bool Checked = false;
        public bool found = false;
        public bool falling = false;

        public bool up = false;

        //The jewel is an object that can move and be randomized. The number for the random jewel is sent along.
        public Jewel(string _textureName, Vector2 _position, int randomJewel, bool _Checked)
            : base(_textureName, _position, 0, 2, new Vector2(0, 1))
        {
            RandomJewel(randomJewel);
            //Checked is to see if this one appears as the result of a checking of other jewels. Which means it appears in mid-air.
            Checked = _Checked;
            if (Checked == true)
            {
                falling = true;
            }
        }
        //Update the jewel. If falling, the acceleration is constant. Otherwise stop the thing.
        //We still use the moving code in the MovingObject class.
        public override void Update(GameTime gameTime)
        {
            if (falling == true)
            {
                acceleration = 1;
            }
            else
            {
                acceleration = 0;
                speed = 0;
            }
            base.Update(gameTime);
        }
        //Depending on what number was recieved for the random jewel, load a different texture.
        public void RandomJewel(int randomJewel)
        {
            if (randomJewel == 0)
            {
                Name = "Circle";
            }
            else if (randomJewel == 1)
            {
                Name = "Square";
            }
            else if (randomJewel == 2)
            {
                Name = "Ruby";
            }
            else if (randomJewel == 3)
            {
                Name = "Platter";
            }
            else if (randomJewel == 4)
            {
                Name = "Triangle";
            }
            else if (randomJewel == 5)
            {
                Name = "Star";
            }
            else if (randomJewel == 6)
            {
                Name = "Tunnel";
            }
            else if (randomJewel == 7)
            {
                Name = "Clover";
            }
            else if (randomJewel == 8)
            {
                Name = "Pokey";
            }
            else if (randomJewel == 9)
            {
                Name = "Shuriken";
            }
            Load(Name);
        }
    }
}
