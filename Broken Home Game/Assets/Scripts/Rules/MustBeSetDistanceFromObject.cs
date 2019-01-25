using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Furniture Rules/MustBeSetDistanceFromObject")]
public class MustBeSetDistanceFromObject : FurnitureRule
{
    [SerializeField] string[] _affectedTags = null;
    [SerializeField] float _distanceToCheck = 3f;
    [SerializeField] LayerMask _checkLayers;

    public override bool Passes(MeetsFengShuiScript checkingObject)
    {
        Collider[] colliders = Physics.OverlapSphere(checkingObject.transform.position, _distanceToCheck, _checkLayers.value);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].GetInstanceID() != checkingObject.GetComponentInChildren<Collider>().GetInstanceID())
            {
                if (colliders[i].transform.root.GetComponentInChildren<MeetsFengShuiScript>().FurnitureTags.Intersect(checkingObject.FurnitureTags).Any())
                {
                    return false;
                }
            }
        }

        return true;
    }
}
