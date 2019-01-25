using UnityEngine;
using UnityEngine.Tilemaps;

public static class TilemapExtensions
{
    public static void SnapToClosestCell(this Tilemap tilemap, Transform transformToSnap)
    {
        Vector3Int gridPosition = tilemap.layoutGrid.WorldToCell(transformToSnap.position);
        transformToSnap.position = tilemap.layoutGrid.CellToWorld(gridPosition);
    }
}
