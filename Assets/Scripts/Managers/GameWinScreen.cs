using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameWinScreen : MonoBehaviour
{
    public TextMeshProUGUI pointsText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI timeElapsedBonusPoints;
    private float timerPoints = 220;
    public void Setup(int score, float timeElapsed) {
        gameObject.SetActive(true);
        timerText.text = "Time elapsed: " + timeElapsed.ToString();
        float Bonus = timerPoints - timeElapsed;
        float roundedBonus = Mathf.Floor(Bonus);
        if (Bonus <= 0) {
            Bonus = 0;
        }
        timeElapsedBonusPoints.text = "Time Bonus: " + Bonus.ToString();
        float newScore = score + Bonus;
        pointsText.text = newScore.ToString() + " POINTS";
    }
}
