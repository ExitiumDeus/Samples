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
    public class TutorialDatabase
    {
        public Texture2D tutorialTex;
        public string tutorialName;

        public TutorialDatabase(string n, Texture2D t)
        {
            tutorialTex = t;
            tutorialName = n;
        }
    }
}
