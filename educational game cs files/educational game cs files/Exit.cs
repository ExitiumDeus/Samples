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
    public class Exit:Event
    {
        private bool exiting; // checks to see if player is wanting to exit 
        public string destinationLevel, destinationDoor; //tags to know which level and loc to travel to
        public string exitName; //tag so other entrances and exits can find it
        public bool isOpen;
        public Exit(Vector2 p, Texture2D t, string n, Game1 g, GameObject o)
            : base(p, t, n, g, o)
        {
            isOpen = true;
            exiting = false;
            TriggerMiser = game.playerRobot;
            depth = .75f;
        }

        //MAIN UPDATE
        public override void Update()
        {
            CheckExit();
            FlipTexture();
            base.Update();
        }


        //checks player input for wanting to enter
        private void CheckExit()
        {
            if (isTriggered && Keyboard.GetState().IsKeyDown(Keys.Space) && isOpen)
            {
                exiting = true;
                
            }
            if (Keyboard.GetState().IsKeyUp(Keys.Space))
            {
                exiting = false;
            }
        }

        private void FlipTexture()
        {
            if (!isOpen)
            {
                texture = game.exitClosedTex;
            }
            else
            {
                texture = game.smallExitTex;
            }
        }

        //MAIN DRAW
        public override void Render(SpriteBatch sb)
        {
            base.Render(sb);
        }


        //PROPERTIES
        public bool Exiting
        {
            set { exiting = value; }
            get { return exiting; }
        }
    }
}
