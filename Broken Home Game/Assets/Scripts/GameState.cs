using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState
{
    public Dictionary<string,RoomState> Rooms = new Dictionary<string, RoomState>();
}

public class RoomState
{
    public List<FurnitureState> Furnitures = new List<FurnitureState>();
    public bool Completed;
}

public class FurnitureState
{
    public Vector2Int Cell;
    public Rotation Rotation;
    public string Name;
}
