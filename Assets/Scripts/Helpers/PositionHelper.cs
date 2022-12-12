using UnityEngine;

public static class PositionHelper
    {
    public const int Rings = 2;
    
    public static float TileSize => 0.5f;
    internal static Position GridPosition(float x, float z)
    {
        z = -z;
        var q = ((Mathf.Sqrt(3) / 3 * x) + (-1f / 3 * z)) / TileSize;
        var r = 2f / 3 * z / TileSize;
        var s = -q - r;

        var pos = Round(new Vector3(q, r, s));

        return new Position((int)pos.x, (int)pos.y, (int)pos.z);
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