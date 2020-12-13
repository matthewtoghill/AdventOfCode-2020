using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day11
{
    class Program
    {
        private static string[] input = File.ReadAllLines(@"..\..\..\data\day11.txt");
        private static int maxRows, maxCols;
        static void Main(string[] args)
        {
            Part1();
            Console.ReadLine();
        }

        private static void Part1()
        {
            maxRows = input.Length;
            maxCols = input[0].Length;

            Console.WriteLine($"Rows: {input.Length}");
            Console.WriteLine($"Columns: {input[0].Length}");

            // Create initial list of seats from input data
            Room room = new Room();
            room.Height = maxRows;
            room.Width = maxCols;

            List<Room> roomStates = new List<Room>();

            for (int row = 0; row < input.Length; row++)
                for (int col = 0; col < input[row].Length; col++)
                    room.Seats.Add(new Seat(col, row, input[row][col]));

            // Add initial room state to roomStates
            roomStates.Add(room);

            //room.Print();

            int emptyCount = room.Seats.Count(s => s.Status == Seat.SeatStatus.Empty);
            int occupiedCount = room.Seats.Count(s => s.Status == Seat.SeatStatus.Occupied);
            int floorCount = room.Seats.Count(s => s.Status == Seat.SeatStatus.Floor);

            Console.WriteLine($"Empty: {emptyCount}, Occupied: {occupiedCount}, Floor: {floorCount}, Total: {room.Seats.Count}");

            // Simulate each room state change and store in roomStates list
            // Keep a count of the number of seats that change status
            // Add new room state to list of roomStates if there are any seat changes
            // End when a room has no seats that change status
            int seatStatusChanges = 0, totalSeatStatusChanges = 0;
            bool keepGoing = true;
            while (keepGoing)
            {
                Room nextRoom = new Room(roomStates.Last());

                //Console.WriteLine();
                //nextRoom.Print();

                seatStatusChanges = nextRoom.SeatChanges;

                emptyCount = nextRoom.Seats.Count(s => s.Status == Seat.SeatStatus.Empty);
                occupiedCount = nextRoom.Seats.Count(s => s.Status == Seat.SeatStatus.Occupied);
                floorCount = nextRoom.Seats.Count(s => s.Status == Seat.SeatStatus.Floor);

                Console.WriteLine($"\nEmpty: {emptyCount}, Occupied: {occupiedCount}, Floor: {floorCount}, Total: {room.Seats.Count}, Changed: {nextRoom.SeatChanges}");


                if (seatStatusChanges > 0)
                {
                    totalSeatStatusChanges += seatStatusChanges;
                    roomStates.Add(nextRoom);
                }

                keepGoing = seatStatusChanges > 0;
            }

            Console.WriteLine($"Total room iterations: {roomStates.Count}");
            Console.WriteLine($"Total seat changes: {totalSeatStatusChanges}");

        }
    }

    public class Room
    {
        public List<Seat> Seats { get; set; } = new List<Seat>();
        public int SeatChanges { get; set; } = 0;
        public int Width { get; set; }
        public int Height { get; set; }

        public Room()
        {
        }

        public Room(Room previousState)
        {
            Width = previousState.Width;
            Height = previousState.Height;

            // Seat Status Change rules:
            // If a Seat is Empty, and there are no Occupied seats adjacent to it (up,down,left,right,diagonal)
            //      Seat becomes Occupied
            // If a Seat is Occupied and 4 or more seats adjacent to it are also Occupied 
            //      Seat becomes Empty

            // Check the 8 adjacent seats

            for (int i = 0; i < previousState.Seats.Count; i++)
            {
                // Create the Seat as a copy of the seat from the previous room state
                Seat thisSeat = new Seat(previousState.Seats[i]);

                // Skip floor spaces
                if (thisSeat.Status != Seat.SeatStatus.Floor)
                {
                    List<Seat.SeatStatus> adjacentSeatStatuses = new List<Seat.SeatStatus>();
                    // The up to 8 adjacent Seats can be found as:
                    // i = thisSeat index

                    // NW N NE      NW = i - rowWidth - 1,  N = i - rowWidth,   NE = i - rowWidth + 1
                    // W  i  E      W  = i - 1,                                 E  = i + 1
                    // SW S SE      SW = i + rowWidth - 1,  S = i + rowWidth,   SE = i + rowWdith + 1

                    // Need to also check whether the values are on the expected Row by checking the Y value

                    // North West
                    if (i - Width - 1 >= 0)
                    {
                        Seat nwSeat = previousState.Seats[i - Width - 1];
                        if (nwSeat.Y == thisSeat.Y - 1) adjacentSeatStatuses.Add(nwSeat.Status);
                    }

                    // North
                    if (i - Width >= 0)
                    {
                        Seat nSeat = previousState.Seats[i - Width];
                        if (nSeat.Y == thisSeat.Y - 1) adjacentSeatStatuses.Add(nSeat.Status);
                    }

                    // North East
                    if (i - Width + 1 >= 0)
                    {
                        Seat neSeat = previousState.Seats[i - Width + 1];
                        if (neSeat.Y == thisSeat.Y - 1) adjacentSeatStatuses.Add(neSeat.Status);
                    }

                    // West
                    if (i - 1 >= 0)
                    {
                        Seat wSeat = previousState.Seats[i - 1];
                        if (wSeat.Y == thisSeat.Y) adjacentSeatStatuses.Add(wSeat.Status);
                    }

                    // East
                    if (i + 1 < previousState.Seats.Count)
                    {
                        Seat eSeat = previousState.Seats[i + 1];
                        if (eSeat.Y == thisSeat.Y) adjacentSeatStatuses.Add(eSeat.Status);
                    }

                    // South West
                    if (i + Width - 1 < previousState.Seats.Count)
                    {
                        Seat swSeat = previousState.Seats[i + Width - 1];
                        if (swSeat.Y == thisSeat.Y + 1) adjacentSeatStatuses.Add(swSeat.Status);
                    }

                    // South
                    if (i + Width < previousState.Seats.Count)
                    {
                        Seat sSeat = previousState.Seats[i + Width];
                        if (sSeat.Y == thisSeat.Y + 1) adjacentSeatStatuses.Add(sSeat.Status);
                    }

                    // South East
                    if (i + Width + 1 < previousState.Seats.Count)
                    {
                        Seat seSeat = previousState.Seats[i + Width + 1];
                        if (seSeat.Y == thisSeat.Y + 1) adjacentSeatStatuses.Add(seSeat.Status);
                    }

                    int countOccupied = adjacentSeatStatuses.Count(s => s == Seat.SeatStatus.Occupied);
                    if (thisSeat.Status == Seat.SeatStatus.Empty && countOccupied == 0)
                    {
                        thisSeat.Status = Seat.SeatStatus.Occupied;
                        //SeatChanges++;
                    }
                    else // Occupied
                    {
                        if (countOccupied >= 4)
                        {
                            thisSeat.Status = Seat.SeatStatus.Empty;
                            //SeatChanges++;
                        }
                    }
                }

                // Add the seat in its updated state to the room state
                Seats.Add(thisSeat);

                if (thisSeat.Status != previousState.Seats[i].Status) SeatChanges++;
            }
        }

        public void Print()
        {
            for (int row = 0; row < Height; row++)
            {
                string thisRow = "";
                for (int col = 0; col < Width; col++)
                {
                    int index = row * Height + col;
                    switch (Seats[index].Status)
                    {
                        case Seat.SeatStatus.Empty:
                            thisRow += "L";
                            break;
                        case Seat.SeatStatus.Occupied:
                            thisRow += "#";
                            break;
                        default:
                            thisRow += ".";
                            break;
                    }
                }
                Console.WriteLine(thisRow);
            }
        }
    }

    public class Seat
    {
        public enum SeatStatus
        {
            Floor,
            Empty,
            Occupied
        }

        public int X { get; set; }
        public int Y { get; set; }
        public SeatStatus Status { get; set; }

        public Seat(int x, int y, char status)
        {
            X = x;
            Y = y;
            Status = SetSeatStatus(status);
        }

        // Copy Constructor
        public Seat(Seat copy)
        {
            X = copy.X;
            Y = copy.Y;
            Status = copy.Status;
        }

        private SeatStatus SetSeatStatus(char status)
        {
            switch (status)
            {
                case 'L':
                    return SeatStatus.Empty;
                case '#':
                    return SeatStatus.Occupied;
                case '.':
                default:
                    return SeatStatus.Floor;
            }
        }
    }
}
