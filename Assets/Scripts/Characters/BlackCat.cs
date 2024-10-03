using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class BlackCat : PlayableCharacter
{
    private SpriteRenderer spriteRenderer;

    public Sprite[] walkAnimationSprites;
    public Sprite[] jumpAnimationSprites;

    public float animationFrameDuration = 0.1f;

    private bool isJumping = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public override void SpecialAbility()
    {
        if (!isJumping)
        {
            StartCoroutine(PerformJump());
        }
    }

    private IEnumerator PerformJump()
    {
        isJumping = true;

        for (int i = 0; i < jumpAnimationSprites.Length; i++)
        {
            spriteRenderer.sprite = jumpAnimationSprites[i];
            yield return new WaitForSeconds(animationFrameDuration);
        }

        Debug.Log("BlackCat used its special ability: Jumped over a trap!");

        isJumping = false;
    }

    public override void OnMovement(InputValue value)
    {
        base.OnMovement(value);

        if (!isJumping && (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0))
        {
            StartCoroutine(PlayWalkAnimation());
        }
    }

    private IEnumerator PlayWalkAnimation()
    {
        for (int i = 0; i < walkAnimationSprites.Length; i++)
        {
            spriteRenderer.sprite = walkAnimationSprites[i];
            yield return new WaitForSeconds(animationFrameDuration);
        }
    }
}
