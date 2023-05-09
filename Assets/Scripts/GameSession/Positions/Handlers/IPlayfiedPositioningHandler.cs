using Elements.GameSession.Positions.Data;
using UnityEngine;

namespace Elements.GameSession.Positions.Handlers
{
    public interface IPlayfiedPositioningHandler
    {
        Vector3 GeneratePosition(int fieldSizeI, PositionData data);
    }
}