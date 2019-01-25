using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Tilemap tilemap;

    private BoundsInt currentBounds;
    private TileObject tileObject;

    void Awake()
    {
        tileObject = gameObject.GetComponentInChildren<TileObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            tileObject.RotateRight();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            tileObject.RotateLeft();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            tileObject.Step(tilemap, tileObject.GetRotation());
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            tileObject.Step(tilemap, tileObject.GetRotation().RotateLeft().RotateLeft());
        }
    }
}
