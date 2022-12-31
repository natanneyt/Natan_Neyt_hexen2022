using UnityEngine;

public class Tile : MonoBehaviour
{
    public Position GridPosition => PositionHelper.GridPosition(transform.position);
}