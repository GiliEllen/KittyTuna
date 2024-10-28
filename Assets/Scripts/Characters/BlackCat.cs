using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using System.Threading.Tasks;

public class BlackCat : PlayableCharacter
{
    public Sprite[] DrinkAnimationFrames; 
    private Coroutine DrinkEffectCoroutine;
    private Coroutine DrinkAnimationCoroutine;
    public GameObject DrinkEffectPrefab;

    private void Awake() {
        base.Awake();
    }

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
        PlayDrinkAnimation();
        if (DrinkEffectCoroutine != null) StopCoroutine(DrinkEffectCoroutine);
        DrinkEffectCoroutine = StartCoroutine(DrinkEffect());
    }

    private async void PlayDrinkAnimation()
    {
        Debug.Log("Play Drink animation");
        canMove = false;

        movement.x = 0;
        movement.y = 0;
        animator.SetBool("IsDrinking", true);

        await Task.Delay(1300);
        animator.SetBool("IsDrinking", false);

        canMove = true;
    }

    private IEnumerator DrinkEffect()
    {
        GameObject effect = Instantiate(DrinkEffectPrefab, transform.position + new Vector3(0, 1f, 0), Quaternion.identity);
        yield return new WaitForSeconds(2);

        Destroy(effect);
        isWalking = true;
    }
}
