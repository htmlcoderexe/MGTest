using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameObjects.MapObjects;
using Microsoft.Xna.Framework;

namespace GameObjects
{
    public class MapGeneratorMesh
    {
        public static void DrawRect(Map Map, int X, int Y, int W, int H, Map.Tile Tile)
        {
            for(int x =0; x<W;x++)
            {
                Map.SetTile(X + x, Y, Tile);
                Map.SetTile(X + x, Y+H-1, Tile);
            }
            for(int y=0;y<(H-1);y++)
            {
                Map.SetTile(X, Y + y, Tile);
                Map.SetTile(X+W-1, Y + y, Tile);
                
            }
        }

        public static void DrawRect(Map Map, Rectangle Rekt, Map.Tile Tile)
        {
            DrawRect(Map, Rekt.X, Rekt.Y, Rekt.Width, Rekt.Height, Tile);
        }

        public static void FillRect(Map Map, int X, int Y, int W, int H, Map.Tile Tile)
        {
            for (int x = 0; x < W; x++)
            {
                for (int y = 0; y < H; y++)
                {
                    Map.SetTile(X + x, Y+y, Tile);
                }
            

            }
        }

        public static int Manhattan(Point A, Point B)
        {
            return Math.Abs(A.X - B.X) + Math.Abs(A.Y - B.Y);
        }

        public static void FillRect(Map Map, Rectangle Rekt, Map.Tile Tile)
        {
           FillRect(Map, Rekt.X, Rekt.Y, Rekt.Width, Rekt.Height, Tile);
        }
        /// <summary>
        /// Places up to a specified number of non-overlapping rectangles within a given area.
        /// </summary>
        /// <param name="W">Area width</param>
        /// <param name="H">Area height</param>
        /// <param name="numattempts">Number of attempts to place</param>
        /// <param name="MinWidth">Minimum width of a rectangle</param>
        /// <param name="MinHeight">Minimum height of a rectangle</param>
        /// <param name="MaxWidth">Maximum width of a rectangle</param>
        /// <param name="MaxHeight">Maximum height of a rectangle</param>
        /// <param name="Spacing">Minimum space between rectangles</param>
        /// <returns></returns>
        public static List<Rectangle> AttemptRectangles(int W, int H, int numattempts, int MinWidth, int MinHeight, int MaxWidth, int MaxHeight,int Spacing)
        {
            W -= 2;H -= 2;
            System.Random RNG = new Random();
            List<Rectangle> result = new List<Rectangle>();
            int CurrentX, CurrentY, CurrentW, CurrentH;
            bool overlap;
            Rectangle CurrentRekt,CurrentHitbox;
            for(int i=0;i<numattempts;i++)
            {
                CurrentW = RNG.Next(MinWidth, MaxWidth);
                CurrentX = RNG.Next(1, W - CurrentW);
                CurrentH = RNG.Next(MinHeight, MaxHeight);
                CurrentY = RNG.Next(1, H - CurrentH);
                CurrentHitbox = new Rectangle(CurrentX-Spacing, CurrentY-Spacing, CurrentW+2*Spacing, CurrentH+2*Spacing);
                CurrentRekt = new Rectangle(CurrentX, CurrentY, CurrentW, CurrentH);
                overlap = false;
                foreach(Rectangle r in result)
                {
                    if(CurrentHitbox.Intersects(r))
                    {
                        overlap = true;
                        break;
                    }
                }
                if(!overlap)
                {
                    result.Add(CurrentRekt);
                }
            }

            return result;
        }
        /// <summary>
        /// Places objects in rooms defined by a list of rectangles.
        /// </summary>
        /// <param name="X">X Coordinate of the selected area</param>
        /// <param name="Y">Y Coordinate of the selected area</param>
        /// <param name="W">Width of the selected area</param>
        /// <param name="H">Height of the selected area</param>
        /// <param name="Rooms">List of Rectangles defining rooms</param>
        /// <param name="Count">Amount of objects to place. The result might be lower than this count</param>
        /// <param name="TakenSpots">A list of cordinates already occupied by objects, for chaining</param>
        /// <param name="ReturnNewOnly">If set to true, it will only return the newly placed points</param>
        /// <returns></returns>
        public static List<Point> PlaceOjects(int X, int Y, int W, int H, List<Rectangle> Rooms, int Count, List<Point> TakenSpots=null, bool ReturnNewOnly=false)
        {
            Random RNG = new Random(0);
            //if no previous list exists, create it
            if (TakenSpots == null )
                TakenSpots = new List<Point>();
            List<Point> NewPoints = new List<Point>();
            //
            int roomnumber = 0;
            int attempts = 0;
            Rectangle currentroom;
            Point currentpoint;
            for(int i=0;i<Count;i++)
            {
                //select a random room
                roomnumber = RNG.Next(Rooms.Count);
                //if after 20 rolls it still lands somewhere taken, it's either impossible or very difficult.
                attempts = 20;
                while(attempts>0)
                {
                    currentroom = Rooms[roomnumber];
                    currentpoint.X = RNG.Next(currentroom.X, currentroom.X + currentroom.Width);
                    currentpoint.Y = RNG.Next(currentroom.Y, currentroom.Y + currentroom.Height);
                    if(!(TakenSpots.Contains(currentpoint)))
                    {
                        TakenSpots.Add(currentpoint);
                        NewPoints.Add(currentpoint);
                        break;
                    }
                    attempts--;
                }
                //basically we just give up and say fuck it here at this point

            }


            return ReturnNewOnly?NewPoints:TakenSpots;
        }

        public int WhichRoom(List<Rectangle> Rooms, Point Point)
        {
            int result = -1;
            Rectangle rekt;
            for(int i=0; i<Rooms.Count;i++)
            {
                rekt = Rooms[i];
                if (rekt.Contains(Point))
                    return i;
            }
            return result;
        }

        public static void AddWalls(Map Map)
        {
            GameObjects.Map.Tile floortile, walltile, sidetile, doortile, doortile2;
            floortile = new Map.Tile();
            floortile.Index = 1;
            floortile.Passable = true;
            walltile = new Map.Tile();
            walltile.Index = 2;
            walltile.Passable = false;
            doortile = new Map.Tile();
            doortile.Index = 4;
            doortile.Passable = true;
            for (int x = 1; x < Map.Width - 1; x++)
                for (int y = 1; y < Map.Width - 1; y++)
                    if (Map.Tiles[x, y].Index == floortile.Index)
                        for (int a = -1; a < 2; a++)
                            for (int b = -1; b < 2; b++)
                                if (Map.Tiles[x + a, y + b].Index == 0)
                                    Map.SetTile(new Point(x + a, y + b),walltile);
        }

        public static void PlaceCorridorTile(Map Map, int X, int Y)
        {
            GameObjects.Map.Tile floortile, walltile, sidetile, doortile, doortile2;
            floortile = new Map.Tile();
            floortile.Index = 1;
            floortile.Passable = true;
            walltile = new Map.Tile();
            walltile.Index = 2;
            walltile.Passable = false;
            doortile = new Map.Tile();
            doortile.Index = 4;
            doortile.Passable = true;

            if (Map.Tiles[X, Y].Index == walltile.Index)
                Map.SetTile(new Point(X, Y), doortile);
            else
            Map.SetTile(new Point(X, Y), floortile);
        }
       
        public Map ConnectRooms(Map Map, Rectangle A, Rectangle B)
        {
            Point a, b, c, d;



            return Map;
        }

        public static Map DrawCorridor(Map Map, Rectangle start, Rectangle end)
        {
           // int start_x_lowerbound = (int)Math.Min(start.X, end.X);

            //determine start and end point

            

            return Map;
        }

        public static Map DrawCorridor(Map Map, Point start, Point end)
        {
            //TODO: this is horrible. definitely fix! 
            GameObjects.Map.Tile floortile, walltile, sidetile, doortile, doortile2;
            floortile = new Map.Tile();
            floortile.Index = 1;
            floortile.Passable = true;
            walltile = new Map.Tile();
            walltile.Index = 2;
            walltile.Passable = false;
            sidetile = new Map.Tile();
            sidetile.Index = 2;
            sidetile.Passable = false;
            doortile = new Map.Tile();
            doortile.Index = 4;
            doortile.Passable = true;
            doortile2 = new Map.Tile();
            doortile2.Index = 5;
            doortile2.Passable = true;
            Random RNG = new Random();


            Point CurrentPosition = start;
            int Xstep = start.X < end.X ? 1 : -1;
            int Ystep = start.Y < end.Y ? 1 : -1;

            bool goingX = RNG.NextDouble() > 0.5;
            bool noSwap = false;
            bool leftRoom = false;
            while(CurrentPosition!=end && CurrentPosition.X<Map.Width && CurrentPosition.Y<Map.Height)
            {
                if(goingX)
                {
                    if(CurrentPosition.X==end.X)
                    {
                        noSwap = true;

                        goingX = !goingX;
                    }
                    else
                    CurrentPosition.X += Xstep;
                }
                else
                {
                    if (CurrentPosition.Y == end.Y)
                    {
                        noSwap = true;

                        goingX = !goingX;
                    }
                    else
                        CurrentPosition.Y += Ystep;
                }
                if (!noSwap)
                    if (RNG.NextDouble() > 0.8)
                        goingX = !goingX;

                if (CurrentPosition.X < 0 || CurrentPosition.X >= Map.Width || CurrentPosition.Y < 0 || CurrentPosition.Y >= Map.Width)
                    return Map;
                if (Map.Tiles[CurrentPosition.X, CurrentPosition.Y].Index == walltile.Index && leftRoom)
                {
                    Map.SetTile(CurrentPosition.X,CurrentPosition.Y, doortile);
                    leftRoom = true;
                    if(goingX)
                    {
                        CurrentPosition.X += Xstep;
                    }
                    else
                    {
                        CurrentPosition.Y += Ystep;
                    }

                }
                    PlaceCorridorTile(Map, CurrentPosition.X, CurrentPosition.Y);
                
            }


            return Map;
        }

        public static Map DrawCorridor2(Map Map, Point start, Point end)
        {

            GameObjects.Map.Tile floortile, walltile, sidetile, doortile, doortile2;
            floortile = new Map.Tile();
            floortile.Index = 1;
            floortile.Passable = true;
            walltile = new Map.Tile();
            walltile.Index = 3;
            walltile.Passable = false;
            sidetile = new Map.Tile();
            sidetile.Index = 2;
            sidetile.Passable = false;
            doortile = new Map.Tile();
            doortile.Index = 4;
            doortile.Passable = true;
            doortile2 = new Map.Tile();
            doortile2.Index = 5;
            doortile2.Passable = true;
            Random RNG = new Random();
            int orientation = 0;
            int split, dx, dy, xo, yo, x, y;
            int LSkew = 1;
            int dist;

            
            //  end = new Point(RNG.Next(W), RNG.Next(H));
            orientation = RNG.Next(1);
            xo = 1;
            yo = 1;
            dx = end.X - start.X;
            if (dx < 0)
            {
                dx = Math.Abs(dx);
                xo = -1;
            }
            dy = end.Y - start.Y;
            if (dy < 0)
            {
                dy = Math.Abs(dy);
                yo = -1;
            }

            dist = Manhattan(start, end);
            if (dist < 3)
                return Map;
            bool leftroom = false;
            if (orientation == 0)
            {
                split = RNG.Next(0, dx + LSkew * 2);
                if (split < LSkew)
                    split = 0;
                if (split > dx + LSkew)
                    split = dx;
                leftroom = false;
                for (int j = 0; j < split; j++)
                {
                    x = start.X + (j * xo);
                    y = start.Y;

                    if (Map.Tiles[x, y].Index == floortile.Index && leftroom)
                    {
                        //Map.SetTile(x, y, doortile2);
                        break;
                    }
                    if (Map.Tiles[x, y].Index == 0)
                        leftroom = true;
                    Map.SetTile(x, y, floortile);
                }
                leftroom = false;
                for (int j = 0; j < dy; j++)
                {
                    x = start.X + (split * xo);
                    y = start.Y + (j * yo);
                    if (Map.Tiles[x, y].Index == floortile.Index && leftroom)
                    {
                        //Map.SetTile(x, y, doortile2);
                        break;
                    }
                    if (Map.Tiles[x, y].Index == 0)
                        leftroom = true;
                    Map.SetTile(x, y, floortile);
                }
                leftroom = false;
                for (int j = split; j < dx; j++)
                {
                    x = start.X + (j * xo);
                    y = start.Y + dy * yo;
                    if (Map.Tiles[x, y].Index == floortile.Index && leftroom)
                    {
                        //Map.SetTile(x, y, doortile2);
                        break;
                    }
                    if (Map.Tiles[x, y].Index == 0)
                        leftroom = true;
                    Map.SetTile(x, y, floortile);
                }
            }

            else
            {
                split = RNG.Next(0, dy + LSkew * 2);
                if (split < LSkew)
                    split = 0;
                if (split > dy + LSkew)
                    split = dy;
                leftroom = false;
                for (int j = 0; j < split; j++)
                {
                    y = start.Y + (j * yo);
                    x = start.X;
                    if (Map.Tiles[x, y].Index == floortile.Index && leftroom)
                    {
                        //Map.SetTile(x, y, doortile2);
                        break;
                    }
                    if (Map.Tiles[x, y].Index == 0)
                        leftroom = true;
                    Map.SetTile(x, y, floortile);
                }
                leftroom = false;
                for (int j = 0; j < dx; j++)
                {
                    y = start.Y + (split * yo);
                    x = start.X + (j * xo);
                    if (Map.Tiles[x, y].Index == floortile.Index && leftroom)
                    {
                        //Map.SetTile(x, y, doortile2);
                        break;
                    }
                    if (Map.Tiles[x, y].Index == 0)
                        leftroom = true;
                    Map.SetTile(x, y, floortile);
                }
                leftroom = false;
                for (int j = split; j < dy; j++)
                {
                    y = start.Y + (j * yo);
                    x = start.X + dx * xo;
                    if (Map.Tiles[x, y].Index == floortile.Index && leftroom)
                    {
                        //Map.SetTile(x, y, doortile2);
                        break;
                    }
                    if (Map.Tiles[x, y].Index == 0)
                        leftroom = true;
                    Map.SetTile(x, y, floortile);
                }
            }
            return Map;
        }


        public static Map Generate(int W, int H, int numrectangles,int spacing, float connectedness=0)
        {
            System.Random RNG = new Random();
            Map Map = new Map(W,H);
            //get a list of rooms
            List<Rectangle> rekts = AttemptRectangles(W, H, numrectangles, 5, 6, 12, 10,spacing);
            //initialize some tiles
            GameObjects.Map.Tile floortile, walltile, sidetile,doortile,doortile2;
            floortile = new Map.Tile();
            floortile.Index = 1;
            floortile.Passable = true;
            walltile = new Map.Tile();
            walltile.Index = 2;
            walltile.Passable = false;
            sidetile = new Map.Tile();
            sidetile.Index = 2;
            sidetile.Passable = false;
            doortile = new Map.Tile();
            doortile.Index = 4;
            doortile.Passable = true;
            doortile2 = new Map.Tile();
            doortile2.Index = 5;
            doortile2.Passable = true;
            Rectangle wall, side;
            //list of room centers
            List<Point> roomcenters = new List<Point>();
            Point currentcenter;

            //draw rooms and add their centers to list
            foreach (Rectangle REKT in rekts)
            {
                side = new Rectangle(REKT.X, REKT.Y - 1, REKT.Width, 1);
                wall = new Rectangle(REKT.X - 1, REKT.Y - 1, REKT.Width + 2, REKT.Height + 2);
                FillRect(Map, wall, walltile);
                FillRect(Map, REKT, floortile);
             //   FillRect(Map, side, sidetile);
             //  DrawRect(Map, wall, walltile);
                currentcenter = REKT.Center;
                
                roomcenters.Add(currentcenter);

                //branch out each direction from a random point until we hit a room
                //up
                Point up = new Point(RNG.Next(REKT.X, REKT.X + REKT.Width), REKT.Y-1);
                Point down = new Point(RNG.Next(REKT.X, REKT.X + REKT.Width), REKT.Y + REKT.Height);
                Point left = new Point(REKT.X-1, RNG.Next(REKT.Y, REKT.Y + REKT.Height));
                Point right = new Point(REKT.X + REKT.Width, RNG.Next(REKT.Y, REKT.Y + REKT.Height));

                //Map.SetTile(up, doortile2);
                //Map.SetTile(down, doortile2);
                //Map.SetTile(right, doortile2);
                //Map.SetTile(left, doortile2);



            }
            Triangulator.Delaunay t = new Triangulator.Delaunay();

            List<Vector3> edges = t.DoEdges(roomcenters);
            List<Vector3> tree = Generation.DisTreeItem.PruneEdges(edges, connectedness);
            

            //Each e is in the form of {roomA,roomB,Distance}
            foreach(Vector3 e in tree)
            {
                DrawCorridor(Map, roomcenters[(int)e.X], roomcenters[(int)e.Y]);
            }

            AddWalls(Map);
            /*/
            for (int x = 1; x < Map.Width; x++)
                for (int y = 1; y < Map.Height; y++)
                {
                    Map.Tile currenttile = Map.Tiles[x, y];
                    Map.Tile checktile;
                    if(currenttile.Index==0)
                    {
                        bool isEdge = false;
                        for(int dx=-1;dx<2;dx++)
                            for(int dy=-1;dy<2;dy++)
                            {
                                if (!Map.InRange(x + dx, y + dy))
                                    break;
                                checktile = Map.Tiles[x + dx, y + dy];
                                if(checktile.Index==1)
                                {
                                    isEdge = true;
                                    break;
                                }
                            }
                        if (isEdge)
                            Map.SetTile(new Point(x, y), walltile);
                    }

                }
            for (int x = 1; x < Map.Width; x++)
                for (int y = 1; y < Map.Height; y++)
                {
                    Map.Tile currenttile = Map.Tiles[x, y];
                    Map.Tile checktile;
                    if (currenttile.Index == floortile.Index)
                    {
                        bool isDoorway = false;
                        int walls = 0;
                        int floors = 0;
                        for (int dx = -1; dx < 2; dx++)
                            for (int dy = -1; dy < 2; dy++)
                            {
                                if (!Map.InRange(x + dx, y + dy))
                                    break;
                                checktile = Map.Tiles[x + dx, y + dy];
                                if (checktile.Index == walltile.Index)
                                {
                                    walls++;
                                }
                                if (checktile.Index == floortile.Index)
                                {
                                    floors++;
                                }
                            }
                        if ((floors==4 || floors==5)&&(walls==4||walls==5))
                            Map.SetTile(new Point(x, y), doortile);
                    }

                }
            //*/
            for (int i=0;i<40;i++)
            {
                
            }




            for (int i=0; i<20; i++)
            {
                
                



            }

            List<Point> spots = PlaceOjects(0, 0, W, H, rekts, 10);
            foreach(Point spot in spots)
            {
                //Map.SetTile(spot, doortile);
            }


            List<Point> p = GameObjects.MapGeneratorMesh.PlaceOjects(0, 0, W, H, rekts, 1, spots,true);
            Point spawn = p[0];
            Map.PlayerSpawn= spawn;

            //return Map;

            p = GameObjects.MapGeneratorMesh.PlaceOjects(0, 0, W, H, rekts, 6, spots, true);
            foreach(Point np in p)
            {
                Monster m = new Monster();
                m.X = np.X;
                m.Y = np.Y;
                m.ParentMap = Map;
                m.Friendliness = Actor.FriendlinessValue.Hostile;
               
                Map.Objects.Add(m);
            }
            p = GameObjects.MapGeneratorMesh.PlaceOjects(0, 0, W, H, rekts, 3);
            foreach (Point np in p)
            {
                ItemDrop c = new ItemDrop(new Items.ItemMLGCan());
                c.X = np.X;
                c.Y = np.Y;
                c.ParentMap = Map;
                Map.Objects.Add(c);
            }

            Map.CardMappings = CreateMappings(Map.CardMappings,31);

            p = PlaceOjects(0, 0, W, H, rekts, 28,p,true);
            foreach (Point np in p)
            { 
                ItemDrop c = new ItemDrop(new Items.ItemCard(RNG.Next(0,31)));
                c.X = np.X;
                c.Y = np.Y;
                c.ParentMap = Map;
                Map.Objects.Add(c);
            }



            return Map;
        }

        public static Dictionary<int,int> CreateMappings(Dictionary<int,int> input, int UpperBound, int LowerBound=0)
        {
            System.Random RNG = new Random();
           // Dictionary<int, int> result = new Dictionary<int, int>();
            List<int> mappings = Enumerable.Range(LowerBound, UpperBound - LowerBound + 1).ToList();
            List<int> results = new List<int>(mappings);
            foreach(KeyValuePair<int,int> kvp in input)
            {
                mappings.Remove(kvp.Key);
                results.Remove(kvp.Key);
            }
            while(results.Count>0)
            {
                int key = mappings[0];
                int value = results[RNG.Next(0, results.Count)];
                input[key] = value;
                mappings.Remove(key);
                results.Remove(value);
            }
            


            return input;
        }

    }
}
