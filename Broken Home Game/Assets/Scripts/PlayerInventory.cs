using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<GameObject> Inventory { get; private set; }

    private void Awake()
    {
        Inventory = new List<GameObject>{ null };
    }

    public GameObject HandItem()
    {
        if (Inventory.Count <= 0) { return null; }

        return Inventory[0];
    }

    public void AddToInventory(GameObject o)
    {
        Inventory.Insert(0, o);
    }

    public void SpinInventory()
    {
        var first = HandItem();
        Inventory.RemoveAt(0);
        Inventory.Add(first);
    }

    public bool IsInventoryEmpty()
    {
        return Inventory.Count <= 1;
    }
}
