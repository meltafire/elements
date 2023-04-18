using System;

public interface IMenuProvider
{
    event Action OnNextButtonClick;
    void SetButtonActive(bool isActive);
}