using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterView : MonoBehaviour, ICharacter
{
    [SerializeField]
    private CharacterType _type;

    public CharacterType Type => _type;

    public Vector3 WorldPosition => transform.position;
}
