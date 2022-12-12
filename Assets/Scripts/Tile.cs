using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour, IPointerClickHandler
{
    public Position GridPosition => PositionHelper.GridPosition(transform.position.x, transform.position.z);

    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log(GridPosition);
    }
}