using Elements.DataSource.Data;
using UnityEngine;

namespace Elements.DataSource
{
    [CreateAssetMenu(fileName = "Level", menuName = "elements game/Level", order = 1)]
    public class Level : ScriptableObject, ILevelDataSourceProvider
    {
        [SerializeField]
        private int _fieldSizeI;
        [SerializeField]
        private Item[] _items;

        public int FieldSizeI => _fieldSizeI;
        public Item[] Items => _items;
    }
}