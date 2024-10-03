using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public PlayableCharacter[] characters; 
    private int currentCharacterIndex = 0;
    private PlayableCharacter currentCharacterInstance;

    private void Start()
    {
        currentCharacterIndex = Random.Range(0, characters.Length);
         Debug.Log($"Selected Character Index: {currentCharacterIndex}");
        SpawnCharacter(currentCharacterIndex);
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
        if (currentCharacterInstance != null)
        {
            Destroy(currentCharacterInstance.gameObject);
        }

        currentCharacterIndex = (currentCharacterIndex + 1) % characters.Length;
        SpawnCharacter(currentCharacterIndex);
    }

    private void SpawnCharacter(int index)
    {
         Debug.Log($"Spawning Character: {characters[index].name}");
        currentCharacterInstance = Instantiate(characters[index], transform.position, Quaternion.identity);
    }
}
