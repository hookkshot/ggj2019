using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Furniture Rules/MustHaveNoOtherFurnitureWithinSetDistance")]
public class MustHaveNoOtherFurnitureWithinSetDistance : FurnitureRule
{
    [SerializeField] int _distanceInGridSpaces = 1;

    public override bool Passes(Furniture checkingObject)
    {
        List<Furniture> furnitureInZone = checkingObject.Zone.Furniture;

        foreach (Furniture furniture in furnitureInZone)
        {
            if (furniture == checkingObject)
            {
                continue;
            }

            Vector2Int a = furniture.TileObject.Cell;
            Vector2Int b = checkingObject.TileObject.Cell;

            if (Mathf.CeilToInt(Vector2Int.Distance(a, b)) <= _distanceInGridSpaces)
            {
                return false;
            }
        }

        return true;
    }
}
