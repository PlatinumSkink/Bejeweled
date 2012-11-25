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

        Vector2 WorldSize;
        int jewelSize = 32;
        Random rand = new Random();

        bool pressedG = false;
        bool pressedLeft = false;

        byte numberOfJewels;
        int matchedJewels = 0;

        public JewelHandler(Vector2 _WorldSize, int _jewelSize, byte _numberOfJewels, Vector2 _position)
            : base(_position)
        {
            WorldSize = _WorldSize;
            jewelSize = _jewelSize;
            numberOfJewels = _numberOfJewels;
            /*directions.Add(1);
            directions.Add(-1);
            directions.Add((int)WorldSize.X);
            directions.Add((int)-WorldSize.X);*/
            directions.Add(new Point(1, 0));
            directions.Add(new Point(-1, 0));
            directions.Add(new Point(0, 1));
            directions.Add(new Point(0, -1));
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
                    if (fall == false && jewelList.jewelList[y].falling == true) 
                    {
                        if (jewelList.jewelList[y].CollisionRectangle().Intersects(jewelList.jewelList[y - 1].CollisionRectangle()) && jewelList.jewelList[y - 1].falling == false)
                        {
                            jewelList.jewelList[y].falling = false;
                            jewelList.jewelList[y].Y = jewelList.jewelList[y - 1].Y - jewelSize;
                        }
                    }
                }
            }
            if (ks.IsKeyDown(Keys.G) && pressedG == false)
            {
                CheckCourse();
                pressedG = true;
            }
            else if (ks.IsKeyUp(Keys.G) && pressedG == true)
            {
                pressedG = false;
            }
            if (ms.LeftButton == ButtonState.Pressed && pressedLeft == false)
            {
                pressedLeft = true;
                for (int x = 0; x < jewelLists.Count; x++)
                {
                    for (int y = (int)WorldSize.Y - 1; y >= 0; y--)
                    {
                        FoundJewels = new List<Point>();
                        matchedJewels = 0;
                        if (jewelLists[x].jewelList[y].CollisionRectangle().Contains(new Point(ms.X, ms.Y)))
                        {
                            CheckAround(jewelLists[x], jewelLists[x].jewelList[y]);
                            if (matchedJewels >= 3)
                            {
                                RemoveStuff();
                                return;
                            }
                            else
                            {

                            }
                        }
                    }
                }
            }
            else if (ms.LeftButton == ButtonState.Released && pressedLeft == true)
            {
                pressedLeft = false;
            }
        }
        public void CheckCourse()
        {
            for (int x = 0; x < jewelLists.Count; x++)
            {
                for (int y = (int)WorldSize.Y - 1; y >= 0; y--)
                {
                    FoundJewels = new List<Point>();
                    matchedJewels = 0;
                    CheckAround(jewelLists[x], jewelLists[x].jewelList[y]);
                    if (matchedJewels >= 3)
                    {
                        RemoveStuff();
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
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (JewelList jewelList in jewelLists)
            {
                jewelList.Draw(spriteBatch);
            }
        }
    }
}
