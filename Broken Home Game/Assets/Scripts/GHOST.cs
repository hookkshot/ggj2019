using System.Collections;
using System.Linq;
using UnityEngine;

public class GHOST : MonoBehaviour
{
    const float GHOST_MIN_TIME = 5;
    const float GHOST_MAX_TIME = 30;

    private void Start()
    {
        StartCoroutine(GhostTimer());
    }

    void FuckWithFurniture()
    {
        var zones = GetComponentsInChildren<ZoneScript>();
        var badZones = zones.Where(z => !z.HasFengShui).ToList();

        var zone = badZones[Random.Range(0, badZones.Count)];

        var furniture = zone.GetComponentsInChildren<InteractableFurniture>();
        var badFurniture = furniture.Where(f => !f.HasFengShui()).ToList();

        var target = badFurniture[Random.Range(0, badFurniture.Count)];

        target.Wobble();
    }

    IEnumerator GhostTimer()
    {
        var ghostoclock = Random.Range(GHOST_MIN_TIME, GHOST_MAX_TIME);
        yield return new WaitForSeconds(ghostoclock);

        FuckWithFurniture();

        StartCoroutine(GhostTimer());
    }
}
