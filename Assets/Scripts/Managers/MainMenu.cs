using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public HowToPlayScreen HowToPlayScreen;
    public void ExitButton () {
        Application.Quit();
        Debug.Log("Game closed");
    }
    public void HowToPlayButton () {
        HowToPlayScreen.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    public void StartGame() {
        SceneManager.LoadScene("Game");
    }
}
