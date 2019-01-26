using UnityEngine;
using System.Linq;

[CreateAssetMenu(menuName = "Furniture Rules/CannotFaceObjects")]
public class CannotFaceObjects : FurnitureRule
{
    [SerializeField] LayerMask _checkLayers;
    [SerializeField] string[] _affectedTags = null;

    public override bool Passes(Furniture checkingObject)
    {
        TileObject t = checkingObject.GetComponent<TileObject>();
        if (!t) { return false; }

        var filter = new ContactFilter2D();
        filter.SetLayerMask(~LayerMask.NameToLayer("Zone"));

        RaycastHit2D[] results = new RaycastHit2D[2];

        var resultCount = Physics2D.Raycast(checkingObject.transform.position, t.transform.forward, filter, results);

        foreach (var hit in results)
        {
            if (!hit.collider) { continue; }
            if (hit.collider.gameObject == checkingObject.gameObject) { continue; }

            var furniture = hit.collider.gameObject.GetComponentInParent<Furniture>();
            if (!furniture) { continue; }

            if (_affectedTags.Intersect(furniture.FurnitureTags).Any())
            {
                return false;
            }
        }

        return true;
    }
}
