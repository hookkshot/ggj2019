using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Furniture Rules/MustFaceAndBeInlineWith")]
public class MustFaceAndBeInlineWith : FurnitureRule
{
    [SerializeField] private string[] tagsToCheck = null;

    public override bool Passes(Furniture checkingObject)
    {
        List<Furniture> furnitureInZone = checkingObject.Zone.Furniture;

        foreach (Furniture furniture in furnitureInZone)
        {
            if (furniture == checkingObject)
            {
                continue;
            }

            if (furniture.HasAnyTags(tagsToCheck))
            {
                if (IsFacingObject(furniture, checkingObject))
                {
                    return true;
                }
            }
        }

        return false;
    }

    public bool IsFacingObject(Furniture target, Furniture furniture)
    {
        if (target.TileObject.GetRotation() == furniture.TileObject.GetRotation())
        {
            return false;
        }

        if (target.TileObject.Cell.x == furniture.TileObject.Cell.x || target.TileObject.Cell.y == furniture.TileObject.Cell.y)
        {
            int oppositeDirection = ((int)target.TileObject.GetRotation() + 2) % 4;
            if (oppositeDirection == (int)furniture.TileObject.GetRotation())
            {
                return true;
            }
        }

        return false;
    }
}
