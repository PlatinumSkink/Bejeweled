using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Bejeweled
{
    class Input
    {
        public string inputted;
        public Input()
        {

        }
        public void InputText()
        {
            KeyboardState ks = Keyboard.GetState();
            
        }
        public void InputNumber()
        {
            KeyboardState ks = Keyboard.GetState();
            if (ks.GetPressedKeys()[0].ToString() == "D1")
            {
                inputted += "1";
            } 
            else if (ks.GetPressedKeys()[0].ToString() == "D2")
            {
                inputted += "2";
            } 
            else if (ks.GetPressedKeys()[0].ToString() == "D3")
            {
                inputted += "3";
            } 
            else if (ks.GetPressedKeys()[0].ToString() == "D4")
            {
                inputted += "4";
            } 
            else if (ks.GetPressedKeys()[0].ToString() == "D5")
            {
                inputted += "5";
            } 
            else if (ks.GetPressedKeys()[0].ToString() == "D6")
            {
                inputted += "6";
            } 
            else if (ks.GetPressedKeys()[0].ToString() == "D7")
            {
                inputted += "7";
            } 
            else if (ks.GetPressedKeys()[0].ToString() == "D8")
            {
                inputted += "8";
            }
            else if (ks.GetPressedKeys()[0].ToString() == "D9")
            {
                inputted += "9";
            }
        }
    }
}
