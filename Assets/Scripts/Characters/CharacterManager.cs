using UnityEngine;
using Cinemachine;

public class CharacterManager : MonoBehaviour
{
    public PlayableCharacter[] characters; 
    private int currentCharacterIndex = 0;
    private PlayableCharacter currentCharacterInstance;
    private CinemachineVirtualCamera virtualCamera;

    private void Start()
    {
        currentCharacterIndex = Random.Range(0, characters.Length);
        Debug.Log($"Selected Character Index: {currentCharacterIndex}");
        virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
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
        Vector3 currentPosition = currentCharacterInstance.transform.position;
        if (currentCharacterInstance != null)
        {
            Destroy(currentCharacterInstance.gameObject);
        }

        currentCharacterIndex = (currentCharacterIndex + 1) % characters.Length;
        SpawnCharacter(currentCharacterIndex, currentPosition);
    }

    private void SpawnCharacter(int index, Vector3 spawnPosition)
    {
        Debug.Log($"Spawning Character: {characters[index].name}");
        currentCharacterInstance = Instantiate(characters[index], spawnPosition, Quaternion.identity);
        virtualCamera.Follow = currentCharacterInstance.transform;
    }
}
