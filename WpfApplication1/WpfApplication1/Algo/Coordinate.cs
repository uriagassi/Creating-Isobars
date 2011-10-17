namespace UriAgassi.Isobars.Algo
{
    public struct Coordinate
    {
        public Coordinate(int x, int y)
            : this()
        {
            Y = y;
            X = x;
        }
        public int Y { get; private set; }
        public int X { get; private set; }
    }
}