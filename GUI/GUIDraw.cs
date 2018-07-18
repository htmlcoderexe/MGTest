using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GUI
{
    public static class Drawing
    {
        public static int GuiTileSize = 32;
        public static Texture2D WindowSkin;
        public static SpriteFont DefaultFont;
        public static Effect Shader;
        public static void DrawFrame(Rectangle Frame, SpriteBatch Batch, Texture2D Texture)
        {

            Rectangle TopLeft = new Rectangle(Frame.X, Frame.Y, GuiTileSize, GuiTileSize);
            Rectangle TopCenter = new Rectangle(Frame.X + GuiTileSize, Frame.Y, Frame.Width - GuiTileSize * 2, GuiTileSize);
            Rectangle TopRight = new Rectangle(Frame.X + Frame.Width - GuiTileSize, Frame.Y, GuiTileSize, GuiTileSize);

            Rectangle CenterLeft = new Rectangle(Frame.X, Frame.Y + GuiTileSize, GuiTileSize, Frame.Height - GuiTileSize * 2);
            Rectangle Center = new Rectangle(Frame.X + GuiTileSize, Frame.Y + GuiTileSize, Frame.Width - GuiTileSize * 2, Frame.Height - GuiTileSize * 2);
            Rectangle CenterRight = new Rectangle(Frame.X + Frame.Width - GuiTileSize, Frame.Y + GuiTileSize, GuiTileSize, Frame.Height - GuiTileSize * 2);

            Rectangle BottomLeft = new Rectangle(Frame.X, Frame.Y + Frame.Height - GuiTileSize, GuiTileSize, GuiTileSize);
            Rectangle BottomCenter = new Rectangle(Frame.X + GuiTileSize, Frame.Y + Frame.Height - GuiTileSize, Frame.Width - GuiTileSize * 2, GuiTileSize);
            Rectangle BottomRight = new Rectangle(Frame.X + Frame.Width - GuiTileSize, Frame.Y + Frame.Height - GuiTileSize, GuiTileSize, GuiTileSize);

            Rectangle[,] wf = new Rectangle[,]
            {
                {TopLeft,CenterLeft,BottomLeft },
                {TopCenter,Center,BottomCenter },
                {TopRight,CenterRight,BottomRight },
            };


            Rectangle src = new Rectangle(0, 0, 32, 32);

            Vector2 pos = new Vector2(Frame.X, Frame.Y);

            Batch.Begin(SpriteSortMode.Deferred, null, null, null, null, Shader);
            for (int a = 0; a < 3; a++) { 
                for (int b = 0; b < 3; b++)
                {
                  src.X = a * GuiTileSize;
                   src.Y = b * GuiTileSize;

                   // Batch.Draw(Texture, pos, wf[a, b], Color.White, 0.0f, Vector2.Zero, 1, SpriteEffects.None, 0.0f);
                    Batch.Draw(Texture, wf[a, b], src, new Color(255,127,0));
                } }
            Batch.End();

        }
        public static void DrawString(Vector2 Position, SpriteBatch Batch,SpriteFont Font, string String, Color Color)
        {
            Batch.Begin(SpriteSortMode.Deferred,null,null,null,null,Shader);
            Batch.DrawString(Font, String, Position, Color);
            Batch.End();

        }
    }
}
