using UnityEngine;
using System.Linq;

public class MeetsFengShuiScript : MonoBehaviour
{
    FurnitureRule[] rules;

    // Start is called before the first frame update
    void Awake()
    {
        rules = GetComponentsInChildren<FurnitureRule>();
    }

    public bool HasFengShui()
    {
        return rules.All(f => f.Passes());
    }
}
