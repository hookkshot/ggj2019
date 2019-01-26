using UnityEngine;
using UnityEngine.Tilemaps;

public class InteractableFurnitureScript : MonoBehaviour
{
    [SerializeField] bool setZOrdering = true;

    private void Awake()
    {
        if (setZOrdering)
        {
            ZOrdering.SetZOrdering(gameObject.transform);
        }
    }

    void Place(Tilemap map, Vector3Int cell)
    {
        gameObject.transform.parent = map.transform;
        gameObject.transform.localPosition = map.CellToLocal(cell);

        if (setZOrdering)
        {
            ZOrdering.SetZOrdering(gameObject.transform);
        }

        gameObject.GetComponentInParent<ZoneScript>().OnUpdateLayout();
    }

    void Pickup(PlayerController player)
    {
        ZoneScript zoneScript = gameObject.GetComponentInParent<ZoneScript>();

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
