using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Bejeweled
{
    class JewelHandler:Position
    {
        List<Point> directions = new List<Point>();

        List<JewelList> jewelLists = new List<JewelList>();
        List<Point> FoundJewels = new List<Point>();
        List<Jewel> Found = new List<Jewel>();
        List<int> FoundList = new List<int>();
        Jewel Selected;
        Point selectedPlace = Point.Zero;

        bool selected = false;

        bool Checkie = true;
        bool Switched = false;
        public bool first = true;

        Point[] switchTargets = new Point[2];

        public Vector2 WorldSize;
        public int jewelSize = 32;
        Random rand = new Random();

        bool pressedG = false;
        bool pressedLeft = false;
        //bool oneIsSelected = false;

        byte numberOfJewels;
        int matchedJewels = 0;
        int desiredNumber = 3;

        GraphicalObject SelectMarker;

        int score = 0;

        //Every time score is assigned, update the WorldWide score so HighScore class can access too.
        public int Score { 
            get { return score; }
            set 
            {
                WorldVariables.score = value;
                score = value;
            } 
        }

        //JewelHandler handles the game. All inputed values from the beginning is added.
        public JewelHandler(Vector2 _WorldSize, int _jewelSize, byte _numberOfJewels, int _desiredNumber, Vector2 _position)
            : base(_position)
        {
            WorldSize = _WorldSize;
            jewelSize = _jewelSize;
            numberOfJewels = _numberOfJewels;
            desiredNumber = _desiredNumber;
            directions.Add(new Point(1, 0));
            directions.Add(new Point(-1, 0));
            directions.Add(new Point(0, 1));
            directions.Add(new Point(0, -1));
            SelectMarker = new GraphicalObject("Selected", new Vector2(0, 0));
            NewWorld();
        }

        //Go through the entire determined WorldSize and create a new jewel at each spot.
        public void NewWorld()
        {
            for (int x = 0; x < WorldSize.X; x++)
            {
                jewelLists.Add(new JewelList(WorldSize, jewelSize, numberOfJewels, new Vector2(X + jewelSize * x, Y)));
                for (int y = (int)WorldSize.Y - 1; y >= 0; y--)
                {
                    jewelLists[x].NewJewel(y, rand.Next(0, numberOfJewels));
                }
            }
        }

        // Update the JewelHandler.
        public void Update(GameTime gameTime) 
        {
            KeyboardState ks = Keyboard.GetState();
            MouseState ms = Mouse.GetState();
            foreach (var jewelList in jewelLists)
            {
                jewelList.Update(gameTime);
                //Updating each jewel. If the jewel falls past its determined location it stops and is placed there.
                //That way having to check for collision is avoided.
                for (int y = (int)WorldSize.Y - 1; y >= 0; y--) 
                {
                    bool fall = false;
                    if (jewelList.jewelList[y].Y > Y + jewelSize * WorldSize.Y)
                    {
                        jewelList.jewelList[y].falling = false;
                        jewelList.jewelList[y].Checked = false;
                        jewelList.jewelList[y].Y = Y + jewelSize * WorldSize.Y - jewelSize;
                    }
                    if (fall == false && jewelList.jewelList[y].falling == true && y != 0) 
                    {
                        if (jewelList.jewelList[y].Y > Y + WorldSize.Y * jewelSize - jewelList.jewelList.IndexOf(jewelList.jewelList[y]) * jewelSize - jewelSize)
                        {
                            jewelList.jewelList[y].Y = Y + WorldSize.Y * jewelSize - jewelList.jewelList.IndexOf(jewelList.jewelList[y]) * jewelSize - jewelSize;
                            jewelList.jewelList[y].falling = false;
                            jewelList.jewelList[y].Checked = false;
                        }
                    }
                }
            }
            //This variable is true when the game is to check through the course for combinations.
            if (Checkie == true)
            {
                //If it is the beginning of the game, do so extra quickly.
                if (first == true)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        CheckCourse();
                        ResetCheck();
                    }
                }
                else
                {
                    //Else, only check the next when everything has stopped falling.
                    if (NoneFalling())
                    {
                        CheckCourse();
                        ResetCheck();
                    }
                }
                //When everything has stopped falling, stop checking and turn everything off to allow player input.
                if (NoneFalling())
                {
                    Checkie = false;
                    first = false;
                }
                //Arriving here means no combinations were made. This promptly makes the game move the jewels back to how they were.
                if (Switched == true)
                {
                    string rememberName = jewelLists[switchTargets[0].X].jewelList[switchTargets[0].Y].Name;
                    jewelLists[switchTargets[0].X].jewelList[switchTargets[0].Y].Name = jewelLists[switchTargets[1].X].jewelList[switchTargets[1].Y].Name;
                    jewelLists[switchTargets[1].X].jewelList[switchTargets[1].Y].Name = rememberName;
                    jewelLists[switchTargets[0].X].jewelList[switchTargets[0].Y].Load(jewelLists[switchTargets[0].X].jewelList[switchTargets[0].Y].Name);
                    jewelLists[switchTargets[1].X].jewelList[switchTargets[1].Y].Load(jewelLists[switchTargets[1].X].jewelList[switchTargets[1].Y].Name);
                    //SwitchLocationsOfJewels(switchTargets[0], switchTargets[1]);
                    //SwitchLocationsOfJewels(switchTargets[1].X, switchTargets[1].Y, switchTargets[0].X, switchTargets[0].Y);
                    Switched = false;
                }
            }
        }
        //When the mouse is pressed.
        public void MouseClick(Point ms)
        {
            pressedLeft = true;
            //If nothing is falling.
            if (NoneFalling())
            {
                Point mousePosition = new Point(ms.X, ms.Y);
                //If nothing is selected, location of the mouse will determine the selected jewel. If outside map, return. Nothing selected.
                if (selected == false)
                {
                    selectedPlace = new Point(ms.X / jewelSize, (int)WorldSize.Y - 1 - (ms.Y / jewelSize));
                    if (ms.X / jewelSize > jewelLists.Count || ms.X / jewelSize < 0 || (int)WorldSize.Y - 1 - (ms.Y / jewelSize) > jewelLists[0].jewelList.Count || (int)WorldSize.Y - 1 - (ms.Y / jewelSize) < 0)
                    {
                        return;
                    }
                    Selected = jewelLists[ms.X / jewelSize].jewelList[(int)WorldSize.Y - 1 - (ms.Y / jewelSize)];
                    selected = true;
                }
                else if (selected == true)
                {
                    //If selected, check in each direction. If pressed on jewel in any direction, switch place on them.
                    //And turn on appropriate booleans.
                    foreach (var direction in directions)
                    {
                        if (selectedPlace.X + direction.X >= jewelLists.Count || selectedPlace.X + direction.X < 0 || selectedPlace.Y + direction.Y >= jewelLists[0].jewelList.Count || selectedPlace.Y + direction.Y < 0)
                        {
                            //... And this is just a fail-safe to make sure the game doesn't check outside the course.
                        }
                        else
                        {
                            if (jewelLists[selectedPlace.X + direction.X].jewelList[selectedPlace.Y + direction.Y].CollisionRectangle().Contains(mousePosition))
                            {
                                string rememberName = Selected.Name;
                                jewelLists[selectedPlace.X].jewelList[selectedPlace.Y].Name = jewelLists[selectedPlace.X + direction.X].jewelList[selectedPlace.Y + direction.Y].Name;
                                jewelLists[selectedPlace.X + direction.X].jewelList[selectedPlace.Y + direction.Y].Name = rememberName;
                                jewelLists[selectedPlace.X].jewelList[selectedPlace.Y].Load(jewelLists[selectedPlace.X].jewelList[selectedPlace.Y].Name);
                                jewelLists[selectedPlace.X + direction.X].jewelList[selectedPlace.Y + direction.Y].Load(jewelLists[selectedPlace.X + direction.X].jewelList[selectedPlace.Y + direction.Y].Name);
                                //SwitchLocationsOfJewels(switchTargets[0], switchTargets[1]);
                                //SwitchLocationsOfJewels(selectedPlace, new Point(selectedPlace.X + direction.X, selectedPlace.Y + direction.Y));
                                Checkie = true;
                                Switched = true;
                                switchTargets[0] = selectedPlace;
                                switchTargets[1] = new Point(selectedPlace.X + direction.X, selectedPlace.Y + direction.Y);
                                break;
                            }
                        }
                    }
                    selected = false;
                }
            }
            //Turn game back to neutral state.
            ResetCheck();
        }

        //This checks through the course. It checks every non-checked jewel individually. 
        public void CheckCourse()
        {
            for (int x = 0; x < jewelLists.Count; x++)
            {
                for (int y = (int)WorldSize.Y - 1; y >= 0; y--)
                {
                    FoundJewels = new List<Point>();
                    matchedJewels = 0;
                    Found = new List<Jewel>();
                    FoundList = new List<int>();
                    FoundJewels = new List<Point>();
                    //For each jewel, reset the Found-lists series and check around the new jewel...
                    CheckAround(jewelLists[x], jewelLists[x].jewelList[y]);

                    //If number of found jewels is above or equalled the number of desired jewels, add score and remove jewels.
                    if (matchedJewels >= desiredNumber)
                    {
                        //If it isn't at the start of the game, that is.
                        if (first == false)
                        {
                            Score += (int)Math.Pow(2, ((matchedJewels - 2) * 2));
                            Console.WriteLine(Score);
                        }
                        RemoveStuff();
                        ResetCheck();
                        return;
                    }
                    else
                    {

                    }
                }
            }
        }

        //Checking around each jewel in each list it is in.
        public void CheckAround(JewelList jewelList, Jewel jewel)
        {
            jewel.Checked = true;

            //Add the jewel and its list in the Found lists.
            FoundJewels.Add(new Point(jewelLists.IndexOf(jewelList), jewelList.jewelList.IndexOf(jewel)));
            Found.Add(jewel);
            FoundList.Add(jewelLists.IndexOf(jewelList));
            jewel.found = true;
            matchedJewels++;

            //Check in each direction. If jewel in that direction has the same Name (is the same jewel), rerun this function on that.
            foreach (var direction in directions)
            {
                int x = jewelLists.IndexOf(jewelList) + direction.X;
                int y = jewelList.jewelList.IndexOf(jewel) + direction.Y;
                if (x >= 0 && x < WorldSize.X && y >= 0 && y < WorldSize.Y)
                {
                    Jewel target = jewelLists[x].jewelList[y];
                    if (target.Checked == false && target.Name == jewel.Name)
                    {
                        CheckAround(jewelLists[x], jewelLists[x].jewelList[y]);
                    }
                }
            }
        }

        //If we are to remove jewels. Check through jewels found and erase the jewels on these spots. Add new jewels for each.
        public void RemoveStuff()
        {
            int index = 0;
            int height = 0;
            Switched = false;
            foreach (Jewel jewel in Found)
            {
                int jewelCount = jewelLists[FoundList[index]].jewelList.IndexOf(jewel);
                jewelLists[FoundList[index]].jewelList.Remove(jewelLists[FoundList[index]].jewelList[jewelCount]);
                jewelLists[FoundList[index]].jewelList.Add(new Jewel("Circle", new Vector2(jewelLists[FoundList[index]].X, Y - jewelSize - jewelSize * height), rand.Next(0, numberOfJewels), true));
                height++;
                for (int i = jewelCount; i < jewelLists[FoundList[index]].jewelList.Count; i++)
                {
                    jewelLists[FoundList[index]].jewelList[i].falling = true;
                }
                index++;
            }
            Found = new List<Jewel>();
            FoundList = new List<int>();
            FoundJewels = new List<Point>();
            return;
        }
        /*public void SwitchLocationsOfJewels(Point location1, Point location2)
        {
            string rememberName = jewelLists[location1.X].jewelList[location1.Y].Name;
            jewelLists[location1.X].jewelList[location1.Y].Name = jewelLists[location2.X].jewelList[location2.Y].Name;
            jewelLists[location2.X].jewelList[location2.Y].Name = rememberName;
            jewelLists[location1.X].jewelList[location1.X].Load(jewelLists[location1.X].jewelList[location1.Y].Name);
            jewelLists[location2.X].jewelList[location2.X].Load(jewelLists[location2.X].jewelList[location2.Y].Name);                
        }*/

        //Attempt at function to switch locations on jewels. Doesn't work here, works up there. I wonder why...?
        public void SwitchLocationsOfJewels(int X1, int Y1, int X2, int Y2)
        {
            string rememberName = jewelLists[X1].jewelList[Y1].Name;
            jewelLists[X1].jewelList[Y1].Name = jewelLists[X2].jewelList[Y2].Name;
            jewelLists[X2].jewelList[Y2].Name = rememberName;
            jewelLists[X1].jewelList[X1].Load(jewelLists[X1].jewelList[Y1].Name);
            jewelLists[X2].jewelList[X2].Load(jewelLists[X2].jewelList[Y2].Name);
        }

        //Reset goes through all jewels and lists to reset everything to a immobile neutral state. This should allow player-input.
        public void ResetCheck()
        {
            foreach (JewelList jewelList in jewelLists)
            {
                foreach (Jewel jewel in jewelList.jewelList)
                {
                    jewel.Checked = false;
                    jewel.found = false;
                    if (jewel.falling == false && jewel.Y > Y + WorldSize.Y * jewelSize - jewelList.jewelList.IndexOf(jewel) * jewelSize)
                    {
                        jewel.Y = Y + WorldSize.Y * jewelSize - jewelList.jewelList.IndexOf(jewel) * jewelSize;
                    }
                }
            }
            Found = new List<Jewel>();
            FoundList = new List<int>();
            FoundJewels = new List<Point>();
        }
        //Function that simply checks if there are any jewels that are falling. If so, false. Used a lot.
        public bool NoneFalling()
        {
            foreach (JewelList jewelList in jewelLists)
            {
                foreach (Jewel jewel in jewelList.jewelList)
                {
                    if (jewel.falling == true)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        //Draw things.
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (JewelList jewelList in jewelLists)
            {
                jewelList.Draw(spriteBatch);
            }
            //If a jewel is selected, draw a rectangle around it so it is clearly visible which is selected.
            if (selected == true)
            {
                SelectMarker.Pos = new Vector2(selectedPlace.X * jewelSize, (WorldSize.Y - selectedPlace.Y - 1) * jewelSize);
                SelectMarker.Draw(spriteBatch);
            }
        }
    }
}
