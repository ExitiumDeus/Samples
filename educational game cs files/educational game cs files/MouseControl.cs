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
    public class MouseControl
    {
        private Texture2D mouseTex;
        private Vector2 pos;
        public Rectangle rect;
        private Game1 game;

        public MouseControl(Game1 g)
        {
            game = g;
            game.IsMouseVisible = false;
            mouseTex = game.mouseTex;
            rect = new Rectangle((int)pos.X, (int)pos.Y,
                (int)mouseTex.Width, (int)mouseTex.Height);
        }

        public void UpdateMouse()
        {
            rect.X = (int)pos.X;
            rect.Y = (int)pos.Y;

            pos.X = Mouse.GetState().X;
            pos.Y = Mouse.GetState().Y;
        }

        public void RenderMouse(SpriteBatch sb)
        {
            sb.Draw(mouseTex, pos, Color.White);
        }
    }
}
