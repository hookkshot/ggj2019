using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(SetSortingLayer))]
public class TileObject : MonoBehaviour
{
    [SerializeField] Rotation rotation = Rotation.NORTH;
    [SerializeField] Animator _animator = null;

    public Vector2Int Cell { get { return cell; } }
    public Rotation GetRotation() { return rotation; }

    private bool isMoving = false;
    private bool isRotating = false;

    Vector2Int cell;

    public bool IsTweening()
    {
        return isMoving || isRotating;
    }

    public void MoveToCell(Tilemap tilemap, Vector2Int newCell)
    {
        if (isMoving || isRotating) return;
        var newPosition = tilemap.GetCellCenterWorld(new Vector3Int(newCell.x, newCell.y, -1));
        if (Physics2D.OverlapPoint(new Vector2(newPosition.x, newPosition.y), ~LayerMask.GetMask("Zone"))) { return; }
        if (!tilemap.GetTile(new Vector3Int(newCell.x, newCell.y, 0))) { return; }

        transform.position = newPosition;
        cell = newCell;

        ZOrdering.SetZOrdering(transform);
    }

    public void MoveToCellTween(Tilemap tilemap, Vector2Int newCell)
    {
        if (isMoving || isRotating) return;
        var newPosition = tilemap.GetCellCenterWorld(new Vector3Int(newCell.x, newCell.y, -1));
        if (Physics2D.OverlapPoint(new Vector2(newPosition.x, newPosition.y), ~LayerMask.GetMask("Zone"))) { return; }
        if (!tilemap.GetTile(new Vector3Int(newCell.x, newCell.y, 0))) { return; }

        StartCoroutine(MoveToPositionI(newPosition, newCell));
    }

    public IEnumerator MoveToPositionI(Vector3 position, Vector2Int newCell)
    {
        if(_animator != null)
        {
            _animator.SetBool("IsWalking", true);
        }

        isMoving = true;
        var from = transform.position;

        float time = 0;

        while (time < 0.4f)
        {
            yield return 0;
            time += Time.deltaTime;
            transform.position = from + (position - from) * (time / 0.4f);
            ZOrdering.SetZOrdering(transform);
        }


        transform.position = position;
        cell = newCell;
        ZOrdering.SetZOrdering(transform);
        isMoving = false;

        if (_animator != null)
        {
            _animator.SetBool("IsWalking", false);
        }
    }

    public void SetCell(Tilemap tilemap)
    {
        cell = (Vector2Int)tilemap.WorldToCell(transform.position);
    }

    public void Step(Tilemap tilemap, Rotation direction)
    {

        MoveToCellTween(tilemap, cell.Step(direction));
    }

    public void Face(Rotation newRotation)
    {
        if (isMoving || isRotating) return;

        StartCoroutine(FaceI(newRotation));
    }

    public void FaceSnap(Rotation newRotation)
    {
        if (isMoving || isRotating) return;
        rotation = newRotation;
        var rotationOffset = Quaternion.Euler(60, 0, 45);

        var to = Quaternion.identity;

        switch (rotation)
        {
            case Rotation.NORTH: to = rotationOffset * Quaternion.AngleAxis(0, Vector3.forward); break;
            case Rotation.EAST: to = rotationOffset * Quaternion.AngleAxis(270, Vector3.forward); break;
            case Rotation.SOUTH: to = rotationOffset * Quaternion.AngleAxis(180, Vector3.forward); break;
            case Rotation.WEST: to = rotationOffset * Quaternion.AngleAxis(90, Vector3.forward); break;
            default: break;
        }

        transform.localRotation = to;
    }

    private IEnumerator FaceI(Rotation newRotation)
    {
        isRotating = true;

        rotation = newRotation;
        var rotationOffset = Quaternion.Euler(60, 0, 45);
        var from = transform.localRotation;
        var to = Quaternion.identity;

        switch (rotation)
        {
            case Rotation.NORTH: to = rotationOffset * Quaternion.AngleAxis(0, Vector3.forward); break;
            case Rotation.EAST: to = rotationOffset * Quaternion.AngleAxis(270, Vector3.forward); break;
            case Rotation.SOUTH: to = rotationOffset * Quaternion.AngleAxis(180, Vector3.forward); break;
            case Rotation.WEST: to = rotationOffset * Quaternion.AngleAxis(90, Vector3.forward); break;
            default: break;
        }

        float time = 0;

        while (time < 0.3f)
        {
            yield return 0;
            time += Time.deltaTime;
            transform.localRotation = Quaternion.Lerp(from, to, (time / 0.3f));
        }


        transform.localRotation = to;
        isRotating = false;
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
