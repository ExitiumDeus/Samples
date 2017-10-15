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
    public class CreditsScreen
    {
        private bool creditsComplete;
        private short creditsPhase, fullSpeed;
        private Vector2 designCredPos, designCredVel;
        private Texture2D blackBackground, designCredTex;
        private Game1 game;

        public CreditsScreen(Game1 g)
        {
            game = g;
            blackBackground = game.blackBackgroundTex;
            designCredTex = game.developerCredits;
            creditsComplete = false;
            designCredPos = new Vector2(300, 800);
            designCredVel = new Vector2(0, 0);
            creditsPhase = 0;
            fullSpeed = 2;
        }

        //MAIN UPDATE
        public void UpdateCredits()
        {
            HandleCreditPhases();
            CheckSkipCredits();
        }

        //handles panning
        private void HandleCreditPhases()
        {
            //update pos
            designCredPos += designCredVel;

            switch (creditsPhase)
            {
                case(0):
                    PanUp(ref designCredVel, ref designCredPos, (-designCredTex.Height - 50));
                    break;
                case(1):
                    EndCredits();
                    break;
            }
        }

        private void PanUp(ref Vector2 vel, ref Vector2 pos, int stopPos)
        {
            vel.Y = -fullSpeed;
            if (pos.Y <= stopPos)
            {
                vel.Y = 0;
                creditsPhase++;
            }
        }

        //finishes out credits
        private void EndCredits()
        {
            creditsComplete = true;
            if (game.gameBeat)
            {
                game.statTracker = new ProgressTracker(game);
            }
        }

        //skip the credits
        private void CheckSkipCredits()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter) ||
                Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                creditsComplete = true;
                if (game.gameBeat)
                {
                    game.statTracker = new ProgressTracker(game);
                }
            }
        }

        //MAIN DRAW
        public void DrawCredits(SpriteBatch sb)
        {
            sb.Draw(blackBackground, new Vector2(0, 0), Color.Black);
            sb.Draw(designCredTex, designCredPos, Color.White);
        }

        //PROPERTIES
        public bool CreditsComplete
        {
            set { creditsComplete = value; }
            get { return creditsComplete; }
        }
    }
}
