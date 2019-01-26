using UnityEngine;
using UnityEngine.Events;

public class Room : MonoBehaviour
{
    public void OnZoneUpdate()
    {
        bool hasFengShui = true;

        var zones = GetComponentsInChildren<ZoneScript>();
        foreach (var zone in zones) {
            hasFengShui &= zone.HasFengShui;
        }

        var player = GetComponentInChildren<PlayerInventory>();
        hasFengShui &= player.IsInventoryEmpty();
    }
}
