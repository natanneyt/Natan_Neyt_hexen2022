using System.Collections.Generic;
using UnityEngine;

public class SlashCard : MoveSet
{
    private readonly Board _board;

    public SlashCard(Board board) : base(board)
    {
        _board = board;
    }

    public override List<TileView> Positions(Position hoverPosition, Position currentPosition)
    {
        List<TileView> tiles = PositionHelper.Neighbours(currentPosition, _board);

        foreach (TileView tile in tiles)
            if (tile.GridPosition.Equals(hoverPosition))
            {
                List<TileView> narrowedList = new();
                if (_board.TryGetTileAt(hoverPosition, out TileView hoverTile))
                    narrowedList.Add(hoverTile);
                if (_board.TryGetTileAt(PositionHelper.RotateLeft(hoverPosition, currentPosition), out TileView leftTile))
                    narrowedList.Add(leftTile);
                if (_board.TryGetTileAt(PositionHelper.RotateRight(hoverPosition, currentPosition), out TileView rightTile))
                    narrowedList.Add(rightTile);
                return narrowedList;
            }
        return tiles;
    }
    public List<TileView> NarrowedPositions(Position hoverPosition, Position currentPosition)
    {
        List<TileView> tiles = PositionHelper.Neighbours(currentPosition, _board);

        foreach (TileView tile in tiles)
            if (tile.GridPosition.Equals(hoverPosition))
            {
                List<TileView> narrowedList = new();
                if (_board.TryGetTileAt(hoverPosition, out TileView hoverTile))
                    narrowedList.Add(hoverTile);
                if (_board.TryGetTileAt(PositionHelper.RotateLeft(hoverPosition, currentPosition), out TileView leftTile))
                    narrowedList.Add(leftTile);
                if (_board.TryGetTileAt(PositionHelper.RotateRight(hoverPosition, currentPosition), out TileView rightTile))
                    narrowedList.Add(rightTile);
                return narrowedList;
            }
        return new();
    }

    public List<CharacterView> Execute(List<TileView> narrowedList, Position playerPosition)
    {
        List<CharacterView> characters = new();
        for (int i = 0; i < narrowedList.Count; i++)
        {
            TileView tileView = narrowedList[i];
            if (_board.TryGetCharacterAt(tileView.GridPosition, out CharacterView character))
            {
                _board.Remove(character.GridPosition);
                characters.Add(character);
            }
        }
        return characters;
    }
}