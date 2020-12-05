namespace Day05
{
    class PlaneSeat
    {
        private int maxRows = 127;
        private int maxCols = 7;

        public int ID { get; private set; }
        public int Row { get; private set; }
        public int Column { get; private set; }
        public string PartitionString { get; private set; }

        public PlaneSeat(string partitionString)
        {
            PartitionString = partitionString;
            Row = GetRow(partitionString.Substring(0, 7));
            Column = GetColumn(partitionString.Substring(7));
            ID = Row * 8 + Column;
        }

        private int GetRow(string frontBack)
        {
            int lowR = 0, highR = maxRows, result = 0;

            for (int i = 0; i < frontBack.Length; i++)
            {
                switch (frontBack[i])
                {
                    case 'F':
                        // Set highR to be the top of lower half of the lowR to highR range
                        highR = (highR - (highR - lowR) / 2) - 1;
                        result = highR;
                        break;
                    case 'B':
                        // Set lowR to be the bottom of the upper half of the lowR to highR range
                        lowR = (lowR + (highR - lowR) / 2) + 1;
                        result = lowR;
                        break;
                    default:
                        break;
                }
            }

            return result;
        }

        private int GetColumn(string rightLeft)
        {
            int lowC = 0, highC = maxCols, result = 0;

            for (int i = 0; i < rightLeft.Length; i++)
            {
                switch (rightLeft[i])
                {
                    case 'L':
                        // Set highC to be the top of the of lower half of the lowC to highC range
                        highC = (highC - (highC - lowC) / 2) - 1;
                        result = highC;
                        break;
                    case 'R':
                        // Set lowC to be the bottom of the upper half of the lowC to highC range
                        lowC = (lowC + (highC - lowC) / 2) + 1;
                        result = lowC;
                        break;
                    default:
                        break;
                }
            }

            return result;
        }
    }
}
