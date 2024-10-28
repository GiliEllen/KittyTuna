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
    private bool canMove = true;
    private Animator animator;
    protected Vector2 movement;
    protected Rigidbody2D rb;
     private PlayerInput playerInput;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    protected override void Start()
    {
        base.Start(); 
        catType = CatType.BlackCat;
        catName = "Thirsty";
        playerInput = GetComponent<PlayerInput>();
    }

     private IEnumerator BlockInputCoroutine(float seconds)
    {
        playerInput.enabled = false; // Disables the PlayerInput component
        yield return new WaitForSeconds(seconds);
        playerInput.enabled = true; // Re-enables the PlayerInput component
    }

        public void BlockInputForSeconds(float seconds)
    {
        StartCoroutine(BlockInputCoroutine(seconds));
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
        Debug.Log("blackCat uses special ability: drink water from floor.");
        PlayDrinkAnimation();
        if (DrinkEffectCoroutine != null) StopCoroutine(DrinkEffectCoroutine);
        DrinkEffectCoroutine = StartCoroutine(DrinkEffect());
        // if (DrinkAnimationCoroutine != null) StopCoroutine(DrinkAnimationCoroutine);
        // DrinkAnimationCoroutine = StartCoroutine(PlayDrinkAnimation());
    }

    private async void PlayDrinkAnimation()
    {
        BlockInputForSeconds(1.5f);
        Debug.Log("Play Drink animation");
        canMove = false;

        movement.x = 0;
        movement.y = 0;
        animator.SetBool("IsDrinking", true);

        await Task.Delay(1500);
        animator.SetBool("IsDrinking", false);

        canMove = true;
    }

    public override void OnMovement(InputValue value)
    {
        if (!canMove) return; 
        // if (!isWalking || FindObjectOfType<GameManager>().IsGameOver()) return;
        movement = value.Get<Vector2>();
        if(movement.x != 0 || movement.y != 0) {
            animator.SetFloat("X", movement.x);
            animator.SetFloat("Y", movement.y);

            animator.SetBool("IsWalking", true);
        } else {
             animator.SetBool("IsWalking", false);
        }
        // base.OnMovement(value); 
    }

    private IEnumerator DrinkEffect()
    {
        GameObject effect = Instantiate(DrinkEffectPrefab, transform.position + new Vector3(0, 1f, 0), Quaternion.identity);
        yield return new WaitForSeconds(2);

        Destroy(effect);
        isWalking = true;
    }
}
