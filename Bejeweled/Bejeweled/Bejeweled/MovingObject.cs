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

        //Moving objects need an acceletation, speed and direction as well as texture and position.
        public MovingObject(string _textureName, Vector2 _position, int _speed, int _acceleration, Vector2 _direction) : base (_textureName, _position)
        {
            speed = _speed;
            acceleration = _acceleration;
            direction = _direction;
        }
        //All of which is updated here. Speed increases with the acceleration, and position is increased by direction times the speed.
        public virtual void Update(GameTime gameTime)
        {
            speed += acceleration;
            Pos += direction * speed;
        }


        //Not really well-used in this game, though.
    }
}
