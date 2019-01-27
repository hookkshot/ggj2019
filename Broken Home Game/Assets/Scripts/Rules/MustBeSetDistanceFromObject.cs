using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Furniture Rules/MustBeSetDistanceFromObject")]
public class MustBeSetDistanceFromObject : FurnitureRule
{
    public enum CheckType
    {
        Equals,
        NotEquals,
        GreaterThan,
        LessThan
    }

    [SerializeField] string _affectedTag = null;
    [SerializeField] int _distanceInGridSpaces = 3;
    [SerializeField] CheckType _checkType = CheckType.NotEquals;

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
                float distance = Mathf.CeilToInt(Vector2Int.Distance(furniture.TileObject.Cell, checkingObject.TileObject.Cell));
                bool failed = false;

                switch(_checkType)
                {
                    case CheckType.NotEquals:
                        failed = distance != _distanceInGridSpaces;
                        break;

                    case CheckType.LessThan:
                        failed = distance < _distanceInGridSpaces;
                        break;

                    case CheckType.GreaterThan:
                        failed = distance > _distanceInGridSpaces;
                        break;

                    case CheckType.Equals:
                        failed = distance == _distanceInGridSpaces;
                        break;
                }

                if(failed)
                {
                    return false;
                }
            }
        }

        return true;
    }
}
