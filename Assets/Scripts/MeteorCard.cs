using System.Collections.Generic;

public class MeteorCard : MoveSet
{
    private readonly Board _board;

    public MeteorCard(Board board) : base(board)
    {
        _board = board;
    }

    public override List<TileView> Positions(Position hoverPosition, Position currentPosition)
    {
        List<TileView> tiles = PositionHelper.Neighbours(hoverPosition, _board);
        if (_board.TryGetTileAt(hoverPosition, out TileView hoverTile))
            tiles.Add(hoverTile);

        return tiles;
    }

    public List<CharacterView> Execute(List<TileView> narrowedList, Position playerPosition)
    {
        List<CharacterView> characters = new();
        for (int i = 0; i < narrowedList.Count; i++)
        {
            TileView tileView = narrowedList[i];
            if (_board.TryGetCharacterAt(tileView.GridPosition, out CharacterView character))
            {
                if (!character.GridPosition.Equals(playerPosition))
                {
                    _board.Remove(character.GridPosition);
                    characters.Add(character);
                }
            }
        }
        return characters;
    }
}
