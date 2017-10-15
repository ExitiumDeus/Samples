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
    public class PowerSource : GameObject
    {
        private float powerSupplied; //to pass to the first wire
        private bool isOn;
        private bool isButtonPressed;
        public string pwrSrcName;
        private Rectangle colRect;
        public PowerSource(Vector2 p, Game1 g, Texture2D t, float pwr, string n)
            : base(t, g)
        {
            position = p;
            pwrSrcName = n;
            powerSupplied = pwr;
            isOn = false;
            texture = game.powerBoxOff;
            width = 50;
            height = 50;
            isCollidable = false;
            colRect = new Rectangle((int)p.X, (int)p.Y, t.Width, t.Height);
        }

        public override void Update()
        {
            PowerBoxLogic();
            base.Update();
        }

        public override void Render(SpriteBatch sb)
        {
            base.Render(sb);
        }
        public void PowerBoxLogic()
        {
            if (this.isButtonPressed == false && this.colRect.Intersects(game.playerRobot.actualRect))
            {
                this.isButtonPressed = game.playerRobot.isButtonPressed; //need to test multiple elevators
            }
            if (this.isButtonPressed == true)
            {
                if (isOn)
                {
                    isOn = false;
                    texture = game.powerBoxOff;
                }
                else if (!isOn)
                {
                    isOn = true;
                    texture = game.powerBoxOn;
                }
                this.isButtonPressed = false;
                game.playerRobot.level.attemptsCount++;
            }
        }
        public bool IsOn
        {
            get { return isOn; }
            set { isOn = value; }
        }
        public float PowerSupplied
        {
            get { return powerSupplied; }
        }
    }
}
