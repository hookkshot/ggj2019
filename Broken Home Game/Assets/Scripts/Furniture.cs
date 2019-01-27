using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(TileObject))]
public class Furniture : MonoBehaviour
{
    [SerializeField] FurnitureRule[] rules;
    [SerializeField] string[] tags;
    [SerializeField] bool snapToGrid = true;
    public string TypeName;

    bool hasFengShui = false;
    Vector3 resetPosition = Vector3.zero;
    Quaternion resetRotation = Quaternion.identity;

    public ZoneScript Zone { get; set; }
    public TileObject TileObject { get; private set; } = null;
    public string[] FurnitureTags { get => tags; }

    private void Awake()
    {
        TileObject = GetComponent<TileObject>();
    }

    private void Start()
    {
        if (snapToGrid)
        {
            Tilemap tilemap = FindObjectOfType<Tilemap>();

            var cell = tilemap.SnapToClosestCell(transform);
            if (TileObject)
            {
                TileObject.SetCell(tilemap);
                TileObject.FaceSnap(TileObject.GetRotation());
                SetResetValues();
            }
        }

        ZOrdering.SetZOrdering(gameObject.transform);
    }

    public void ResetPositionRotation()
    {
        transform.position = resetPosition;
        transform.rotation = resetRotation;
    }

    public void SetResetValues()
    {
        resetPosition = transform.position;
        resetRotation = transform.rotation;
    }

    public bool HasFengShui()
    {
        return hasFengShui;
    }

    public void UpdateFengShui()
    {
        hasFengShui = rules.All(f => f.Passes(this));

#if UNITY_EDITOR
        if (hasFengShui == false)
        {
            if (GetComponentInChildren<MeshRenderer>() != null)
                GetComponentInChildren<MeshRenderer>().material.color = Color.red;
        }
        else
        {
            if (GetComponentInChildren<MeshRenderer>() != null)
                GetComponentInChildren<MeshRenderer>().material.color = Color.white;
        }
#endif
    }

    public bool HasAnyTags(string[] tags)
    {
        return FurnitureTags.Intersect(tags).Any();
    }

    #region testing
    [ContextMenu("Test FengShui")]
    public void TestFungShui()
    {
        if (Zone == null)
        {
            Zone = FindObjectOfType<ZoneScript>();
        }

        UpdateFengShui();

        if (hasFengShui == false)
        {
            GetComponentInChildren<MeshRenderer>().material.color = Color.red;
        }
        else
        {
            GetComponentInChildren<MeshRenderer>().material.color = Color.white;
        }
    }
    [ContextMenu("Set Test Position")]
    public void SetTestPosition()
    {
        Tilemap tilemap = FindObjectOfType<Tilemap>();

        var cell = tilemap.SnapToClosestCell(transform);
        if (TileObject)
        {
            TileObject.SetCell(tilemap);
            TileObject.FaceSnap(TileObject.GetRotation());
            SetResetValues();
        }

        ZOrdering.SetZOrdering(gameObject.transform);
    }
    #endregion
}
