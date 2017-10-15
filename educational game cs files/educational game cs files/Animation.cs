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
    public class AnimationComponent
    {
        Texture2D staticSprite,dynamicSprite;
        Vector2 pos,origin;
        GameObject host;
        int currentFrame,frameLimit,row, frameCounter, cyclePerSecond;
        Rectangle posRect, sourceRect;
        Color color;
        float scale,rotation,depth,frameSpeed;
        
        public AnimationComponent(GameObject g) //constructor for static objects, update later if we want to have scale rotation and depth for static
        {
            host = g;
            staticSprite = host.texture;
            pos = host.position;
            color = Color.White;
            UpdateHostInformation();
        }
        public AnimationComponent(GameObject g, int frames, int rw, int wdth, int hght)
        {
            host = g;
            dynamicSprite = host.texture;
            pos = host.position;
            origin = host.origin;
            posRect = new Rectangle((int)pos.X, (int)pos.Y, wdth, hght);
            sourceRect = new Rectangle(0, 0, wdth, hght);
            frameLimit = frames;
            currentFrame = 0;
            row = rw;
            scale = host.scale;
            depth = host.depth;
            rotation = host.rotation;
            color = Color.White;
            frameCounter = 0;
            cyclePerSecond = 60 / frames;

            UpdateHostInformation();
        }
        public void UpdateAnimation()//call this in the host class
        {            
            ResetFrame(); //run first
            UpdateHostInformation();
            
            
        }
        public void UpdateHostInformation()
        {
            if (host != null)
            {
                pos = host.position;
                row = host.AnimationRow;
                posRect.X = (int)pos.X;
                posRect.Y = (int)pos.Y;
                sourceRect.X = currentFrame * posRect.Width;
                sourceRect.Y = posRect.Height * row;
                scale = host.scale;
                depth = host.depth;
                rotation = host.rotation;
                color = host.color;

                
            }
        }
        public void ResetFrame()
        {
            if (currentFrame >= frameLimit)
            {
                currentFrame = 0;
            }
        }
        public bool nextFrameReady()
        {
            if (frameCounter < cyclePerSecond*frameSpeed)
            {
                frameCounter++;
                return false;
            }
            else
            {
                frameCounter = 0;
                return true;
            }

        }
        public void RenderAnimation(SpriteBatch sb)//call this in the host class
        {
            if (staticSprite != null)
            {
                //sb.Begin();
                sb.Draw(staticSprite, pos, new Rectangle(0, 0, staticSprite.Width, staticSprite.Height), color, rotation, Vector2.Zero, scale, SpriteEffects.None, depth);//draw for static objects
                //sb.End();
            }
            if (dynamicSprite != null)
            {
                //sb.Begin();
                if (host.isRightFacing)
                {
                    sb.Draw(dynamicSprite, pos, sourceRect, color, rotation, Vector2.Zero, scale, SpriteEffects.None, depth);
                }
                else if (!host.isRightFacing)
                {
                    sb.Draw(dynamicSprite, new Vector2(pos.X+host.width,pos.Y), sourceRect, color, rotation, Vector2.Zero, scale, SpriteEffects.FlipHorizontally, depth);
                }
                //sb.End();

                if (nextFrameReady())
                {
                    currentFrame++;
                }
            }
        }
        //properties
        public Color Color
        {
            get { return color; }
            set { color = value; }
        }
        public int Row
        {
            get { return row; }            
            set { row = value; }
        }
        public float FrameSpeed
        {
            get { return frameSpeed; }
            set { frameSpeed = value; }
        }
        public float Depth
        {
            get { return depth; }
            set { depth = value; }
        }
        public float Scale
        {
            get { return scale; }
            set { scale = value; }
        }
        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

    }
}
