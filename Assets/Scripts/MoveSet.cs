using System.Collections.Generic;

public abstract class MoveSet
{
    private readonly Board _board;

    protected Board Board => _board;

    public MoveSet(Board board)
    {
        _board = board;
    }

    public abstract List<TileView> Positions(Position hoverPosition, Position currentPosition);
}