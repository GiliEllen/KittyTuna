using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool isGameOver = false;

    public GameOverScreen GameOverScreen;

    public void GameOver() {
        isGameOver = true;
        GameOverScreen.Setup(1);
    }

    public bool IsGameOver() {
        return isGameOver;
    }
}
