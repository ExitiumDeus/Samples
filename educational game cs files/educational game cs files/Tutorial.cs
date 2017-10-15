using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace CGDD4303_Silverlight
{
    public class Tutorial : Event
    {
        private string eventName;
        private string eventType;
        private bool tutorialFinished;
        private Texture2D tutorialBackground, tutorialTex;
        private Vector2 backgroundPos, textPos;

        public Tutorial(Vector2 p, Texture2D t, string n, string en, string et,
            Game1 g, GameObject o)
            : base(p, t, n, g, o)
        {
            eventName = en;
            eventType = et;
            TriggerMiser = game.playerRobot;
            depth = .75f;
            tutorialFinished = false;
            tutorialBackground = game.tutorialBackground;
            backgroundPos = new Vector2(200, 200);
            textPos = new Vector2(225, 225);
            depth = .001f;

            FindTexture();
        }

        //MAIN UPDATE
        public override void Update()
        {
            CheckTutorialDone();
            PauseGameUpdate();
            base.Update();
        }

        //checks to see if player pressed enter to end tutorial
        private void CheckTutorialDone()
        {
            if (isTriggered && Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                tutorialFinished = true;
            }
        }

        //pauses main game update when in tutorial
        private void PauseGameUpdate()
        {
            if (this.isTriggered)
            {
                game.tutorialPause = true;
            }
            if (tutorialFinished)
            {
                game.tutorialPause = false;
            }
        }

        //method to search for correct tutorial to draw
        private void FindTexture()
        {
            if (game.tutorialDataList != null)
            {
                foreach (TutorialDatabase t in game.tutorialDataList)
                {
                    if (t.tutorialName == this.eventName)
                    {
                        this.tutorialTex = t.tutorialTex;
                    }
                }
            }
        }

        //MAIN DRAW
        public override void Render(SpriteBatch sb)
        {
            if (tutorialTex != null && game.tutorialPause && isTriggered)
            {
                sb.Draw(tutorialTex, backgroundPos, new Rectangle(0, 0, tutorialBackground.Width, tutorialBackground.Height),
                    Color.White, rotation, Vector2.Zero, scale, SpriteEffects.None, depth);
            }
        }

    }
}