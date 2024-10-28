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
        timeText.text = FormatTime(timeElapsed);
    }

    private string FormatTime(float totalSeconds)
    {
        int minutes = Mathf.FloorToInt(totalSeconds / 60);
        int seconds = Mathf.FloorToInt(totalSeconds % 60);
        int milliseconds = Mathf.FloorToInt((totalSeconds - Mathf.Floor(totalSeconds)) * 1000);

        return string.Format("{0:D2}:{1:D2}:{2:D3}", minutes, seconds, milliseconds);
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
