using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Furniture Rules/MustHaveAtLeastNumberOfObjects")]
public class MustHaveAtLeastNumberOfObjects : FurnitureRule
{
    [SerializeField] string _affectedTag = null;
    [SerializeField] int _distanceInGridSpaces = 1;
    [SerializeField] int _numberOfObjects = 2;
    [SerializeField] CheckType _distanceCheckType = CheckType.GreaterThan;

    public override bool Passes(Furniture checkingObject)
    {
        List<Furniture> furnitureInZone = checkingObject.Zone.Furniture;

        int count = 0;
        foreach (Furniture furniture in furnitureInZone)
        {
            if (furniture == checkingObject)
            {
                continue;
            }

            if (furniture.HasAnyTags(new string[] { _affectedTag }))
            {
                float distance = Mathf.CeilToInt(Vector2Int.Distance(furniture.TileObject.Cell, checkingObject.TileObject.Cell));
                bool failed = false;

                switch (_distanceCheckType)
                {
                    case CheckType.NotEquals:
                        failed = !(distance != _distanceInGridSpaces);
                        break;

                    case CheckType.LessThan:
                        failed = !(distance < _distanceInGridSpaces);
                        break;

                    case CheckType.GreaterThan:
                        failed = !(distance > _distanceInGridSpaces);
                        break;

                    case CheckType.Equals:
                        failed = !(distance == _distanceInGridSpaces);
                        break;
                }

                if (!failed)
                {
                    count++;
                }
            }
        }

        return (count >= _numberOfObjects);
    }
}
