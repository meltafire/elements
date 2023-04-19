namespace Elements.GameSession.Data
{
    public class SwapData
    {
        public readonly PositionData FromPosition;
        public readonly PositionData ToPosition;

        public SwapData(PositionData fromPosition, PositionData toPosition)
        {
            FromPosition = fromPosition;
            ToPosition = toPosition;
        }
    }
}