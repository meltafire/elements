using Elements.GameSession.Containers.Implementation;
using Elements.GameSession.Factories;

namespace Elements.GameSession.Positions
{
    public class PositionsFacade
    {
        private readonly PositionControllerFactory _positionControllerFactory;

        public PositionsFacade(PositionControllerFactory positionControllerFactory)
        {
            _positionControllerFactory = positionControllerFactory;
        }

        public PositionController SpawnPosition(int columnI, int rawJ)
        {
            var controller = _positionControllerFactory.Create();

            controller.InitializeData(columnI, rawJ);
            controller.CreateView();

            return controller;
        }
    }
}