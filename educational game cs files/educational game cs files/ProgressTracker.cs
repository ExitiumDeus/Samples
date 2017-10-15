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
    public class ProgressTracker
    {
        private GradingMeter tutorialM, z1L1M, z1L2M, z1L3M,
            z2L1M, z2L2M, z2L3M, z3L1M, z3L2M, z3L3M,
            z4L1M, z4L2M, z4L3M;
        private Vector2 pos1, pos2, pos3, pos4, pos5, pos6, pos7,
            pos8, pos9, pos10, pos11, pos12, pos13;
        private Texture2D background, incomplete;
        private Vector2 backgroundPos;
        private Rectangle backgroundRect;
        private Game1 game;
        private bool okayToExit;
        private int exitCount;

        public ProgressTracker(Game1 g)
        {
            game = g;
            background = game.statScreen;
            incomplete = game.incomplete;
            okayToExit = false;
            exitCount = 60;
            backgroundPos = new Vector2(0, 0);
            backgroundRect = new Rectangle((int)backgroundPos.X, 
                (int)backgroundPos.Y, (int)background.Width, (int)background.Height);

            pos1 = new Vector2(360, 250);

            pos2 = new Vector2(110, 320);
            pos3 = new Vector2(110, 395);
            pos4 = new Vector2(110, 470);
            pos5 = new Vector2(110, 545);

            pos6 = new Vector2(360, 320);
            pos7 = new Vector2(360, 395);
            pos8 = new Vector2(360, 470);
            pos9 = new Vector2(360, 545);

            pos10 = new Vector2(590, 320);
            pos11 = new Vector2(590, 395);
            pos12 = new Vector2(590, 470);
            pos13 = new Vector2(590, 545);

            tutorialM = new GradingMeter(pos1, 2, game.statBorder, game.powerBar, game,
                null, game.scoreList[0]);
            z1L1M = new GradingMeter(pos2, 2, game.statBorder, game.powerBar, game,
                null, game.scoreList[1]);
            z1L2M = new GradingMeter(pos3, 2, game.statBorder, game.powerBar, game,
                null, game.scoreList[2]);
            z1L3M = new GradingMeter(pos4, 2, game.statBorder, game.powerBar, game,
                null, game.scoreList[3]);
            z2L1M = new GradingMeter(pos5, 2, game.statBorder, game.powerBar, game,
                null, game.scoreList[4]);

            z2L2M = new GradingMeter(pos6, 2, game.statBorder, game.powerBar, game,
                null, game.scoreList[5]);
            z2L3M = new GradingMeter(pos7, 2, game.statBorder, game.powerBar, game,
                null, game.scoreList[6]);
            z3L1M = new GradingMeter(pos8, 2, game.statBorder, game.powerBar, game,
                null, game.scoreList[7]);
            z3L2M = new GradingMeter(pos9, 2, game.statBorder, game.powerBar, game,
                null, game.scoreList[8]);

            z3L3M = new GradingMeter(pos10, 2, game.statBorder, game.powerBar, game,
                null, game.scoreList[9]);
            z4L1M = new GradingMeter(pos11, 2, game.statBorder, game.powerBar, game,
                null, game.scoreList[6]);
            z4L2M = new GradingMeter(pos12, 2, game.statBorder, game.powerBar, game,
                null, game.scoreList[7]);
            z4L3M = new GradingMeter(pos13, 2, game.statBorder, game.powerBar, game,
                null, game.scoreList[8]);
        }


        //MAIN UPDATE
        public void UpdateProgress()
        {
            AllowExit();
            UpdateMeters();

        }

        private void UpdateMeters()
        {
            tutorialM.UpdateMeter();
            z1L1M.UpdateMeter();
            z1L2M.UpdateMeter();
            z1L3M.UpdateMeter();
            z2L1M.UpdateMeter();
            z2L2M.UpdateMeter();
            z2L3M.UpdateMeter();
            z3L1M.UpdateMeter();
            z3L2M.UpdateMeter();
            z4L1M.UpdateMeter();
            z4L2M.UpdateMeter();
            z4L3M.UpdateMeter();
        }

        private void AllowExit()
        {
            if (exitCount > 0)
            {
                exitCount--;
            }
            if (exitCount == 0)
            {
                okayToExit = true;
            }
            if (okayToExit && Keyboard.GetState().IsKeyDown(Keys.P))
            {
                game.drawTracking = false;
                game.statOpenCount = 60;
            }
        }

        //MAIN DRAW
        public void DrawProgress(SpriteBatch sb)
        {
            sb.Draw(background, backgroundRect, new Rectangle(0, 0, background.Width, background.Height), 
                Color.White, 0f, Vector2.Zero, SpriteEffects.None, .00001f);
            DrawMeters(sb);
        }

        private void DrawMeters(SpriteBatch sb)
        {
            tutorialM.DrawMeter(sb);
            z1L1M.DrawMeter(sb);
            z1L2M.DrawMeter(sb);
            z1L3M.DrawMeter(sb);
            z2L1M.DrawMeter(sb);
            z2L2M.DrawMeter(sb);
            z2L3M.DrawMeter(sb);
            z3L1M.DrawMeter(sb);
            z3L2M.DrawMeter(sb);
            z3L3M.DrawMeter(sb);
            z4L1M.DrawMeter(sb);
            z4L2M.DrawMeter(sb);
            z4L3M.DrawMeter(sb);
        }
    }
}
