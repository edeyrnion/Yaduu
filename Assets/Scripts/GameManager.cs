using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[System.Serializable] public class EventGameState : UnityEvent<GameManager.GameState, GameManager.GameState> { }

public class GameManager : Singleton<GameManager>
{
    public enum GameState
    {
        PREGAME,
        RUNNING,
        PAUSED
    }

    public GameObject[] SystemPrefabs; // Prefabs the Gamemanager might need to keep trake off
    public EventGameState OnGameStateChanged;

    private List<GameObject> _instancedSystemPrefabs; // List of Prefabs after they have been created
    private List<AsyncOperation> _loadOperations; // List of Scenes beeing loaded
    GameState _currentGameState = GameState.PREGAME;

    private string _currentLevelName = string.Empty;

    public GameState CurrentGameState
    {
        get { return _currentGameState; }
        private set { _currentGameState = value; }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        _instancedSystemPrefabs = new List<GameObject>();
        _loadOperations = new List<AsyncOperation>();

        InstantiateSystemPrefabs();
    }

    private void Update()
    {
        if (_currentGameState == GameState.PREGAME)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    void OnLoadOperationComplete(AsyncOperation ao)
    {
        if (_loadOperations.Contains(ao))
        {
            _loadOperations.Remove(ao);

            if (_loadOperations.Count == 0)
            {
                UpdateState(GameState.RUNNING);
            }
        }

        Debug.Log("Load Complete.");
    }

    void OnUnloadOperationComplete(AsyncOperation ao)
    {
        Debug.Log("Unload Complete.");
    }

    void UpdateState(GameState state)
    {
        GameState previousGameState = _currentGameState;
        _currentGameState = state;

        Debug.Log(_currentGameState);

        switch (_currentGameState)
        {
            case GameState.PREGAME:
                Time.timeScale = 1.0f;
                break;
            case GameState.RUNNING:
                Time.timeScale = 1.0f;
                break;
            case GameState.PAUSED:
                Time.timeScale = 0.0f;
                break;

            default:
                break;
        }

        OnGameStateChanged.Invoke(_currentGameState, previousGameState);
    }

    void InstantiateSystemPrefabs()
    {
        GameObject prefabInstance;
        for (int i = 0; i < SystemPrefabs.Length; i++)
        {
            prefabInstance = Instantiate(SystemPrefabs[i]);
            _instancedSystemPrefabs.Add(prefabInstance);
        }
    }

    public void LoadLevel(string levelName)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
        if (ao == null)
        {
            Debug.LogError("[GameManager Unable to load level " + levelName);
            return;
        }
        ao.completed += OnLoadOperationComplete;
        _loadOperations.Add(ao);
        _currentLevelName = levelName;
    }

    public void UnLoadLevel(string levelName)
    {
        AsyncOperation ao = SceneManager.UnloadSceneAsync(levelName);
        if (ao == null)
        {
            Debug.LogError("[GameManager Unable to unlead level " + levelName);
            return;
        }
        ao.completed += OnUnloadOperationComplete;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        for (int i = 0; i < _instancedSystemPrefabs.Count; i++)
        {
            Destroy(_instancedSystemPrefabs[i]);
        }
        _instancedSystemPrefabs.Clear();
    }

    public void StartGame()
    {
        LoadLevel("Main");
    }

    public void TogglePause()
    {
        UpdateState(_currentGameState == GameState.RUNNING ? GameState.PAUSED : GameState.RUNNING);
    }
}
