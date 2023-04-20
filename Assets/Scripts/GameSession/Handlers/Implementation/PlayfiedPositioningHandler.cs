using Elements.DataSource.Data;
using Elements.GameSession.Data;
using Elements.GameSession.Handlers.Infrastructure;
using UnityEngine;

namespace Elements.GameSession.Handlers.Implementation
{
    public class PlayfiedPositioningHandler : IPlayfiedPositioningHandler
    {
        private const float CenterOfScreenX = 0f;
        private const float DepthStartingValue = 0f;
        private const float DepthDeltaValue = 0.1f;
        private const float BlockUnitSize = 1f;
        private const float BlockLowerBorderJ = 0f;

        private readonly ILevelDataSourceProvider _levelDataSourceProvider;

        public PlayfiedPositioningHandler(ILevelDataSourceProvider levelDataSourceProvider)
        {
            _levelDataSourceProvider = levelDataSourceProvider;
        }

        public Vector3 GeneratePosition(PositionData data)
        {
            var isDimensionIEven = _levelDataSourceProvider.FieldSizeI % 2 == 0;

            var halfOfItemCountI = _levelDataSourceProvider.FieldSizeI / 2;
            var loverLeftCornerX = CenterOfScreenX - halfOfItemCountI * BlockUnitSize;

            var positionX = isDimensionIEven ? (loverLeftCornerX + data.I * BlockUnitSize + (BlockUnitSize / 2f)) : (loverLeftCornerX + data.I * BlockUnitSize);

            return new Vector3(
                positionX,
                BlockLowerBorderJ + data.J * BlockUnitSize,
                DepthStartingValue - data.I * DepthDeltaValue - data.J * DepthDeltaValue
                );
        }
    }
}