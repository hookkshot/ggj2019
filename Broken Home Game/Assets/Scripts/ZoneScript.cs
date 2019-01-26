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
            var fn = f.GetComponent<InteractableFurniture>();
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

    public void RemoveFurniture(InteractableFurniture f)
    {
        var fn = f.GetComponent<Furniture>();

        furniture.Remove(fn);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (furniture == null) return;
        var f = collision.gameObject.GetComponent<Furniture>();
        if (f && !furniture.Contains(f)) {
            furniture.Add(f);

            var fn = f.GetComponent<InteractableFurniture>();
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
