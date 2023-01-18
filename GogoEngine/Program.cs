using System.Numerics;

namespace GogoEngine
{
    class GameObject
    {
        string character;
        public float X, Y;
        public float rotation;
        public void CreateCharacter(char chr, ref GameObject obj)
        {
            obj.character = chr.ToString();
        }
        public void Spawn(ref string[,] map, GameObject obj, float objX, float objY)
        {
            obj.X = objX;
            obj.Y = objY;
            map[(int)objY, (int)objX] = obj.character + " ";
        }
        public void SetLocation(ref GameObject obj, ref string[,] map, ref float x, ref float y)
        {
            obj.X = x;
            obj.Y = y;
            if(obj.X >= map.GetLength(1) - 1)
            {
                obj.X = map.GetLength(1) - 2;
            }
            if (obj.X <= 0)
            {
                obj.X = 0;
            }
            if (obj.Y >= map.GetLength(0) - 1)
            {
                obj.Y = map.GetLength(0) - 2;
            }
            if (obj.Y <= 0)
            {
                obj.Y = 0;
            }
            map[(int)obj.Y, (int)obj.X] = obj.character + " ";
        }
        public bool Collide(GameObject obj, GameObject tObj)
        {
            if(obj.X == tObj.X)
            {
                if(obj.Y == tObj.Y)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
    class Map
    {
        public void MapCreator(ref string[,] map, int x, int y)
        {
            map = new string[y, x];
            for(int i = 0; i < y - 1; i++)
            {
                for(int j = 0; j < x - 1; j++)
                {
                    map[i, j] = "- ";
                }
            }
        }
        public void ClearMap(ref string[,] map)
        {
            for (int i = 0; i < map.GetLength(0) - 1; i++)
            {
                for (int j = 0; j < map.GetLength(1) - 1; j++)
                {
                    map[i, j] = "- ";
                }
            }
            Console.Clear();
        }
        public void MapDisplay(string[,] map)
        {
            for(int i = 0; i < map.GetLength(0) - 1; i++)
            {
                Console.WriteLine();
                for(int j = 0; j < map.GetLength(1) - 1; j++)
                {
                    Console.Write(map[i, j]);
                }
            }
        }
    }
    internal class Program
    {
        // 0 = right ||| 90 = up ||| 180 = left ||| 270 = down
        public static void Main(string[] args)
        {
            ConsoleKeyInfo key;
            bool bulletFired = false;
            bool targetDown = false;
            string[,] sMap = new string[1, 1];
            GameObject player = new GameObject();
            GameObject target = new GameObject();
            GameObject bullet = new GameObject();
            Map map = new Map();
            map.MapCreator(ref sMap, 10, 10);
            player.CreateCharacter('X', ref player);
            target.CreateCharacter('Y', ref target);
            bullet.CreateCharacter('B', ref bullet);
            player.Spawn(ref sMap, player, 4, 4);
            target.Spawn(ref sMap, target, 4, 8);
            int ammo = 5;
            while (true)
            {
                map.MapDisplay(sMap);
                Console.WriteLine();
                Console.WriteLine("X: " + player.X);
                Console.WriteLine("Y: " + player.Y);
                Console.WriteLine("Ammo: " + ammo);
                Console.WriteLine();
                Console.WriteLine("Reload = R");
                Console.WriteLine("Fire = Space");
                key = Console.ReadKey();
                if (key.Key == ConsoleKey.W)
                {
                    sMap[(int)player.Y, (int)player.X] = "- ";
                    player.Y--;
                    player.rotation = 90;
                    player.SetLocation(ref player, ref sMap, ref player.X, ref player.Y);
                }
                if (key.Key == ConsoleKey.S)
                {
                    sMap[(int)player.Y, (int)player.X] = "- ";
                    player.Y++;
                    player.rotation = 270;
                    player.SetLocation(ref player, ref sMap, ref player.X, ref player.Y);
                }
                if (key.Key == ConsoleKey.A)
                {
                    sMap[(int)player.Y, (int)player.X] = "- ";
                    player.X--;
                    player.rotation = 180;
                    player.SetLocation(ref player, ref sMap, ref player.X, ref player.Y);
                }
                if (key.Key == ConsoleKey.D)
                {
                    sMap[(int)player.Y, (int)player.X] = "- ";
                    player.X++;
                    player.rotation = 0;
                    player.SetLocation(ref player, ref sMap, ref player.X, ref player.Y);
                }
                if (key.Key == ConsoleKey.Spacebar && bulletFired == false && ammo > 0)
                {
                    bulletFired = true;
                    bullet.rotation = player.rotation;
                    bullet.Spawn(ref sMap, bullet, player.X, player.Y);
                }
                if (key.Key == ConsoleKey.R && bulletFired == true)
                {
                    bulletFired = false;
                    ammo--;
                }
                if (key.Key == ConsoleKey.Escape)
                {
                    break;
                }
                target.SetLocation(ref target, ref sMap, ref target.X, ref target.Y);
                if (bulletFired)
                {
                    sMap[(int)bullet.Y, (int)bullet.X] = "- ";
                    if (bullet.rotation == 0)
                    {
                        bullet.X++;
                    }
                    if (bullet.rotation == 90)
                    {
                        bullet.Y--;
                    }
                    if (bullet.rotation == 180)
                    {
                        bullet.X--;
                    }
                    if (bullet.rotation == 270)
                    {
                        bullet.Y++;
                    }
                    bullet.SetLocation(ref bullet, ref sMap, ref bullet.X, ref bullet.Y);
                }
                if (!bulletFired)
                {
                    sMap[(int)bullet.Y, (int)bullet.X] = "- ";
                }
                if (bullet.Collide(bullet, target))
                {
                    targetDown = true;
                }
                if (targetDown)
                {
                    sMap[(int)target.Y, (int)target.X] = "- ";
                }
                Console.Clear();
            }
        }
    }
}