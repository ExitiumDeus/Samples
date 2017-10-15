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


    class MainMenu : Level
    {
        private Texture2D background;
        private MouseControl mouseControl;
        private LBButton playButton, creditsButton;
        private Game1 game;
        private short buttonCounter;

        public MainMenu(string t, Game1 g, string l)
            :base(t, g, l)
            
        {
        
            //set texture
            game = g;
            background = game.computerBackgroundTex;
            mouseControl = new MouseControl(game);
            playButton = new LBButton(game.playButtonTex, new Vector2(360, 320), game,
                mouseControl, "play");
            creditsButton = new LBButton(game.creditsButtonTex, new Vector2(360, 425), game,
                mouseControl, "exit");
            buttonCounter = 60;
        }

        public override void Update()
        {
            //update mouse and buttons

            mouseControl.UpdateMouse();
            playButton.Update();
            creditsButton.Update();

            PlayGame();
            Credits();

            //DONT DO BASE.UPDATE!!!

        }

        private void PlayGame()
        {
            if (playButton.isReleased)
            {
                game.currentLevel = game.tutorialLevel;
                game.playingGame = true;
            }
        }

        private void Credits()
        {
            if (buttonCounter > 0)
            {
                buttonCounter--;
            }

            if (buttonCounter == 0 && creditsButton.isReleased)
            {
                buttonCounter = 60;
                //credits here instead i guess
                game.InitializeCredits();
            }
        }



        public override void Draw(SpriteBatch sb)
        {
            //draw background, mouse, and objects

            sb.Draw(background, new Vector2(0, 0), new Rectangle(0,0,background.Width, background.Height), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.9999999f);
            
            playButton.RenderButton(sb);
            creditsButton.RenderButton(sb);
            mouseControl.RenderMouse(sb);

            //DONT DO BASE.DRAW!!!
        }
   
            

       

        //public override void SpawnEnemies()
        //{
        //    //should remain empty
        //    //base.SpawnEnemies();
        //}
        //public override void SpawnObjects()
        //{
        //    //should remain empty
        //    //base.SpawnObjects();
        //}
    }
}
