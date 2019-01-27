using System.Collections.Generic;
using UnityEngine;

public class ZoneScript : MonoBehaviour
{
    public List<Furniture> Furniture { get; private set; } = new List<Furniture>();
    public bool HasFengShui { get; private set; }

    public void OnUpdateLayout()
    {
        HasFengShui = true;

        foreach (var script in Furniture)
        {
            script.UpdateFengShui();
            HasFengShui &= script.HasFengShui();
        }

        var room = GetComponentInParent<Room>();
        room.OnZoneUpdate();
    }

    public void RemoveFurniture(Furniture f)
    {
        Furniture.Remove(f);
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
