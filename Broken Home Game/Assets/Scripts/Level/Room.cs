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

    public bool IsFengShui { get; private set; }

    private void Awake()
    {
        zones = GetComponentsInChildren<ZoneScript>();
    }

    private float ghostTimer = 0;

    private void Update()
    {
        ghostTimer += Time.deltaTime;

        if (ghostTimer > 20f)
        {

        }
    }

    public void Setup(PlayerController playerController, RoomState state)
    {
        this.playerController = playerController;
        playerInventory = playerController.GetComponent<PlayerInventory>();

        if(state != null)
        {
            FromState(state);

        }

        foreach (var zone in zones)
        {
            zone.OnUpdateLayout();
        }
    }

    public void FromState(RoomState state)
    {
        foreach (var furniture in FindObjectsOfType<Furniture>().Where(f => f.GetComponent<InteractableFurniture>() != null))
        {
            Destroy(furniture.gameObject);
        }

        foreach (var furnitureState  in state.Furnitures)
        {
            var furniture = Instantiate(GameStateManager.Instance.GetFurniture(furnitureState.Name));

            furniture.TileObject.MoveToCell(Tilemap, furnitureState.Cell);
            furniture.TileObject.FaceSnap(furnitureState.Rotation);
        }
    }

    public RoomState ToState()
    {
        var state = new RoomState();

        foreach (var zone in zones)
        {

            foreach (var furniture in zone.Furniture.Where(f => f.GetComponent<InteractableFurniture>() != null))
            {
                var furnitureState = new FurnitureState();

                furnitureState.Rotation = furniture.TileObject.GetRotation();
                furnitureState.Cell = furniture.TileObject.GetCell();
                furnitureState.Name = furniture.TypeName;

                state.Furnitures.Add(furnitureState);
            }
        }

        state.Completed = IsFengShui;
        return state;
    }

    public void End()
    {
        enabled = false;
        foreach (var zone in zones)
        {
            zone.enabled = false;
        }
    }

    public LevelDoor GetDoor(string name)
    {
        return doors.FirstOrDefault(d => d.Name == name);
    }

    public void OnZoneUpdate()
    {
        if (!playerInventory || zones == null) return;
        IsFengShui = true;

        foreach (var zone in zones)
        {
            IsFengShui &= zone.HasFengShui;
        }

        IsFengShui &= playerInventory.IsInventoryEmpty();
        GameStateManager.Instance.CheckWinConditions();
    }
}
