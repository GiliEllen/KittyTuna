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
    private bool canMove = true;
    private Animator animator;
    protected Vector2 movement;
    protected Rigidbody2D rb;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    protected override void Start()
    {
        base.Start(); 
        catType = CatType.CalicoCat;
        catName = "Trippy";
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

        await Task.Delay(1500);
        animator.SetBool("IsTripping", false);

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

    private IEnumerator TripEffect()
    {
        GameObject effect = Instantiate(TripEffectPrefab, transform.position + new Vector3(0, 1f, 0), Quaternion.identity);
        yield return new WaitForSeconds(2);

        Destroy(effect);
        isWalking = true;
    }
}


