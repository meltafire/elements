namespace Elements.GameSession.Data
{
    public class PositionData
    {
        private int _i;
        private int _j;

        public int I => _i;
        public int J => _j;

        public void SetData(int i, int j)
        {
            _i = i;
            _j = j;
        }
    }
}