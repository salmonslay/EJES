using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private bool _isDebugManager = false;
    private bool _startedDebug = false;

    // Different ways to change level in the game
    public enum TransitionState
    {
        Lost,
        Won,
        New, // new game
    }

    public static GameManager Instance { get; private set; }

    public Minigame CurrentMinigame { get; set; }

    // The amount of seconds allowed per mini-game
    public float MinigameTime => 8.5f / TimeModifier;

    // The speed of the minigames. 2 is double speed, 0.5 is half speed
    public float TimeModifier { get; private set; } = 1f;

    // A list of all the minigame scene names, filled in the inspector
    [SerializeField] private string[] _minigameSceneNames;
    private int _currentMinigameIndex = 0;
    public bool GameRunning { get; set; }
    public int PassedLevels { get; private set; }
    public int FailedLevels { get; private set; }
    public int TotalLevels => PassedLevels + FailedLevels;
    private bool _isWaitingForRestart = false;

    public Canvas overlayCanvas;
    public Text canvasText;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject); // this is a singleton
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        Cursor.visible = false;

        overlayCanvas.gameObject.SetActive(!_isDebugManager);
    }

    private void Update()
    {
        if ((_isWaitingForRestart && Input.GetKeyDown(KeyCode.UpArrow)) || // restart the game by either pressing up arrow or R + shift
            (Input.GetKeyDown(KeyCode.R) && Input.GetKey(KeyCode.LeftShift)))
        {
            Instance = null;
            SceneManager.LoadScene("Main");
            Destroy(gameObject);

            Debug.Log("Resetting new game manager...");
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && !GameRunning && !_isDebugManager)
            StartGame();

        if (_isDebugManager && CurrentMinigame != null && !CurrentMinigame.IsRunning && !_startedDebug)
        {
            Debug.Log("start debug");
            _startedDebug = true;
            CurrentMinigame.StartMinigame();
        }
    }

    public void StartGame()
    {
        GameRunning = true;
        StartCoroutine(Transition(TransitionState.New));
    }

    public void FinishMinigame(bool won)
    {
        if (won)
            PassedLevels++;
        else
            FailedLevels++;

        _currentMinigameIndex++;
        if (_currentMinigameIndex >= _minigameSceneNames.Length)
            _currentMinigameIndex = 0; // roll over

        StartCoroutine(Transition(won ? TransitionState.Won : TransitionState.Lost));
    }

    /// <summary>
    /// Transitions one scene to another
    /// </summary>
    private IEnumerator Transition(TransitionState state)
    {
        if (_isDebugManager)
            yield break; // don't do anything if this is a debug manager

        string sceneName = _minigameSceneNames[_currentMinigameIndex];
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName); // load async the game doed not freeze
        asyncLoad.allowSceneActivation = false;

        Debug.Log($"started loading {sceneName}");

        if (state != TransitionState.New) // we came from a minigame
        {   
            CurrentMinigame.TargetMusicVolume = 0f; // mute the old game

            yield return new WaitForSecondsRealtime(0.5f);
            // play a sound or something
            // transition animation
        }

        if (FailedLevels < 3) // check if we're still alive (maybe change condition?)
        {
            canvasText.text = $"{3 - FailedLevels} lives left!\n{PassedLevels} levels passed.";
            overlayCanvas.gameObject.SetActive(true);
            while (!asyncLoad.isDone)
            {
                // Check if the load has finished
                if (asyncLoad.progress >= 0.9f)
                {
                    asyncLoad.allowSceneActivation = true;
                    // remove overlay/loading screen
                    yield return new WaitForSeconds(1f);
                    overlayCanvas.gameObject.SetActive(false);
                    CurrentMinigame.StartMinigame();
                }

                yield return null;
            }
        }
        else
        {
            // lose
            overlayCanvas.gameObject.SetActive(true);
            canvasText.text = $"You lost!\n{PassedLevels} levels passed.";
            _isWaitingForRestart = true;
        }
    }
}