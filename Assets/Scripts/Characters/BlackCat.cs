using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class BlackCat : PlayableCharacter
{
    public Sprite[] DrinkAnimationFrames; 
    private Coroutine DrinkEffectCoroutine;
    private Coroutine DrinkAnimationCoroutine;
    public GameObject DrinkEffectPrefab;
    private bool canMove = true;

    protected override void Start()
    {
        base.Start(); 
        catType = CatType.BlackCat;
        catName = "Thirsty";
    }

    public override void SpecialAbility()
    {
        base.SpecialAbility();
        canMove = false;
        Debug.Log("blackCat uses special ability: drink water from floor.");
        // PlayDrinkAnimation();
        if (DrinkEffectCoroutine != null) StopCoroutine(DrinkEffectCoroutine);
        DrinkEffectCoroutine = StartCoroutine(DrinkEffect());
        if (DrinkAnimationCoroutine != null) StopCoroutine(DrinkAnimationCoroutine);
        DrinkAnimationCoroutine = StartCoroutine(PlayDrinkAnimation());
    }

    private IEnumerator PlayDrinkAnimation()
    {
        float animationDuration = 2f;
        float elapsedTime = 0f;
        int index = 0;

        while (elapsedTime < animationDuration)
        {
            spriteRenderer.sprite = DrinkAnimationFrames[index];
            index = (index + 1) % DrinkAnimationFrames.Length;

            yield return new WaitForSeconds(0.25f); 

            elapsedTime += 0.25f; 
        }

        spriteRenderer.sprite = DrinkAnimationFrames[0];
        canMove = true;
    }

    public override void OnMovement(InputValue value)
    {
        if (!canMove) return; 
        base.OnMovement(value); 
    }

    private IEnumerator DrinkEffect()
    {
        GameObject effect = Instantiate(DrinkEffectPrefab, transform.position + new Vector3(0, 1f, 0), Quaternion.identity);
        yield return new WaitForSeconds(2);

        Destroy(effect);
        isWalking = true;
    }
}
