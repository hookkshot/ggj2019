using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class InteractableFurniture : Furniture
{
    private bool inUse;

    public void Place(PlayerInventory player, Tilemap map, Vector2Int cell)
    {
        if (inUse) return;
        player.Drop(gameObject);

        var tileObject = GetComponent<TileObject>();
        tileObject.MoveToCell(map, cell);

        ZOrdering.SetZOrdering(gameObject.transform);

        if (Zone) { Zone.OnUpdateLayout(); }
    }

    private void Update()
    {
        //if(Input.GetKeyDown(KeyCode.P))
        //    Wobble();
    }

    public void Wobble()
    {
        if (inUse) return;
        StartCoroutine(WobbleI());
    }

    private IEnumerator WobbleI()
    {
        inUse = false;
        var t = transform.GetChild(0);
        var pFrom = t.position;
        var rFrom = t.eulerAngles;
        var sFrom = t.localScale;
        var pTo = t.position + (t.forward * 0.5f);
        var rTo = t.eulerAngles + new Vector3(0,0,360);
        var sTo = new Vector3(sFrom.x * 0.6f, sFrom.y * 0.6f, sFrom.z * 1.2f);

        float time = 0;

        Instantiate(GameStateManager.Instance.GetEffect("GhostParticles"), transform.position, Quaternion.identity);

        while (time < 1f)
        {
            yield return 0;
            time += Time.deltaTime;

            var d = time / 1f;
            var bc = GameStateManager.Instance.BounceCurve.Evaluate(d);
            var wc = GameStateManager.Instance.WobbleCurve.Evaluate(d);
            var oc = GameStateManager.Instance.OverCurve.Evaluate(d);


            t.position = Vector3.Lerp(pFrom, pTo, wc);
            t.eulerAngles = Vector3.LerpUnclamped(rFrom, rTo, oc);
            t.localScale = Vector3.Lerp(sFrom, sTo, bc);
        }

        t.position = pFrom;
        t.eulerAngles = rFrom;
        t.localScale = sFrom;

        inUse = false;
    }

    public void Pickup(PlayerInventory player)
    {
        player.AddToInventory(gameObject);
    }

    private void OnDisable()
    {
        if (Zone) { Zone.RemoveFurniture(this); }
    }

    public void Rotate()
    {
        gameObject.transform.localRotation *= Quaternion.AngleAxis(90, gameObject.transform.forward);
    }
}
