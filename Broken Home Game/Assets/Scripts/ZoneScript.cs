using System.Collections.Generic;
using UnityEngine;

public class ZoneScript : MonoBehaviour
{
    public List<Furniture> Furniture { get; private set; } = new List<Furniture>();
    public bool HasFengShui { get; private set; }

    public void OnUpdateLayout()
    {
        var room = GetComponentInParent<Room>();
        if (!room) { return; }

        HasFengShui = true;

        foreach (var script in Furniture)
        {
            script.UpdateFengShui();
            HasFengShui &= script.HasFengShui();
        }

        
        room.OnZoneUpdate();
    }

    public void RemoveFurniture(Furniture f)
    {
        Furniture.Remove(f);
        OnUpdateLayout();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Furniture == null) return;
        var f = collision.gameObject.GetComponent<Furniture>();
        if (f && !Furniture.Contains(f))
        {
            Furniture.Add(f);
            f.Zone = this;
        }

        OnUpdateLayout();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var f = collision.gameObject.GetComponent<Furniture>();
        Furniture.Remove(f);

        OnUpdateLayout();
    }
}
