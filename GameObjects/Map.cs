using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameObjects
{
    public class Map
    {
        public struct Tile
        {
            public int Index;
            public int Type;
            public bool Revealed;
            public byte LightLevel;
            public bool Visible;
            public bool Passable;
            public int WallAutoTileCode;

        }

        public static Dictionary<int, Vector2> BlobIndices = new Dictionary<int, Vector2>()
        {
            #region BlobMappings
            {0, new Vector2(5,1)},
            {2, new Vector2(0,2)},
            {8, new Vector2(3,3)},
            {10, new Vector2(6,2)},
            {11, new Vector2(3,2)},
            {16, new Vector2(1,3)},
            {18, new Vector2(4,2)},
            {22, new Vector2(1,2)},
            {24, new Vector2(2,3)},
            {26, new Vector2(5,2)},
            {27, new Vector2(6,4)},
            {30, new Vector2(5,4)},
            {31, new Vector2(2,2)},
            {64, new Vector2(0,0)},
            {66, new Vector2(0,1)},
            {72, new Vector2(6,0)},
            {74, new Vector2(6,1)},
            {75, new Vector2(5,3)},
            {80, new Vector2(4,0)},
            {82, new Vector2(4,1)},
            {86, new Vector2(6,3)},
            {88, new Vector2(5,0)},
            {90, new Vector2(8,1)},
            {91, new Vector2(8,3)},
            {94, new Vector2(9,3)},
            {95, new Vector2(8,0)},
            {104, new Vector2(3,0)},
            {106, new Vector2(7,4)},
            {107, new Vector2(3,1)},
            {120, new Vector2(4,3)},
            {122, new Vector2(8,4)},
            {123, new Vector2(7,1)},
            {126, new Vector2(3,4)},
            {127, new Vector2(7,0)},
            {208, new Vector2(1,0)},
            {210, new Vector2(4,4)},
            {214, new Vector2(1,1)},
            {216, new Vector2(7,3)},
            {218, new Vector2(9,4)},
            {219, new Vector2(2,4)},
            {222, new Vector2(9,1)},
            {223, new Vector2(9,0)},
            {248, new Vector2(2,0)},
            {250, new Vector2(8,2)},
            {251, new Vector2(7,2)},
            {254, new Vector2(9,2)},
            {255, new Vector2(2,1)}
#endregion
        };

        public int GetWall(int X, int Y)
        {
            try
            {
                return this.Tiles[X, Y].Passable ? 0 : 1;
            }
            catch (Exception ex)
            {
                return 1;
            }
        }

        public void DoAutoTile(int X, int Y)
        {
            if (X >= this.Width || X < 0 || Y >= this.Height || Y < 0)
                return;
            int code = 0;

            int left, right, top, bottom, tl, tr, bl, br;
            left = GetWall(X - 1, Y);
            right = GetWall(X + 1, Y);
            top = GetWall(X, Y - 1);
            bottom = GetWall(X, Y + 1);

            tl = GetWall(X - 1, Y - 1);
            tr = GetWall(X + 1, Y - 1);
            bl = GetWall(X - 1, Y + 1);
            br = GetWall(X + 1, Y + 1);

            if (top == 0)
            {
                tl = 0; tr = 0;
            }
            if (bottom == 0)
            {
                bl = 0; br = 0;
            }
            if (right == 0)
            {
                br = 0; tr = 0;
            }
            if (left == 0)
            {
                tl = 0; bl = 0;
            }

            code = tl + top * 2 + tr * 4 + left * 8 + right * 16 + bl * 32 + bottom * 64 + br * 128;
            Tiles[X, Y].WallAutoTileCode = code;

        }

        public int Width
        {
            get
            {
                return this.Tiles.GetLength(0);
            }
        }
        public int Height
        {
            get
            {
                return this.Tiles.GetLength(1);
            }
        }

        public static Dictionary<int, int> CardMappings;

        public Point PlayerSpawn { get; set; }

        public Tile[,] Tiles;
        public List<MapObject> Objects;
        public MapObjects.Player Player;
        public const int spriteWidth=16;
        public static GUI.Renderer Renderer;

        public TimeSystem.SchedulerATB Scheduler;
        public Map(int W, int H)
        {
            this.Tiles = new Tile[W, H];
            this.Objects = new List<MapObject>();
            this.Scheduler = new TimeSystem.SchedulerATB();
            CardMappings = new Dictionary<int, int>();
            //Suicide King
            CardMappings[1] = 1;
            //Joker A
            CardMappings[7] = 7;
            //Joker B
            CardMappings[23] = 23;
            //Blank Card
            CardMappings[15] = 15;
            //Discount Card
            CardMappings[31] = 31;
            //Ace of Spades
            CardMappings[24] = 24;
            //Queen of Spades
            CardMappings[26] = 26;
            //10 of Diamonds
            CardMappings[12] = 12;

        }
        public static void Generate()
        {

        }

        public void SetTile(int X, int Y, Tile Tile)
        {
            if (!InRange(X, Y))
                return;
            if (Tiles[X, Y].Index == 3 && Tile.Index==1)
            {
                Tile.Index = 4;
            }
                Tiles[X, Y] = Tile;
                
        }

        public void SetTile(Point Point, Tile Tile)
        {
            SetTile(Point.X, Point.Y, Tile);
        }

        public void AddObject(MapObject Object)
        {
            this.Objects.Add(Object);
            if (Object is TimeSystem.IActor actor)
                Scheduler.Add(actor);
        }
        public void RemoveObject(MapObject Object)
        {
            this.Objects.Remove(Object);
            if (Object is TimeSystem.IActor actor)
                Scheduler.Remove(actor);
        }

        public bool InRange(int X, int Y)
        {
            if (X >= this.Width || X < 0)
                return false;
            if (Y >= this.Height || Y < 0)
                return false;
            return true;
        }

        public bool Passable(int X, int Y)
        {
            if (!InRange(X, Y))
                return false;
            return this.Tiles[X, Y].Passable;


        }

        public bool Occupied(int X, int Y)
        {
            return ItemAt(X, Y) == null;
        }

        public MapObject ItemAt(int X, int Y)
        {
            if (!InRange(X, Y))
                return null;
            foreach (MapObject o in this.Objects)
            {
                if (o.X == X && o.Y == Y)
                    return o;
            }
            if (Player.X == X && Player.Y == Y)
                return Player;
            return null;
        }

        public void Tick()
        {
            this.Tick(1);
        }
        public void Tick(int Ticks)
        {
            List<MapObject> deadUnits = new List<MapObject>();
            foreach (MapObject o in this.Objects)
            {
                if(o.IsDead)
                {
                    deadUnits.Add(o);
                }
                else
                {
                    o.Tick(Ticks);
                }
            }
            foreach(MapObject d in deadUnits)
            {
                RemoveObject(d);
            }
        }
        public void Render(SpriteBatch b, Texture2D t,Texture2D autot, Matrix m,float Scale)
        {
            Rectangle rekt = new Rectangle(0, 0, spriteWidth, spriteWidth);
            b.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, m);
            for (int y = 0; y < this.Tiles.GetLength(1); y++)
            {
                for (int x = 0; x < this.Tiles.GetLength(0); x++)
                {
                    if (Scale < 0.1f)
                        Scale = 0.1f;
                    if(this.Tiles[x,y].Index==0)
                    {
                        Vector2 AutoTileStart = BlobIndices[this.Tiles[x,y].WallAutoTileCode] * spriteWidth;
                        rekt = new Rectangle((int)AutoTileStart.X, (int)AutoTileStart.Y, spriteWidth, spriteWidth);
                        b.Draw(autot, new Vector2(x * spriteWidth * Scale, y * spriteWidth * Scale), rekt, Color.White, 0.0f, Vector2.Zero, Scale, SpriteEffects.None, 0.0f);

                    }
                    else
                    { 
                        rekt= new Rectangle(this.Tiles[x, y].Index * spriteWidth, 0, spriteWidth, spriteWidth);
                        b.Draw(t, new Vector2(x * spriteWidth * Scale, y * spriteWidth * Scale), rekt, Color.White, 0.0f, Vector2.Zero, Scale, SpriteEffects.None, 0.0f);
                    }
                }

            }
            b.End();
            
        }

    }
}
