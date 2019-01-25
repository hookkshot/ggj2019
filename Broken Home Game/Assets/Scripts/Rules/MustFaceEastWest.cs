﻿using UnityEngine;

[CreateAssetMenu(menuName = "Furniture Rules/MustFaceEastWest")]
public class MustFaceEastWest : FurnitureRule
{
    public override bool Passes(MeetsFengShuiScript checkingObject)
    {
        if (checkingObject.TileObject.GetRotation() == Rotation.EAST || checkingObject.TileObject.GetRotation() == Rotation.WEST)
        {
            return true;
        }

        return false;
    }
}
