using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class GHOST : MonoBehaviour
{
    const float GHOST_MIN_TIME = 4;
    const float GHOST_MAX_TIME = 10;

    [SerializeField] PostProcessVolume _postProcessingProfile = null;
    [SerializeField] Color startBloomColor = Color.white;
    [SerializeField] Color endBloomColor = new Color(0f, 0.5f, 0f);

    Grain grain = null;
    Bloom bloom = null;

    public bool IsPaused = false;

    private void Start()
    {
        _postProcessingProfile = FindObjectOfType<PostProcessVolume>();
        grain = _postProcessingProfile.profile.GetSetting<Grain>();
        bloom = _postProcessingProfile.profile.GetSetting<Bloom>();
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

        if (target.InUse == false)
        {
            target.Wobble();
            StartCoroutine(FuckWithPlayerScreen());
            AudioManager.Instance.PlayBadSound();
        }
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
            bloom.intensity.value = GameStateManager.Instance.BloomCurve.Evaluate(d);
            bloom.color.value = Color.Lerp(startBloomColor, endBloomColor, d);
        }
    }

    IEnumerator GhostTimer()
    {
        var ghostoclock = Random.Range(GHOST_MIN_TIME, GHOST_MAX_TIME);
        yield return new WaitForSeconds(ghostoclock);

        if (IsPaused)
            yield break;
        FuckWithFurniture();

        StartCoroutine(GhostTimer());
    }
}
