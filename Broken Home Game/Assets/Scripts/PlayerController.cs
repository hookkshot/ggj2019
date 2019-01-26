using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Tilemap tilemap;

    private BoundsInt currentBounds;
    private TileObject tileObject;

    void Awake()
    {
        tileObject = gameObject.GetComponentInChildren<TileObject>();
        tileObject.Face(tileObject.GetRotation());
        tilemap.SnapToClosestCell(transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            tileObject.RotateRight();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            tileObject.RotateLeft();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            tileObject.Step(tilemap, tileObject.GetRotation());
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            tileObject.Step(tilemap, tileObject.GetRotation().RotateLeft().RotateLeft());
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Interact();            
        }
    }

    private void Interact()
    {
        var cellPoint = tileObject.GetCell().Step(tileObject.GetRotation());
        var worldPoint = tilemap.CellToWorld(new Vector3Int(cellPoint.x, cellPoint.y,0));
        var inventory = GetComponent<PlayerInventory>();

        var handItem = inventory.HandItem();
        if (handItem)
        {
            var handFurniture = handItem.GetComponent<InteractableFurnitureScript>();
            handFurniture.Place(tilemap, cellPoint);

            return;
        }
        
        var collider = Physics2D.OverlapPoint(worldPoint);
        if (!collider) { return; }

        var door = collider.GetComponent<LevelDoor>();
        if (door)
        {
            GameStateManager.Instance.MoveToLevel(door.SceneConnection, door.DoorConnection);
        }
        var furniture = collider.GetComponent<InteractableFurnitureScript>();
        if (furniture)
        {
            furniture.Pickup(this);
        }
    }
}
