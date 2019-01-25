using UnityEngine;

[CreateAssetMenu()]
public class ExampleRule : FurnitureRule
{
    public override bool Passes(MeetsFengShuiScript checkingObject)
    {
        return true;
    }
}
