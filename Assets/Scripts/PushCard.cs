using System.Collections.Generic;
using UnityEngine;
public class PushCard : MoveSet
{
    private readonly Board _board;
    private bool _isCardUsed = false;

    public PushCard(Board board) : base(board)
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
                _isCardUsed = true;
                return narrowedList;
            }
        _isCardUsed = false;
        return new();
    }

    public bool Execute(List<TileView> narrowedList, Position playerPosition)
    {
        for(int i = 0; i < narrowedList.Count; i++)
        {
            if (_board.TryGetCharacterAt(narrowedList[i].GridPosition, out CharacterView enemy))
            {
                _board.Place(PositionHelper.Add(enemy.GridPosition, PositionHelper.Subtract(enemy.GridPosition, playerPosition)), enemy);
                _board.Move(enemy.GridPosition, PositionHelper.Add(enemy.GridPosition, PositionHelper.Subtract(enemy.GridPosition, playerPosition)));
                _isCardUsed = true;
            }
        }
        return _isCardUsed;
    }
}