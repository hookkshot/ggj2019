using UnityEngine;

[CreateAssetMenu(menuName = "Furniture Rules/CannotBeNextToOtherFurniture")]
public class CannotBeNextToOtherFurniture : FurnitureRule
{
    [SerializeField] LayerMask _checkLayers;
    [SerializeField] float _checkRadius = 1.5f;

    public override bool Passes(Furniture checkingObject)
    {
        // redo

        return true;
    }
}
