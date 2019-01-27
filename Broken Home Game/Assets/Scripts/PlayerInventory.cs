using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] Camera itemCamera = null;
    Vector3 handPos;

    public List<GameObject> Inventory { get; private set; } = new List<GameObject> { null };

    private void Awake()
    {
        itemCamera = transform.root.GetComponentInChildren<Camera>();
        Debug.Assert(itemCamera != null, "No Item camera on player heirachy");
        itemCamera.gameObject.SetActive(false);
    }

    private void Update()
    {
        var h = HandItem();
        if (!h) { return; }

        h.transform.localPosition = handPos + new Vector3(0, 0.25f + Mathf.Sin(Time.realtimeSinceStartup * 2) / 16, 0);
    }

    public GameObject HandItem()
    {
        if (Inventory.Count <= 0) { return null; }

        return Inventory[0];
    }

    public void Drop(GameObject o)
    {
        if (o == HandItem()) { o.transform.localPosition = handPos; }
        if (!o) { return; }

        Inventory.Remove(o);

        if (HandItem())
        {
            handPos = HandItem().transform.localPosition;
            HandItem().SetActive(true);
        }
    }

    public void AddToInventory(GameObject o)
    {
        Inventory.Insert(0, o);

        if (HandItem())
        {
            handPos = HandItem().transform.localPosition;
        }
    }

    public void SpinInventory()
    {
        var first = HandItem();
        if (first)
        {
            var tileObject = first.GetComponent<TileObject>();
            if (tileObject.IsTweening()) { return; }

            first.transform.localPosition = handPos;
        }

        Inventory.RemoveAt(0);
        Inventory.Add(first);
        SetNextItemIcon();

        foreach (GameObject o in Inventory)
        {
            if (!o) { continue; }
            o.SetActive(false);
        }

        if (HandItem())
        {
            HandItem().SetActive(true);
            handPos = HandItem().transform.localPosition;
        }
    }

    private void SetNextItemIcon()
    {
        GameObject nextItem = (Inventory.Count > 1) ? Inventory[1] : null;

        // ewww magic strings and fragile code.. yeh don't care
        if (nextItem != null)
        {
            nextItem.SetActive(true);
            nextItem.SetLayer(LayerMask.NameToLayer("Inventory"));

            Quaternion rotation = nextItem.transform.rotation;
            nextItem.transform.LookAt(itemCamera.transform, itemCamera.transform.up);
            nextItem.transform.rotation *= Quaternion.Euler(new Vector3(90f, 0f, 0f));

            itemCamera.Render();

            nextItem.transform.rotation = rotation;

            nextItem.SetLayer(LayerMask.NameToLayer("Furniture"));
            nextItem.SetActive(false);
        }
        else
        {
            itemCamera.Render();
        }
    }

    public bool IsInventoryEmpty()
    {
        return Inventory.Count <= 1;
    }
}
