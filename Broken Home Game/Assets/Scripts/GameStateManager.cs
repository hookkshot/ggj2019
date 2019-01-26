using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
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

    [SerializeField]
    public Furniture[] FurniturePrefabs;

    [SerializeField]
    private GameObject GameUi;

    [SerializeField]
    private TextMeshProUGUI roomText;


    private bool sceneHasLoaded;

    private Room currentRoom;
    private PlayerController playerController;

    private void Awake()
    {
        if (_instance) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        _instance = this;

        gameState = new GameState();

        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }

    public Furniture GetFurniture(string name)
    {
        return FurniturePrefabs.FirstOrDefault(f => f.TypeName == name);
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        sceneHasLoaded = true;
    }

    public void StartGame()
    {
        GameUi.SetActive(true);
        MoveToLevel(FirstScene, FirstDoor);
    }

    public void EndGame()
    {
        GameUi.SetActive(false);
        SceneManager.LoadScene("Menu");
    }

    public void MoveToLevel(string levelName, string doorName)
    {
        if(currentRoom != null)
        {
            var roomState = currentRoom.ToState();
            
            if(gameState.Rooms.ContainsKey(currentRoom.SceneName))
            {
                gameState.Rooms[currentRoom.SceneName] = roomState;
            }
            else
            {
                gameState.Rooms.Add(currentRoom.SceneName, roomState);
            }
        }

        StartCoroutine(MoveToLevelI(levelName, doorName));
    }

    GameState gameState;

    private IEnumerator MoveToLevelI(string levelName, string doorName)
    {
        sceneHasLoaded = false;
        SceneManager.LoadScene(levelName);

        while(!sceneHasLoaded)
        {
            yield return 0;
        }

        currentRoom = FindObjectOfType<Room>();
        //Update UI
        roomText.text = currentRoom.SceneName;

        //Update State
        var roomState = gameState.Rooms.ContainsKey(currentRoom.SceneName) ? gameState.Rooms[currentRoom.SceneName] : null;
        
        var door = currentRoom.GetDoor(doorName);
        var spawnPoint = door?.transform.position ?? Vector3.zero;

        playerController = Instantiate(playerControllerPrefab, spawnPoint, Quaternion.identity);
        playerController.Setup(currentRoom.Tilemap, door.Rotation);

        currentRoom.Setup(playerController, roomState);
    }
}


