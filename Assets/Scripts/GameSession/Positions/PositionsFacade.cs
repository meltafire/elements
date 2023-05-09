using Elements.GameSession.Positions.Controllers;
using Elements.GameSession.Positions.Factories;

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