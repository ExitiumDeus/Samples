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
    public class ScreenTransition
    {
        private Texture2D blackBackground;
        private Vector2 pos, vel;
        private bool transitionDone;
        private Random randomGenerator;
        private short transitionDir, fullSpeed;
        private Game1 game;

        public ScreenTransition(Game1 g)
        {
            game = g;
            blackBackground = game.blackBackgroundTex;
            pos = new Vector2(0, 0);
            vel = new Vector2(0, 0);
            fullSpeed = 10;
            transitionDone = false;
            randomGenerator = new Random();
            transitionDir = (short)randomGenerator.Next(0, 3);
            if (transitionDir > 1)
            {
                fullSpeed = 16;
            }
        }


        //MAIN UPDATE
        public void UpdateTransition()
        {
            HandleDirection();
        }

        private void HandleDirection()
        {
            pos += vel;
            switch (transitionDir)
            {
                case(0):
                    vel.Y = -fullSpeed;
                    if (pos.Y <= (int)(-blackBackground.Height - 100))
                    {
                        transitionDone = true;
                    }
                    break;
                case(1):
                    vel.Y = fullSpeed;
                    if (pos.Y >= (int)(blackBackground.Height + 100))
                    {
                        transitionDone = true;
                    }
                    break;
                case(2):
                    vel.X = fullSpeed;
                    if (pos.X >= (int)(blackBackground.Width + 100))
                    {
                        transitionDone = true;
                    }
                    break;
                case(3):
                    vel.X = -fullSpeed;
                    if (pos.X <= (int)(-blackBackground.Width - 100))
                    {
                        transitionDone = true;
                    }
                    break;
            }
        }

        //MAIN DRAW
        public void DrawTransition(SpriteBatch sb)
        {
            sb.Draw(blackBackground, pos, Color.Black);
        }


        //PROPERTIES
        public bool TransitionDone
        {
            set { transitionDone = value; }
            get { return transitionDone; }
        }

    }
}
