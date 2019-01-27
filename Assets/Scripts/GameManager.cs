using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private PlayerController player;
    public PlayerController Player
    {
        get => player;
    }

    private CameraManager camera;
    public CameraManager Camera
    {
        get => camera;
    }

    private AstarPath aStarPath;
    public AstarPath AStarPath => aStarPath;

    private UIManager uiManager;
    public UIManager UIManager => uiManager;



    [SerializeField] private float volumeMaster = 100;// from 0 to 100
    public float VolumeMaster
    {
        get => volumeMaster;
        set => volumeMaster = value;
    }

    [SerializeField] private float volumeMusic = 100;// from 0 to 100
    public float VolumeMusic
    {
        get => volumeMusic;
        set => volumeMusic = value;
    }

    [SerializeField] private float volumeSound = 100;// from 0 to 100
    public float VolumeSound
    {
        get => volumeSound;
        set => volumeSound = value;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoadingScene;
    }


    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoadingScene;
    }
    
    //this function is activated every time a scene is loaded
    private void OnLevelFinishedLoadingScene(Scene scene, LoadSceneMode mode)
    {
        Setup();
        Debug.Log("Scene Loaded");
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
    }


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        Setup();
    }

    public void Setup()
    {
        player = GameObject.FindObjectOfType<PlayerController>();
        camera = GameObject.FindObjectOfType<CameraManager>();
        aStarPath = GameObject.FindObjectOfType<AstarPath>();
        uiManager = GameObject.FindObjectOfType<UIManager>();
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void GameOver()
    {
        player.MyAnimator.SetTrigger("Death");
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
    }
}
