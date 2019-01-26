using UnityEngine;
using UnityEngine.Tilemaps;

public enum Rotation
{
    NORTH,
    EAST,
    SOUTH,
    WEST,
    ASKEW
}

static class ExtensionMethods
{
    public static Vector2Int Step(this Vector2Int pos, Rotation rotation)
    {
        switch (rotation)
        {
            case Rotation.NORTH: ++pos.y; break;
            case Rotation.EAST: --pos.x; break;
            case Rotation.SOUTH: --pos.y; break;
            case Rotation.WEST: ++pos.x; break;
            default: break;
        }

        return pos;
    }

    public static Rotation RotateLeft(this Rotation rotation)
    {
        switch (rotation)
        {
            case Rotation.NORTH: return Rotation.WEST;
            case Rotation.EAST: return Rotation.NORTH;
            case Rotation.SOUTH: return Rotation.EAST;
            case Rotation.WEST: return Rotation.SOUTH;
        }

        return Rotation.ASKEW;
    }

    public static Rotation RotateRight(this Rotation rotation)
    {

        return rotation.RotateLeft().RotateLeft().RotateLeft();
    }

    public static void SnapToClosestCell(this Tilemap tilemap, Transform transformToSnap)
    {
        Vector3Int gridPosition = tilemap.layoutGrid.WorldToCell(transformToSnap.position);
        transformToSnap.position = tilemap.layoutGrid.CellToWorld(gridPosition);
    }
}