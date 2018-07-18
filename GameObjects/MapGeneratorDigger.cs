using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace GameObjects
{
    public class MapGeneratorDigger
    {
        private Map _currentmap;
        private int[,] _genmap;
        private Random _RNG;
        const int DIR_RIGHT = 3;
        const int DIR_LEFT = 2;
        const int DIR_DOWN = 1;
        const int DIR_UP = 0;
        enum TileType
        {
            Nothing,
            Floor,
            RoomSideWall,
            RoomWall,
            CorridorWall,
            CorridorSideWall,
            Door
        }
        public MapGeneratorDigger(int W, int H)
        {
            _currentmap = new Map(W, H);
            _genmap = new int[W, H];
            _RNG = new Random((int)System.DateTime.Now.Ticks);
        }
        public Vector4 FindWall()
        {
            Vector4 result = new Vector4();
            bool found = false;
            int X, Y,Target, Up, Down, Left, Right;
            int wallcount;
            int walltype;
            int sidewalltype;
            int doortype = (int)TileType.Door;
            int floortype=(int)TileType.Floor;
            while(!found)
            {
                X = _RNG.Next(1, _currentmap.Width-1);
                Y = _RNG.Next(1, _currentmap.Height-1);
                Target = _genmap[X, Y];
                Up = _genmap[X, Y-1];
                Down = _genmap[X, Y+1];
                Left = _genmap[X-1, Y];
                Right = _genmap[X+1, Y];
                
                if(Target!=(int)TileType.RoomWall && Target!=(int)TileType.CorridorWall)
                {
                    continue;
                }
                walltype = Target;
                sidewalltype = walltype == (int)TileType.CorridorWall ? (int)TileType.CorridorSideWall : (int)TileType.RoomSideWall;
                int canmakeroom  = walltype == (int)TileType.CorridorWall ? 0 : 1;
                wallcount = 0;

                if(Up==walltype && Down== walltype)
                {
                    if (Left == floortype && Right == 0)
                    {
                        return new Vector4(X+1, Y, DIR_RIGHT,canmakeroom);
                    }
                    if (Left == 0 && Right == floortype)
                    {
                        return new Vector4(X-1, Y, DIR_LEFT, canmakeroom);
                    }
                }
                if(Left==walltype&&Right==walltype)
                {
                    if (Up == floortype && Down == 0)
                    {
                        
                        return new Vector4(X, Y + 1, DIR_DOWN, canmakeroom);
                    }
                    if (Up == 0 && Down == sidewalltype)
                    {
                       
                        return new Vector4(X, Y -2,DIR_UP, canmakeroom);
                    }
                }
            }
            return result;
        }
        public int[,] GenerateFeature(bool vertical=false,bool gencorridorsonly=false)
        {
            int[,] feature;
            bool success = false;
            int roll = _RNG.Next(20);
            
            if(roll<18 && !gencorridorsonly)
            {
                feature = GenerateRoom(5,6,12,13);
            }
            else
            {
                feature = GenerateCorridor(5, 20,vertical?0:1);
            }
            return feature;
        }

        public int[,] GenerateRoom(int MinWidth, int MinHeight, int MaxWidth, int MaxHeight)
        {
            int w = _RNG.Next(MinWidth, MaxWidth + 1);
            int h = _RNG.Next(MinHeight, MaxHeight + 1);
            int[,] room = new int[w, h];

            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    room[x, y] = (int)TileType.RoomWall;
                } }
            for (int x = 1; x < w - 1; x++)
            {
                for (int y = 1; y < h - 1; y++)
                {
                    room[x, y] = (int)TileType.Floor;
                } }
            for(int x=1;x<w-1;x++)
            {
                room[x, 1] = (int)TileType.RoomSideWall;
            }


            return room;
        }

        public int[,] GenerateCorridor(int MinLength, int MaxLength, int orientation = 2)
        {
            int length = _RNG.Next(MinLength, MaxLength);
            bool vertical = _RNG.Next(1) == 0;
            if (orientation < 2)
                vertical = orientation == 0;
            int w = vertical ? 3 : length;
            int h = vertical ? length : 4;
            int[,] corridor = new int[w, h];

            if(vertical)
            {
                for(int y=0;y<length;y++)
                {
                    corridor[0, y] = (int)TileType.CorridorWall;
                    corridor[1, y] = (int)TileType.Floor;
                    corridor[2, y] = (int)TileType.CorridorWall;
                }
            }
            else
            {
                for (int x = 0; x < length; x++)
                {
                    corridor[x, 0] = (int)TileType.CorridorWall;
                    corridor[x, 1] = (int)TileType.CorridorSideWall;
                    corridor[x, 2] = (int)TileType.Floor;
                    corridor[x, 3] = (int)TileType.CorridorWall;
                }
            }
            return corridor;
        }


        public bool CheckFeature(int[,] Feature, int X, int Y, bool Burn=false)
        {
            //checking the obvious first
            if (X < 0 || Y < 0)
                return false;
            //get feature dimensions
            int w = Feature.GetLength(0);
            int h = Feature.GetLength(1);
            //check out of bounds
            if (w + X > _genmap.GetLength(0) || h + Y > _genmap.GetLength(1))
                return false;

            int target;
            int source;

            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    if(Burn)
                    {
                        _genmap[x + X, y + Y] = Feature[x, y];
                      //  _genmap[x , y] = Feature[x, y];
                    }
                    else
                    { 
                        //correspond tiles in feature and map
                        target = _genmap[x + X, y + Y];
                        source = Feature[x, y];
                        //if either is 0, no overlap, check next
                        if (target == (int)TileType.Nothing || source == (int)TileType.Nothing)
                            break;
                        //if source and target are same, doesn't matter
                        if (source == target)
                            break;
                        //source and target not equal and not 0, collision detected
                        return false;
                    }
                }
            }
            return true;
        }
        public Map Generate(int generations)
        {
            int[,] currentfeature = GenerateRoom(8, 9, 12, 13);
            CheckFeature(currentfeature,(int)_currentmap.Width / 2, (int)_currentmap.Height / 2,true);
            for(int i=0;i<generations;i++)
            {
                Vector4 W = FindWall();
                currentfeature = GenerateFeature(W.Z==DIR_DOWN||W.Z==DIR_UP,W.W==1);
                int cfW = currentfeature.GetLength(0);
                int cfH = currentfeature.GetLength(1);
                switch ((int)W.Z)
                {
                    case DIR_DOWN:
                        {
                            if (CheckFeature(currentfeature, (int)W.X - cfW / 2, (int)W.Y)) { 
                                CheckFeature(currentfeature, (int)W.X - cfW / 2, (int)W.Y, true);
                                _genmap[(int)W.X, (int)W.Y-1] = (int)TileType.Door;
                                _genmap[(int)W.X, (int)W.Y-2] = (int)TileType.Floor;
                            }
                            break;
                        }
                    case DIR_UP:
                        {
                            if (CheckFeature(currentfeature, (int)W.X - cfW / 2, (int)W.Y + cfH))
                            {
                                CheckFeature(currentfeature, (int)W.X - cfW / 2, (int)W.Y + cfH, true);
                                _genmap[(int)W.X, (int)W.Y+1] = (int)TileType.Door;
                            }
                            break;
                        }
                    case DIR_RIGHT:
                        {
                            bool dr = CheckFeature(currentfeature, (int)W.X-1, (int)W.Y - cfH / 2);
                            if (dr)
                            {
                                CheckFeature(currentfeature, (int)W.X-1, (int)W.Y - cfH / 2, true);
                                _genmap[(int)W.X-1, (int)W.Y] = (int)TileType.Door;
                            }
                            break;
                        }
                    case DIR_LEFT:
                        {
                            if (CheckFeature(currentfeature, (int)W.X - cfW+1, (int)W.Y - cfH / 2))
                            {
                                CheckFeature(currentfeature, (int)W.X - cfW+1, (int)W.Y - cfH / 2, true);
                                _genmap[(int)W.X+1, (int)W.Y] = (int)TileType.Door;
                            }
                                break;
                        }
                }
            }
            int tile = 0;
            Map.Tile t = new Map.Tile();
            for(int x=0;x<_currentmap.Width;x++)
                for(int y=0;y<_currentmap.Height;y++)
                {
                    tile = _genmap[x, y];
                    t.Index = 0;
                    if (tile == (int)TileType.RoomWall || tile == (int)TileType.CorridorWall)
                        t.Index = 3;
                    if (tile == (int)TileType.RoomSideWall || tile == (int)TileType.CorridorSideWall)
                        t.Index = 2;
                    if (tile == (int)TileType.Floor)
                        t.Index = 1;
                    if (tile == (int)TileType.Door)
                        t.Index = 4;
                    _currentmap.SetTile(x, y,t);
                }
            _currentmap.PlayerSpawn = new Point(32, 32);
            return _currentmap;
        }
    }
}
