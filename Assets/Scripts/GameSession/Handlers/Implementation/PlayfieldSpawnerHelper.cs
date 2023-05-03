﻿using Elements.DataSource.Data;
using Elements.GameSession.Containers.Implementation;
using Elements.GameSession.Containers.Infrastructure;
using Elements.GameSession.Factories;
using Elements.GameSession.Handlers.Infrastructure;
using Elements.GameSession.Positions;
using System.Linq;

namespace Elements.GameSession.Handlers.Implementation
{
    public class PlayfieldSpawnerHelper : IPlayfieldSpawnerHelper
    {
        private readonly ILevelContainer _levelContainer;
        private readonly ILevelContainerFiller _levelContainerFiller;
        private readonly ILevelDataSourceProvider _dataSourceProvider;
        private readonly PositionsFacade _positionsFacade;
        private readonly ItemMediatorFactory _itemMediatorFactory;

        public PlayfieldSpawnerHelper(
            ILevelContainer levelContainer,
            ILevelContainerFiller levelContainerFiller,
            ILevelDataSourceProvider dataSourceProvider,
            PositionsFacade positionsFacade,
            ItemMediatorFactory itemMediatorFactory)
        {
            _levelContainer = levelContainer;
            _levelContainerFiller = levelContainerFiller;
            _dataSourceProvider = dataSourceProvider;
            _positionsFacade = positionsFacade;
            _itemMediatorFactory = itemMediatorFactory;
        }

        public void Spawn()
        {
            var fieldSizeI = _dataSourceProvider.FieldSizeI;
            var fieldSizeJ = _dataSourceProvider.Items.Max(item => item.J) + 1;

            var itemContainers = new PositionContainer[fieldSizeI, fieldSizeJ];

            for (var i = 0; i < fieldSizeI; i++)
            {
                for (int j = 0; j < fieldSizeJ; j++)
                {
                    var positionController = _positionsFacade.SpawnPosition(i, j);

                    itemContainers[i, j] = new PositionContainer(positionController);
                }

            }

            foreach (var item in _dataSourceProvider.Items)
            {
                var container = itemContainers[item.I, item.J];

                var mediator = _itemMediatorFactory.Create(item.ItemType);
                mediator.CreateView();
                mediator.RegisterAtNewPosition(container.PositionController);
                mediator.MoveToPositionImediately();

                container.ItemMediator = mediator;
            }

            _levelContainerFiller.Fill(itemContainers, fieldSizeI, fieldSizeJ);
        }

        public void Despawn()
        {
            foreach (var container in _levelContainer.PositionContainers)
            {
                if (!container.IsEmpty())
                {
                    container.ItemMediator.RemoveView();
                }

                container.PositionController.RemoveView();
            }
        }
    }
}