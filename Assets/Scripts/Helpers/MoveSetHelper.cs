using Assets.Scripts;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MoveSetHelper<TCharacter> : MonoBehaviour where TCharacter : ICharacter
{
    private readonly TCharacter _character;
    private readonly Position _currentPosition;
    private readonly Board<TCharacter> _board;
    private List<Position> _positions = new List<Position>();

    public MoveSetHelper(Position position, Board<TCharacter> board)
    {
        _currentPosition = position;
        _board = board;

        if (!_board.TryGetPieceAt(_currentPosition, out _character))
            Debug.Log($"Passed in a position {_currentPosition} which contains no piece to {nameof(MoveSetHelper<TCharacter>)}.");
    }

    public MoveSetHelper<TCharacter> TopLeft(int maxSteps = int.MaxValue, params Validator[] condition)
                => Collect(new Vector2Int(0, -1), maxSteps, condition);
    public MoveSetHelper<TCharacter> TopRight(int maxSteps = int.MaxValue, params Validator[] condition)
                => Collect(new Vector2Int(1, -1), maxSteps, condition);
    public MoveSetHelper<TCharacter> Left(int maxSteps = int.MaxValue, params Validator[] condition)
                => Collect(new Vector2Int(-1, 0), maxSteps, condition);
    public MoveSetHelper<TCharacter> Right(int maxSteps = int.MaxValue, params Validator[] condition)
                => Collect(new Vector2Int(1, 0), maxSteps, condition);
    public MoveSetHelper<TCharacter> BottomLeft(int maxSteps = int.MaxValue, params Validator[] condition)
                => Collect(new Vector2Int(-1, 1), maxSteps, condition);
    public MoveSetHelper<TCharacter> BottomRight(int maxSteps = int.MaxValue, params Validator[] condition)
                => Collect(new Vector2Int(1, 1), maxSteps, condition);

    public delegate bool Validator(Position currentPosition, Board<TCharacter> board, Position targetTile);

    public MoveSetHelper<TCharacter> Collect(Vector2Int direction, int maxSteps = int.MaxValue, params Validator[] condition)
    {
        if (_character == null)
            return this;

        var currentStep = 0;
        var position = new Position(_currentPosition.Q + direction.x, _currentPosition.R + direction.y, -(_currentPosition.Q + direction.x) - (_currentPosition.R + direction.y));

        while (_board.IsValid(position)
            && currentStep < maxSteps
            && (condition == null || condition.All((v) => v(_currentPosition, _board, position)))
            )
        {
            _positions.Add(position);

            position = new Position(_currentPosition.Q + direction.x, _currentPosition.R + direction.y, -(_currentPosition.Q + direction.x) - (_currentPosition.R + direction.y));
            currentStep++;
        }

        return this;
    }
}