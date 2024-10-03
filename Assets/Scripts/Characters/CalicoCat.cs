using UnityEngine;
using System.Collections;

public class CalicoCat : PlayableCharacter
{
    private SpriteRenderer spriteRenderer;

    public Sprite[] walkAnimationSprites;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public override void SpecialAbility()
    {
        // TODO: ability to trip the toddler trap
        Debug.Log("calicoCat uses special ability: trip the toddler");
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
