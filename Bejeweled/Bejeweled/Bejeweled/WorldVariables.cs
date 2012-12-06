using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bejeweled
{
    static class WorldVariables
    {
        //These are the variables that the entire world is to know and I don't want to worry about their location.
        //Most need to be accessed from Main at times when managers need to interact with one another.
        public static int jewels = 8;
        public static int width = 50;
        public static int height = 50;
        public static int time = 100;
        public static int score = 0;
        public static bool newGame = false;
        public static bool transfer = false;
    }
}
