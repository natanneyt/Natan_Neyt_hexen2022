﻿using System.Collections.Generic;
using UnityEngine;

public class PlayingState : State
{
    private Board _board;
    private BoardView _boardView;
    private readonly List<TileView> _positions = new();
    private Deck _deck = new();
    private Position _currentPosition;
    private GameLoop _gameLoop;

    public override void OnEnter()
    {
        base.OnEnter();


        _positions.AddRange(GameObject.FindObjectsOfType<TileView>());
        _board = new Board(_positions);
        _gameLoop = GameObject.FindObjectOfType<GameLoop>();
        _gameLoop.ShowDeck();

        _deck = GameObject.FindObjectOfType<Deck>();

        InitializeScene();
    }

    private void InitializeScene()
    {
        _boardView = GameObject.FindObjectOfType<BoardView>();

        if (_boardView != null)
        {
            _boardView.CardHovering += OnHoveringOverChild;
            _boardView.CardDropped += OnDroppedOnChild;
        }

        var characterViews = GameObject.FindObjectsOfType<CharacterView>();
        foreach (var characterView in characterViews)
        {
            _board.Place(PositionHelper.GridPosition(characterView.transform.position), characterView);
            if (characterView.Type == CharacterType.Player) _currentPosition = PositionHelper.GridPosition(characterView.transform.position);
        }
    }

    public override void OnExit()
    {
        base.OnExit();

        if (_boardView != null)
        {
            _boardView.CardHovering -= OnHoveringOverChild;
            _boardView.CardDropped -= OnDroppedOnChild;
        }
        _gameLoop.HideDeck();
    }

    private void OnHoveringOverChild(object sender, CardDragEventArgs e)
    {
        if (e.Card.CardType == CardType.MoveCard)
        {
            MoveSet card = new MoveCard(_board);
            List<TileView> positionViews = card.Positions(e.Position, _currentPosition);
            foreach (TileView positionView in positionViews) positionView.Activate();
        }

        else if (e.Card.CardType == CardType.PushCard)
        {
            PushCard card = new(_board);
            List<TileView> positionViews = card.Positions(e.Position, _currentPosition);
            foreach (TileView positionView in positionViews) positionView.Activate();
        }

        else if (e.Card.CardType == CardType.SlashCard)
        {
            SlashCard card = new(_board);
            List<TileView> positionViews = card.Positions(e.Position, _currentPosition);
            foreach (TileView positionView in positionViews) positionView.Activate();
        }

        else if (e.Card.CardType == CardType.RowAttackCard)
        {
            RowAttackCard card = new(_board);
            List<TileView> positionViews = card.Positions(e.Position, _currentPosition);
            foreach (TileView positionView in positionViews) positionView.Activate();
        }

        else if (e.Card.CardType == CardType.MeteorCard)
        {
            MeteorCard card = new(_board);
            List<TileView> positionViews = card.Positions(e.Position, _currentPosition);
            foreach (TileView positionView in positionViews) positionView.Activate();
        }
    }

    private void OnDroppedOnChild(object sender, CardDragEventArgs e)
    {
        if (e.Card.CardType == CardType.MoveCard)
        {
            MoveCard card = new(_board);
            List<TileView> positionViews = card.Positions(e.Position, _currentPosition);
            foreach (TileView tileView in positionViews)
            {
                if (tileView.GridPosition.Equals(e.Position))
                {
                    _deck.UseCard(e.Card);
                    _board.Move(_currentPosition, e.Position);
                    _currentPosition = e.Position;
                    tileView.Deactivate();
                    break;
                }
            }
        }

        else if (e.Card.CardType == CardType.PushCard)
        {
            PushCard card = new(_board);
            List<TileView> positions = card.NarrowedPositions(e.Position, _currentPosition);
            if (card.Execute(positions, _currentPosition))
                if (positions.Count != 0) _deck.UseCard(e.Card);
        }

        else if (e.Card.CardType == CardType.SlashCard)
        {
            SlashCard card = new(_board);
            List<TileView> positions = card.NarrowedPositions(e.Position, _currentPosition);
            foreach (CharacterView character in card.Execute(positions, _currentPosition))
            {
                GameObject.Destroy(character.gameObject);
            }
            if (positions.Count != 0) _deck.UseCard(e.Card);
        }

        else if (e.Card.CardType == CardType.RowAttackCard)
        {
            RowAttackCard card = new(_board);
            List<TileView> positions = card.NarrowedPositions(e.Position, _currentPosition);
            foreach (CharacterView character in card.Execute(positions, _currentPosition))
            {
                GameObject.Destroy(character.gameObject);
            }
            if (positions.Count != 0) _deck.UseCard(e.Card);
        }

        else if (e.Card.CardType == CardType.MeteorCard)
        {
            MeteorCard card = new(_board);
            List<TileView> positions = card.Positions(e.Position, _currentPosition);
            List<CharacterView> characters = card.Execute(positions, _currentPosition);
            foreach (CharacterView character in characters)
            {
                GameObject.Destroy(character.gameObject);
            }
            if (positions.Count != 0) _deck.UseCard(e.Card);
        }
        _boardView.DeactivateAll();
    }
}