using Elements.GameSession.Data;
using System.Collections.Generic;

namespace Elements.GameSession.Handlers.Infrastructure
{
    public interface IDropHandler
    {
        IEnumerable<PositionData> CalculateDropPositions(IEnumerable<int> columnsForCheck);
    }
}