using UnityEngine;

[CreateAssetMenu(menuName = "Furniture Rules/CannotBeNextToOtherFurniture")]
public class CannotBeNextToOtherFurniture : FurnitureRule
{
    [SerializeField] LayerMask _checkLayers;
    [SerializeField] float _checkRadius = 1.5f;

    public override bool Passes(Furniture checkingObject)
    {
        Collider[] colliders = Physics.OverlapSphere(checkingObject.transform.position, _checkRadius, _checkLayers.value);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].GetInstanceID() != checkingObject.GetComponentInChildren<Collider>().GetInstanceID())
            {
                return false;
            }
        }

        return true;
    }
}
