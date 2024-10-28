using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using System.Threading.Tasks;

public class CalicoCat : PlayableCharacter
{
    public Sprite[] TripAnimationFrames; 
    private Coroutine TripEffectCoroutine;
    private Coroutine TripAnimationCoroutine;
    public GameObject TripEffectPrefab;

    private void Awake() {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start(); 
        catType = CatType.CalicoCat;
        catName = "Trippy";
    }

    public override void SpecialAbility()
    {
        base.SpecialAbility();
        canMove = false;
        Debug.Log("CalicoCat uses special ability: trip the toddler.");
        PlayTripAnimation();
        if (TripEffectCoroutine != null) StopCoroutine(TripEffectCoroutine);
        TripEffectCoroutine = StartCoroutine(TripEffect());
    }

    private async void PlayTripAnimation()
    {
        canMove = false;

        movement.x = 0;
        movement.y = 0;
        animator.SetBool("IsTripping", true);

        await Task.Delay(1300);
        animator.SetBool("IsTripping", false);

        canMove = true;
    }

    private IEnumerator TripEffect()
    {
        GameObject effect = Instantiate(TripEffectPrefab, transform.position + new Vector3(0, 1f, 0), Quaternion.identity);
        yield return new WaitForSeconds(2);

        Destroy(effect);
        isWalking = true;
    }
}


