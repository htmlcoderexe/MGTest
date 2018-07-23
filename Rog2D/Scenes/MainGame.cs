using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameObjects;
using GameObjects.MapObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Rog2D.Scenes
{
    public class MainGame : IGameScene
    {
        public KeyboardState pks{get;set;}

        public MouseState pms { get; set; }

        public GameObjects.Map Map;

        private bool IsPlayerTurn { get; set; }

        private Player Player;

        float Scale=4;

        int Scroll;

        public GUI.WindowManager WM;

        public UI.ConsoleWindow Console;

        public UI.InventoryWindow InWin;

        public void ScreenResized(GraphicsDevice device)
        {
            int ScreenWidth = device.PresentationParameters.BackBufferWidth;
            int ScreenHeight = device.PresentationParameters.BackBufferHeight;
            //Screen = new RenderTarget2D(device, ScreenWidth, ScreenHeight, false, device.PresentationParameters.BackBufferFormat, device.PresentationParameters.DepthStencilFormat);
            //b = new SpriteBatch(device);
            WM.ScreenResized(ScreenWidth, ScreenHeight);
           // this.Viewport = new Rectangle(this.Viewport.X, this.Viewport.Y, ScreenWidth, ScreenHeight);
        }

        public void HandleInput(GameTime gameTime)
        {

            while(Volatile.MessageLog.Count>0)
            {
                string msg = Volatile.MessageLog.Pop();
                Console.AppendMessage(msg);
            }



            MouseState m = Mouse.GetState();
            KeyboardState k = Keyboard.GetState();
            WM.MouseX = pms.X;
            WM.MouseY = pms.Y;
            WM.HandleMouse(m, (float)gameTime.ElapsedGameTime.Milliseconds / 1000f);
            

            if (Player.IsActive)
            { 
            if (k.IsKeyUp(Keys.W) && pks.IsKeyDown(Keys.W))
                Player.RequestMove(0, -1, Map, 10);
            if (k.IsKeyUp(Keys.S) && pks.IsKeyDown(Keys.S))
                Player.RequestMove(0, 1, Map, 10);
            if (k.IsKeyUp(Keys.A) && pks.IsKeyDown(Keys.A))
                Player.RequestMove(-1, 0, Map, 10);
            if (k.IsKeyUp(Keys.D) && pks.IsKeyDown(Keys.D))
                Player.RequestMove(1, 0, Map, 10);
                if (k.IsKeyUp(Keys.E) && pks.IsKeyDown(Keys.E))
                {
                    if (Player.Hotkey1Item != null)
                        Player.RequestUseItem(Player.Hotkey1Item);
                }
                if (k.IsKeyUp(Keys.B) && pks.IsKeyDown(Keys.B))
                {
                    if (InWin != null)
                    {
                        InWin.Close();
                        InWin = null;
                    }
                    else
                    {
                        InWin = new UI.InventoryWindow(WM, Player.Inventory);
                        WM.Add(InWin);
                    }
                }
                if (k.IsKeyDown(Keys.LeftShift))
                Player.CanPhase = true;
            if (k.IsKeyUp(Keys.LeftShift))
                Player.CanPhase = false;
            if (k.IsKeyUp(Keys.F5) && pks.IsKeyDown(Keys.F5))
                InitMap();

            pks = k;
                pms = m;

             //   WM.HandleMouse(m, (float)gameTime.ElapsedGameTime.Milliseconds / 1000f);
        }

            if (m.ScrollWheelValue >Scroll)
            {
               Scale += 0.2f;
            }
            else if (m.ScrollWheelValue <Scroll)
            {
               Scale -= 0.2f;
            }
           Scroll = m.ScrollWheelValue;
        }

        public void InitMap()
        {
            Map = new GameObjects.Map(20, 20);
            //GameObjects.MapGeneratorDigger Mapper = new GameObjects.MapGeneratorDigger(64, 64);

            Map = GameObjects.MapGeneratorMesh.Generate(48, 48, 50, 3, 0.05f);
            //World.Map = Mapper.Generate(100);
            Player.X = Map.PlayerSpawn.X;
            Player.Y = Map.PlayerSpawn.Y;
            Map.Player = Player;
            Player.ParentMap = Map;
            Player.Act();
            foreach (MapObject o in Map.Objects)
            {
                o.Command = Volatile.Scheduler;
                Monster m = o as Monster;
                if (m != null)
                    m.Target = Player;
                TimeSystem.IActor a = o as TimeSystem.IActor;
                if (a != null)
                    a.Act();

            }
        }

        public void Init()
        {
           Map = new GameObjects.Map(16, 10);
           Player = new Player();
            Player.Command = Volatile.Scheduler;
            pms = Mouse.GetState();
           Scroll = pms.ScrollWheelValue;

            InitMap();
            Console = new UI.ConsoleWindow(WM);
            Console.Visible = true;
            Console.Title = "Console";
            Console.X = 100000;
            Console.Y = 0;
            WM.Add(Console);
            WM.Screen.X = 0;
            WM.Screen.Y = 0;
            //WM.Add(new UI.InventoryWindow(WM, Map.Player.Inventory));
        }

        public void Render(GameTime gameTime, GraphicsDevice device, SpriteBatch batch)
        {
            
            int spriteWidth = 16;
            device.Clear(new Color(20, 20, 20));

            //this matrix centers the view on the player.
            Matrix m = Matrix.Identity;
            float X =Player.X *Scale * -spriteWidth;
            float Y =Player.Y *Scale * -spriteWidth;
            X += device.PresentationParameters.Bounds.Width / 2;
            Y += device.PresentationParameters.Bounds.Height / 2;
            m = Matrix.CreateTranslation(new Vector3(X, Y, 0));
           Map.Render(batch, Assets.SpriteSheets["tiles1"],Assets.SpriteSheets["autotile1"], m,Scale);
            foreach (GameObjects.MapObject o in Map.Objects)
            {
                o.Render(batch, Assets.SpriteSheets["sprites1"], m,Scale);
            }
           Player.Render(batch, Assets.SpriteSheets["sprites1"], m,Scale);

            WM.Render(device);
            /*
            Rectangle chatwin = new Rectangle(16, device.PresentationParameters.Bounds.Height - 256, 320, 240);
            GUI.Window w = new GUI.Window();
            w.Text = "Turn #" + Volatile.Scheduler.GetTime();
            w.Bounds = chatwin;
            w.Render(batch);
            //*/
            // GUI.Drawing.DrawFrame(chatwin, batch, Assets.SpriteSheets["GUI"]);
            /*/
            Vector2 strpos = new Vector2(16 + 3, device.PresentationParameters.Bounds.Height - 26);
            int c = 15;
            foreach (string s in Volatile.MessageLog)
            {
                c--;
                strpos.Y -= 13;
               // GUI.Drawing.DrawString(strpos, batch, Assets.Fonts["console"], s, Color.White);
                if (c < 0)
                    break;
            }
            strpos.Y = device.PresentationParameters.Bounds.Height - 19*13;
      //*/
            //  GUI.Drawing.DrawString(strpos, batch, Assets.Fonts["console"], , Color.Red);
        }

        public void Update(GameTime gameTime)
        {
            int countactors = 0;
            foreach(MapObject o in Map.Objects)
            {
                if ((o as Monster) != null)
                    countactors++;
            }
           if(!Player.IsActive ||countactors==0)
            {
                //do stuff as long as it's not the player's turn
                while(Volatile.Scheduler.Next()!=-1 && !Player.IsActive)
                {

                }
               // Player.Message("Turn #" + Volatile.Scheduler.GetTime());
            }
            WM.Update((float)gameTime.ElapsedGameTime.Milliseconds / 1000f);
        }
    }
}
