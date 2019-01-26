using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    Vector3 handPos;

    public List<GameObject> Inventory { get; private set; }

    private void Awake()
    {
        Inventory = new List<GameObject>{ null };
    }

    private void Update()
    {
        var h = HandItem();
        if (!h) { return; }

        h.transform.localPosition = handPos + new Vector3(0, 0.25f + Mathf.Sin(Time.realtimeSinceStartup*2) / 16, 0);
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
        if (first) { first.transform.localPosition = handPos; }

        Inventory.RemoveAt(0);
        Inventory.Add(first);

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

    public bool IsInventoryEmpty()
    {
        return Inventory.Count <= 1;
    }
}
