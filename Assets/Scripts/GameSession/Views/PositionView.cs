using System;
using UnityEngine;

public class PositionView : MonoBehaviour
{
    public event Action OnClick;

    public Vector3 Position => transform.position;

    private void OnMouseDown()
    {
        OnClick?.Invoke();
    }

    public void SetPosition(Vector3 position)
    {
        transform.localPosition = position;
    }

    public void Remove()
    {
        Destroy(gameObject);
    }
}
