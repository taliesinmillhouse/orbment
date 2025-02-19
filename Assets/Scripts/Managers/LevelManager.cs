using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // Scene names.
    public const string m_strSplashScreenName = "Splash_Screen";
    public const string m_strMainMenuSceneName = "Main_Menu";
    public const string m_strTutorialSceneName = "Tutorial";
    public const string m_strLevelOneSceneName = "Level_1";
    public const string m_strLevelTwoSceneName = "Level_2";
    public const string m_strCreditsSceneName = "Credits";

    private bool m_bSceneLoadComplete = false;

    private string m_strCurrentSceneName;

    // Player level start positions.
    private Vector3 m_v3PlayerTutorialStartPosition = new Vector3(-3.33f, 0.0f, -34.77f);
    private Vector3 m_v3PlayerLevelOneStartPosition = new Vector3(-3.33f, 0.0f, -34.77f);
    private Vector3 m_v3PlayerLevelTwoStartPosition = new Vector3(-3.33f, 0.0f, -34.77f);
    // Cameras level start positions.
    private Vector3 m_v3PlayerCameraTutorialStartPosition = new Vector3(-3.33f, 13.7f, -38.26f);
    private Vector3 m_v3PlayerCameraLevelOneStartPosition = new Vector3(-3.33f, 13.7f, -38.26f);
    private Vector3 m_v3PlayerCameraLevelTwoStartPosition = new Vector3(-3.33f, 13.7f, -38.26f);

    private AsyncOperation m_loadNextLevelAsyncOperation;

    // Static reference to the LevelManager.
    public static LevelManager m_levelManager = null;

    // Variable getters and setters.
    public string CurrentSceneName { get { return m_strCurrentSceneName; } }

    // Player level start positions.
    public Vector3 PlayerTutorialStartPosition { get { return m_v3PlayerTutorialStartPosition; } }
    public Vector3 PlayerLevelOneStartPosition { get { return m_v3PlayerLevelOneStartPosition; } }
    public Vector3 PlayerLevelTwoStartPosition { get { return m_v3PlayerLevelTwoStartPosition; } }
    // Cameras level start positions.
    public Vector3 PlayerCameraTutorialStartPosition { get { return m_v3PlayerCameraTutorialStartPosition; } }
    public Vector3 PlayerCameraLevelOneStartPosition { get { return m_v3PlayerCameraLevelOneStartPosition; } }
    public Vector3 PlayerCameraLevelTwoStartPosition { get { return m_v3PlayerCameraLevelTwoStartPosition; } }

    public AsyncOperation LoadNextLevelAsyncOperation { get { return m_loadNextLevelAsyncOperation; } }

    private void Awake()
    {
        if (m_levelManager == null)
        {
            m_levelManager = this;
        }
        else if (m_levelManager != this)
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoadComplete;
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name != m_strMainMenuSceneName)
        {

        }
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoadComplete;
    }

    private void OnSceneLoadComplete(Scene a_scene, LoadSceneMode a_loadSceneMode)
    {
        // Deactivate cameras from previous scene.
        //!? Is required to stop black screen bug when loaded between scenes.
        if (SceneManager.GetActiveScene().name != m_strMainMenuSceneName)
        {
            if (PerkTreeCamera.m_perkTreeCamera != null)
            {
                PerkTreeCamera.m_perkTreeCamera.gameObject.SetActive(false);
            }

            if (IsoCam.m_playerCamera != null)
            {
                IsoCam.m_playerCamera.gameObject.SetActive(false);
            }

            if (Player.m_player != null)
            {
                Player.m_player.m_currHealth = Player.m_player.m_maxHealth;
                Player.m_player.ManaPool.m_currentMana = Player.m_player.ManaPool.m_maxMana;
            }
        }
        
        if (SerializationManager.m_serializationManager != null)
        {
            SerializationManager.m_serializationManager.Load();
        }

        switch (a_scene.name)
        {
            case m_strMainMenuSceneName:
                {
                    Time.timeScale = 1.0f;

                    m_strCurrentSceneName = m_strMainMenuSceneName;

                    DontDestroyOnLoad(gameObject);
                    DontDestroyOnLoad(AudioManager.m_audioManager.gameObject);

                    AudioManager.m_audioManager.PlaySceneMusic(m_strCurrentSceneName);
                    break;
                }

            case m_strTutorialSceneName:
                {
                    m_strCurrentSceneName = m_strTutorialSceneName;

                    // Initialise canvasses.
                    PauseMenuManager.m_pauseMenuManager.gameObject.SetActive(false);
                    DeathMenuManager.m_deathMenuManager.gameObject.SetActive(false);
                    PlayerHUDManager.m_playerHUDManager.gameObject.SetActive(true);

                    InGameCanvas.m_inGameCanvas.FadeIn = true;
                    InGameCanvas.m_inGameCanvas.FadeInComplete = false;
                    InGameCanvas.m_inGameCanvas.FadeOutComplete = false;

                    AudioManager.m_audioManager.PlaySceneMusic(m_strCurrentSceneName);
                    AudioManager.m_audioManager.FadeIn = true;

                    // Set player's new position.
                    Player.m_player.transform.position = m_v3PlayerTutorialStartPosition;
                    // Set player camera's new position.
                    IsoCam.m_playerCamera.transform.position = m_v3PlayerCameraTutorialStartPosition;

                    // Initialise cameras.
                    IsoCam.m_playerCamera.gameObject.SetActive(true);

                    // Initialise pooling.
                    EnemyLootManager.m_enemyLootManager.DisableOrbs();
                    break;
                }

            case m_strLevelOneSceneName:
                {
                    m_strCurrentSceneName = m_strLevelOneSceneName;

                    // Initialise canvasses.
                    PauseMenuManager.m_pauseMenuManager.gameObject.SetActive(false);
                    DeathMenuManager.m_deathMenuManager.gameObject.SetActive(false);
                    PlayerHUDManager.m_playerHUDManager.gameObject.SetActive(true);

                    InGameCanvas.m_inGameCanvas.FadeIn = true;
                    InGameCanvas.m_inGameCanvas.FadeInComplete = false;
                    InGameCanvas.m_inGameCanvas.FadeOutComplete = false;

                    AudioManager.m_audioManager.PlaySceneMusic(m_strCurrentSceneName);
                    AudioManager.m_audioManager.FadeIn = true;

                    // Set player's new position.
                    Player.m_player.transform.position = m_v3PlayerLevelOneStartPosition;
                    // Set player camera's new position.
                    IsoCam.m_playerCamera.transform.position = m_v3PlayerCameraLevelOneStartPosition;

                    // Initialise cameras.
                    IsoCam.m_playerCamera.gameObject.SetActive(true);

                    // Initialise pooling.
                    EnemyLootManager.m_enemyLootManager.DisableOrbs();
                    break;
                }

            case m_strLevelTwoSceneName:
                {
                    m_strCurrentSceneName = m_strLevelTwoSceneName;

                    // Initialise canvasses.
                    PauseMenuManager.m_pauseMenuManager.gameObject.SetActive(false);
                    DeathMenuManager.m_deathMenuManager.gameObject.SetActive(false);
                    PlayerHUDManager.m_playerHUDManager.gameObject.SetActive(true);

                    InGameCanvas.m_inGameCanvas.FadeIn = true;
                    InGameCanvas.m_inGameCanvas.FadeInComplete = false;
                    InGameCanvas.m_inGameCanvas.FadeOutComplete = false;

                    AudioManager.m_audioManager.PlaySceneMusic(m_strCurrentSceneName);
                    AudioManager.m_audioManager.FadeIn = true;

                    // Set player's new position.
                    Player.m_player.transform.position = m_v3PlayerLevelTwoStartPosition;
                    // Set player camera's new position.
                    IsoCam.m_playerCamera.transform.position = m_v3PlayerCameraLevelTwoStartPosition;

                    // Initialise cameras.
                    IsoCam.m_playerCamera.gameObject.SetActive(true);

                    // Initialise pooling.
                    EnemyLootManager.m_enemyLootManager.DisableOrbs();
                    break;
                }

            case m_strCreditsSceneName:
                {
                    m_strCurrentSceneName = m_strCreditsSceneName;

                    AudioManager.m_audioManager.FadeIn = false;
                    AudioManager.m_audioManager.FadeComplete = false;
                    break;
                }

            default:
                {
                    Debug.Log("Scene name: " + a_scene.name + " not recognised.");
                    break;
                }
        }

        m_bSceneLoadComplete = true;
    }

    private void Update()
    {

//#if UNITY_EDITOR
        if (DebugLevelSwitcher.m_bCheatsEnabled)
        {
            // Debug shortcut for Level pass
            if (Input.GetKeyDown(KeyCode.P))
            {
                m_loadNextLevelAsyncOperation.allowSceneActivation = true;

                if (SceneManager.GetActiveScene().name != m_strLevelTwoSceneName || SceneManager.GetActiveScene().name != m_strMainMenuSceneName)
                {
                    InitialiseDontDestroyOnLoad();
                }
                else if (SceneManager.GetActiveScene().name == m_strLevelTwoSceneName)
                {
                    DestroyAllDontDestroyOnLoad();
                }
            }
        }
//#endif

        if (m_bSceneLoadComplete)
        {
            StartCoroutine(LoadNextLevelAsync(false));
            m_bSceneLoadComplete = false;
        }
    }

    public void InitialiseDontDestroyOnLoad()
    {
        Player.m_player.transform.position = m_v3PlayerLevelTwoStartPosition;
        IsoCam.m_playerCamera.transform.position = m_v3PlayerCameraLevelTwoStartPosition;

        // Debuggers.
        DontDestroyOnLoad(DebugLevelSwitcher.m_debugLevelSwitcher.transform.parent);

        // Managers.
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(GameManager.m_gameManager.gameObject);
        DontDestroyOnLoad(AudioManager.m_audioManager.gameObject);

        // Actors.
        DontDestroyOnLoad(Player.m_player.gameObject);

        // Canvasses.
        DontDestroyOnLoad(PlayerHUDManager.m_playerHUDManager.transform.parent);
        DontDestroyOnLoad(PerkTreeManager.m_perkTreeManager.gameObject);
        DontDestroyOnLoad(PauseMenuManager.m_pauseMenuManager.transform.parent); // This also handles the DeathMenuManager.

        // Cameras.
        DontDestroyOnLoad(IsoCam.m_playerCamera.gameObject);
        DontDestroyOnLoad(PerkTreeCamera.m_perkTreeCamera.gameObject);
    }

    public void DestroyAllDontDestroyOnLoad()
    {
        if (DebugLevelSwitcher.m_debugLevelSwitcher != null)
        {
            Destroy(DebugLevelSwitcher.m_debugLevelSwitcher.transform.parent.gameObject);
        }

        if (GameManager.m_gameManager != null)
        {
            Destroy(GameManager.m_gameManager.gameObject);
        }

        if (AudioManager.m_audioManager != null && LevelManager.m_levelManager.m_strCurrentSceneName != LevelManager.m_strLevelTwoSceneName)
        {
            Destroy(AudioManager.m_audioManager.gameObject);
        }

        if (Player.m_player != null)
        {
            Destroy(Player.m_player.gameObject);
        }

        if (PlayerHUDManager.m_playerHUDManager != null)
        {
            Destroy(PlayerHUDManager.m_playerHUDManager.transform.parent.gameObject);
        }

        if (PerkTreeManager.m_perkTreeManager != null)
        {
            Destroy(PerkTreeManager.m_perkTreeManager.gameObject);
        }

        if (PauseMenuManager.m_pauseMenuManager != null)
        {
            Destroy(PauseMenuManager.m_pauseMenuManager.transform.parent.gameObject); // This also handles the DeathMenuManager.
        }

        if (IsoCam.m_playerCamera != null)
        {
            Destroy(IsoCam.m_playerCamera.gameObject);
        }

        if (PerkTreeCamera.m_perkTreeCamera != null)
        {
            Destroy(PerkTreeCamera.m_perkTreeCamera.gameObject);
        }
    }

    public IEnumerator LoadNextLevelAsync(bool a_bActivateScene)
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case m_strSplashScreenName:
                {
                    m_loadNextLevelAsyncOperation = SceneManager.LoadSceneAsync(m_strMainMenuSceneName);
                    break;
                }

            case m_strMainMenuSceneName:
                {
                    m_loadNextLevelAsyncOperation = SceneManager.LoadSceneAsync(m_strTutorialSceneName);
                    break;
                }

            case m_strTutorialSceneName:
                {
                    m_loadNextLevelAsyncOperation = SceneManager.LoadSceneAsync(m_strLevelOneSceneName);
                    break;
                }

            case m_strLevelOneSceneName:
                {
                    m_loadNextLevelAsyncOperation = SceneManager.LoadSceneAsync(m_strLevelTwoSceneName);
                    break;
                }

            case m_strLevelTwoSceneName:
                {
                    m_loadNextLevelAsyncOperation = SceneManager.LoadSceneAsync(m_strCreditsSceneName);
                    break;
                }

            case m_strCreditsSceneName:
                {
                    m_loadNextLevelAsyncOperation = SceneManager.LoadSceneAsync(m_strMainMenuSceneName);
                    break;
                }

            default:
                {
                    Debug.Log("Scene name: " + SceneManager.GetActiveScene().name + " not recognised.");
                    break;
                }
        }

        m_loadNextLevelAsyncOperation.allowSceneActivation = a_bActivateScene;

        if (!a_bActivateScene)
        {
            yield return null;
        }
        else
        {
            yield return m_loadNextLevelAsyncOperation;
        }
    }
}
