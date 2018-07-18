using System;
using GameObjects.MapObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Rog2D
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Main : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        IGameScene CurrentScene;
        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1200;
            graphics.PreferredBackBufferHeight = 800;
            graphics.ApplyChanges();
            Content.RootDirectory = "Content";
            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += new System.EventHandler<System.EventArgs>(Window_ClientSizeChanged);

        }

        private void Window_ClientSizeChanged(object sender, EventArgs e)
        {
            //graphics.PreferredBackBufferWidth = Window.ClientBounds.Width;
            //graphics.PreferredBackBufferHeight = Window.ClientBounds.Height;
           // graphics.ApplyChanges();


            GraphicsDevice.Viewport = new Viewport(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height);
        }



        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            CurrentScene = new Scenes.MainGame();
            CurrentScene.Init();

            base.Initialize();
            
        }

        void SetText(string s)
        {
            Volatile.MessageLog.Push(s);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {

            

            //World.Player.CanPhase = true;
         //   GameObjects.MapGenerator.DrawRect(World.Map, 3, 5, 8, 8, floor);
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Player.MessageCallback = new System.Action<string>(SetText);
            Assets.SpriteSheets["tiles1"] = Texture2D.FromStream(GraphicsDevice, new System.IO.FileStream("graphics\\MapTiles.png", System.IO.FileMode.Open));
            Assets.SpriteSheets["sprites1"] = Texture2D.FromStream(GraphicsDevice, new System.IO.FileStream("graphics\\Sprites.png", System.IO.FileMode.Open));
            Assets.SpriteSheets["items1"] = Texture2D.FromStream(GraphicsDevice, new System.IO.FileStream("graphics\\items.png", System.IO.FileMode.Open));
            Assets.SpriteSheets["GUI"] = Texture2D.FromStream(GraphicsDevice, new System.IO.FileStream("graphics\\GUI.png", System.IO.FileMode.Open));
            Assets.SpriteSheets["test_bus"] = Texture2D.FromStream(GraphicsDevice, new System.IO.FileStream("graphics\\bus.png", System.IO.FileMode.Open));
            GUI.Drawing.WindowSkin = Assets.SpriteSheets["GUI"];
            Assets.Fonts["console"] = Content.Load<SpriteFont>("Play");
            GUI.Drawing.DefaultFont = Assets.Fonts["console"];
            Assets.Shaders["GUI"] = Content.Load<Effect>("File");
            GUI.Drawing.Shader = Assets.Shaders["GUI"];
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            CurrentScene.HandleInput(gameTime);
            CurrentScene.Update(gameTime);
            // TODO: Add your update logic here

            base.Update(gameTime);

        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            CurrentScene.Render(gameTime, GraphicsDevice, spriteBatch);

            base.Draw(gameTime);
        }

        private void BusDemo()
        {


            Color[] Rainbow = new Color[]
            {
                Color.Red,
                Color.Orange,
                Color.Yellow,
                Color.Lime,
                Color.Green,
                Color.Cyan,
                Color.Blue,
                Color.Indigo,
                Color.Purple,
                Color.Black,
                Color.White,
                Color.Gray
            };
            Vector2 offset = new Vector2(320, 100);
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, Assets.Shaders["GUI"]);
            for (int i = 0; i < Rainbow.Length; i++)
            {

                offset.Y += 100;
                spriteBatch.Draw(Assets.SpriteSheets["test_bus"], offset, null, Rainbow[i], 0, Vector2.Zero, 2.0f, SpriteEffects.None, 0);
                if ((i + 1) % 4 == 0)
                {
                    offset.X += 140;
                    offset.Y = 100;
                }
            }

            spriteBatch.End();
        }

    }
}
