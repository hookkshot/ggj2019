using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(TileObject), typeof(PlayerInventory))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] Tilemap tilemap;

    private BoundsInt currentBounds;
    private TileObject tileObject;

    public void Setup(Tilemap tilemap, Rotation rotation)
    {
        this.tilemap = tilemap;
        tileObject = GetComponent<TileObject>();
        tileObject.FaceSnap(rotation);

        var cell = tilemap.SnapToClosestCell(transform);
        tileObject.MoveToCellNoCollisions(tilemap, new Vector2Int(cell.x, cell.y).Step(rotation));
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
            if (Input.GetKey(KeyCode.UpArrow))
            {
                tileObject.Step(tilemap, tileObject.GetRotation());
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                tileObject.Step(tilemap, tileObject.GetRotation().RotateLeft().RotateLeft());
            }
            if(Input.GetKey(KeyCode.Escape))
            {
                Application.Quit();
            }

            var cellPoint = tileObject.Cell.Step(tileObject.GetRotation());
            var worldPoint = tilemap.GetCellCenterWorld(new Vector3Int(cellPoint.x, cellPoint.y, 0));

            foreach (GameObject o in inventory.Inventory)
            {
                if (!o) { continue; }
                o.transform.position = worldPoint;
                ZOrdering.SetZOrdering(o.transform);
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
        var cellPoint = tileObject.Cell.Step(tileObject.GetRotation());
        var worldPoint = tilemap.GetCellCenterWorld(new Vector3Int(cellPoint.x, cellPoint.y, 0));
        var inventory = GetComponent<PlayerInventory>();

        var handItem = inventory.HandItem();
        if (handItem)
        {
            var handFurniture = handItem.GetComponent<InteractableFurniture>();
            handFurniture.Place(inventory, tilemap, cellPoint);
            handFurniture.SetResetValues();

            return;
        }

        var collider = Physics2D.OverlapPoint(worldPoint, LayerMask.GetMask(new string[] { "Furniture", "Interactable" }));
        if (!collider) { return; }

        var door = collider.GetComponent<LevelDoor>();
        if (door)
        {
            GameStateManager.Instance.MoveToLevel(door.SceneConnection, door.DoorConnection);
        }
        var furniture = collider.GetComponent<InteractableFurniture>();
        if (furniture)
        {
            furniture.Pickup(inventory);
        }
    }
}
