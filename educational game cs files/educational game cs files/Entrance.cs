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
    public class Entrance: Event
    {
        private bool entering; // checks to see if player is wanting to enter 
        public string destinationLevel, destinationDoor; //tags to know which level and loc to travel to
        public string entranceName; //tag so other entrances and exits can find it
        public Entrance(Vector2 p, Texture2D t, string n, Game1 g, GameObject o)
            : base(p, t, n, g, o)
        {
            entering = false;
            TriggerMiser = game.playerRobot;
            depth = .75f;
            
        }

        //MAIN UPDATE
        public override void Update()
        {
            CheckEnter();
            base.Update();
        }


        //checks player input for wanting to enter
        private void CheckEnter()
        {
            if (isTriggered && Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                entering = true;
            }
            if (Keyboard.GetState().IsKeyUp(Keys.Space))
            {
                entering = false;
            }
        }

        //MAIN DRAW
        public override void Render(SpriteBatch sb)
        {
            base.Render(sb);
        }


        //PROPERTIES
        public bool Entering
        {
            set { entering = value; }
            get { return entering; }
        }
    }
}
