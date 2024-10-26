using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSpawner : MonoBehaviour
{
    public bool isContainingBird = false;
    public string assignedBirdColor; 
    public Bird birdPrefab;

    public void GenerateBird(Sprite[] birdSprites, GameManager gameManager, string birdColor) {
        if (birdPrefab == null) {
            Debug.LogError("Bird prefab is not assigned!");
            return;
        }

        if (isContainingBird) return;

        Debug.Log("Bird Color: " + birdColor);

        Bird spawnedBird = Instantiate(birdPrefab, transform.position, Quaternion.identity);
        spawnedBird.spritesArray = birdSprites;
        spawnedBird.gameManager = gameManager;
        spawnedBird.SetSprite();
        isContainingBird = true;
        Debug.Log(spawnedBird);
    }
}
