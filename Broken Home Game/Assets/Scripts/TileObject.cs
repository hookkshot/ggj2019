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

    [SerializeField]
    private Transform actualFuckingTransform;

    public void MoveToCell(Tilemap tilemap, Vector2Int newCell)
    {
        transform.position = tilemap.CellToWorld(new Vector3Int(newCell.x, newCell.y, 0));
        cell = newCell;
    }

    public void Step(Tilemap tilemap, Rotation direction)
    {
        MoveToCell(tilemap, cell.Step(direction));
    }

    public void Face(Rotation newRotation)
    {
        rotation = newRotation;

        switch(rotation)
        {
            case Rotation.NORTH: transform.localRotation = Quaternion.AngleAxis(0, transform.up); break;
            case Rotation.EAST: transform.localRotation = Quaternion.AngleAxis(90, transform.up); break;
            case Rotation.SOUTH: transform.localRotation = Quaternion.AngleAxis(180, transform.up); break;
            case Rotation.WEST: transform.localRotation = Quaternion.AngleAxis(270, transform.up); break;
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
