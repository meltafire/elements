using System.Collections.Generic;

public interface IDropHandler
{
    IEnumerable<PositionData> CalculateDropPositions(IEnumerable<int> columnsForCheck);
}