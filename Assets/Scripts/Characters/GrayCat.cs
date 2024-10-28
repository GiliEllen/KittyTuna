using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using System.Threading.Tasks;

public class GrayCat : PlayableCharacter
{
    public Sprite[] MeowAnimationFrames; 
    private Coroutine MeowEffectCoroutine;
    private Coroutine MeowAnimationCoroutine;
    public GameObject MeowEffectPrefab;
    private AudioSource audioSource;


    private void Awake() {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
    }

    protected override void Start()
    {
        base.Start(); 
        catType = CatType.GrayCat;
        catName = "Meowy";
        audioSource = GetComponent<AudioSource>();
    }

    public override void SpecialAbility()
    {
        base.SpecialAbility();
        canMove = false;
        Debug.Log("grayCat uses special ability: Meow to put the dog to sleep.");
        PlayMeowAnimation();
        if (MeowEffectCoroutine != null) StopCoroutine(MeowEffectCoroutine);
        MeowEffectCoroutine = StartCoroutine(MeowEffect());
    }

    private async void PlayMeowAnimation()
    {   
        canMove = false;
        PlaySoundEffect();
        movement.x = 0;
        movement.y = 0;
        animator.SetBool("IsMeow", true);

        await Task.Delay(1500);
        animator.SetBool("IsMeow", false);

        canMove = true;
    }

    private System.Collections.IEnumerator MeowEffect()
    {
        GameObject effect = Instantiate(MeowEffectPrefab, transform.position + new Vector3(0, 1f, 0), Quaternion.identity);
        yield return new WaitForSeconds(2);

        Destroy(effect);
    }

    public async void PlaySoundEffect()
    {
        if (audioSource != null)
        {
            audioSource.Play();
            await Task.Delay(2000);
            audioSource.Stop();
        }
    }
}
