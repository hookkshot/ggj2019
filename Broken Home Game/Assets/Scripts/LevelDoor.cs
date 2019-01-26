using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDoor : MonoBehaviour
{
    public string Name;
    public string SceneConnection;
    public string DoorConnection;

    public override string ToString()
    {
        return Name;
    }
}
