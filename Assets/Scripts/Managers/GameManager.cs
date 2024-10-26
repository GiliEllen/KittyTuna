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

    public void AddPoint() {
        points += points;
    }

    public void GameOver() {
        isGameOver = true;
        GameOverScreen.Setup(points);
    }

    public bool IsGameOver() {
        return isGameOver;
    }
}
