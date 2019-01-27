using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Furniture Rules/MustBeSetDistanceFromObject")]
public class MustBeSetDistanceFromObject : FurnitureRule
{
    [SerializeField] string _affectedTag = null;
    [SerializeField] float _distanceInGridSpaces = 3f;

    public override bool Passes(Furniture checkingObject)
    {
        List<Furniture> furnitureInZone = checkingObject.Zone.Furniture;

        foreach (Furniture furniture in furnitureInZone)
        {
            if (furniture == checkingObject)
            {
                continue;
            }

            if (furniture.HasAnyTags(new string[] { _affectedTag }))
            {
                Vector2Int a = furniture.TileObject.Cell;
                Vector2Int b = checkingObject.TileObject.Cell;

                if (Vector2Int.Distance(a, b) > _distanceInGridSpaces)
                {
                    return false;
                }
            }
        }

        return true;
    }
}
