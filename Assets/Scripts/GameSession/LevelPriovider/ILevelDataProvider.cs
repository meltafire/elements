using Elements.DataSource.Data;

namespace Elements.GameSession.LevelProvider
{
    public interface ILevelDataProvider
    {
        int FieldSizeI { get; }
        public Item[] Items { get; }
    }
}