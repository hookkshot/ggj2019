using UnityEngine;
using UnityEngine.Tilemaps;

public class InteractableFurniture : Furniture
{
    [SerializeField] bool setZOrdering = true;

    ZoneScript zone;

    public void SetZone(ZoneScript z)
    {
        zone = z;
    }

    private void Awake()
    {
        if (setZOrdering)
        {
            ZOrdering.SetZOrdering(gameObject.transform);
        }
    }

    public void Place(PlayerInventory player, Tilemap map, Vector2Int cell)
    {
        player.Drop(gameObject);

        var tileObject = GetComponent<TileObject>();
        tileObject.MoveToCell(map, cell);

        if (setZOrdering)
        {
            ZOrdering.SetZOrdering(gameObject.transform);
        }

        if (zone) { zone.OnUpdateLayout(); }
    }

    public void Pickup(PlayerInventory player)
    {
        player.AddToInventory(gameObject);
    }

    private void OnDisable()
    {
        if (zone) { zone.RemoveFurniture(this); }
    }

    public void Rotate()
    {
        gameObject.transform.localRotation *= Quaternion.AngleAxis(90, gameObject.transform.forward);
    }
}
