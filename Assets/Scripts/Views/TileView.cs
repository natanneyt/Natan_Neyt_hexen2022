using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TileView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDropHandler
{
    [SerializeField]
    private UnityEvent OnActivate;

    [SerializeField]
    private UnityEvent OnDeactivate;

    private BoardView _parent;
    private Deck _deck;

    private bool _isActive;

    public Position GridPosition => PositionHelper.GridPosition(transform.position);

    void Start()
    {
        _parent = GetComponentInParent<BoardView>();
        _deck = GameObject.FindObjectOfType<Deck>();
    }

    internal void Activate()
    {
        OnActivate?.Invoke();
        _isActive = true;
    }

    internal void Deactivate()
    {
        OnDeactivate?.Invoke();
        _isActive = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.gameObject.TryGetComponent(out CardView card);
            _parent.HoveringOverChild(this, card);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _parent.DeactivateAll();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag.gameObject.TryGetComponent(out CardView card))
        _parent.DroppedOnChild(this, card);
    }
}
