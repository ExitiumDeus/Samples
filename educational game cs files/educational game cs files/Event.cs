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
    public class Event:GameObject
    {
        public bool isTriggered, triggeredOnce; //other classes need to see this
        private GameObject triggerMiser; //object such as player called in to trigger events when
        //collided with
        private string name; //name of event

        public Event(Vector2 p, Texture2D t, string n, Game1 g, GameObject o)
            : base(t, g)
        {
            triggerMiser = o;
            isTriggered = false;
            triggeredOnce = false;
            position = p;
            name = n;
            isCollidable = false; // passable object, doesn't manipulate physics of other objects

            rect = new Rectangle((int)position.X, (int)position.Y, (int)texture.Width, (int)texture.Height);
            //origin = new Vector2(position.X + ((int)texture.Width / 2), position.Y + ((int)texture.Height / 2));
        }

        //MAIN UPDATE
        public override void Update()
        {
            CheckIfTriggered();
            
            base.Update();
        }


        //checks to see if you are within the bounds of the event
        private void CheckIfTriggered()
        {
            if (this.rect.Intersects(game.playerRobot.actualRect))
            {
                isTriggered = true;
                triggeredOnce = true;
            }
            else
            {
                isTriggered = false;
            }
        }



        //MAIN DRAW
        public override void Render(SpriteBatch sb)
        {
            base.Render(sb);
        }


        //PROPERTIES

        public GameObject TriggerMiser
        {
            set{triggerMiser = value;}
            get{return triggerMiser;}
        }
        public string Name
        {
            set { name = value; }
            get { return name; }
        }
    }
}
