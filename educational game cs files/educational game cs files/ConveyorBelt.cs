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
    public class ConveyorBelt : GameObject
    {
        AnimationComponent testAnimation;
        public float powerRequired,speed,maxPower,powerModifier;
        public bool isPowered, isButtonPressed, isMovingClockwise;
        public Wire connectedWire;
        private Level level;
        public Rectangle colRect;
        public BarrierComponent barrier;
        public string conveyorName;

        public ConveyorBelt(Vector2 p, Texture2D t, Game1 g, Level l, Wire cw, float pwrReq,float maxPwr, float pwrMod, bool c, string n) //position, texture, game, currentlevel, connected wire, power required(for the ideal state), max power, power modifier(the msb of max power)
            : base(t, g)
        {
            conveyorName = n;
            position = p;
            texture = t;
            powerRequired = pwrReq;
            maxPower = maxPwr;
            powerModifier = pwrMod;
            isPowered = false;
            isButtonPressed = false;
            isCollidable = false;
            isMovingClockwise = c;
            connectedWire = cw;
            level = l;
            speed = 1f;
            animationRow = 0;
            width = 200;
            height = 20;
            barrier = new BarrierComponent(this.texture, this.position, this.game, width, height);
            level.barrierList.Add(barrier);
            testAnimation = new AnimationComponent(this, 5, animationRow, width, height);
            testAnimation.FrameSpeed = .5f;
            colRect = new Rectangle((int)position.X, (int)position.Y-10, width, height);
            depth = 0.3f;
        }
        public override void Update()
        {
            testAnimation.UpdateAnimation();
            DetermineIsPowered();
            DetermineSpeed();
            DetermineDirection();

            isOnBelt();
            barrier.Update();
            base.Update();
        }
        public override void Render(SpriteBatch sb)
        {

            //sb.Draw(game.barrierDebug, colRect, colRect, Color.White, rotation, Vector2.Zero, SpriteEffects.None, .1f);
            testAnimation.RenderAnimation(sb);
            
        }
        public void DetermineDirection()
        {
            if (isPowered)
            {
                if (isMovingClockwise == true)
                {                    
                    animationRow = 0;
                }
                else if (isMovingClockwise == false)
                {
                    animationRow = 1;
                }
            }
            else if (!isPowered)
            {
                animationRow = 2;
            }  
        }
        public void SwitchDirection()
        {
            if (isPowered)
            {
                if (isMovingClockwise == false)
                {
                    isMovingClockwise = true;
                }
                else if (isMovingClockwise == true)
                {
                    isMovingClockwise = false;
                }
            }
        }
        public void DetermineSpeed()
        {
            if (isPowered)
            {
                if (connectedWire.powerLevel < powerRequired)
                {
                    speed = powerModifier*(connectedWire.powerLevel/this.maxPower);
                }
                else if (connectedWire.powerLevel == powerRequired)
                {
                    speed = powerModifier * (connectedWire.powerLevel / this.maxPower);
                }
                else if (connectedWire.powerLevel > powerRequired)
                {
                    speed = powerModifier * (connectedWire.powerLevel / this.maxPower);
                }
            }
            else if (!isPowered)
            {
                speed = 0f;
            }
        }
        public void ModifyObjectPostionHorizontal(GameObject g, bool b)
        {
            if (!b)
            {
                if (!g.fromTheLeft)
                {
                    g.position.X -= 2 * speed; //true is left
                    //if (g.negativeXSpd > -g.velCap/2)
                    //{
                    //    g.negativeXSpd -= g.inertia;
                    //    if (g.positiveXSpd > 0)
                    //    {
                    //        g.positiveXSpd -= g.inertia;
                    //    }
                    //}
                }
                else if (g.fromTheLeft)
                {
                    g.position.X += 0;
                }
                
            }
            else if (b)
            {
                if (!g.fromTheRight)
                {
                    g.position.X += 2 * speed; //false is right
                    //if (g.positiveXSpd < g.velCap/2)
                    //{
                    //    g.positiveXSpd += g.inertia;
                    //    if (g.negativeXSpd < 0)
                    //    {
                    //        g.negativeXSpd += g.inertia;
                    //    }

                    //}
                }
                else if (g.fromTheRight)
                {
                    g.position.X += 0;
                }
            }
            else
            {
                //otherwise apply friction to direction not 
                //being pressed by the relative key
                g.ApplyPosXFriction();
            }
            
            
        }
        public void isOnBelt()
        {
            //case player
            if (this.colRect.Intersects(game.playerRobot.actualRect))
            {
                ModifyObjectPostionHorizontal(game.playerRobot, isMovingClockwise);
            }
            //case enemies
            //case items
            foreach (PickupableItem i in level.itemInLevelList)
            {
                if(this.colRect.Intersects(i.colRect))
                {
                    ModifyObjectPostionHorizontal(i,isMovingClockwise);
                }
            }
        }
        public void DetermineIsPowered()
        {
            if (connectedWire.IsPowered)
            {
                this.isPowered = true;
            }
            else this.isPowered = false;            
        }
        public bool IsButtonPressed
        {
            get { return isButtonPressed; }
            set { isButtonPressed = value; }
        }
        public bool IsPowered
        {
            get { return isPowered; }
        }
    }
}
