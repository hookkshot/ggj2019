using UnityEngine;

[ExecuteInEditMode]
public class SetSortingLayer : MonoBehaviour
{
    [SerializeField] Renderer[] _myRenderers;
    [SerializeField] string _mySortingLayer;
    [SerializeField] int _mySortingOrderInLayer;

    // Use this for initialization
    void OnValidate()
    {
        if (_myRenderers == null)
        {
            _myRenderers = this.GetComponentsInChildren<Renderer>();
        }

        SetLayer();
    }


    public void SetLayer()
    {
        foreach (Renderer renderer in _myRenderers)
        {
            renderer.sortingLayerName = _mySortingLayer;
            renderer.sortingOrder = _mySortingOrderInLayer;
        }
    }

}