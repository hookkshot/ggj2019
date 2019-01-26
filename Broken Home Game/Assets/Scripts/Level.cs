using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Tilemaps;

public class Level : MonoBehaviour
{
    public string SceneName;

    [SerializeField]
    private ZoneScript[] zones;

    [SerializeField]
    private LevelDoor[] doors;

    public Tilemap Tilemap;

    public LevelDoor GetDoor(string name)
    {
        return doors.FirstOrDefault(d => d.Name == name);
    }
}
