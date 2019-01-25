using UnityEngine;

[CreateAssetMenu()]
public class ExampleRule : FurnitureRule
{
    public override bool Passes()
    {
        return true;
    }
}
