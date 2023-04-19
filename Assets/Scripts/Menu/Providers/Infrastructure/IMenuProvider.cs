using System;

namespace Elements.Menu.Providers.Infrastructure
{
    public interface IMenuProvider
    {
        event Action OnNextButtonClick;
        void SetButtonActive(bool isActive);
    }
}