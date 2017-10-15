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
    public class LBButton
    {
        //button states
        public bool isHighlighted, isPressed, isReleased; //public bc it needs
        //to be seen from edit menu class

        //calling in the mouse to get its rectangle and other info
        private MouseControl mouseControl;

        //to keep track of original button rectangle
        public Rectangle originalRect;

        //holds the text that will be on the button
        private string buttonText;
        //position of the text on button
        public Vector2 bTextPos;
        private short textOffset;

        private Texture2D texture;
        private Vector2 position;
        private Rectangle rect;
       

        public LBButton(Texture2D t, Vector2 p, Game1 g, MouseControl m, string b)
            
        {
            position = p;
            texture = t;
            rect = new Rectangle((int)position.X, (int)position.Y,
                (int)texture.Width, (int)texture.Height);
            buttonText = b;
            mouseControl = m;
            this.rect.Width += 10;
            this.rect.Height += 10;
            originalRect = rect;

            textOffset = 3;
            //ResetTextPos();
            ResetButtonBools();
        }


        ///MAIN UPDATE
        public void Update()

        {
            HighlightButton();
            PressButton();
            ReleaseButton();
            ClearButtonStates();

        }



        //sets all button bools to false;
        public void ResetButtonBools()
        {
            isHighlighted = false;
            isPressed = false;
            isReleased = false;


            this.rect = originalRect;
            //ResetTextPos();
        }

        //highlights a button when mouse rect intersects it
        private void HighlightButton()
        {
            if (mouseControl.rect.Intersects(this.rect) && !isPressed)
            {
                isReleased = false;
                isHighlighted = true;
                rect.Width = (originalRect.Width * 10) / 9;
                rect.Height = (originalRect.Height * 10) / 9;

                //bTextPos.X = originalRect.X + (textOffset * 2);
                //bTextPos.Y = originalRect.Y + (textOffset * 2);
            }
        }

        //allows you to press a button when you click with the mouse
        private void PressButton()
        {
            if (mouseControl.rect.Intersects(this.rect) && !isPressed
                && isHighlighted && Mouse.GetState().LeftButton ==
                ButtonState.Pressed)
            {
                isPressed = true;
                isHighlighted = false;
                rect.Width = (originalRect.Width * 9) / 10;
                rect.Height = (originalRect.Height * 9) / 10;
                rect.X = originalRect.X + 5;
                rect.Y = originalRect.Y + 5;

                //bTextPos.X = originalRect.X + (textOffset);
                //bTextPos.Y = originalRect.Y + (textOffset / 2);
            }
        }


        //allows you to release the pressed button
        //this is usually when something happens in the engine
        private void ReleaseButton()
        {
            if (mouseControl.rect.Intersects(this.rect) && isPressed
                  && Mouse.GetState().LeftButton ==
                ButtonState.Released)
            {
                isPressed = false;
                isReleased = true;
                isHighlighted = true;
                rect = originalRect;
                ResetTextPos();
            }
        }

        //if you aren't intersecting with the button at all, 
        //clear all the states
        private void ClearButtonStates()
        {
            if (!(mouseControl.rect.Intersects(this.rect)))
            {
                //ResetTextPos();
                ResetButtonBools();
            }
        }

        //resets the pos of the button text
        private void ResetTextPos()
        {
            bTextPos.X = originalRect.X + textOffset;
            bTextPos.Y = originalRect.Y + textOffset;
        }



        ///MAIN DRAW METHOD
        public void RenderButton(SpriteBatch sb)
        {
            sb.Draw(texture, rect, new Rectangle(0,0,texture.Width,texture.Height), 
                Color.White, 0f, Vector2.Zero, SpriteEffects.None, .1f);

            //draws the text on the button
            //sb.DrawString(font, buttonText, bTextPos, Color.LightGreen);
        }


        //PROPERTIES
        public MouseControl MouseControl
        {
            set { mouseControl = value; }
            get { return mouseControl; }
        }

        public string ButtonText
        {
            set { buttonText = value; }
            get { return buttonText; }
        }

        public short TextOffset
        {
            set { textOffset = value; }
            get { return textOffset; }
        }
    }
}
