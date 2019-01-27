using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Furniture Rules/MustBeSetDistanceFromObject")]
public class MustBeSetDistanceFromObject : FurnitureRule
{
    [SerializeField] string[] _affectedTags = null;
    [SerializeField] float _distanceToCheck = 3f;
    [SerializeField] LayerMask _checkLayers;

    public override bool Passes(Furniture checkingObject)
    {
// redo required

        return false;
    }
}
