using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class OptionMenu : Screen
{
    private Resolution[] resolutions;

    [SerializeField]
    private TMP_Dropdown resolutionDropdown;

    [SerializeField]
    private AudioMixer mixer;

    [SerializeField]
    private Toggle fullscreenToggle;

    [SerializeField]
    private Slider audioSlider;

    private void OnEnable()
    {
        resolutions = UnityEngine.Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> resOptions = new List<string>();
        int currentResolution = 0;
        foreach (var res in resolutions)
        {
            resOptions.Add(res.width + " x " + res.height);

            if (res.width == UnityEngine.Screen.currentResolution.width && res.height == UnityEngine.Screen.currentResolution.height)
            {
                currentResolution = resOptions.Count;
            }
        }

        resolutionDropdown.AddOptions(resOptions);
        resolutionDropdown.value = currentResolution;
        resolutionDropdown.RefreshShownValue();
        fullscreenToggle.isOn = UnityEngine.Screen.fullScreen;

        audioSlider.value = PlayerPrefs.GetFloat("Volume", 0);
    }

    public void SetVolume(float volume)
    {
        mixer.SetFloat("Volume", volume);
        PlayerPrefs.SetFloat("Volume", volume);
    }

    public void SetResolution(int resIndex)
    {
        var resolution = resolutions[resIndex];
        UnityEngine.Screen.SetResolution(resolution.width, resolution.height, fullscreenToggle.isOn);
    }

    private void OnDisable()
    {
        
    }
}
