using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    public float moveSpeed = 5f; 
    public float runSpeed = 8f; 
    public float dashDistance = 5f; 
    public float dashSpeed = 20f; 
    public float stamina = 100f; 
    public float staminaDrainRate = 10f; 
    public float staminaRegenRate = 5f; 

    private Rigidbody rb; 
    private Vector3 movement;
    private bool isDashing = false; 
    private bool isRunning = false; 

    private Vector3 dashTargetPosition; 
    private float dashTime; 

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; 
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        movement = new Vector3(moveX, 0f, moveZ).normalized;

        if (movement != Vector3.zero && !isDashing)
        {
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            toRotation = Quaternion.Euler(0f, toRotation.eulerAngles.y, 0f);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 720 * Time.deltaTime); 
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && !isRunning && !isDashing)
        {
            StartDash();
        }

        if (Input.GetKey(KeyCode.LeftShift) && !isDashing && stamina > 0)
        {
            isRunning = true;
            movement *= runSpeed / moveSpeed; 
            DrainStamina();
        }
        else
        {
            isRunning = false;
            RegenerateStamina();
        }
    }

    void FixedUpdate()
    {
        if (!isDashing)
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
        else
        {
            ContinueDash();
        }
    }

    void StartDash()
    {
        isDashing = true;

        dashTargetPosition = rb.position + movement * dashDistance;

        dashTime = dashDistance / dashSpeed;
    }

    void ContinueDash()
    {
        if (dashTime > 0)
        {
            rb.MovePosition(Vector3.MoveTowards(rb.position, dashTargetPosition, dashSpeed * Time.fixedDeltaTime));
            dashTime -= Time.fixedDeltaTime;
        }
        else
        {
            EndDash();
        }
    }

    void EndDash()
    {
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
        if (!isRunning && stamina < 100f)
        {
            stamina += staminaRegenRate * Time.deltaTime;
            if (stamina > 100f) stamina = 100f;
        }
    }
}
