using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool isGameOver = false;
    public int points = 0;

    public GameOverScreen GameOverScreen;
    public GameWinScreen GameWinScreen;
    public TextMeshProUGUI pointsText;
    public TextMeshProUGUI timeText;

    void Update()
    {
        float timeElapsed = Time.timeSinceLevelLoad;
        timeText.text = timeElapsed.ToString("F2");
    }

    public void AddPoint(int num) {
        points += num;
        Debug.Log(points);
        pointsText.text = points.ToString();
    }

    public void GameOver() {
        isGameOver = true;
        
        GameOverScreen.Setup(points);
    }
    public void GameWin() {
        isGameOver = true;
        float timeElapsed = Time.timeSinceLevelLoad;
        GameWinScreen.Setup(points, timeElapsed);
    }

    public bool IsGameOver() {
        return isGameOver;
    }

    public void RestartButton(){
        SceneManager.LoadScene("Game");
    }
    public void ExitButton(){
        SceneManager.LoadScene("MainMenu");
    }
}
