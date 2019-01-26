using System.Collections.Generic;
using UnityEngine;

public class ZoneScript : MonoBehaviour
{
    List<Furniture> furniture;
    public List<Furniture> Furniture { get { return furniture; } }
    public bool HasFengShui { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        furniture = new List<Furniture>(GetComponentsInChildren<Furniture>());

        foreach (var f in furniture)
        {
            f.Zone = this;
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

    public void RemoveFurniture(Furniture f)
    {
        furniture.Remove(f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (furniture == null) return;
        var f = collision.gameObject.GetComponent<Furniture>();
        if (f && !furniture.Contains(f)) {
            furniture.Add(f);
            f.Zone = this;
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
