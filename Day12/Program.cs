using AoCHelpers;
using System;
using System.IO;

namespace Day12
{
    class Program
    {
        private static readonly string[] input = File.ReadAllLines(@"..\..\..\data\day12.txt");

        static void Main(string[] args)
        {
            Part1();
            Part2();
            Console.ReadLine();
        }

        private static void Part1()
        {
            int x = 0, y = 0, degrees = 90;

            foreach (var line in input)
            {
                char action = line[0];
                int value = int.Parse(line.Substring(1));

                switch (action)
                {
                    case 'N': y -= value; break;
                    case 'S': y += value; break;
                    case 'E': x += value; break;
                    case 'W': x -= value; break;
                    case 'R': degrees = (degrees + value) % 360; break;
                    case 'L': degrees -= value; if (degrees < 0) degrees += 360; break;
                    case 'F':
                        switch (degrees)
                        {
                            case 0: y -= value; break; // N
                            case 90: x += value; break; // E
                            case 180: y += value; break; // S
                            case 270: x -= value; break; // W
                            default: break;
                        }
                        break;
                    default: break;
                }
            }

            Console.WriteLine($"Part 1: End at ({x},{y})");
            Console.WriteLine($"Manhattan distance: {(x, y).ManhattanDistance((0, 0))}");
        }

        private static void Part2()
        {
            int wx = 10, wy = -1, x = 0, y = 0;

            foreach (var line in input)
            {
                char action = line[0];
                int value = int.Parse(line.Substring(1));

                switch (action)
                {
                    case 'N': wy -= value; break;
                    case 'S': wy += value; break;
                    case 'E': wx += value; break;
                    case 'W': wx -= value; break;
                    case 'R':
                    case 'L':
                        if ((action == 'R' && value == 90) || (action == 'L' && value == 270))
                        {
                            GenericExtensions.Swap(ref wx, ref wy);
                            wx *= -1;
                            break;
                        }

                        if ((action == 'R' && value == 270) || (action == 'L' && value == 90))
                        {
                            GenericExtensions.Swap(ref wx, ref wy);
                            wy *= -1;
                            break;
                        }

                        if (value == 180)
                        {
                            wx *= -1;
                            wy *= -1;
                            break;
                        }

                        break;
                    case 'F':
                        // move to the waypoint the given number of times
                        x += (wx * value);
                        y += (wy * value);
                        break;
                    default: break;
                }
            }

            Console.WriteLine($"\nPart 2: End at ({x},{y})");
            Console.WriteLine($"Manhattan Distance: {(x, y).ManhattanDistance((0, 0))}");
        }
    }
}
