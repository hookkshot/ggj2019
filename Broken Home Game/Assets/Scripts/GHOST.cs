using System.Collections;
using System.Linq;
using UnityEngine;

public class GHOST : MonoBehaviour
{
    const float GHOST_MIN_TIME = 20;
    const float GHOST_MAX_TIME = 100;

    void FuckWithFurniture()
    {
        var zones = GetComponentsInChildren<ZoneScript>();
        var badZones = zones.Where(z => !z.HasFengShui).ToList();

        var zone = badZones[Random.Range(0, badZones.Count)];

        var furniture = zone.GetComponentsInChildren<Furniture>();
        var badFurniture = furniture.Where(f => !f.HasFengShui()).ToList();

        var target = badFurniture[Random.Range(0, badFurniture.Count)];
        FuckWithThisFurnitureInParticular(target);
    }

    void FuckWithThisFurnitureInParticular(Furniture f)
    {
        f.GetComponent<TileObject>().Face(Rotation.ASKEW);
    }

    IEnumerator GhostTimer()
    {
        var ghostoclock = Random.Range(GHOST_MIN_TIME, GHOST_MAX_TIME);
        yield return new WaitForSeconds(ghostoclock);

        FuckWithFurniture();

        StartCoroutine(GhostTimer());
    }
}
