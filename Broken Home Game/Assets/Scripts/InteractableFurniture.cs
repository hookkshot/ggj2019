using UnityEngine;
using UnityEngine.Tilemaps;

public class InteractableFurniture : Furniture
{
    public void Place(PlayerInventory player, Tilemap map, Vector2Int cell)
    {
        player.Drop(gameObject);

        var tileObject = GetComponent<TileObject>();
        tileObject.MoveToCell(map, cell);

        ZOrdering.SetZOrdering(gameObject.transform);

        if (Zone) { Zone.OnUpdateLayout(); }
    }

    public void Pickup(PlayerInventory player)
    {
        player.AddToInventory(gameObject);
    }

    private void OnDisable()
    {
        if (Zone) { Zone.RemoveFurniture(this); }
    }

    public void Rotate()
    {
        gameObject.transform.localRotation *= Quaternion.AngleAxis(90, gameObject.transform.forward);
    }
}
