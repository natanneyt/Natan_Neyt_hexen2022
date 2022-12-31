using UnityEngine;

public class CharacterView : MonoBehaviour
{
    [SerializeField]
    private CharacterType _type;

    public CharacterType Type => _type;

    public Vector3 WorldPosition => transform.position;

    public Position GridPosition => PositionHelper.GridPosition(transform.position);

}
