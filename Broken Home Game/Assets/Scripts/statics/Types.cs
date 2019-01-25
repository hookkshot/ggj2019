using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Rotation
{
    NORTH,
    EAST,
    SOUTH,
    WEST,
    ASKEW
}

static class ExtensionMethods
{
    public static Vector2Int Step(this Vector2Int pos, Rotation rotation)
    {
        switch (rotation)
        {
            case Rotation.NORTH: ++pos.y; break;
            case Rotation.EAST: ++pos.x; break;
            case Rotation.SOUTH: --pos.y; break;
            case Rotation.WEST: --pos.x; break;
            default: break;
        }

        return pos;
    }
}