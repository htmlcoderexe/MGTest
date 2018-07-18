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
        int spriteWidth=16;
        public Map(int W, int H)
        {
            this.Tiles = new Tile[W, H];
            this.Objects = new List<MapObject>();
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
                Objects.Remove(d);
            }
        }
        public void Render(SpriteBatch b, Texture2D t, Matrix m,float Scale)
        {
            Rectangle rekt = new Rectangle(0, 0, spriteWidth, spriteWidth);
            b.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, m);
            for (int y = 0; y < this.Tiles.GetLength(1); y++)
            {
                for (int x = 0; x < this.Tiles.GetLength(0); x++)
                {
                    rekt.X = this.Tiles[x, y].Index * spriteWidth;
                    if (Scale < 0.1f)
                        Scale = 0.1f;
                    b.Draw(t, new Vector2(x * spriteWidth*Scale, y * spriteWidth*Scale), rekt, Color.White, 0.0f, Vector2.Zero, Scale, SpriteEffects.None, 0.0f);
                }

            }
            b.End();
        }

    }
}
