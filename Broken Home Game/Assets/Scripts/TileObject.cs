using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileObject : MonoBehaviour
{
    Vector2Int cell = new Vector2Int(0, 0);
    Rotation rotation = Rotation.NORTH;

    public Vector2Int GetCell() { return cell; }
    public Rotation GetRotation() { return rotation; }

    public void MoveToCell(Tilemap tilemap, Vector2Int newCell)
    {
        transform.localPosition = tilemap.CellToLocal(new Vector3Int(newCell.x, newCell.y, -1));
        cell = newCell;
    }

    public void Step(Tilemap tilemap, Rotation direction)
    {
        MoveToCell(tilemap, cell.Step(direction));
    }

    public void Face(Rotation newRotation)
    {
        var rotationOffset = Quaternion.Euler(60, 0, 45);

        rotation = newRotation;

        switch(rotation)
        {
            case Rotation.NORTH: transform.localRotation = rotationOffset * Quaternion.AngleAxis(0, Vector3.forward); break;
            case Rotation.EAST: transform.localRotation = rotationOffset * Quaternion.AngleAxis(90, Vector3.forward); break;
            case Rotation.SOUTH: transform.localRotation = rotationOffset * Quaternion.AngleAxis(180, Vector3.forward); break;
            case Rotation.WEST: transform.localRotation = rotationOffset * Quaternion.AngleAxis(270, Vector3.forward); break;
            default: break;
        }
    }

    public void RotateLeft()
    {
        Face(rotation.RotateLeft());
    }

    public void RotateRight()
    {
        Face(rotation.RotateRight());
    }
}
