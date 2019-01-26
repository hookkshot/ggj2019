using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class Room : MonoBehaviour
{
    public string SceneName;

    [SerializeField]
    private LevelDoor[] doors;

    public Tilemap Tilemap;
    private ZoneScript[] zones;
    private PlayerController playerController;
    private PlayerInventory playerInventory;

    public void Setup(PlayerController playerController)
    {
        zones = GetComponentsInChildren<ZoneScript>();
        this.playerController = playerController;
        playerInventory = playerController.GetComponent<PlayerInventory>();
    }

    public LevelDoor GetDoor(string name)
    {
        return doors.FirstOrDefault(d => d.Name == name);
    }

    public void OnZoneUpdate()
    {
        bool hasFengShui = true;

        if (zones == null) { return; }

        foreach (var zone in zones)
        {
            hasFengShui &= zone.HasFengShui;
        }

        hasFengShui &= playerInventory.IsInventoryEmpty();
    }


}
