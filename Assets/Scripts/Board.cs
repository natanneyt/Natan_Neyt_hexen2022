using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;

public class Board<TCharacter> : MonoBehaviour where TCharacter : ICharacter
{
    private Dictionary<Position, TCharacter> _characters = new Dictionary<Position, TCharacter>();

    private readonly int _rings;

    public Board(int rings)
    {
        _rings = rings;
    }

    internal bool IsValid(Position currentPosition) 
        => (currentPosition.Q <= 2 && currentPosition.Q >= -2) 
        && (currentPosition.R <= 2 && currentPosition.R >= -2);

    internal bool TryGetPieceAt(Position currentPosition, out TCharacter character) 
        => _characters.TryGetValue(currentPosition, out character);

    public bool Move(Position fromPosition, Position toPosition)
    {
        if (!IsValid(toPosition)) return false;

        if (_characters.ContainsKey(toPosition)) return false;

        if (!_characters.TryGetValue(fromPosition, out TCharacter character)) return false;

        _characters.Remove(fromPosition);
        _characters.Add(toPosition, character);

        return true;
    }
    
}