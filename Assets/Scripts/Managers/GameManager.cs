using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject welcomeScreen; 
    public GameObject hpPanel; 

    private void Start()
    {
        ShowWelcomeScreen();
    }

    private void ShowWelcomeScreen()
    {
        welcomeScreen.SetActive(true); 
        // hpPanel.SetActive(false); 
        Time.timeScale = 0; 
    }

    public void StartGame()
    {
        welcomeScreen.SetActive(false); 
        // hpPanel.SetActive(true); 
        Time.timeScale = 1; 
       
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Return))
        {
            StartGame();
        }
    }
}
