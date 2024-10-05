using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSpawner : MonoBehaviour
{
    public GameObject[] traps; 
    public float spawnDelay = 0f; 

    private void Start()
    {
        StartCoroutine(SpawnRandomTrap());
    }

    private IEnumerator SpawnRandomTrap()
    {
        yield return new WaitForSeconds(spawnDelay);
        
        int randomIndex = Random.Range(0, traps.Length);
        GameObject trapToSpawn = traps[randomIndex];

        Instantiate(trapToSpawn, transform.position, Quaternion.identity);
    }
}
