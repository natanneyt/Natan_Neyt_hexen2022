using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardView : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private CardType _cardType;

    public Image Image => GetComponent<Image>();

    public CardType CardType => _cardType;

    private Vector3 _startPosition;

    private LayerMask _layerMask = 1 << 3;

    private PositionView _selectedTile;

    private Vector2 _distance;

    private BoardView _board;

    private void OnEnable()
    {
        _board = gameObject.GetComponent<BoardView>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Vector3 distance = transform.position - Input.mousePosition;
        _distance = new(distance.x, distance.y);
        _startPosition = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(eventData.position), out RaycastHit raycastHit, _layerMask))
        {
            raycastHit.collider.gameObject.TryGetComponent(out PositionView positionView);
            if (positionView != null)
            {
                if (_selectedTile != null) _selectedTile.Deactivate();
                _selectedTile = positionView;
                _selectedTile.Activate();
            }
        }
        transform.position = eventData.position + _distance;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(eventData.position), out RaycastHit raycastHit, _layerMask))
        {
            if (raycastHit.collider.gameObject.TryGetComponent<PositionView>(out PositionView positionView))
            {
                _board.ChildSelected(positionView, CardType);
            }
        }
        else transform.position = _startPosition;
    }

}
