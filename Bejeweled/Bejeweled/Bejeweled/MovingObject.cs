using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Bejeweled
{
    class MovingObject : GraphicalObject
    {
        protected Vector2 direction;
        protected int speed;
        protected int acceleration;
        public MovingObject(string _textureName, Vector2 _position, int _speed, int _acceleration, Vector2 _direction) : base (_textureName, _position)
        {
            speed = _speed;
            acceleration = _acceleration;
            direction = _direction;
        }
        public virtual void Update(GameTime gameTime)
        {
            speed += acceleration;
            Pos += direction * speed;
        }
    }
}
