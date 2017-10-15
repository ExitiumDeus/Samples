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
    public class Door : GameObject
    {
        //additional attr for doors
        bool isOpen,isPowered, isOscillating;
        public int powerRequired;        
        AnimationComponent doorAnimation;
        BarrierComponent barrier;
        public enum state { open, closed, oscillating };
        public state doorState;
        public string doorName;
        public Wire connectedWire; //this wire would be the wire that is connected to the door ie end of circuit
        //some sort of int to determine how much eletricity is required to open the door
        //information from child node of the circuit
        private Level level;

        public Door(Vector2 p, Game1 g, Texture2D t, int pwr, Wire wr, Level l, string n)
            : base(t, g)
        {
            doorName = n;
            level = l;
            position = p;
            width = 20;
            height = 150;
            powerRequired = pwr;
            connectedWire = wr;
            doorState = state.closed;
            isOpen = false;
            isPowered = false;
            isOscillating = false;
            doorAnimation = new AnimationComponent(this, 4, this.animationRow, 20, 150); //0 is closing animation, 1 is opening, 2 oscillating, 3 closed, 4 open
            doorAnimation.FrameSpeed = .3f;
            barrier = new BarrierComponent(texture, position, game, 20, 150);
            level.barrierList.Add(barrier);
            isCollidable = false;
            depth = .3f;
        }
        public override void Update()
        {
            determineState();
            doorAnimation.UpdateAnimation();
            
            base.Update();
        }
        public override void Render(SpriteBatch sb)
        {
            doorAnimation.RenderAnimation(sb);
        }
        public void determineState()
        {
            if (determineIsPowered() && !determineOscillating())
            {
                doorState = state.open;
                animationRow = 4; //open row
                isOpen = true;
                level.barrierList.Remove(barrier);
            }
            else if (determineOscillating())
            {
                doorState = state.oscillating;
                animationRow = 2; //oscillating row
                isOpen = false;
                if (!level.barrierList.Contains(barrier))
                {
                    level.barrierList.Add(barrier);
                }
            }
            else if (!determineIsPowered())
            {
                doorState = state.closed;
                animationRow = 3; //closed row
                if (!level.barrierList.Contains(barrier))
                {
                    level.barrierList.Add(barrier);
                }
            }
        }
        
        private bool determineOscillating() //determines if the door should be oscillating, 
        {
            if (!determineIsPowered() && powerRequired < connectedWire.powerLevel) //ispowered is false because the powerlevels dont match
            {
                isOscillating = true;
            }
            else if (determineIsPowered() && powerRequired == connectedWire.powerLevel)
            {
                isOscillating = false;
            }
            else if (determineIsPowered() && powerRequired > connectedWire.powerLevel)
            {
                isOscillating = false;
            }
            else if (!determineIsPowered())
            {
                isOscillating = false;
            }
            return isOscillating;
        }
        private bool determineIsPowered() //determines door is powered AND! the power level is correct
        {
            if (connectedWire.IsPowered && ((powerRequired  - connectedWire.powerLevel) > -5 && (powerRequired - connectedWire.powerLevel) < 5))
            {
                this.isPowered = true;
            }
            else if (connectedWire.isPowered && !((powerRequired - connectedWire.powerLevel) > -15 && (powerRequired - connectedWire.powerLevel) < 15))
            {
                this.isPowered = false;
            }
            else if (!connectedWire.isPowered)
            {
                this.isPowered = false;
            }
            return isPowered;
        }
        public Wire ConnectedWire
        {
            get { return connectedWire; }
        }
        /*We can use this section here to talk about what all a door can do.
         * it must be interactable, ie a button can be pressed open it if it has power, or will it automatically open once power is restored and shut if power is removed
         * this means there are two states, open, closed, which will have different graphics.  Will it have an animation for opening and closing?
         * doors while open must not have collision but while closed have collision
         * 
         * feel free to insert ideas here
         * 
         */
    }
}
