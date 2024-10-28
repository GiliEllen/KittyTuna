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
    private bool canMove = true;
    private Animator animator;
    protected Vector2 movement;
    protected Rigidbody2D rb;

    private void Awake() {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    protected override void Start()
    {
        base.Start(); 
        catType = CatType.GrayCat;
        catName = "Meowy";
    }

    private void FixedUpdate()
    {
        if (movement != Vector2.zero)
        {
            rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        }
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

        movement.x = 0;
        movement.y = 0;
        animator.SetBool("IsMeow", true);

        await Task.Delay(1500);
        animator.SetBool("IsMeow", false);

        canMove = true;
    }

    public override void OnMovement(InputValue value)
    {
        if (!canMove) return; 
        if (!canMove || FindObjectOfType<GameManager>().IsGameOver()) return;
        movement = value.Get<Vector2>();
        if(movement.x != 0 || movement.y != 0) {
            animator.SetFloat("X", movement.x);
            animator.SetFloat("Y", movement.y);

            animator.SetBool("IsWalking", true);
        } else {
             animator.SetBool("IsWalking", false);
        }
    }

    private System.Collections.IEnumerator MeowEffect()
    {
        GameObject effect = Instantiate(MeowEffectPrefab, transform.position + new Vector3(0, 1f, 0), Quaternion.identity);
        yield return new WaitForSeconds(2);

        Destroy(effect);
    }

}
