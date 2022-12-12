using Assets.Scripts;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoop<TCharacter> : MonoBehaviour where TCharacter : ICharacter
{
    private Board<TCharacter> _board;
    private BoardView _boardView;

    public void Start()
    {
        _board = new Board<TCharacter>(PositionHelper.Rings);
    //    _board.PieceMoved += (s, e) => e.Piece.MoveTo(PositionHelper.WorldPosition(e.ToPosition));
    //    _board.PieceTaken += (s, e) => e.Piece.Taken();
    //    _board.PiecePlaced += (s, e) => e.Piece.Placed(PositionHelper.WorldPosition(e.ToPosition));

    //    _engine = new Engine<PieceView>(_board, _commandQueue);

    //    var op = SceneManager.LoadSceneAsync("Game", LoadSceneMode.Additive);
    //    op.completed += InitializeScene;
    }

    //private void InitializeScene(AsyncOperation obj)
    //{
    //    _boardView = GameObject.FindObjectOfType<BoardView>();
    //    if (_boardView != null)
    //    {
    //        _boardView.PositionClicked -= OnPositionClicked;
    //        _boardView.PositionClicked += OnPositionClicked;
    //    }

    //    var pieceViews = GameObject.FindObjectsOfType<CharacterView>();
    //    foreach (var pieceView in pieceViews)
    //        _board.Place(PositionHelper.GridPosition(pieceView.WorldPosition), pieceView);
    //}

    //private void OnPositionClicked(object sender, PositionEventArgs e)
    //{
    //    var pieceClicked = _board.TryGetPieceAt(e.Position, out var clickedPiece);
    //    var ownPieceClicked = pieceClicked && clickedPiece.Player == _engine.CurrentPlayer;

    //    if (ownPieceClicked)
    //    {
    //        _currentPosition = e.Position;
    //        var validPositions = _engine.MoveSets.For(clickedPiece.Type).Positions(e.Position);
    //        _boardView.ActivePosition = validPositions;

    //    }
    //    else if (_currentPosition != null)
    //    {
    //        if (_engine.Move(_currentPosition.Value, e.Position))
    //            _boardView.ActivePosition = new List<Position>(0);

    //    }
    //}
}
