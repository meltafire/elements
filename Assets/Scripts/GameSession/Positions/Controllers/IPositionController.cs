using Elements.GameSession.Data;
using System;
using UnityEngine;

namespace Elements.GameSession.Containers.Infrastructure
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