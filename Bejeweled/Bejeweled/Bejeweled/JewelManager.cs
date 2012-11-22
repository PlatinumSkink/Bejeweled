using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Bejeweled
{
    class JewelManager
    {
        List<List<Jewel>> JewelLines = new List<List<Jewel>>();
        //List<Jewel> jewels = new List<Jewel>();
        List<Point> directions = new List<Point>();
        byte numberOfJewels = 0;
        Vector2 gridSize = new Vector2(10, 10);
        Random rand = new Random();
        int numberFound;

        public Vector2 managerPosition;

        bool pressedG = false;

        public JewelManager(Vector2 _managerPosition)
        {
            managerPosition = _managerPosition;
            directions.Add(new Point(1, 0));
            directions.Add(new Point(-1, 0));
            directions.Add(new Point(0, 1));
            directions.Add(new Point(0, -1));
            NewSet();
        }
        public void Update(GameTime gameTime)
        {
            foreach (var jewelLine in JewelLines)
            {
                foreach (var jewel in jewelLine)
                {
                    jewel.Update(gameTime);
                }
            }
            foreach (var jewelLine in JewelLines)
            {
                for (int i = 0; i < jewelLine.Count; i++)
                {
                    if (jewelLine[i].falling == false) 
                    {
                        //break;
                    }
                    for (int j = i; j < jewelLine.Count; j++)
                    {
                        if (j != i)                                                                                                                         
                        {
                            if (jewelLine[j].falling == false && jewelLine[i].CollisionRectangle().Intersects(jewelLine[j].CollisionRectangle())) 
                            {
                                jewelLine[i].falling = false;
                                jewelLine[i].Y = jewelLine[j].Y - jewelLine[i].Height;
                            }
                        }
                    }
                    if (i == jewelLine.Count - 1)
                    {
                        if (jewelLine[i].Y > managerPosition.Y + jewelLine[i].Height * jewelLine.Count) 
                        {
                            jewelLine[i].falling = false;
                            jewelLine[i].Y = managerPosition.Y + jewelLine[i].Height * jewelLine.Count - jewelLine[i].Height;
                        }
                    }
                }
            }
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.G) && pressedG == false)
            {
                foreach (var jewels in JewelLines)
                {
                    foreach (var jewel in jewels)
                    {
                        jewel.found = false;
                        jewel.Checked = false;
                    }
                }
                CheckSpace();
                pressedG = true;
            }
            if (ks.IsKeyUp(Keys.G) && pressedG == true)
            {
                pressedG = false;
            }
        }
        public void NewSet()
        {
            JewelLines = new List<List<Jewel>>();
            for (int i = 0; i < gridSize.X; i++)
            {
                JewelLines.Add(new List<Jewel>());
            }
            for (int i = 0; i < gridSize.Y; i++)
            {
                for (int j = 0; j < gridSize.X; j++)
                {
                    JewelLines[j].Add(new Jewel("Circle", new Vector2(j * 32, i * 32), rand.Next(0, 6), false));
                }
            }
        }
        public void CheckSpace()
        {
            for (int i = 0; i < JewelLines.Count; i++)// (List<Jewel> jewels in JewelLines)
            {
                for (int j = 0; j < JewelLines[i].Count; j++)// foreach (Jewel jewel in jewels)
                {
                    if (JewelLines[i][j].Checked == false)
                    {
                        CheckAround(JewelLines[i], JewelLines[i][j]);
                        if (numberFound >= 3)
                        {
                            RemoveFound(JewelLines[i][j].Name);
                            numberFound = 0;
                        }
                        else
                        {
                            numberFound = 0;
                            foreach (var jewels in JewelLines)
                            {
                                foreach (var jewel in jewels)
                                {
                                    jewel.found = false;
                                }
                            }
                        }
                    }
                }
            }
            foreach (var jewels in JewelLines)
            {
                foreach (var jewel in jewels)
                {
                    jewel.Checked = false;
                }
            }
        }
        public void CheckAround(List<Jewel> jewels, Jewel jewel)
        {
            jewel.Checked = true;
            jewel.found = true;
            numberFound++;
            int lineX = JewelLines.IndexOf(jewels);
            int lineY = jewels.IndexOf(jewel);

            foreach (var direction in directions) 
            {
                int X = lineX + direction.X;
                int Y = lineY + direction.Y;
                if (X > 0 && Y > 0 && X < gridSize.X && Y < gridSize.Y)
                {
                    if (JewelLines[X][Y].Checked == false && JewelLines[X][Y].Name == jewel.Name)
                    {
                        CheckAround(JewelLines[X], JewelLines[X][Y]);
                    }
                }
            }

            /*if (JewelLines[lineX - 1][lineY].Checked == false && JewelLines[lineX - 1][lineY].Name == jewel.Name)
            {
                CheckAround(JewelLines[lineX - 1], JewelLines[lineX - 1][lineY]);
            }
            if (JewelLines[lineX + 1][lineY].Checked == false && JewelLines[lineX + 1][lineY].Name == jewel.Name)
            {
                CheckAround(JewelLines[lineX + 1], JewelLines[lineX + 1][lineY]);
            }
            if (JewelLines[lineX][lineY - 1].Checked == false && JewelLines[lineX][lineY - 1].Name == jewel.Name)
            {
                CheckAround(JewelLines[lineX], JewelLines[lineX][lineY - 1]);
            }
            if (JewelLines[lineX][lineY + 1].Checked == false && JewelLines[lineX][lineY + 1].Name == jewel.Name)
            {
                CheckAround(JewelLines[lineX], JewelLines[lineX][lineY + 1]);
            }*/
        }
        public void RemoveFound(string targetName)
        {
            string rememberName = "";
            foreach (var jewels in JewelLines)
            {
                for (int i = jewels.Count -1; i >= 0; i--)
                {
                    if (jewels[i].found == true && jewels[i].Name == targetName)
                    {
                        Vector2 jewelPos = jewels[i].Pos;
                        for (int j = i; j >= 0; j--)
                        {
                            rememberName = jewels[j].Name;
                            if (j > 0)
                            {
                                jewels[j].Name = jewels[j - 1].Name;
                            }
                            else
                            {
                                jewels[j].RandomJewel(numberOfJewels);
                            }
                            jewels[j].Load(jewels[j].Name);
                            jewels[j].Y -= jewels[j].Height;
                            jewels[j].found = false;
                            jewels[j].Checked = false;
                            jewels[j].falling = true;
                        }
                        //jewels.Remove(jewels[i]);
                        //i--;
                        //jewels.Add(new Jewel("Circle", jewelPos, rand.Next(0, 6), true));
                        break;
                    }
                    else
                    {
                        jewels[i].found = false;
                    }
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (List<Jewel> jewels in JewelLines)
            {
                foreach (Jewel jewel in jewels)
                {
                    jewel.Draw(spriteBatch);
                }
            }
        }
    }
}
