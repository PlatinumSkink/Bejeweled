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
        //List<Jewel> jewelList = new List<Jewel>();
        //List<int> directions = new List<int>();
        List<Point> directions = new List<Point>();

        List<JewelList> jewelLists = new List<JewelList>();
        List<Point> FoundJewels = new List<Point>();
        List<Jewel> Found = new List<Jewel>();
        List<int> FoundList = new List<int>();
        Jewel Selected;
        Point selectedPlace = Point.Zero;

        bool selected = false;

        bool Checkie = true;

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

        public JewelHandler(Vector2 _WorldSize, int _jewelSize, byte _numberOfJewels, int _desiredNumber, Vector2 _position)
            : base(_position)
        {
            WorldSize = _WorldSize;
            jewelSize = _jewelSize;
            numberOfJewels = _numberOfJewels;
            desiredNumber = _desiredNumber;
            /*directions.Add(1);
            directions.Add(-1);
            directions.Add((int)WorldSize.X);
            directions.Add((int)-WorldSize.X);*/
            directions.Add(new Point(1, 0));
            directions.Add(new Point(-1, 0));
            directions.Add(new Point(0, 1));
            directions.Add(new Point(0, -1));
            SelectMarker = new GraphicalObject("Selected", new Vector2(0, 0));
            NewWorld();
        }
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
        public void Update(GameTime gameTime) 
        {
            KeyboardState ks = Keyboard.GetState();
            MouseState ms = Mouse.GetState();
            foreach (var jewelList in jewelLists)
            {
                jewelList.Update(gameTime);
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
                        //if (jewelList.jewelList[y].CollisionRectangle().Intersects(jewelList.jewelList[y - 1].CollisionRectangle()) && jewelList.jewelList[y - 1].falling == false)

                        //if (jewelList.jewelList[y].Y + jewelSize >= jewelList.jewelList[y - 1].Y && jewelList.jewelList[y - 1].falling == false)
                        if (jewelList.jewelList[y].Y > Y + WorldSize.Y * jewelSize - jewelList.jewelList.IndexOf(jewelList.jewelList[y]) * jewelSize - jewelSize)
                        {
                            jewelList.jewelList[y].Y = Y + WorldSize.Y * jewelSize - jewelList.jewelList.IndexOf(jewelList.jewelList[y]) * jewelSize - jewelSize;
                        /*}
                        {*/
                            jewelList.jewelList[y].falling = false;
                            jewelList.jewelList[y].Checked = false;
                            //jewelList.jewelList[y].Y = jewelList.jewelList[y - 1].Y - jewelSize;
                        }
                    }
                }
            }
            if (Checkie == true)
            {
                /*if (NoneFalling())
                {*/
                //for (int i = 0; i < 10; i++)
                //{
                    CheckCourse();
                    //}
                    ResetCheck();
                //}
                if (NoneFalling())
                {
                    Checkie = false;
                }
            }
            if (ks.IsKeyDown(Keys.G) && pressedG == false)
            {
                if (NoneFalling())
                {
                    CheckCourse();
                }
                ResetCheck();
                pressedG = true;
            }
            else if (ks.IsKeyUp(Keys.G) && pressedG == true)
            {
                pressedG = false;
            }
            if (ms.LeftButton == ButtonState.Pressed && pressedLeft == false)
            {
                
            }
            else if (ms.LeftButton == ButtonState.Released && pressedLeft == true)
            {
                pressedLeft = false;
            }
        }
        public void MouseClick(Point ms)
        {
            pressedLeft = true;
            if (NoneFalling())
            {
                Point mousePosition = new Point(ms.X, ms.Y);
                if (selected == false)
                {
                    selectedPlace = new Point(ms.X / jewelSize, (int)WorldSize.Y - 1 - (ms.Y / jewelSize));
                    if (ms.X / jewelSize > jewelLists.Count || ms.X / jewelSize < 0 || (int)WorldSize.Y - 1 - (ms.Y / jewelSize) > jewelLists[0].jewelList.Count || (int)WorldSize.Y - 1 - (ms.Y / jewelSize) < 0)
                    {
                        return;
                    }
                    //jewelLists[ms.X / jewelSize].jewelList[(int)WorldSize.Y - 1 - (ms.Y / jewelSize)].Selected = true;
                    Selected = jewelLists[ms.X / jewelSize].jewelList[(int)WorldSize.Y - 1 - (ms.Y / jewelSize)];


                    for (int x = 0; x < jewelLists.Count; x++)
                    {
                        for (int y = (int)WorldSize.Y - 1; y >= 0; y--)
                        {
                            FoundJewels = new List<Point>();
                            matchedJewels = 0;
                            if (jewelLists[x].jewelList[y].CollisionRectangle().Contains(new Point(ms.X, ms.Y)))
                            {
                                CheckAround(jewelLists[x], jewelLists[x].jewelList[y]);
                                if (matchedJewels >= desiredNumber)
                                {
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
                    selected = true;
                }
                else if (selected == true)
                {
                    foreach (var direction in directions)
                    {
                        if (selectedPlace.X + direction.X >= jewelLists.Count || selectedPlace.X + direction.X < 0 || selectedPlace.Y + direction.Y >= jewelLists[0].jewelList.Count || selectedPlace.Y + direction.Y < 0)
                        {
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
                                Checkie = true;
                                break;
                            }
                        }
                    }
                    selected = false;
                }
            }
            ResetCheck();
        }
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
                    CheckAround(jewelLists[x], jewelLists[x].jewelList[y]);
                    if (matchedJewels >= desiredNumber)
                    {
                        RemoveStuff();
                        ResetCheck();
                        return;
                    }
                    else
                    {

                    }
                }
            }
            /*for (int i = 0; i < jewelList.Count; i++)
            {
                if (jewelList[i].Checked == false)
                {
                    CheckAround(jewelList[i]);
                }
            }*/
        }
        public void CheckAround(JewelList jewelList, Jewel jewel)
        {
            jewel.Checked = true;
            FoundJewels.Add(new Point(jewelLists.IndexOf(jewelList), jewelList.jewelList.IndexOf(jewel)));
            Found.Add(jewel);
            FoundList.Add(jewelLists.IndexOf(jewelList));
            jewel.found = true;
            matchedJewels++;
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
                //int index = jewelLists[]
                /*int index = jewelList.IndexOf(jewel) + direction;
                if (index >= 0 && index < jewelList.Count)
                {
                    if (jewelList[index].Checked == false && jewelList[index].Name == jewel.Name)
                    {
                        CheckAround(jewelList[index]);
                    }
                }*/
            }
        }
        public void RemoveStuff()
        {
            /*foreach (Point position in FoundJewels)
                        {
                            jewelLists[position.X].jewelList.Remove(jewelLists[position.X].jewelList[position.Y]);
                        }*/
            int index = 0;
            int height = 0;
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
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (JewelList jewelList in jewelLists)
            {
                jewelList.Draw(spriteBatch);
            }
            if (selected == true)
            {
                SelectMarker.Pos = new Vector2(selectedPlace.X * jewelSize, (WorldSize.Y - selectedPlace.Y - 1) * jewelSize);
                SelectMarker.Draw(spriteBatch);
            }
        }
    }
}
