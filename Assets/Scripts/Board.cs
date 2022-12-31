using System.Collections.Generic;
using UnityEngine;

public class Board
{
    internal List<TileView> _positions = new();
    private readonly Dictionary<Position, CharacterView> _characters = new();

    public Board(List<TileView> tileViews)
    {
        _positions = tileViews;
    }

    internal bool IsValid(Position currentPosition)
        => (currentPosition.Q <= PositionHelper.Rings && currentPosition.Q >= -PositionHelper.Rings)
        && (currentPosition.R <= PositionHelper.Rings && currentPosition.R >= -PositionHelper.Rings);

    public bool TryGetCharacterAt(Position position, out CharacterView character)
            => _characters.TryGetValue(position, out character);

    public bool TryGetTileAt(Position position, out TileView tile)
    {
        foreach (TileView tileView in _positions)
            if (tileView.GridPosition.Equals(position))
            {
                tile = tileView;
                return true;
            }
        tile = null;
        return false;
    }

    public bool Remove(Position position)
    {
        if (!_characters.TryGetValue(position, out CharacterView character))
            return false;

        if (!IsValid(position))
            return false;

        _characters.Remove(position);

        return true;
    }

    public bool Place(Position position, CharacterView character)
    {
        if (character == null)
            return false;

        if (!IsValid(position))
            return false;

        if (_characters.ContainsKey(position))
            return false;

        if (_characters.ContainsValue(character))
            return false;

        _characters[position] = character;
        return true;
    }

    public bool Move(Position fromPosition, Position toPosition)
    {
        if (!IsValid(toPosition))
            return false;

        if (_characters.ContainsKey(toPosition))
            return false;

        if (!_characters.TryGetValue(fromPosition, out CharacterView character))
            return false;

        _characters.Remove(fromPosition);
        _characters[toPosition] = character;

        character.transform.position = PositionHelper.WorldPosition(toPosition) + new Vector3(0, character.transform.localScale.y + 0.1f, 0);

        return true;
    }
}