using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace Rog2D
{
    public static class Assets
    {
        public static Dictionary<string, Texture2D> SpriteSheets = new Dictionary<string, Texture2D>();
        public static Dictionary<string, SpriteFont> Fonts = new Dictionary<string, SpriteFont>();
        public static Dictionary<string, Effect> Shaders = new Dictionary<string, Effect>();
    }
}
