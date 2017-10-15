using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
namespace CGDD4303_Silverlight
{
    //simlpe class to create a button that can be pressed 
    class Button
    {
        //We could use multiple button constructors for different types, though for the most part I think this first one will handle well
        Vector2 position;
        Rectangle rect;
        Texture2D texture;
        bool isPressed;
        string drawText;
        public Button(Vector2 pos) //another param would be level/destination?  Clicking the button would take you to that level, a different contructor could be used to toggle a boolean in options or something
        {
            position = pos;
            //texture = Content.Load<Texture2D>("buttonTexture");
            //Rect = new Rectangle((int)position.X,(int)position.Y,width,height);
            isPressed = false;
            drawText = null;
        }
        public Button(Rectangle r, String s) //this second implementation could be used if we want to have a word in the middle of the button.  Probably a better coice then the first, which we can delete if you agree
        {
            position.X = r.X;
            position.Y = r.Y;
            drawText = s;
            isPressed = false;
        }
        public void Update()
        {
            //if mousepos intersects rectangle highlight rectangle
            //if ispressed is false and mousepos interescts and mouse lmb is down run button logic
        }
        public void Draw(SpriteBatch sb)
        {
            //sb.Draw(texture,rect);
            //if drawtext != null then draw the string in the button
        }
        public void ClickLogic() //possibly not void
        {
            //run the logic here for when you click on a button
        }
    }
}
