using Elements.DataSource.Data;
using Elements.GameSession.Containers.Implementation;
using Elements.GameSession.Containers.Infrastructure;
using Elements.GameSession.Factories;

namespace Elements.GameSession.Items
{
    public class ItemsFacade
    {
        private readonly ItemControllerFactory _itemMediatorFactory;

        public ItemsFacade(ItemControllerFactory itemMediatorFactory)
        {
            _itemMediatorFactory = itemMediatorFactory;
        }

        public ItemController SpawnItem(ItemType itemtype)
        {
            var controller = _itemMediatorFactory.Create(itemtype);

            controller.CreateView();

            return controller;
        }
    }
}