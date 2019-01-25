using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    private float x = 1;
    private float y = 0.5f;

    private int xCell = 0;
    private int yCell = 0;

    [SerializeField]
    private Tilemap tilemap;

    private BoundsInt currentBounds;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveToCell(xCell + 1, yCell);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveToCell(xCell - 1, yCell);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveToCell(xCell, yCell + 1);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveToCell(xCell, yCell - 1);
        }


    }

    private void MoveToCell(int x, int y)
    {
        var tile = tilemap.GetTile(new Vector3Int(x, y, 0));
        if (!tile) return;
        xCell = x;
        yCell = y;
        transform.position = tilemap.CellToWorld(new Vector3Int(x, y, 0));
    }
}
