using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    public TextMeshProUGUI pointsText;
    public void Setup(int score) {
        gameObject.SetActive(true);
        pointsText.text = score.ToString() + " POINTS";
    }

    public void RestartButton(){
        SceneManager.LoadScene("Game");
    }
    public void ExitButton(){
        SceneManager.LoadScene("MainMenu");
    }
}
