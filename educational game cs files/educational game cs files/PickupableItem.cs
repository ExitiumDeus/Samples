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
    public class PickupableItem : GameObject
    {
        //needs animation, game list for items in the world, player list for items in inv
        
        public Rectangle colRect;
        Level level;
        public string name;
        public bool isButtonPressed,delete;
        public PickupableItem(Vector2 p, Game1 g, Texture2D t, float pM, Level l)
            : base(t, g)
        {
            position = p;
            delete = false;
            powerModifier = pM;
            depth = .2f;
            level = l;
            width = 50;
            height = 50;
            isCollidable = true;
            name = "not yet set";
            colRect = new Rectangle((int)p.X, (int)p.Y, width, height);
            DetermineTexture();
        }
        public override void Update()
        {
            colRect.X = (int)position.X;
            colRect.Y = (int)position.Y;
            origin.X = position.X + (width / 2);
            origin.Y = position.Y + (height / 2);
            HandleYMovementDynamicObj();
            if (isButtonPressed == false && this.colRect.Intersects(game.playerRobot.actualRect))
            {
                isButtonPressed = game.playerRobot.isButtonPressed;
            }
            base.Update();
        }
        public void DetermineTexture()
        {
            if (powerModifier == 9.71f)
            {
                this.texture = game.ironTex;
                name = "iron wire";
            }
            else if (powerModifier == 1.68f)
            {
                this.texture = game.copperTex;
                name = "copper wire";
            }
            else if (powerModifier == 1.59f)
            {
                this.texture = game.silverTex;
                name = "silver wire";
            }
            else if (powerModifier == 2.65f)
            {
                this.texture = game.aluminumTex;
                name = "aluminum wire";
            }
        }
        public override void Render(SpriteBatch sb)
        {
            base.Render(sb);
        }
        public float PowerModifier
        {
            get { return powerModifier; }
            
        }
        
    }
}
