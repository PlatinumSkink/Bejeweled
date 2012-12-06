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

        //A timer. First input how much time the timer is to clock.
        public Timer(int _amountOfTime, bool _active)
        {
            Maximum = _amountOfTime;
            active = _active;
        }

        //Updates timer. If active, increase "current". If the current time goes over the maximum time, then return true and reset timer.
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

        //Returns the time remaining by subtracting current from maximum.
        public int TimeRemaining()
        {
            int timeRemainingMiliseconds = Maximum - current;
            int timeRemainingSeconds = timeRemainingMiliseconds / 1000;
            return timeRemainingSeconds;
        }
    }
}
