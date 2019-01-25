using UnityEngine;

public static class ZOrdering
{
    public static void SetZOrdering(Transform unorderedObject)
    {
        unorderedObject.localPosition = new Vector3(
            unorderedObject.localPosition.x,
            unorderedObject.localPosition.y,
            unorderedObject.localPosition.y * 2f);
    }
}
