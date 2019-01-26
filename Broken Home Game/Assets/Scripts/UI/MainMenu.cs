using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : Screen
{
    [SerializeField]
    private Button playButton;

    [SerializeField]
    private Button quitButton;

    private void OnEnable()
    {
        playButton.onClick.AddListener(Play);
        quitButton.onClick.AddListener(Quit);
    }

    private void OnDisable()
    {
        playButton.onClick.RemoveListener(Play);
        quitButton.onClick.RemoveListener(Quit);
    }

    public void Play()
    {
        //Create game state manager
    }

    public void Quit()
    {
        Application.Quit();
    }
}
