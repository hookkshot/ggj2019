using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Furniture Rules/MustFaceDifferentDirectionToObject")]
public class MustFaceDifferentDirectionToObject : FurnitureRule
{
    [SerializeField] string[] _affectedTags = null;

    public override bool Passes(Furniture checkingObject)
    {
        if (!checkingObject.Zone) { return false; }

        var checkingTile = checkingObject.GetComponent<TileObject>();
        if (!checkingTile) { return true; }

        var rotation = checkingTile.GetRotation();

        foreach (var f in checkingObject.Zone.Furniture)
        {
            if (f == checkingObject) { continue; }
            if (!f.FurnitureTags.Intersect(_affectedTags).Any()) { continue; }

            var tileComponent = f.GetComponent<TileObject>();
            if (!tileComponent) { continue; }

            if (tileComponent.GetRotation() == rotation) { return false; }
        }

        return true;
    }
}
