using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelDoor : MonoBehaviour
{
    public string Name;
    public string SceneConnection;
    public string DoorConnection;
    public Rotation Rotation;

    private void Start()
    {
        Tilemap tilemap = FindObjectOfType<Tilemap>();

        tilemap.SnapToClosestCell(transform);
    }

    public override string ToString()
    {
        return Name;
    }
}
