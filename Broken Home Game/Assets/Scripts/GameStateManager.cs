using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    private static GameStateManager _instance;

    [SerializeField]
    private string[] gameLevels;

    [SerializeField]
    private PlayerController playerController;

    private void Awake()
    {
        if (_instance) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        _instance = this;
    }

    public void StartGame()
    {
        Debug.Log("Start Game");
    }
}
