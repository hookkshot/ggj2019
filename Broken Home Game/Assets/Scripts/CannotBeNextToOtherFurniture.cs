using UnityEngine;

[CreateAssetMenu(menuName = "Furniture Rules/CannotBeNextToOtherFurniture")]
public class CannotBeNextToOtherFurniture : FurnitureRule
{
    [SerializeField] private LayerMask _checkLayers;
    [SerializeField] private float _checkRadius = 1.5f;

    public override bool Passes(MeetsFengShuiScript checkingObject)
    {
        Collider[] colliders = Physics.OverlapSphere(checkingObject.transform.position, _checkRadius, _checkLayers.value);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].GetInstanceID() != checkingObject.GetComponentInChildren<Collider>().GetInstanceID())
            {
                checkingObject.GetComponentInChildren<MeshRenderer>().material.SetColor("_Color", Color.red);   // TEMP
                return false;
            }
        }

        checkingObject.GetComponentInChildren<MeshRenderer>().material.SetColor("_Color", Color.white); // TEMP

        return true;
    }
}
