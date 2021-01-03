using AoCHelpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day17
{
    class Program
    {
        private static readonly string[] input = File.ReadAllLines(@"..\..\..\data\day17.txt");
        //private static readonly string[] input = new string[] { ".#.", "..#", "###" }; // test input

        private static HashSet<(int x, int y, int z)> activeCubes3D = new HashSet<(int x, int y, int z)>();
        private static HashSet<(int x, int y, int z, int w)> activeCubes4D = new HashSet<(int x, int y, int z, int w)>();

        private static (int x, int y, int z)[] directions3D = CreateDirections3D();
        private static (int x, int y, int z, int w)[] directions4D = CreateDirections4D();

        private static Dictionary<(int x, int y, int z), int> relevantLocations3D = new Dictionary<(int x, int y, int z), int>();
        private static Dictionary<(int x, int y, int z, int w), int> relevantLocations4D = new Dictionary<(int x, int y, int z, int w), int>();

        static void Main(string[] args)
        {
            Part1();
            Part2();
            Console.ReadLine();
        }

        private static void Part1()
        {
            // Parse the input to get all of the current active cubes
            for (int row = 0; row < input.Length; row++)
                for (int col = 0; col < input[row].Length; col++)
                    if (input[row][col] == '#') activeCubes3D.Add((col, row, 0));

            // Run 6 iterations
            for (int i = 0; i < 6; i++)
            {
                var nextIteration = RunNextCycle3D();
                activeCubes3D = nextIteration;
            }

            Console.WriteLine($"\nPart 1: {activeCubes3D.Count}");
        }

        private static void Part2()
        {
            // Parse the input to get all of the current active cubes
            for (int row = 0; row < input.Length; row++)
                for (int col = 0; col < input[row].Length; col++)
                    if (input[row][col] == '#') activeCubes4D.Add((col, row, 0, 0));

            // Run 6 cycles
            for (int i = 0; i < 6; i++)
            {
                var nextIteration = RunNextCycle4D();
                activeCubes4D = nextIteration;
            }

            Console.WriteLine($"\nPart 2: {activeCubes4D.Count}");
        }

        private static HashSet<(int x, int y, int z)> RunNextCycle3D()
        {
            // Copy the active cubes to a new set
            var newCubes = activeCubes3D.ToHashSet();

            // Clear the list of cubes that have been checked for the new cycle
            relevantLocations3D.Clear();

            // Iterate through each cube in the original active cube list
            // Count the active neighbours and then check if the condition is met to flip the Active status (remove from active list)
            foreach (var cube in activeCubes3D)
            {
                int activeNeighbours = CountActiveNeighbours3D(cube);
                if (!activeNeighbours.IsBetween(2, 3)) newCubes.Remove(cube);
            }

            // If the inactive location has exactly 3 active neighbours then add it to the new cubes list
            foreach (var loc in relevantLocations3D.Keys)
            {
                if (relevantLocations3D[loc] == 3) newCubes.Add(loc);
            }

            return newCubes;
        }

        private static HashSet<(int x, int y, int z, int w)> RunNextCycle4D()
        {
            // Copy the active cubes to a new set
            var newCubes = activeCubes4D.ToHashSet();

            // Clear the list of cubes that have been checked for the new cycle
            relevantLocations4D.Clear();

            // Iterate through each item in the original cubes list
            // Count the active neighbours and then check if the condition is met to flip the Active status (remove from active list)
            foreach (var cube in activeCubes4D)
            {
                int activeNeighbours = CountActiveNeighbours4D(cube);
                if (!activeNeighbours.IsBetween(2, 3)) newCubes.Remove(cube);
            }

            // If the inactive location has exactly 3 active neighbours then add it to the new cubes list
            foreach (var loc in relevantLocations4D.Keys)
            {
                if (relevantLocations4D[loc] == 3) newCubes.Add(loc);
            }

            return newCubes;
        }

        private static int CountActiveNeighbours3D((int x, int y, int z) cube)
        {
            int result = 0;
            foreach (var (x, y, z) in directions3D)
            {
                int nX = cube.x + x;
                int nY = cube.y + y;
                int nZ = cube.z + z;

                // If an active cube exists then increment the counter
                if (activeCubes3D.Contains((nX, nY, nZ)))
                    result++;
                // Else, record the location as a relevant inactive cube
                else
                    relevantLocations3D.IncrementAt((nX, nY, nZ));
            }
            return result;
        }

        private static int CountActiveNeighbours4D((int x, int y, int z, int w) cube)
        {
            int result = 0;
            foreach (var (x, y, z, w) in directions4D)
            {
                int nX = cube.x + x;
                int nY = cube.y + y;
                int nZ = cube.z + z;
                int nW = cube.w + w;

                // If an active cube exists then increment the counter
                if (activeCubes4D.Contains((nX, nY, nZ, nW)))
                    result++;
                // Else, record the location as a relevant inactive cube
                else
                    relevantLocations4D.IncrementAt((nX, nY, nZ, nW));
            }
            return result;
        }

        private static (int x, int y, int z)[] CreateDirections3D()
        {
            return Enumerable.Range(-1, 3)
                .SelectMany(x => Enumerable.Range(-1, 3)
                    .SelectMany(y => Enumerable.Range(-1, 3)
                        .Select(z => (x, y, z))))
                .Where(d => d != (0, 0, 0))
                .ToArray();
        }

        private static (int x, int y, int z, int w)[] CreateDirections4D()
        {
            return Enumerable.Range(-1, 3)
                .SelectMany(x => Enumerable.Range(-1, 3)
                    .SelectMany(y => Enumerable.Range(-1, 3)
                        .SelectMany(w => Enumerable.Range(-1, 3)
                            .Select(z => (x, y, z, w)))))
                .Where(d => d != (0, 0, 0, 0))
                .ToArray();
        }
    }
}
