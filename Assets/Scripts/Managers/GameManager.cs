using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameObject welcomeScreen; 
    public GameObject gameOverPanel; 
    private bool isGameOver = false;

    // private void Awake()
    // {
    //     GameManager gameManager = GameManager.Instance;
    //     if (Instance == null)
    //     {
    //         Instance = this;
    //         DontDestroyOnLoad(gameObject); 
    //     }
    // }

    // private void Start()
    // {
    //     ShowWelcomeScreen();
    //     HideGameOverScreen();
    // }

    // private void OnEnable()
    // {
    //     SceneManager.sceneLoaded += OnSceneLoaded; 
    // }

    // private void OnDisable()
    // {
    //     SceneManager.sceneLoaded -= OnSceneLoaded;
    // }

    // private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    // {
    //     ManageGameObjectsForScene(scene.name);
    // }

    // private void ManageGameObjectsForScene(string sceneName)
    // {
    //     if (sceneName == "GameManagerScene")
    //     {
    //         HideGameOverScreen();
    //         ShowWelcomeScreen(); 
    //     }
    //     else if (sceneName == "SampleScene")
    //     {
    //         HideGameOverScreen(); 
    //     }
    // }

    // private void ShowWelcomeScreen()
    // {
    //     if (welcomeScreen != null)
    //     {
    //         welcomeScreen.SetActive(true); 
    //         Time.timeScale = 0; 
    //     }
    // }
    // private void ShowGameOverScreen()
    // {
    //     if (gameOverPanel != null)
    //     {
    //         gameOverPanel.SetActive(true);
    //     }
    // }
    // private void HideGameOverScreen()
    // {
    //     if (gameOverPanel != null)
    //     {
    //         gameOverPanel.SetActive(false);
    //     }
    // }

    // public void StartGame()
    // {
    //     if (welcomeScreen != null)
    //     {
    //         welcomeScreen.SetActive(false); 
    //     }
    //     Time.timeScale = 1;
    //     SceneManager.LoadScene("SampleScene");   
    // }

    // private void Update()
    // {
    //     if (!isGameOver && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Return)))
    //     {
    //         StartGame();
    //     }
    // }

    // public bool IsGameOver()
    // {
    //     return isGameOver;
    // }

    // public void GameOver(string catName)
    // {
    //     SceneManager.LoadScene("GameManagerScene");
    //     isGameOver = true;
    //     Debug.Log(welcomeScreen);
    //     if (welcomeScreen != null) {
    //         welcomeScreen.SetActive(false);
    //     }
    //      if (gameOverPanel != null)
    //     {
    //         gameOverPanel.SetActive(true);
    //     }
    //     // gameOverText.text = $"Game Over! The {catName} fell asleep.";
    //     Time.timeScale = 0; 
    // }

    // public void PlayAgain()
    // {
    //     isGameOver = false;
    //     Debug.Log("Play Again button clicked.");
    //     Time.timeScale = 1;
    //     SceneManager.LoadScene("SampleScene"); 
    // }
}
