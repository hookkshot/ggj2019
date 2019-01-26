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

    void Start()
    {
        if (tilemap != null)
            Setup(tilemap);
        
    }

    public void Setup(Tilemap tilemap)
    {
        this.tilemap = tilemap;
        tileObject = GetComponent<TileObject>();
        tileObject.Face(tileObject.GetRotation());

        var cell = tilemap.SnapToClosestCell(transform);
        tileObject.MoveToCell(tilemap, new Vector2Int(cell.x, cell.y));
    }

    // Update is called once per frame
    void Update()
    {
        var inventory = GetComponent<PlayerInventory>();
        var handItem = inventory.HandItem();

        if (handItem)
        {
            var inventoryTileObject = handItem.GetComponent<TileObject>();

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                inventoryTileObject.RotateRight();
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                inventoryTileObject.RotateLeft();
            }
        }
        else
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
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Interact();            
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            inventory.SpinInventory();
        }
    }

    private void Interact()
    {
        var cellPoint = tileObject.GetCell().Step(tileObject.GetRotation());
        var worldPoint = tilemap.GetCellCenterWorld(new Vector3Int(cellPoint.x, cellPoint.y,0));
        var inventory = GetComponent<PlayerInventory>();

        var handItem = inventory.HandItem();
        if (handItem)
        {
            var handFurniture = handItem.GetComponent<InteractableFurnitureScript>();
            handFurniture.Place(inventory, tilemap, cellPoint);

            return;
        }

        var collider = Physics2D.OverlapPoint(worldPoint);
        
        Debug.DrawRay(transform.position, (worldPoint - transform.position).normalized * 1);
        if (!collider) { return; }

        var door = collider.GetComponent<LevelDoor>();
        if (door)
        {
            GameStateManager.Instance.MoveToLevel(door.SceneConnection, door.DoorConnection);
        }
        var furniture = collider.GetComponent<InteractableFurnitureScript>();
        if (furniture)
        {
            furniture.Pickup(inventory);
        }
    }
}
