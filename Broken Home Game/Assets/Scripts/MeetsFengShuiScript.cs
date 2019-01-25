using UnityEngine;
using System.Linq;

[RequireComponent(typeof(TileObject))]
public class MeetsFengShuiScript : MonoBehaviour
{
    [SerializeField] FurnitureRule[] rules;
    [SerializeField] string[] tags;

    bool hasFengShui = false;

    public TileObject TileObject { get; private set; } = null;
    public string[] FurnitureTags { get => tags; }

    private void Awake()
    {
        TileObject = GetComponent<TileObject>();
        TileObject.Face(Rotation.NORTH);
    }

    public bool HasFengShui()
    {
        return hasFengShui;
    }

    public void UpdateFengShui()
    {
        hasFengShui = rules.All(f => f.Passes(this));
    }
}
