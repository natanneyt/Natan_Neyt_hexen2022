using System;
using System.Collections.Generic;
using UnityEngine;

public class CardDragEventArgs : EventArgs
{
    public Position Position { get; }

    public CardView Card { get; }

    public CardDragEventArgs(Position position, CardView card)
    {
        Position = position;
        Card = card;
    }
}

public class BoardView : MonoBehaviour
{
    public event EventHandler<CardDragEventArgs> CardHovering;

    public event EventHandler<CardDragEventArgs> CardDropped;

    public readonly Dictionary<Position, TileView> _tiles = new();

    private void OnEnable()
    {
        var tiles = GetComponentsInChildren<TileView>();
        foreach (var tile in tiles) _tiles.Add(tile.GridPosition, tile);
    }

    internal void HoveringOverChild(TileView positionView, CardView card)
        => OnCardHovering(new CardDragEventArgs(positionView.GridPosition, card));

    internal void DroppedOnChild(TileView positionView, CardView card)
        => OnCardDropped(new CardDragEventArgs(positionView.GridPosition, card));

    protected virtual void OnCardHovering(CardDragEventArgs e)
    {
        var handler = CardHovering;
        handler?.Invoke(this, e);
    }

    protected virtual void OnCardDropped(CardDragEventArgs e)
    {
        var handler = CardDropped;
        handler?.Invoke(this, e);
    }

    public void DeactivateAll()
    {
        foreach (var tile in _tiles) tile.Value.Deactivate();
    }
}