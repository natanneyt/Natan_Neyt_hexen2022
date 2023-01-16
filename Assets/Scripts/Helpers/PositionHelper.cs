using System.Collections.Generic;
using UnityEngine;

public static class PositionHelper
{
    public const int Rings = 3;

    public static float TileSize => 0.5f;

    public static int BoardDiameter => Rings * 2 + 1;

    internal static Position GridPosition(Vector3 position)
    {
        position.z = -position.z;
        var q = ((Mathf.Sqrt(3) / 3 * position.x) + (-1f / 3 * position.z)) / TileSize;
        var r = 2f / 3 * position.z / TileSize;
        var s = -q - r;

        var pos = Round(new Vector3(q, r, s));

        return new Position((int)pos.x, (int)pos.y, (int)pos.z);
    }

    internal static Vector3 WorldPosition(Position position)
    {
        var x = TileSize * (Mathf.Sqrt(3) * position.Q + Mathf.Sqrt(3) / 2 * position.R);
        var z = TileSize * (3f / 2 * position.R);

        return new Vector3(x, 0, -z);
    }

    internal static List<TileView> Neighbours(Position currentPosition, Board board)
    {
        List<TileView> neighbours = new();
        neighbours.AddRange(TopLeft(currentPosition, board, 1));
        neighbours.AddRange(TopRight(currentPosition, board, 1));
        neighbours.AddRange(Left(currentPosition, board, 1));
        neighbours.AddRange(Right(currentPosition, board, 1));
        neighbours.AddRange(BottomLeft(currentPosition, board, 1));
        neighbours.AddRange(BottomRight(currentPosition, board, 1));

        return neighbours;
    }

    internal static List<TileView> Neighbours(Position currentPosition, Board board, int amount)
    {
        List<TileView> neighbours = new();
        neighbours.AddRange(TopLeft(currentPosition, board, amount));
        neighbours.AddRange(TopRight(currentPosition, board, amount));
        neighbours.AddRange(Left(currentPosition, board, amount));
        neighbours.AddRange(Right(currentPosition, board, amount));
        neighbours.AddRange(BottomLeft(currentPosition, board, amount));
        neighbours.AddRange(BottomRight(currentPosition, board, amount));

        return neighbours;
    }

    internal static Position RotateLeft(Position hoverPosition, Position currentPosition)
    {
        Position position = Subtract(hoverPosition, currentPosition);
        position = new Position(-position.S, -position.Q, -position.R);
        position = Add(position, currentPosition);
        return position;
    }

    internal static Position RotateRight(Position hoverPosition, Position currentPosition)
    {
        Position position = Subtract(hoverPosition, currentPosition);
        position = new Position(-position.R, -position.S, -position.Q);
        position = Add(position, currentPosition);
        return position;
    }

    internal static List<TileView> TopLeft(Position currentPosition, Board board, int amount)
    {
        List<TileView> positions = new();
        for (int i = 1; i < amount + 1; i++)
        {
            if (board.TryGetTileAt(Add(currentPosition, new Position(0, -i, i)), out TileView tileView))
                if (board.IsValid(tileView.GridPosition)) positions.Add(tileView);
                else break;
        }
        return positions;
    }

    internal static List<TileView> TopRight(Position currentPosition, Board board, int amount)
    {
        List<TileView> positions = new();
        for (int i = 1; i < amount + 1; i++)
        {
            if (board.TryGetTileAt(Add(currentPosition, new Position(i, -i, 0)), out TileView tileView))
                if (board.IsValid(tileView.GridPosition)) positions.Add(tileView);
                else break;
        }
        return positions;
    }

    internal static List<TileView> Left(Position currentPosition, Board board, int amount)
    {
        List<TileView> positions = new();
        for (int i = 1; i < amount + 1; i++)
        {
            if (board.TryGetTileAt(Add(currentPosition, new Position(-i, 0, i)), out TileView tileView))
                if (board.IsValid(tileView.GridPosition)) positions.Add(tileView);
                else break;
        }
        return positions;
    }

    internal static List<TileView> Right(Position currentPosition, Board board, int amount)
    {
        List<TileView> positions = new();
        for (int i = 1; i < amount + 1; i++)
        {
            if (board.TryGetTileAt(Add(currentPosition, new Position(i, 0, -i)), out TileView tileView))
                if (board.IsValid(tileView.GridPosition)) positions.Add(tileView);
                else break;
        }
        return positions;
    }

    internal static List<TileView> BottomLeft(Position currentPosition, Board board, int amount)
    {
        List<TileView> positions = new();
        for (int i = 1; i < amount + 1; i++)
        {
            if (board.TryGetTileAt(Add(currentPosition, new Position(-i, i, 0)), out TileView tileView))
                if (board.IsValid(tileView.GridPosition)) positions.Add(tileView);
                else break;
        }
        return positions;
    }

    internal static List<TileView> BottomRight(Position currentPosition, Board board, int amount)
    {
        List<TileView> positions = new();
        for (int i = 1; i < amount + 1; i++)
        {
            if (board.TryGetTileAt(Add(currentPosition, new Position(0, i, -i)), out TileView tileView))
                if (board.IsValid(tileView.GridPosition)) positions.Add(tileView);
                else break;
        }
        return positions;
    }

    internal static Position Add(Position firstPosition, Position secondPosition)
    {
        return new Position(firstPosition.Q + secondPosition.Q, firstPosition.R + secondPosition.R, firstPosition.S + secondPosition.S);
    }

    internal static Position Subtract(Position firstPosition, Position secondPosition)
    {
        return new Position(firstPosition.Q - secondPosition.Q, firstPosition.R - secondPosition.R, firstPosition.S - secondPosition.S);
    }

    private static Vector3 Round(Vector3 position)
    {
        var q = Mathf.Round(position.x);
        var r = Mathf.Round(position.y);
        var s = Mathf.Round(position.z);

        var q_diff = Mathf.Abs(q - position.x);
        var r_diff = Mathf.Abs(r - position.y);
        var s_diff = Mathf.Abs(s - position.z);

        if (q_diff > r_diff && q_diff > s_diff)
            q = -r - s;
        else if (r_diff > s_diff)
            r = -q - s;
        else s = -q - r;

        return new Vector3(q, r, s);
    }
}