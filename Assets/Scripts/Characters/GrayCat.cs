using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class GrayCat : PlayableCharacter
{
    public Sprite[] MeowAnimationFrames; 
    private Coroutine MeowEffectCoroutine;
    private Coroutine MeowAnimationCoroutine;
    public GameObject MeowEffectPrefab;
    private bool canMove = true;

    protected override void Start()
    {
        base.Start(); 
        catType = CatType.GrayCat;
        catName = "Meowy";
    }
    public override void SpecialAbility()
    {
        base.SpecialAbility();
        canMove = false;
        Debug.Log("grayCat uses special ability: Meow to put the dog to sleep.");
        PlayMeowAnimation();
        if (MeowEffectCoroutine != null) StopCoroutine(MeowEffectCoroutine);
        MeowEffectCoroutine = StartCoroutine(MeowEffect());
        if (MeowAnimationCoroutine != null) StopCoroutine(MeowAnimationCoroutine);
        MeowAnimationCoroutine = StartCoroutine(PlayMeowAnimation());
    }

    private IEnumerator PlayMeowAnimation()
    {
        float animationDuration = 2f;
        float elapsedTime = 0f;
        int index = 0;

        while (elapsedTime < animationDuration)
        {
            spriteRenderer.sprite = MeowAnimationFrames[index];
            index = (index + 1) % MeowAnimationFrames.Length;

            yield return new WaitForSeconds(0.25f); 

            elapsedTime += 0.25f; 
        }

        spriteRenderer.sprite = MeowAnimationFrames[0];
        canMove = true;
    }

    public override void OnMovement(InputValue value)
    {
        if (!canMove) return; 

        base.OnMovement(value); 
    }

    private System.Collections.IEnumerator MeowEffect()
    {
        GameObject effect = Instantiate(MeowEffectPrefab, transform.position + new Vector3(0, 1f, 0), Quaternion.identity);
        yield return new WaitForSeconds(2);

        Destroy(effect);
        isWalking = true;
    }

}
