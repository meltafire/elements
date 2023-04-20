using Elements.GameSession.Data;

namespace Elements.GameSession.Providers
{
    public interface ILevelDataSourceProvider
    {
        int FieldSizeI { get; }
        public Item[] Items { get; }
    }
}