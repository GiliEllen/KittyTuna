using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdsManager : MonoBehaviour
{
    public BirdSpawner[] birdSpawnersArray;
    private int generatedBirdsNumber = 0;
    private string[] birdColors = { "Red", "Blue", "White", "Brown", "Black" };
    private List<string> spawnedBirds = new List<string>();
    public GameManager gameManager;
    public Dictionary<string, Sprite[]> birdSpritesDictionary = new Dictionary<string, Sprite[]>();
    public Sprite[] redBirdSprites;
    public Sprite[] blueBirdSprites;
    public Sprite[] whiteBirdSprites;
    public Sprite[] brownBirdSprites;
    public Sprite[] blackBirdSprites;

      void Awake()
        {
            birdSpritesDictionary["Red"] = redBirdSprites;
            birdSpritesDictionary["Blue"] = blueBirdSprites;
            birdSpritesDictionary["White"] = whiteBirdSprites;
            birdSpritesDictionary["Brown"] = brownBirdSprites;
            birdSpritesDictionary["Black"] = blackBirdSprites;
        }
    void Start()
    {
        ShuffleSpawners(birdSpawnersArray);
        for (int i = 0; i < birdColors.Length; i++)
        {
            if (i >= birdSpawnersArray.Length) break;
            if (i >= 2) break;

            birdSpawnersArray[i].assignedBirdColor = birdColors[i];
            birdSpawnersArray[i].GenerateBird(birdSpritesDictionary[birdColors[i]], gameManager, birdColors[i]);
            spawnedBirds.Add(birdColors[i]);
        }
    }

    void ShuffleSpawners(BirdSpawner[] spawners)
    {
        for (int i = 0; i < spawners.Length; i++)
        {
            int randIndex = Random.Range(i, spawners.Length);
            BirdSpawner temp = spawners[i];
            spawners[i] = spawners[randIndex];
            spawners[randIndex] = temp;
        }
    }
}
