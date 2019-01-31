using System;
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
    
    public AnimationCurve BounceCurve;
    public AnimationCurve WobbleCurve;
    public AnimationCurve OverCurve;

    public AnimationCurve GrainCurve;
    public AnimationCurve BloomCurve;

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

    [SerializeField]
    private GameObject[] effects;

    private bool sceneHasLoaded;

    private Room currentRoom;
    private GHOST currentGhost;
    private PlayerController playerController;
    private TutorialManager tutorialManager;

    private void Awake()
    {
        if (_instance) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        _instance = this;

        gameState = new GameState();
        GameUi.SetActive(false);

        

        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }

    private void Start()
    {
        AudioManager.Instance.PlayMusic();

        tutorialManager = GameUi.GetComponent<TutorialManager>();
        if (tutorialManager)
        {
            tutorialManager.LessonActivated.AddListener(OnLessonActivated);
            tutorialManager.LessonDismissed.AddListener(OnLessonDismissed);
        }
    }

    private void Update()
    {
#if UNITY_EDITOR
        DebugRun();
#endif
    }

    public Furniture GetFurniture(string name)
    {
        return FurniturePrefabs.FirstOrDefault(f => f.TypeName == name);
    }

    public GameObject GetEffect(string name)
    {
        return effects.FirstOrDefault(g => g.name.Equals(name, System.StringComparison.InvariantCultureIgnoreCase));
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
        SceneManager.LoadScene("Win");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void CheckWinConditions()
    {
        var scenes = SceneManager.sceneCountInBuildSettings - 2;
        var completedGameStates = gameState.Rooms.Where(r => r.Value.Completed).Count();

        if(completedGameStates > scenes-1)
        {
            if (currentRoom.IsFengShui)
            {
                EndGame();
            }
        }
    }

    public void MoveToLevel(string levelName, string doorName)
    {
        if(currentRoom != null)
        {
            currentRoom.OnZoneUpdate();
            var roomState = currentRoom.ToState();
            currentRoom.End();
            gameState.Rooms[currentRoom.SceneName] = roomState;
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
        currentGhost = FindObjectOfType<GHOST>();
        //Update UI
        roomText.text = currentRoom.SceneName;

        //Update State
        var roomState = gameState.Rooms.ContainsKey(currentRoom.SceneName) ? gameState.Rooms[currentRoom.SceneName] : null;
        
        var door = currentRoom.GetDoor(doorName);
        var spawnPoint = door?.transform.position ?? Vector3.zero;

        playerController = Instantiate(playerControllerPrefab, spawnPoint, Quaternion.identity);
        playerController.Setup(currentRoom.Tilemap, door.Rotation);

        currentRoom.Setup(playerController, roomState);
        if (roomState == null)
        {
            gameState.Rooms.Add(currentRoom.SceneName, new RoomState());
        }

        if(tutorialManager)
        {
            tutorialManager.LevelLoaded(currentRoom.SceneName);
        }
    }

    private void OnLessonActivated()
    {
        playerController.IsPaused = true;
        if (currentGhost)
            currentGhost.IsPaused = true;
    }

    private void OnLessonDismissed()
    {
        playerController.IsPaused = false;
        if (currentGhost)
            currentGhost.IsPaused = false;
    }

#if UNITY_EDITOR
    private void DebugRun()
    {
        if (Input.GetKeyDown(KeyCode.F12))
        {
            string path = System.IO.Path.Combine(Application.persistentDataPath, "Screenshot" + DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss") + ".png");
            Debug.Log(path);
            ScreenCapture.CaptureScreenshot(path);
        }
    }
#endif
}


