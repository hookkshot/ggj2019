using UnityEngine;

public abstract class FurnitureRule : ScriptableObject
{
    public abstract bool Passes();
}
