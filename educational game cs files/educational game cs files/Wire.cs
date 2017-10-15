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
    public class Wire : GameObject //addsecond child
    {
        public Wire parent;
        public Wire child;
        public Wire secondChild;
        public PowerSource powerSource;
        public PickupableItem socketItem; //player sets this and removes this
        public bool isPowered;
        public bool isBroken;
        public bool hasSocket;
        public enum dir { horizontal, leftDown, leftUp, rightDown, rightUp, vertical };
        public dir direction;
        public float powerLevel;
        public Rectangle colRect;
        public string name;
        public Wire(Vector2 p, Game1 g, Texture2D t, int d, bool s, string n)
            : base(t, g)
        {
            position = p;
            name = n;
            width = 50;
            height = 50;
            direction = (dir)d;
            hasSocket = s;
            determineTexture();
            socketItem = null;
            HandleIsBroken();
            depth = .75f;
            isCollidable = false;
            isPowered = false;//pass ispowered through list
            colRect = new Rectangle((int)p.X, (int)p.Y, 50, 50);
        }

        public override void Update()
        {
            HandlePower();
            if (parent != null)
            {
                if (this.powerSource.IsOn && !this.isBroken && (this.parent.isPowered))
                {
                    this.isPowered = true;
                }
                else if (!this.powerSource.IsOn || this.isBroken || (!this.parent.isPowered))
                {
                    this.isPowered = false;
                }
            }
            else
            {
                if (this.powerSource.IsOn && !this.isBroken)
                {
                    this.isPowered = true;
                }
                else if (!this.powerSource.IsOn || this.isBroken)
                {
                    this.isPowered = false;
                }
            }
            base.Update();
        }
        public void determineTexture()
        {
            switch (direction)
            {
                case (dir.horizontal):
                    this.texture = game.horizontal;
                    break;
                case (dir.leftDown):
                    this.texture = game.leftDown;
                    break;
                case (dir.leftUp):
                    this.texture = game.leftUp;
                    break;
                case (dir.rightDown):
                    this.texture = game.rightDown;
                    break;
                case (dir.rightUp):
                    this.texture = game.rightUp;
                    break;
                case (dir.vertical):
                    this.texture = game.vertical;
                    break;
            }
            if (hasSocket)
            {
                this.texture = game.socketed;
            }
        }
        public void HandleIsBroken()
        {
            if (hasSocket && socketItem == null)
            {
                isBroken = true;
            }
            else if (hasSocket && socketItem != null)
            {
                isBroken = false;
            }
            else if (!hasSocket)
            {
                isBroken = false;
            }
        }
        public void ModifyPower()
        {
            powerLevel /= socketItem.PowerModifier;
        }
        public void HandlePower()
        {
            if (!isBroken && parent != null && !hasSocket)
            {
                this.powerLevel = parent.powerLevel;
            }
            else if (isBroken && parent != null && !hasSocket)
            {
                powerLevel = 0; //this case shouldnt happen
            }
            else if (isBroken && parent != null && hasSocket)
            {
                powerLevel = 0;
            }
            else if (!isBroken && parent != null && hasSocket)
            {
                powerLevel = parent.powerLevel;
                ModifyPower(); //does this continue to modify
            }
            if (parent == null && powerSource != null)
            {
                if (powerSource.IsOn)
                {
                    this.powerLevel = powerSource.PowerSupplied;
                }
                else this.powerLevel = 0;
            }
            
        }
        public override void Render(SpriteBatch sb)
        {
            base.Render(sb); //draw socket item when applicable
        }
        public bool IsPowered
        {
            get { return isPowered; }
            set { isPowered = true; }
        }
        public float PowerLevel
        {
            get { return powerLevel; }
        }
    }
}
