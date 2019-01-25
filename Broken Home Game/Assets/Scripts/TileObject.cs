using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileObject : MonoBehaviour
{

    Vector2Int cell;

    public Vector2Int GetCell() { return cell; }

    void MoveToCell(Tilemap tilemap, Vector2Int newCell)
    {
        transform.parent = tilemap.transform;
        transform.position = tilemap.CellToLocal(new Vector3Int(newCell.x, newCell.y, 0));
        cell = newCell;
    }


}
