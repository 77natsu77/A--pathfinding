using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A_star_pathfinding
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // A 10x10 Map
            int[,] map = new int[10, 10];

            //simple "Wall" across the middle
            for (int i = 2; i < 8; i++)
            {
                map[i, 5] = 1;
            }

            // Define Start and End
            Node start = new Node(0, 0);
            Node target = new Node(9, 9);
            List<Node> NodeList = new List<Node>();

            Pathfinding(map, start, target, NodeList);
        }

        static void OutputGrid(int[,] map)
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (map[i, j] == 1)
                    {
                        Console.Write('#');//WALL
                    }
                    else if (map[i, j] == 2 || map[i, j] == 0)
                    {
                        Console.Write('.');//GROUND
                    }
                    else if (map[i, j] == 7)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write('*');//PATH
                        Console.ResetColor();
                    }
                }

                Console.WriteLine("| " + String.Format("{0,3}", i));

            }
        }

        static int GetDistance(int x1, int y1, int x2, int y2)
        {
            return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
        }

        static void PathfindingV1(int[,] map, Node start, Node target, List<Node> NodeList)
        {
            Node current;
            NodeList.Append(start);
            while (NodeList.Count != 0)
            {

                current = NodeList.Find(i => i.F == NodeList.Min(j => j.F));
                NodeList.Remove(current);
                if (map[current.X, current.Y] == 2)
                {
                    continue;
                }
                map[current.X, current.Y] = 2;

                if (current.X == target.X && current.Y == target.Y)
                {
                    Console.WriteLine("Path Found!");
                    Node temp = current;
                    while (temp != null)
                    {
                        map[temp.X, temp.Y] = 7; // Mark the final path with a new number
                        temp = temp.Parent;
                    }
                    OutputGrid(map);
                    return;
                }



                if (current.X - 1 >= 0)//LEFT
                {
                    if (map[current.X - 1, current.Y] != 1)
                    {
                        NodeList.Add(new Node(current.X - 1, current.Y, current.G + 1, GetDistance(current.X - 1, current.Y,target.X,target.Y), current));
                    }

                }
                if (current.X + 1 <= 9)//RIGHT
                {
                    if (map[current.X + 1, current.Y] != 1)
                    {
                        NodeList.Add(new Node(current.X + 1, current.Y, current.G + 1, GetDistance(current.X + 1, current.Y, target.X, target.Y), current));
                    }

                }
                if (current.Y - 1 >= 0)//UP
                {
                    if (map[current.X, current.Y - 1] != 1)
                    {
                        NodeList.Add(new Node(current.X, current.Y - 1, current.G + 1, GetDistance(current.X, current.Y - 1, target.X, target.Y), current));
                    }

                }
                if (current.Y + 1 <= 9)//DOWN
                {
                    if (map[current.X, current.Y + 1] != 1)
                    {
                        NodeList.Add(new Node(current.X, current.Y + 1, current.G + 1, GetDistance(current.X, current.Y + 1, target.X, target.Y), current));
                    }

                }



            }
        }//A* Pathfinding

        static void Pathfinding(int[,] map, Node start, Node target, List<Node> NodeList)
        {
            NodeList.Add(start);

            while (NodeList.Count > 0)
            {
                Node current = NodeList.OrderBy(n => n.F).ThenBy(n => n.H).First();
                NodeList.Remove(current);

                if (map[current.X, current.Y] == 2) continue;
                map[current.X, current.Y] = 2;

                if (current.X == target.X && current.Y == target.Y)
                {
                    Console.WriteLine("Path Found!");
                    Node temp = current;
                    while (temp != null)
                    {
                        map[temp.X, temp.Y] = 7; // Mark the final path with a new number
                        temp = temp.Parent;
                    }
                    OutputGrid(map);
                    return;
                }

                int[] dx = { -1, 1, 0, 0 };
                int[] dy = { 0, 0, -1, 1 };

                for (int i = 0; i < 4; i++)
                {
                    int newX = current.X + dx[i];
                    int newY = current.Y + dy[i];

                    if (newX >= 0 && newX < 10 && newY >= 0 && newY < 10 && map[newX, newY] != 1 && map[newX, newY] != 2)
                    {
                        int g = current.G + 1;
                        int h = GetDistance(newX, newY, target.X, target.Y);
                        NodeList.Add(new Node(newX, newY, g, h, current));
                    }
                }
            }
        }

        class Node
        {
            public int X, Y;
            public Node Parent;
            public int G; // Distance from start
            public int H; // Distance to target
            public int F => G + H; // Total cost (shortcut property)

            public Node(int x, int y, int g = 0, int h = 0, Node parent = null)
            {
                X = x;
                Y = y;
                G = g;
                H = h;
                Parent = parent;
            }
        }
    }
}

    
