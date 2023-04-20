using UnityEngine;

namespace Elements.DataSource
{
    [CreateAssetMenu(fileName = "Levels", menuName = "elements game/Levels", order = 1)]
    public class Levels : ScriptableObject
    {
        [SerializeField]
        private Level[] _levelsCollection;

        public Level[] LevelsCollection => _levelsCollection;
    }
}