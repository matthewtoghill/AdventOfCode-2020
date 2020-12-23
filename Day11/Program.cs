using AoCHelpers;
using System;
using System.IO;
using System.Linq;

namespace Day11
{
    class Program
    {
        private static readonly string[] input = File.ReadAllLines(@"..\..\..\data\day11.txt");
        //                                                         NW        N       NE        W       E       SW       S       SE
        private static readonly (int x, int y)[] directions = { (-1, -1), (0, -1), (1, -1), (-1, 0), (1, 0), (-1, 1), (0, 1), (1, 1) };
        private static int maxRows, maxCols;
        private static string[] currRoom;
        static void Main(string[] args)
        {
            int seatsOccupiedAtEnd;

            // Part 1
            seatsOccupiedAtEnd = SimulateRoomChanges(4, false);
            Console.WriteLine($"Part 1: {seatsOccupiedAtEnd}");

            // Part 2
            seatsOccupiedAtEnd = SimulateRoomChanges(5, true);
            Console.WriteLine($"\nPart 2: {seatsOccupiedAtEnd}");

            Console.ReadLine();
        }


        private static int SimulateRoomChanges(int occupiedCountReq, bool skipFloorSpaces)
        {
            maxRows = input.Length;
            maxCols = input[0].Length;

            currRoom = input.ToArray();

            int emptyCount = currRoom.Sum(s => s.Count(c => c == 'L'));
            int occupiedCount = currRoom.Sum(s => s.Count(c => c == '#'));
            int floorCount = currRoom.Sum(s => s.Count(c => c == '.'));

            //Console.WriteLine($"Empty: {emptyCount}, Occupied: {occupiedCount}, Floor: {floorCount}\n");

            // Simulate room changes
            int totalSeatStatusChanges = 0;
            bool keepGoing = true;
            while (keepGoing)
            {
                int seatStatusChanges = 0;

                // Create a copy of the room 
                string[] nextRoom = currRoom.ToArray();

                // Check each seat in the current room array
                // update the next room array if it meets criteria to change seat status
                for (int row = 0; row < nextRoom.Length; row++)
                {
                    for (int col = 0; col < nextRoom[row].Length; col++)
                    {
                        char thisSeat = currRoom[row][col];
                        if (thisSeat == '.') continue; // Skip floor spaces

                        int occupiedNearby = CountNearbyOccupiedSeats(col, row, skipFloorSpaces);

                        // If the seat is currently Occupied and there are 4 or more occupied nearby
                        // Then change the seat to Empty and continue to the next seat
                        if (thisSeat == '#' && occupiedNearby >= occupiedCountReq)
                        {
                            nextRoom[row] = nextRoom[row].ReplaceAtIndex(col, 'L');
                            seatStatusChanges++;
                        }

                        // If the seat is currently Empty and there are 0 occupied nearby
                        // Then change the seat to Occupied and continue to the next seat
                        if (thisSeat == 'L' && occupiedNearby == 0)
                        {
                            nextRoom[row] = nextRoom[row].ReplaceAtIndex(col, '#');
                            seatStatusChanges++;
                        }
                    }
                }

                // Replace the current room with the completed next room array
                currRoom = nextRoom.ToArray();

                emptyCount = currRoom.Sum(s => s.Count(c => c == 'L'));
                occupiedCount = currRoom.Sum(s => s.Count(c => c == '#'));
                floorCount = currRoom.Sum(s => s.Count(c => c == '.'));
                //Console.WriteLine($"\nEmpty: {emptyCount}, Occupied: {occupiedCount}, Floor: {floorCount} Changed: {seatStatusChanges}");

                if (seatStatusChanges > 0)
                {
                    totalSeatStatusChanges += seatStatusChanges;
                }

                keepGoing = seatStatusChanges > 0;
            }
            return occupiedCount;
        }

        // Use the directions x,y array to find the next seat in each direction
        // For Part 1, just check the next space in each direction
        // For Part 2, check the next seat in each direction, i.e. keep 'travelling' in direction
        //             until a seat is reached, so skip floor spaces
        // If reach out of bound location then stop checking in that direction
        // Return the number of Occupied '#' seats from all directions
        private static int CountNearbyOccupiedSeats(int startX, int startY, bool skipFloorSpaces)
        {
            int occupiedSeatsCount = 0;

            // Check each direction
            for (int i = 0; i < directions.Length; i++)
            {
                int directionMultiplier = 1;
                do
                {
                    // Get the next X,Y position in the direction 
                    // Direction Multiplier used to move along direction until a non floor space is reached
                    int newX = startX + (directionMultiplier * directions[i].x);
                    int newY = startY + (directionMultiplier * directions[i].y);

                    // Check if the X,Y values fall outside the room bounds
                    if (newX < 0 || newX >= maxCols) break;
                    if (newY < 0 || newY >= maxRows) break;

                    // On an occupied seat, increment counter and end loop to move onto next direction check
                    if (currRoom[newY][newX] == '#')
                    {
                        occupiedSeatsCount++;
                        break;
                    }

                    // On an empty seat, end loop to move onto next direction check
                    if (currRoom[newY][newX] == 'L') break;

                    directionMultiplier++;
                } while (skipFloorSpaces);
            }
            return occupiedSeatsCount;
        }
    }
}
