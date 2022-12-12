using System;
using System.Collections.Generic;
using UnityEngine;

public class PositionEventArgs : EventArgs
{
    public Position Position { get; }

    public CardType Card { get; }

    public PositionEventArgs(Position position, CardType card)
    {
        Position = position;
        Card = card;
    }
}

public class BoardView : MonoBehaviour
{
    private event EventHandler<PositionEventArgs> PositionSelected;
    private readonly Dictionary<Position, PositionView> _tiles = new();

    public Action<object, PositionEventArgs> PositionClicked { get; internal set; }

    private void OnEnable()
    {
        var tiles = GetComponentsInChildren<PositionView>();
        foreach (var tile in tiles) _tiles.Add(tile.GridPosition, tile);
    }

    internal void ChildSelected(PositionView positionView, CardType card)
        => OnPositionSelected(new PositionEventArgs(positionView.GridPosition, card));

    protected virtual void OnPositionSelected(PositionEventArgs e)
    {
        var handler = PositionSelected;
        handler?.Invoke(this, e);
    }
    
}