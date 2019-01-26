﻿using System.Collections.Generic;
using UnityEngine;

public class ZoneScript : MonoBehaviour
{
    List<Furniture> furniture;
    public bool HasFengShui { get; private set; }

    // Start is called before the first frame update
    void Awake()
    {
        furniture = new List<Furniture>(GetComponentsInChildren<Furniture>());

        foreach (var f in furniture)
        {
            var fn = f.GetComponent<InteractableFurnitureScript>();
            if (fn) { fn.SetZone(this); }
        }
    }

    public void OnUpdateLayout() {
        HasFengShui = true;

        foreach (var script in furniture)
        {
            script.UpdateFengShui();
            HasFengShui &= script.HasFengShui();
        }

        var room = GetComponentInParent<Room>();
        room.OnZoneUpdate();
    }

    public void RemoveFurniture(InteractableFurnitureScript f)
    {
        var fn = f.GetComponent<Furniture>();

        furniture.Remove(fn);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var f = collision.gameObject.GetComponent<Furniture>();
        if (f && !furniture.Contains(f)) {
            furniture.Add(f);

            var fn = f.GetComponent<InteractableFurnitureScript>();
            if (fn) { fn.SetZone(this); }
        }

        OnUpdateLayout();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var f = collision.gameObject.GetComponent<Furniture>();
        furniture.Remove(f);

        OnUpdateLayout();
    }
}
