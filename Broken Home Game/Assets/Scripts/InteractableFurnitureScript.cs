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

    public void Place(Tilemap map, Vector2Int cell)
    {
        gameObject.transform.parent = map.transform;
        gameObject.transform.localPosition = map.CellToLocal(new Vector3Int(cell.x, cell.y, -1));

        if (setZOrdering)
        {
            ZOrdering.SetZOrdering(gameObject.transform);
        }

        gameObject.GetComponentInParent<ZoneScript>().OnUpdateLayout();
    }

    public void Pickup(PlayerController player)
    {
        ZoneScript zoneScript = gameObject.GetComponentInParent<ZoneScript>();

        gameObject.transform.parent = player.transform;
        gameObject.transform.localPosition = new Vector3(0, 0, 1);
        gameObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

        zoneScript.OnUpdateLayout();
    }

    public void Rotate()
    {
        gameObject.transform.localRotation *= Quaternion.AngleAxis(90, gameObject.transform.forward);
    }
}
