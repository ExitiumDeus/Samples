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
    public class SplashScreen
    {
        private bool screensComplete;
        private short splashPhase, pauseCounter, fullSpeed;
        private int stopPoint;
        private float velIncrement;
        private Vector2 oogieWarePos, spsuPos, oogieWareVel, spsuVel;
        private Texture2D oogieWareTex, spsuTex, blackBackground;
        private Game1 game;

        public SplashScreen(Game1 g)
        {
            game = g;
            screensComplete = false;
            splashPhase = 0;
            pauseCounter = 60;
            velIncrement = .075f;
            fullSpeed = 5;
            stopPoint = 100;
            oogieWareTex = game.oogieWareTex;
            spsuTex = game.spsuTex;
            blackBackground = game.blackBackgroundTex;
            oogieWarePos = new Vector2(175, -100);
            spsuPos = new Vector2(175, -100);
            oogieWareVel = new Vector2(0, 0);
            spsuVel = new Vector2(0, 0);
        }

        //MAIN UPDATE
        public void UpdateSplashScreen()
        {
            HandlePhases();
            CheckForSkip();
        }

        //handles movement and animation switching
        private void HandlePhases()
        {
            //update pos
            oogieWarePos += oogieWareVel;
            spsuPos += spsuVel;

            switch (splashPhase)
            {
                    //move oogieware down
                case 0:
                    MoveDown(ref oogieWareVel, ref oogieWarePos);
                    break;

                    //slow oogieware down
                case 1:
                    SlowDown(ref oogieWareVel);
                    break;

                    //pause oogieware position, play shock animation
                case 2:
                    PauseOW();
                    break;

                    //start downward movement on oogie ware from center
                case 3:
                    DownAgain(ref oogieWareVel, ref oogieWarePos);
                    break;

                    //move spsu down
                case 4:
                    MoveDown(ref spsuVel, ref spsuPos);
                    break;

                    //slow spsu down
                case 5:
                    SlowDown(ref spsuVel);
                    break;

                    //pause spsu, play animation
                case 6:
                    PauseOW();
                    break;

                    //start downward movement from center
                case 7:
                    DownAgain(ref spsuVel, ref spsuPos);
                    break;

                    //finish splash screen
                case 8:
                    screensComplete = true;
                    break;
            }
        }

        //moves texture down
        private void MoveDown(ref Vector2 vel, ref Vector2 pos)
        {
            vel.Y = fullSpeed;
            //once reaches middle of screen, switch state to slow it down
            if (pos.Y >= stopPoint)
            {
                splashPhase += 1;
            }
        }

        //slows texture to a stop
        private void SlowDown(ref Vector2 vel)
        {
            if (vel.Y > 0)
            {
                vel.Y -= velIncrement;
            }
            //hault movement
            if (vel.Y > -.05f && vel.Y < .05f)
            {
                vel.Y = 0;
                //switch to waiting phase
                splashPhase += 1;
            }
        }

        //pauses oogieware logo in middle of the screen, plays animation
        //REMEMBER TO MAKE SEPERATE METHOD FOR SPSU LOGO
        private void PauseOW()
        {
            //START SHOCK ANIMATION

            if (pauseCounter > 0)
            {
                pauseCounter--;
            }
            if (pauseCounter == 0)
            {
                splashPhase += 1;
                pauseCounter = 60;
            }
        }

        //moves logo down from center to off the screen
        private void DownAgain(ref Vector2 vel, ref Vector2 pos)
        {
            if (vel.Y < fullSpeed)
            {
                vel.Y += velIncrement;
            }
            if (vel.Y > fullSpeed)
            {
                vel.Y = fullSpeed;
            }

            if (pos.Y > 1000)
            {
                vel.Y = 0;
                splashPhase += 1;
            }
        }

        //if you're a strewth, skip my intros
        private void CheckForSkip()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space) ||
                Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                screensComplete = true;
            }
        }


        //MAIN DRAW
        public void DrawSplashScreen(SpriteBatch sb)
        {
            sb.Draw(blackBackground, new Vector2(0, 0), Color.Black);
            sb.Draw(oogieWareTex, oogieWarePos, Color.White);
            sb.Draw(spsuTex, spsuPos, Color.White);
        }

        //Properties
        public bool ScreensComplete
        {
            set { screensComplete = value; }
            get { return screensComplete; }
        }

    }
}
