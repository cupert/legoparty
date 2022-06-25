using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]
    private float movementSpeed;

    [SerializeField]
    private bool enableJump;

    [SerializeField]
    private float jumpForce;

    [SerializeField]
    private float jumpInputBufferTime;

    [Header("Constraints")]
    [SerializeField]
    private float deathPlaneY;

    [Header("Physics")]
    [SerializeField]
    private float gravityStrength;

    [SerializeField]
    private LayerMask groundLayer;

    [SerializeField]
    private bool enableKnockback;

    [SerializeField]
    private float knockbackForce;

    [SerializeField]
    private float knockbackInertia;

    [SerializeField]
    private Vector3 groundCheckOffset;

    [SerializeField]
    private float groundCheckRadius;

    [Header("Events")]
    [SerializeField]
    private GameStartedEvent gameStartedEvent;

    [SerializeField]
    private GameEndedEvent gameEndedEvent;

    [SerializeField]
    private PlayerDamagedEvent damagedEvent;

    [SerializeField]
    private PlayerEvent deathEvent;

    [SerializeField]
    private PlayerEvent startPressed;

    [Header("Effects")]
    [SerializeField] HitFlash hitFlash;

    [Header("Sounds")]
    [SerializeField]
    private float characterSoundVolumeMultiplier = 0.3f;

    [SerializeField]
    private AudioClip jumpSound;

    [SerializeField]
    private AudioClip loseSound;

    [SerializeField]
    private float bulletSoundVolumeMultiplier = 0.7f;

    [SerializeField]
    private AudioClip hitSound;

    private AudioSource playerAudioSource;

    private PlayerProfile profile;
    private GameObject model;
    private Animator animator;
    private HealthBar healthBar;

    private new Rigidbody rigidbody;

    private Vector3 movementInput;
    private bool jumpPressed;
    private float lastJumpInput;
    private static float rotationMovementSqrMagnitudeThreshold = 0.01f;

    private const float maxHealth = 100f;
    private float health;

    private bool onGround;
    private bool frozen;

    private Vector3 knockbackDirection;
    private float currentKnockbackForce;

    private bool IsAlive => health > 0f;

    private bool isTouchingSomething = false;

    public void Setup(PlayerProfile profile)
    {
        frozen = true;

        this.name = profile.name;
        this.profile = profile;

        if (model == null)
        {
            model = Instantiate(profile.ModelPrefab, transform);
        }
        else
        {
            Destroy(model);
            model = null;
        }

        health = maxHealth;

        gameStartedEvent.Add((args) => { frozen = false; });
        gameEndedEvent.Add((args) => { frozen = true; });
    }

    public float GetHealthFraction()
    {
        return health / 100;
    }

    public void Damage(float value, Vector3? knockback = null)
    {
        playerAudioSource.PlayOneShot(hitSound, bulletSoundVolumeMultiplier);

        if (frozen) return;

        if (knockback != null)
        {
            knockback = new Vector3(knockback.Value.x, 0, knockback.Value.z);
            knockback.Value.Normalize();
            knockbackDirection = knockbackDirection * currentKnockbackForce + knockback.Value * knockbackForce;
            knockbackDirection.Normalize();
            currentKnockbackForce = knockbackForce;
        }

        health -= value;
        hitFlash.SetBlinkTimer();

        animator.SetTrigger("takeDamage");

        healthBar.ResetLerpTimer();

        damagedEvent.Raise(new PlayerDamagedEventArgs() { Player = profile, NewValue = health });

        if (health <= 0)
        {
            health = 0;
            deathEvent.Raise(new PlayerEventArgs() { Player = profile });
            OnDeath();
        }
    }

    private void FallOff()
    {
        Damage(health);
        playerAudioSource.PlayOneShot(loseSound, characterSoundVolumeMultiplier);
        StartCoroutine(nameof(DelayDisable));
    }

    private void OnMove(InputValue inputValue)
    {
        Vector2 input = inputValue.Get<Vector2>();
        movementInput = new Vector3(input.x, 0, input.y);
        UpdateAnimator();
    }

    private void UpdateAnimator()
    {
        Vector3 localVelocity = transform.InverseTransformDirection(movementInput);
        float speed = localVelocity.z;
        animator.SetFloat("forwardSpeed", speed);
    }

    private void OnJump(InputValue inputValue)
    {
        jumpPressed = inputValue.Get<float>() > 0.5f;
        if (jumpPressed) lastJumpInput = Time.time;
    }

    private void OnStart(InputValue inputValue)
    {
        startPressed.Raise(new PlayerEventArgs() { Player = profile });
    }

    private void OnDeath()
    {
        playerAudioSource.PlayOneShot(loseSound, characterSoundVolumeMultiplier);
        StartCoroutine(nameof(DelayDestroy));
    }

    IEnumerator DelayDestroy()
    {
        //delay destruction of game object for audio source to play out
        yield return new WaitForSeconds(0.4f);
        Destroy(gameObject);
    }

    IEnumerator DelayDisable()
    {
        //delay disabling the game object for audio source to play out
        yield return new WaitForSeconds(0.4f);
        gameObject.SetActive(false);
    }

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        hitFlash = GetComponent<HitFlash>();
        playerAudioSource = GetComponent<AudioSource>();
        animator = GetComponentInChildren<Animator>();
        healthBar = GetComponentInChildren<HealthBar>();
    }

    private void FixedUpdate()
    {
        // Ground check
        onGround = Physics.CheckSphere(transform.position + groundCheckOffset, groundCheckRadius, groundLayer);

        Vector3 velocity = Vector3.zero;

        if (IsAlive && !frozen)
        {
            // Only update rotation if there's enough motion - prevents sudden rotation reset when using the keyboard
            if (movementInput.sqrMagnitude > rotationMovementSqrMagnitudeThreshold) transform.rotation = Quaternion.LookRotation(movementInput);

            // Movement
            if (!isTouchingSomething || onGround)
                velocity += movementInput * movementSpeed;

            // Jump
            if (enableJump)
            {
                if (onGround && (jumpPressed || Time.time - lastJumpInput <= jumpInputBufferTime))
                {
                    velocity += Vector3.up * jumpForce;
                    onGround = false;
                    playerAudioSource.PlayOneShot(jumpSound, 0.3f);
                }
            }

            // Apply gravity
            if (onGround)
            {
                velocity -= new Vector3(0, rigidbody.velocity.y, 0);
            }
            else
            {
                velocity += Vector3.down * gravityStrength;
            }

            // Apply knockback
            if (currentKnockbackForce < 0.1f) currentKnockbackForce = 0;
            if (currentKnockbackForce > 0)
            {
                velocity += knockbackDirection * currentKnockbackForce;
                currentKnockbackForce -= knockbackInertia;
            }


            // Check if the player fell off and kill them if needed
            if (transform.position.y < deathPlaneY)
            {
                FallOff();
            }

            // Set final velocity
            rigidbody.velocity = new Vector3(velocity.x, rigidbody.velocity.y + velocity.y, velocity.z);
        }
        else
        {
            rigidbody.velocity = Vector3.zero;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        isTouchingSomething = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        isTouchingSomething = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position + groundCheckOffset, groundCheckRadius);
    }
}
