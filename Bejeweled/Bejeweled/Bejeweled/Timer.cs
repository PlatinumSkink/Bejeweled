using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Bejeweled
{
    class Timer
    {
        public int Maximum { get; set; }
        public int current = 0;
        public bool active = false;
        public Timer(int _amountOfTime, bool _active)
        {
            Maximum = _amountOfTime;
            active = _active;
        }
        public bool Update(GameTime gameTime)
        {
            if (active == true)
            {
                current += gameTime.ElapsedGameTime.Milliseconds;
                if (current > Maximum)
                {
                    current = 0;
                    return true;
                }
            }
            return false;
        }
        public int TimeRemaining()
        {
            int timeRemainingMiliseconds = Maximum - current;
            int timeRemainingSeconds = timeRemainingMiliseconds / 1000;
            return timeRemainingSeconds;
        }
    }
}
