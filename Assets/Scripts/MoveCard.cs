using System.Collections.Generic;

public class MoveCard : MoveSet
{
    private readonly Board _board;

    public MoveCard(Board board) : base(board)
    {
        _board = board;
    }

    public override List<TileView> Positions(Position hoverPosition, Position currentPosition)
    {
        List<TileView> tileViews = new();
        foreach (TileView tileView in _board._positions)
        {
            if (tileView.GridPosition.Equals(hoverPosition))
            {
                if (!Board.TryGetCharacterAt(tileView.GridPosition, out CharacterView character))
                    tileViews.Add(tileView);
                break;
            }
        }
        return tileViews;
    }
}