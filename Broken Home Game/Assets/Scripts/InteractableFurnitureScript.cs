using UnityEngine;
using UnityEngine.Tilemaps;

public class InteractableFurnitureScript : MonoBehaviour
{
    [SerializeField] bool adjustZOrdering = true;

    ZoneScript zoneScript = null;

    private void Awake()
    {
        zoneScript = gameObject.GetComponentInParent<ZoneScript>();
        if (adjustZOrdering)
        {
            ZOrdering.SetZOrdering(transform);
        }

    }

    void Place(Tilemap map, Vector3Int cell)
    {
        gameObject.transform.parent = map.transform;
        gameObject.transform.localPosition = map.CellToLocal(cell);

        zoneScript.OnUpdateLayout();

        if (adjustZOrdering)
        {
            ZOrdering.SetZOrdering(transform);
        }
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
        gameObject.transform.localRotation *= Quaternion.AngleAxis(90, gameObject.transform.up);
    }
}
