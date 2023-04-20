using Elements.Menu.Providers.Infrastructure;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Elements.Menu.Providers.Implementation
{
    public class MenuProviderView : MonoBehaviour, IMenuProvider
    {
        [SerializeField]
        private Button _button;

        public event Action OnNextButtonClick;

        private void OnEnable()
        {
            _button.onClick.AddListener(HandleButtonPressed);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(HandleButtonPressed);
        }

        public void SetButtonActive(bool isActive)
        {
            _button.interactable = isActive;
        }

        private void HandleButtonPressed()
        {
            OnNextButtonClick?.Invoke();
        }
    }
}