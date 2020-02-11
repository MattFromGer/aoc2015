namespace ClassLib.helper
{
    public class MstEdge
    {
        public int Distance { get; }
        public string A { get; }
        public string B { get; }

        public MstEdge(string node1, string node2, int distance)
        {
            Distance = distance;
            A = node1;
            B = node2;
        }
    }
}