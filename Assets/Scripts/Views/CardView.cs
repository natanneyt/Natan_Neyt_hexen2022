using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardView : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private CardType _cardType;

    public Image Image => GetComponent<Image>();

    public CardType CardType => _cardType;

    private Vector2 _distance;

    public GameObject dragCard;

    public void OnBeginDrag(PointerEventData eventData)
    {
        dragCard = Instantiate(gameObject);
        dragCard.TryGetComponent<CardView>(out CardView dragCardView);
        dragCard.GetComponent<Image>().raycastTarget = false;
        dragCard.transform.SetParent(this.transform.parent.parent);
        dragCard.transform.localScale = transform.localScale;
        Vector3 distance = transform.position - Input.mousePosition;
        _distance = new Vector2(distance.x, distance.y);
    }

    public void OnDrag(PointerEventData eventData)
    {
        dragCard.transform.position = eventData.position + _distance;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Destroy(dragCard.gameObject);
    }
}