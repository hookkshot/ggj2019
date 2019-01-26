using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Furniture Rules/MustFaceDifferentDirectionToObject")]
public class MustFaceDifferentDirectionToObject : FurnitureRule
{
    [SerializeField] string[] _affectedTags = null;
    [SerializeField] LayerMask _checkLayers;

    public override bool Passes(Furniture checkingObject)
    {
        return false;
    }
}
