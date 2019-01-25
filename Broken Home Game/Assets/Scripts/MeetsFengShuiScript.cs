using UnityEngine;
using System.Linq;

public class MeetsFengShuiScript : MonoBehaviour
{
    [SerializeField()]
    FurnitureRule[] rules;

    bool hasFengShui = false;

    public bool HasFengShui()
    {
        return hasFengShui;
    }

    public void UpdateFengShui() { 
        hasFengShui = rules.All(f => f.Passes(this));
    }
}
