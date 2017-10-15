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
    public class Inventory : GameObject
    {
        public List<PickupableItem> inventoryList;
        public List<Rectangle> rectsForDrawingList;
        public Inventory(Vector2 p, Game1 g, Texture2D t)
            : base(t, g)
        {
            position = p;
            isCollidable = false;
            inventoryList = new List<PickupableItem>();
            rectsForDrawingList = new List<Rectangle>();
            FillRectList();
        }
        public override void Update()
        {
            base.Update();
        }
        public void FillRectList()
        {
            rectsForDrawingList.Add(new Rectangle((int)(position.X + 17), (int)(position.Y + 39), 35, 22));
            rectsForDrawingList.Add(new Rectangle((int)(position.X + 67), (int)(position.Y + 39), 35, 22));
            rectsForDrawingList.Add(new Rectangle((int)(position.X + 117), (int)(position.Y + 39), 35, 22));
            rectsForDrawingList.Add(new Rectangle((int)(position.X + 167), (int)(position.Y + 39), 35, 22));
            rectsForDrawingList.Add(new Rectangle((int)(position.X + 218), (int)(position.Y + 39), 35, 22));
            rectsForDrawingList.Add(new Rectangle((int)(position.X + 267), (int)(position.Y + 39), 35, 22));
            rectsForDrawingList.Add(new Rectangle((int)(position.X + 17), (int)(position.Y + 73), 35, 22));
            rectsForDrawingList.Add(new Rectangle((int)(position.X + 67), (int)(position.Y + 73), 35, 22));
            rectsForDrawingList.Add(new Rectangle((int)(position.X + 117), (int)(position.Y + 73), 35, 22));
            rectsForDrawingList.Add(new Rectangle((int)(position.X + 167), (int)(position.Y + 73), 35, 22));
            rectsForDrawingList.Add(new Rectangle((int)(position.X + 218), (int)(position.Y + 73), 35, 22));
            rectsForDrawingList.Add(new Rectangle((int)(position.X + 267), (int)(position.Y + 73), 35, 22));
        }
        public void HandleObjectDraw(SpriteBatch sb)
        {
            if (inventoryList != null && inventoryList.Count > 0 && rectsForDrawingList != null && rectsForDrawingList.Count > 0)
            {
                for (int i = 0; i <= inventoryList.Count - 1; i++)
                {
                    sb.Draw(inventoryList[i].texture, rectsForDrawingList[i], new Rectangle(0, 0, inventoryList[i].texture.Width, inventoryList[i].texture.Height), Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.01f);
                }
            }
        }

        public override void Render(SpriteBatch sb)
        {
            sb.Draw(texture, new Rectangle((int)(position.X), (int)(position.Y), texture.Width, texture.Height), new Rectangle(0, 0, texture.Width, texture.Height), Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.02f);
            if (game.playerRobot.InvCursorCount >= 0 &&game.playerRobot.scanner.state == Scanner.ScannerState.Items)
            {
                sb.Draw(game.invCursorText, new Rectangle(rectsForDrawingList[game.playerRobot.InvCursorCount].X, rectsForDrawingList[game.playerRobot.InvCursorCount].Y,
                    rectsForDrawingList[game.playerRobot.InvCursorCount].Width, rectsForDrawingList[game.playerRobot.InvCursorCount].Height),
                    new Rectangle(0, 0, game.invCursorText.Width, game.invCursorText.Height), Color.White, 0f, Vector2.Zero, SpriteEffects.None, .005f);
            }
            HandleObjectDraw(sb);
        }
    }
}
