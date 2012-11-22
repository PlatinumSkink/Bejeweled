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

        public bool Selected = false;

        public Jewel(string _textureName, Vector2 _position, int randomJewel, bool _Checked)
            : base(_textureName, _position, 0, 2, new Vector2(0, 1))
        {
            RandomJewel(randomJewel);
            Checked = _Checked;
        }
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
            if (Selected == true)
            {
                color = Color.Red;
            }
            else
            {
                color = Color.White;
            }
            base.Update(gameTime);
        }
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
            Load(Name);
        }
    }
}
