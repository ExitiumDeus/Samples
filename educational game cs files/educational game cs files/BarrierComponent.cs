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
    public class BarrierComponent: GameObject
    {
        
       
        //public Texture2D sprite;
        
        bool isInfluenced;
        //Constructor
        public BarrierComponent(Texture2D t, Vector2 p, Game1 g, int w, int h)
            : base(t, g)
        {
            texture = t;
            width = w;
            height = h;
            position = p;
            rect = new Rectangle((int)position.X, (int)position.Y, width, height);
            rect.X = (int)position.X;
            rect.Y = (int)position.Y;
            origin = new Vector2(position.X + (width / 2), position.Y + (height / 2));            
            isInfluenced = false;
            isCollidable = false;  //barriers should not affect eachother
        }

        ////Main draw method to be called in Game1
        //public void Render(SpriteBatch sb)
        //{
        //    sb.Draw(sprite, rect, Color.White);
        //}
        public override void Update()
        {
            isInfluenced = false;
            rect.X = (int)position.X;
            rect.Y = (int)position.Y;
            origin = new Vector2(position.X + (width / 2), position.Y + (height / 2));
            base.Update();
        }
        public override void Render(SpriteBatch sb)
        {
            sb.Draw(game.barrierDebug, this.rect, new Rectangle(0,0,rect.Width,rect.Height),Color.White, 0f, Vector2.Zero,SpriteEffects.None, .1f);
        }
        #region Properties

        public Rectangle Rect
        {
            set { this.rect = value; }
            get { return this.rect; }
        }
        
        #endregion
    }
    
}
