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

    private void Awake()
    {
        zones = GetComponentsInChildren<ZoneScript>();
    }

    public void Setup(PlayerController playerController, RoomState state)
    {
        this.playerController = playerController;
        playerInventory = playerController.GetComponent<PlayerInventory>();

        if(state != null)
        {
            ToState(state);
        }
    }

    public void ToState(RoomState state)
    {
        foreach (var furniture in FindObjectsOfType<Furniture>())
        {
            Destroy(furniture.gameObject);
        }

        foreach (var furnitureState  in state.Furnitures)
        {
            var furniture = Instantiate(GameStateManager.Instance.GetFurniture(furnitureState.Name));

            furniture.TileObject.MoveToCell(Tilemap, furnitureState.Cell);
            furniture.TileObject.Face(furnitureState.Rotation);
        }
    }

    public RoomState ToState()
    {
        var state = new RoomState();

        foreach (var zone in zones)
        {

            foreach (var furniture in zone.Furniture)
            {
                var furnitureState = new FurnitureState();

                furnitureState.Rotation = furniture.TileObject.GetRotation();
                furnitureState.Cell = furniture.TileObject.GetCell();
                furnitureState.Name = furniture.TypeName;

                state.Furnitures.Add(furnitureState);
            }
        }

        return state;
    }

    public LevelDoor GetDoor(string name)
    {
        return doors.FirstOrDefault(d => d.Name == name);
    }

    public void OnZoneUpdate()
    {
        if (!playerInventory || zones == null) return;
        bool hasFengShui = true;

        foreach (var zone in zones)
        {
            hasFengShui &= zone.HasFengShui;
        }

        hasFengShui &= playerInventory.IsInventoryEmpty();
    }
}
