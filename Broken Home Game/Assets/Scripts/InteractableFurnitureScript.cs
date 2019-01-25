using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class InteractableFurnitureScript : MonoBehaviour
{
    void Place(Tilemap map, Vector3Int cell)
    {
        gameObject.transform.parent = map;
        gameObject.transform.localPosition = map.CellToLocal(cell);

        var zoneScript = gameObject.GetComponentInParent<ZoneScript>();
        zoneScript.OnUpdateLayout();
    }

    void Pickup(PlayerController player)
    {
        var zoneScript = gameObject.GetComponentInParent<ZoneScript>();
        
        gameObject.transform.parent = player.transform;
        gameObject.transform.localPosition = new Vector3(0, 0, 1);
        gameObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

        zoneScript.OnUpdateLayout();
    }

    void Rotate()
    {
        gameObject.transform.localRotation *= Quaternion.AngleAxis(90, new Vector3(0, 0, 1));
    }
}
