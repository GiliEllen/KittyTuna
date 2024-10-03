using UnityEngine;
using System.Collections;

public class GrayCat : PlayableCharacter
{
    private SpriteRenderer spriteRenderer;

    public Sprite[] walkAnimationSprites;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public override void SpecialAbility()
    {
        // TODO: meow ability put the barking dog trap to sleep
        Debug.Log("grayCat uses special ability: Meow to put the dog to sleep.");
    }

    private IEnumerator PlayWalkAnimation()
    {
        for (int i = 0; i < walkAnimationSprites.Length; i++)
        {
            spriteRenderer.sprite = walkAnimationSprites[i];
            yield return new WaitForSeconds(0.1f);
        }
    }
}
