using UnityEngine;
using Cinemachine;
using System.Collections.Generic;

public class CharacterManager : MonoBehaviour
{
    public PlayableCharacter[] characters; 
    private int currentCharacterIndex = 0;
    private PlayableCharacter currentCharacterInstance;
    private CinemachineVirtualCamera virtualCamera;
    private Dictionary<string, int> catHPs = new Dictionary<string, int>();


    private void Start()
    {
        currentCharacterIndex = Random.Range(0, characters.Length);

        virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();

        foreach (var cat in characters)
        {
            catHPs[cat.name] = 3; 
        }

        SpawnCharacter(currentCharacterIndex, transform.position);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            SwitchCharacter();
        }
    }

    private void SwitchCharacter()
    {
        if (currentCharacterInstance.isPlayingAnimation || currentCharacterInstance.isPlayingAudio) {
            return;
        }

        Vector3 currentPosition = currentCharacterInstance.transform.position;
        if (currentCharacterInstance != null)
        {
            catHPs[currentCharacterInstance.name] = currentCharacterInstance.CurrentHp;
            Destroy(currentCharacterInstance.gameObject);
        }

        currentCharacterIndex = (currentCharacterIndex + 1) % characters.Length;
        SpawnCharacter(currentCharacterIndex, currentPosition);
    }

   private void SpawnCharacter(int index, Vector3 spawnPosition)
    {
        currentCharacterInstance = Instantiate(characters[index], spawnPosition, Quaternion.identity);

        if (catHPs.TryGetValue(currentCharacterInstance.name, out int storedHP))
        {
            currentCharacterInstance.SetHP(storedHP);
        }
        else
        {
            currentCharacterInstance.SetHP(currentCharacterInstance.maxHP);
        }

        virtualCamera.Follow = currentCharacterInstance.transform;
    }



}
