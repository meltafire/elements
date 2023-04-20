using Elements.GameSession.Handlers.Infrastructure;
using UnityEditor;

namespace Elements.GameSession.Handlers.Implementation
{
    public class GameEndRulesHandler : IGameEndRulesHandler
    {
        private readonly ILevelContainer _levelContainer;

        public bool CheckGameCompletion()
        {
            var containers = _levelContainer.PositionContainers;

            foreach (var container in containers)
            {
                if(!container.IsEmpty())
                {
                    return false;
                }
            }

            return true;
        }
    }
}