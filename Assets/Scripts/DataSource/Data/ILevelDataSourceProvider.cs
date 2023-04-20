namespace Elements.DataSource.Data
{
    public interface ILevelDataSourceProvider
    {
        int FieldSizeI { get; }
        public Item[] Items { get; }
    }
}