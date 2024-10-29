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
    private AudioSource audioSource;


    private void Awake() {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start(); 
        catType = CatType.CalicoCat;
        catName = "Trippy";
        audioSource = GetComponent<AudioSource>();
    }

    public override void SpecialAbility()
    {
        base.SpecialAbility();
        isPlayingAnimation = true;
        canMove = false;
        Debug.Log("CalicoCat uses special ability: trip the toddler.");
        PlayTripAnimation();
        if (TripEffectCoroutine != null) StopCoroutine(TripEffectCoroutine);
        TripEffectCoroutine = StartCoroutine(TripEffect());
    }

    private async void PlayTripAnimation()
    {   
        canMove = false;
        PlaySoundEffect();
        movement.x = 0;
        movement.y = 0;
        animator.SetBool("IsTripping", true);

        await Task.Delay(1300);
        animator.SetBool("IsTripping", false);

        canMove = true;
        isPlayingAnimation = false;
    }

    private IEnumerator TripEffect()
    {
        GameObject effect = Instantiate(TripEffectPrefab, transform.position + new Vector3(0, 1f, 0), Quaternion.identity);
        yield return new WaitForSeconds(2);

        Destroy(effect);
        isWalking = true;
    }

    public async void PlaySoundEffect()
    {   
        if (audioSource != null)
        {   
            isPlayingAudio = true;
            audioSource.Play();
            await Task.Delay(2000);
            isPlayingAudio = false;
        }
    }
}


