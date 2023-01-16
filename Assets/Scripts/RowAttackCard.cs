using System.Collections.Generic;

public class RowAttackCard : MoveSet
{
    private readonly Board _board;

    public RowAttackCard(Board board) : base(board)
    {
        _board = board;
    }

    public override List<TileView> Positions(Position hoverPosition, Position currentPosition)
    {
        List<TileView> positions = new();
        _board.TryGetTileAt(hoverPosition, out TileView hoverTile);

        List<TileView> topLeft = PositionHelper.TopLeft(currentPosition, _board, PositionHelper.BoardDiameter);
        if (topLeft.Contains(hoverTile)) return topLeft;

        List<TileView> topRight = PositionHelper.TopRight(currentPosition, _board, PositionHelper.BoardDiameter);
        if (topRight.Contains(hoverTile)) return topRight;

        List<TileView> left = PositionHelper.Left(currentPosition, _board, PositionHelper.BoardDiameter);
        if (left.Contains(hoverTile)) return left;

        List<TileView> right = PositionHelper.Right(currentPosition, _board, PositionHelper.BoardDiameter);
        if (right.Contains(hoverTile)) return right;

        List<TileView> bottomLeft = PositionHelper.BottomLeft(currentPosition, _board, PositionHelper.BoardDiameter);
        if (bottomLeft.Contains(hoverTile)) return bottomLeft;

        List<TileView> bottomRight = PositionHelper.BottomRight(currentPosition, _board, PositionHelper.BoardDiameter);
        if (bottomRight.Contains(hoverTile)) return bottomRight;

        positions.AddRange(PositionHelper.TopLeft(currentPosition, _board, PositionHelper.BoardDiameter)); 
        positions.AddRange(PositionHelper.TopRight(currentPosition, _board, PositionHelper.BoardDiameter));
        positions.AddRange(PositionHelper.Left(currentPosition, _board, PositionHelper.BoardDiameter));
        positions.AddRange(PositionHelper.Right(currentPosition, _board, PositionHelper.BoardDiameter));
        positions.AddRange(PositionHelper.BottomLeft(currentPosition, _board, PositionHelper.BoardDiameter));
        positions.AddRange(PositionHelper.BottomRight(currentPosition, _board, PositionHelper.BoardDiameter));

        return positions;
    }

    public List<TileView> NarrowedPositions(Position hoverPosition, Position currentPosition)
    {
        List<TileView> positions = new();
        _board.TryGetTileAt(hoverPosition, out TileView hoverTile);

        List<TileView> topLeft = PositionHelper.TopLeft(currentPosition, _board, PositionHelper.BoardDiameter);
        if (topLeft.Contains(hoverTile)) return topLeft;

        List<TileView> topRight = PositionHelper.TopRight(currentPosition, _board, PositionHelper.BoardDiameter);
        if (topRight.Contains(hoverTile)) return topRight;

        List<TileView> left = PositionHelper.Left(currentPosition, _board, PositionHelper.BoardDiameter);
        if (left.Contains(hoverTile)) return left;

        List<TileView> right = PositionHelper.Right(currentPosition, _board, PositionHelper.BoardDiameter);
        if (right.Contains(hoverTile)) return right;

        List<TileView> bottomLeft = PositionHelper.BottomLeft(currentPosition, _board, PositionHelper.BoardDiameter);
        if (bottomLeft.Contains(hoverTile)) return bottomLeft;

        List<TileView> bottomRight = PositionHelper.BottomRight(currentPosition, _board, PositionHelper.BoardDiameter);
        if (bottomRight.Contains(hoverTile)) return bottomRight;

        return positions;
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