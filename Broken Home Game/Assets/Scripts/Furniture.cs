﻿using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(TileObject))]
public class Furniture : MonoBehaviour
{
    [SerializeField] FurnitureRule[] rules;
    [SerializeField] string[] tags;

    [SerializeField] bool snapToGrid = true;
    public string TypeName;

    bool hasFengShui = false;

    public ZoneScript Zone { get; set; }

    public TileObject TileObject { get; private set; } = null;
    public string[] FurnitureTags { get => tags; }

    private void Awake()
    {
        TileObject = GetComponent<TileObject>();
        if (snapToGrid)
        {
            Tilemap tilemap = FindObjectOfType<Tilemap>();

            var cell = tilemap.SnapToClosestCell(transform);
            if (TileObject) {
                TileObject.SetCell(tilemap);
                TileObject.FaceSnap(TileObject.GetRotation());
            }
        }

        ZOrdering.SetZOrdering(gameObject.transform);
    }

    public bool HasFengShui()
    {
        return hasFengShui;
    }

    public void UpdateFengShui()
    {
        hasFengShui = rules.All(f => f.Passes(this));
    }
}
