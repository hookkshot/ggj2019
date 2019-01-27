using System.Collections.Generic;
using UnityEngine;

public class ZoneScript : MonoBehaviour
{
    public List<Furniture> Furniture { get; private set; }
    public bool HasFengShui { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        Furniture = new List<Furniture>(GetComponentsInChildren<Furniture>());

        foreach (var f in Furniture)
        {
            f.Zone = this;
        }

        OnUpdateLayout();
    }

    public void OnUpdateLayout() {
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
        if (f && !Furniture.Contains(f)) {
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
