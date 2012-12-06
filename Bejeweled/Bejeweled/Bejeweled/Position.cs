using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Bejeweled
{
    //This is a class that will simply contain a position for all other objects to use. It is the top of the heirarchy. 
    //There is no need for it to be anything but abstract, a simple Vector2 is sufficient in all other cases.
    abstract class Position
    {
        Vector2 position;

        //Since it is on top of all the other objects, it contains a few static variables all other objects might want to use.
        public static ContentManager content;
        public static int ScreenWidth;
        public static int ScreenHeight;

        //A simple position.
        public Position(Vector2 _position)
        {
            position = _position;
        }

        //Functions to retrieve Vector2 X and Y, only X or only Y.
        public Vector2 Pos
        {
            get { return position; }
            set { position = value; }
        }
        public float X
        {
            get { return position.X; }
            set { position.X = value; }
        }
        public float Y
        {
            get { return position.Y; }
            set { position.Y = value; }
        }
    }
}
