using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class CalicoCat : PlayableCharacter
{
    public Sprite[] TripAnimationFrames; 
    private Coroutine TripEffectCoroutine;
    private Coroutine TripAnimationCoroutine;
    public GameObject TripEffectPrefab;
    private bool canMove = true;

    protected override void Start()
    {
        base.Start(); 
        catType = CatType.CalicoCat;
    }

    public override void SpecialAbility()
    {
        base.SpecialAbility();
        canMove = false;
        Debug.Log("CalicoCat uses special ability: trip the toddler.");
        if (TripEffectCoroutine != null) StopCoroutine(TripEffectCoroutine);
        TripEffectCoroutine = StartCoroutine(TripEffect());
        if (TripAnimationCoroutine != null) StopCoroutine(TripAnimationCoroutine);
        TripAnimationCoroutine = StartCoroutine(PlayTripAnimation());
    }

    private IEnumerator PlayTripAnimation()
    {
        float animationDuration = 2f;
        float elapsedTime = 0f;
        int index = 0;

        while (elapsedTime < animationDuration)
        {
            spriteRenderer.sprite = TripAnimationFrames[index];
            index = (index + 1) % TripAnimationFrames.Length;

            yield return new WaitForSeconds(0.25f); 

            elapsedTime += 0.25f; 
        }

        spriteRenderer.sprite = TripAnimationFrames[0];
        canMove = true;
    }

    public override void OnMovement(InputValue value)
    {
        if (!canMove) return; 
        base.OnMovement(value); 
    }

    private IEnumerator TripEffect()
    {
        GameObject effect = Instantiate(TripEffectPrefab, transform.position + new Vector3(0, 1f, 0), Quaternion.identity);
        yield return new WaitForSeconds(2);

        Destroy(effect);
        isWalking = true;
    }
}


