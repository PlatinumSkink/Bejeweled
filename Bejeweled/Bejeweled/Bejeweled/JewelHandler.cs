using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Bejeweled
{
    class JewelHandler:Position
    {
        List<Jewel> jewelList = new List<Jewel>();

        Vector2 WorldSize;

        byte numberOfJewels;

        public JewelHandler(Vector2 _WorldSize, Vector2 _position)
            : base(_position)
        {
            WorldSize = _WorldSize;
        }
    }
}
