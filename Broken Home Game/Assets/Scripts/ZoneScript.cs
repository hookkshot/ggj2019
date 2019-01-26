using System.Collections.Generic;
using UnityEngine;

public class ZoneScript : MonoBehaviour
{
    List<Furniture> furniture;

    // Start is called before the first frame update
    void Awake()
    {
        furniture = new List<Furniture>(GetComponentsInChildren<Furniture>());
    }

    public void OnUpdateLayout() {
        foreach (var script in furniture)
        {
            script.UpdateFengShui();
        }
    }
}
