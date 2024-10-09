using System.Collections;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float runSpeed = 8f;
    [SerializeField] float dashDistance = 5f;
    [SerializeField] float dashSpeed = 20f;
    [SerializeField] float stamina = 100f;
    [SerializeField] float staminaDrainRate = 10f;
    [SerializeField] float staminaRegenRate = 5f;
    [SerializeField] float dashStaminaCost = 20f;
    [SerializeField] float dashHoldThreshold = 0.2f;

    private Rigidbody rb;
    private Vector3 movement;
    private bool isDashing = false;
    private bool isRunning = false;
    private Vector3 dashDirection;
    private float holdTime = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        movement = new Vector3(moveX, 0f, moveZ);
        movement.Normalize();

        if (movement != Vector3.zero && !isDashing)
        {
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            toRotation = Quaternion.Euler(0f, toRotation.eulerAngles.y, 0f);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 720 * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.LeftShift) && !isDashing && stamina > 0)
        {
            holdTime += Time.deltaTime;

            if (holdTime >= dashHoldThreshold)
            {
                isRunning = true;
            }
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            if (holdTime < dashHoldThreshold && !isDashing)
            {
                StartDash();
            }
            holdTime = 0f;
            isRunning = false;
        }

        if (isRunning && stamina > 0)
        {
            movement *= runSpeed / moveSpeed;
            DrainStamina();
        }
        else if (!Input.GetKey(KeyCode.LeftShift) || stamina <= 0)
        {
            RegenerateStamina();
        }
        
    }

    void FixedUpdate()
    {
        if (!isDashing)
        {
            Vector3 velocity = new Vector3(movement.x * moveSpeed, rb.velocity.y, movement.z * moveSpeed);
            rb.velocity = velocity;
        }
    }

    private IEnumerator CountHoldTime()
    {
        holdTime = 0f;
        while (Input.GetKey(KeyCode.LeftShift))
        {
            holdTime += Time.deltaTime;
            yield return null;
        }
    }

    void StartDash()
    {
        if (stamina >= dashStaminaCost)
        {
            isDashing = true;
            stamina -= dashStaminaCost;
            dashDirection = movement;
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        float dashTime = dashDistance / dashSpeed;
        float elapsedTime = 0f;

        while (elapsedTime < dashTime)
        {
            rb.velocity = dashDirection * dashSpeed;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isDashing = false;
    }

    void DrainStamina()
    {
        stamina -= staminaDrainRate * Time.deltaTime;
        if (stamina < 0)
        {
            stamina = 0;
            isRunning = false;
        }
    }

    void RegenerateStamina()
    {
        if (!isRunning && !isDashing && stamina < 100f)
        {
            stamina += staminaRegenRate * Time.deltaTime;
            if (stamina > 100f) stamina = 100f;
        }
    }
}
