using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class BlackCat : PlayableCharacter
{
    private bool isJumping = false;

    protected override void Start()
    {
        base.Start(); 
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
    }
}
