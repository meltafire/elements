using Elements.GameSession.Data;
using UnityEngine;

namespace Elements.GameSession.Handlers.Infrastructure
{
    public interface IPlayfiedPositioningHandler
    {
        Vector3 GeneratePosition(PositionData data);
    }
}