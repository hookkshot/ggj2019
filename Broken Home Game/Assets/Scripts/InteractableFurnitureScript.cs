﻿using UnityEngine;
using UnityEngine.Tilemaps;

public class InteractableFurnitureScript : MonoBehaviour
{
    [SerializeField] bool setZOrdering = true;

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

        // TODO FIND ZONE
        // gameObject.GetComponentInParent<ZoneScript>().OnUpdateLayout();
    }

    public void Pickup(PlayerInventory player)
    {
        ZoneScript zoneScript = gameObject.GetComponentInParent<ZoneScript>();

        // TODO find zone
        //zoneScript.OnUpdateLayout();

        player.AddToInventory(gameObject);
    }

    public void Rotate()
    {
        gameObject.transform.localRotation *= Quaternion.AngleAxis(90, gameObject.transform.forward);
    }
}
