using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[Serializable]
public class ActivationChangedUnityEvent : UnityEvent<bool> { }


public class PositionView : MonoBehaviour
{
    [SerializeField]
    private UnityEvent OnActivate;

    [SerializeField]
    private UnityEvent OnDeactivate;

    [SerializeField]
    private ActivationChangedUnityEvent OnActivationChanged;

    private BoardView _parent;

    public Position GridPosition => PositionHelper.GridPosition(transform.position.x, transform.position.z);

    void Start() => _parent = GetComponentInParent<BoardView>();

    public void OnTileSelected(CardType cardType) => _parent.ChildSelected(this, cardType);

    internal void Activate()
    {
        OnActivate?.Invoke();
        OnActivationChanged.Invoke(true);
    }

    internal void Deactivate()
    {
        OnDeactivate?.Invoke();
        OnActivationChanged.Invoke(false);
    }
}
