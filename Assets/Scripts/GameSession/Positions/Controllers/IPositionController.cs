using Elements.GameSession.Positions.Data;
using System;
using UnityEngine;

namespace Elements.GameSession.Positions.Controllers
{
    public interface IPositionController
    {
        Vector3 Position { get; }
        PositionData Data { get; }

        void CreateView();
        void RemoveView();

        event Action<PositionData> OnItemClick;
    }
}