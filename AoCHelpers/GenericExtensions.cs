namespace AoCHelpers
{
    public static class GenericExtensions
    {
        public static void Swap<T>(ref T firstVal, ref T secondVal)
        {
            T temp = firstVal;
            firstVal = secondVal;
            secondVal = temp;
        }
    }
}
