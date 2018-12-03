using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Rog2D
{
    public interface IGameScene
    {
        KeyboardState pks { get; set; }
        MouseState pms { get; set; }
        void Update(GameTime gameTime);
        void HandleInput(GameTime gameTime);
        void Render(GameTime gameTime, GraphicsDevice device, SpriteBatch batch);
        void ScreenResized(GraphicsDevice device);
        void Init();
    } 
}
