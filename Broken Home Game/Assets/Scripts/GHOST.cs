using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class GHOST : MonoBehaviour
{
    const float GHOST_MIN_TIME = 5;
    const float GHOST_MAX_TIME = 15;

    [SerializeField] PostProcessVolume _postProcessingProfile = null;

    Grain grain = null;

    private void Start()
    {
        _postProcessingProfile = FindObjectOfType<PostProcessVolume>();
        grain = _postProcessingProfile.profile.GetSetting<Grain>();
        StartCoroutine(GhostTimer());
    }

    void FuckWithFurniture()
    {
        var zones = GetComponentsInChildren<ZoneScript>();
        var badZones = zones.Where(z => !z.HasFengShui).ToList();

        if (badZones.Count <= 0)
        {
            AudioManager.Instance.PlayGoodSound();
            return;
        }

        var zone = badZones[Random.Range(0, badZones.Count)];
        var badFurniture = zone.Furniture.Where(f => f is InteractableFurniture).Where(f => !f.HasFengShui()).Select(f => f as InteractableFurniture).ToList();

        if (badFurniture.Count <= 0)
        {
            AudioManager.Instance.PlayGoodSound();
            return;
        }

        var target = badFurniture[Random.Range(0, badFurniture.Count)];

        target.Wobble();
        StartCoroutine(FuckWithPlayerScreen());
        AudioManager.Instance.PlayBadSound();
    }

    IEnumerator FuckWithPlayerScreen()
    {
        float time = 0;

        while (time < 1f)
        {
            yield return 0;
            time += Time.deltaTime;

            var d = time / 1f;

            grain.intensity.value = GameStateManager.Instance.GrainCurve.Evaluate(d);
        }
    }

    IEnumerator GhostTimer()
    {
        var ghostoclock = Random.Range(GHOST_MIN_TIME, GHOST_MAX_TIME);
        yield return new WaitForSeconds(ghostoclock);

        FuckWithFurniture();

        StartCoroutine(GhostTimer());
    }
}
