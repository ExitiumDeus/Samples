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
    public class LevelPiece : GameObject
    {
        public BarrierComponent barrier;
        AnimationComponent levelAnimation;
        private string texName;
        private Level level;

        public LevelPiece(Vector2 p, Texture2D t, string tN, Game1 g, int w, int h, Level l)
            : base(t, g)
        {
            level = l;
            texName = tN;
            position = p;
            texture = t;
            depth = .7f;
            OverrideTexture();
            rect = new Rectangle((int)position.X, (int)position.Y, (int)w, (int)h);
            origin = new Vector2(position.X + (w / 2), position.Y + (h / 2));
            barrier = new BarrierComponent(texture, position, game, (int)w, (int)h);
            level.barrierList.Add(barrier);
            levelAnimation = new AnimationComponent(this);
        }

        //used to assign a texture to levelpiece from a text file
        private void OverrideTexture()
        {
            if (texName == "platform")
            {
                texture = game.platformTex;
            }
            else if (texName == "left wall")
            {
                texture = game.leftWallTex;
            }
            else if (texName == "right wall")
            {
                texture = game.rightWallTex;
            }
            else if (texName == "top wall")
            {
                texture = game.topWallTex;
            }
        }

        public override void Update()
        {
            levelAnimation.UpdateAnimation();
            base.Update();
        }
        public override void Render(SpriteBatch sb)
        {
            levelAnimation.RenderAnimation(sb);
        }

        public BarrierComponent Barrier
        {
            set { this.barrier = value; }
            get { return barrier; }
        }
    }
}