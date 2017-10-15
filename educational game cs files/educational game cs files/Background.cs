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
    public class Background:GameObject
    {
        private string backgroundName;

        public Background(Texture2D t, Game1 g, string b)
            : base(t, g)
        {
            backgroundName = b;
            ChooseBackground();
            depth = .99f;
        }

        //selects image for background
        private void ChooseBackground()
        {
            if (backgroundName == "main background")
            {
                texture = game.mainBackground;
            }
            else if (backgroundName == "basement")
            {
                texture = game.basement;
            }
            else if (backgroundName == "foundary")
            {
                texture = game.foundary;
            }
            else if (backgroundName == "upper factory")
            {
                texture = game.upperFactory;
            }
        }

        //MAIN UPDATE
        public override void Update()
        {
            base.Update();
        }

        //MAIN DRAW
        public override void Render(SpriteBatch sb)
        {
            base.Render(sb);
        }

        //PROPERTIES
        public string BackgroundName
        {
            set { backgroundName = value; }
            get { return backgroundName; }
        }
    }
}
