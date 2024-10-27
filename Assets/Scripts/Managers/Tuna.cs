using UnityEngine;

public class Tuna : MonoBehaviour
{
    public GameManager gameManager;
    public void HandleCollected() {
        gameObject.SetActive(false);
        gameManager.AddPoint(10);
        gameManager.GameWin();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        HandleCollected();
    }
}
