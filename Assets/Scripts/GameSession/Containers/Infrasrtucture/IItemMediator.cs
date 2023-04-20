using Elements.GameSession.Data;
using System;

namespace Elements.GameSession.Containers.Infrasrtucture
{
    public interface IItemMediator
    {
        event Action<PositionData> OnItemSelect;

        void RegisterAtNewPosition(IPositionMediator positionMediator);
    }
}