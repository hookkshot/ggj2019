using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    private static GameStateManager _instance;
    public static GameStateManager Instance
    {
        get { return _instance; }
    }

    [SerializeField]
    private string[] gameLevels;

    [SerializeField]
    private PlayerController playerControllerPrefab;

    [SerializeField]
    private string FirstScene;

    [SerializeField]
    private string FirstDoor;


    private bool sceneHasLoaded;

    private Level currentLevel;
    private PlayerController playerController;

    private void Awake()
    {
        if (_instance) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        _instance = this;

        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        sceneHasLoaded = true;
    }

    public void StartGame()
    {
        Debug.Log("Start Game");
        MoveToLevel(FirstScene, FirstDoor);
    }

    public void MoveToLevel(string levelName, string doorName)
    {
        StartCoroutine(MoveToLevelI(levelName, doorName));
    }

    private IEnumerator MoveToLevelI(string levelName, string doorName)
    {
        sceneHasLoaded = false;
        SceneManager.LoadScene(levelName);

        while(!sceneHasLoaded)
        {
            yield return 0;
        }

        currentLevel = FindObjectOfType<Level>();

        var spawnPoint = currentLevel.GetDoor(doorName)?.transform.position ?? Vector3.zero;

        playerController = Instantiate(playerControllerPrefab, spawnPoint, Quaternion.identity);
        playerController.Setup(currentLevel.Tilemap);
    }
}
