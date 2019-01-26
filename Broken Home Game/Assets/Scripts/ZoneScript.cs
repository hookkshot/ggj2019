using System.Collections.Generic;
using UnityEngine;

public class ZoneScript : MonoBehaviour
{
    List<Furniture> furniture;
    public bool HasFengShui { get; private set; }

    // Start is called before the first frame update
    void Awake()
    {
        furniture = new List<Furniture>(GetComponentsInChildren<Furniture>());
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
}
