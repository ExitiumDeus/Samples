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

    public class Meter
    {
        public Texture2D border, bar;
        public Rectangle outlineRect, barRect;
        public Vector2 pos;
        public int size;
        public float maxAmount, currentAmount;
        public float percentage;
        public Game1 game;
        public GameObject gameObject;
        //private float depth;
        public int counter;

        public Meter(Vector2 p, int s, Texture2D bo, Texture2D ba, Game1 g, GameObject o)
        {
            border = bo;
            bar = ba;
            gameObject = o;
            pos = p;
            size = s;
            game = g;
            //depth = .9f;
            outlineRect = new Rectangle((int)pos.X, (int)pos.Y, border.Width / size, border.Height / size);
            barRect = new Rectangle((int)pos.X + 1, (int)pos.Y + 1, (bar.Width / size) + 1, (bar.Height / size) - 1);

        }

        public virtual void UpdateMeter()
        {
            outlineRect.X = (int)pos.X;
            outlineRect.Y = (int)pos.Y;

            barRect.X = (int)pos.X + (12 / size);
            barRect.Y = (int)pos.Y + 1;
        }


        public virtual void DrawMeter(SpriteBatch sb)
        {
            sb.Draw(border, outlineRect, new Rectangle(0, 0, border.Width, border.Height),
                Color.White, 0f, Vector2.Zero, SpriteEffects.None, .0002f);

            sb.Draw(bar, barRect, new Rectangle(0, 0, bar.Width, bar.Height), Color.White, 0f,
                Vector2.Zero, SpriteEffects.None, .0001f);
        }
    }

    public class HealthMeter : Meter
    {
        public HealthMeter(Vector2 p, int s, Texture2D bo, Texture2D ba, Game1 g, GameObject o)
            : base(p, s, bo, ba, g, o)
        {
            maxAmount = gameObject.health;

        }

        public override void UpdateMeter()
        {

            currentAmount = gameObject.health;
            percentage = ((float)currentAmount / (float)maxAmount) * (float)200 / size;
            barRect.Width = (int)percentage;

            base.UpdateMeter();
        }
    }

    public class ConductivityMeter : Meter
    {
        float value;
        public ConductivityMeter(Vector2 p, int s, Texture2D bo, Texture2D ba, Game1 g, GameObject o, float v)
            : base(p, s, bo, ba, g, o)
        {
            barRect.X += 10 / size;
            maxAmount = 10f;
            value = v;
            //outlineRect.X -= 20;
            UpdateMeter();
        }

        public override void UpdateMeter()
        {
            currentAmount = value;
            percentage = ((float)currentAmount / (float)maxAmount) * (float)200 / size;
            barRect.Width = (int)percentage;

            base.UpdateMeter();
        }
    }

    public class PowerMeter : Meter
    {
        float value;
        public PowerMeter(Vector2 p, int s, Texture2D bo, Texture2D ba, Game1 g, GameObject o, float v)
            : base(p, s, bo, ba, g, o)
        {
            maxAmount = 1500;
            value = v;
            barRect.X += 10 / size;
            UpdateMeter();
        }

        public override void UpdateMeter()
        {
            currentAmount = value;
            percentage = ((float)currentAmount / (float)maxAmount) * (float)200 / size;
            barRect.Width = (int)percentage;

            base.UpdateMeter();
        }
    }


    public class GradingMeter : Meter
    {
        float value;
        public GradingMeter(Vector2 p, int s, Texture2D bo, Texture2D ba, Game1 g, GameObject o, float v)
            : base(p, s, bo, ba, g, o)
        {
            maxAmount = 100;
            value = v;
            barRect.X += 10 / size;
            UpdateMeter();
        }

        public override void UpdateMeter()
        {
            currentAmount = value;
            percentage = ((float)currentAmount / (float)maxAmount) * (float)200 / size;
            barRect.Width = (int)percentage;

            base.UpdateMeter();
        }

        public override void DrawMeter(SpriteBatch sb)
        {
            sb.Draw(border, outlineRect, new Rectangle(0, 0, border.Width, border.Height),
                Color.White, 0f, Vector2.Zero, SpriteEffects.None, .000002f);

            sb.Draw(bar, barRect, new Rectangle(0, 0, bar.Width, bar.Height), Color.White, 0f,
                Vector2.Zero, SpriteEffects.None, .000001f);
        }
    }
}


