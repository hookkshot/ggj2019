using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneScript : MonoBehaviour
{
    List<MeetsFengShuiScript> furniture;

    // Start is called before the first frame update
    void Awake()
    {
        furniture = new List<MeetsFengShuiScript>(GetComponentsInChildren<MeetsFengShuiScript>());
    }

    public void OnUpdateLayout() {
        foreach (var script in furniture)
        {
            script.UpdateFengShui();
        }
    }
}
