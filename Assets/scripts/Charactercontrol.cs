using UnityEngine;
using System.Collections; // Adiciona essa diretiva para usar coroutines

[RequireComponent(typeof(CharacterController))]
public class TopDownMovement : MonoBehaviour
{
    public float moveSpeed = 6f; // Velocidade de movimentação
    public float rotateSpeed = 720f; // Velocidade de rotação (graus por segundo)
    public Transform cameraTransform; // Referência para a câmera
    public float gravity = -9.81f; // Força da gravidade
    public float groundCheckDistance = 0.1f; // Distância para checar o chão
    public float dashSpeed = 25f; // Velocidade do dash
    public float dashDuration = 0.5f; // Duração do dash
    public float maxShiftPressDuration = 0.5f; // Tempo máximo para ser considerado um dash
    public float stamina;

    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;
    private Vector3 velocity;
    private bool isGrounded;
    private bool isDashing = false;
    private float shiftPressTime;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Verifica se o personagem está no chão
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);

        if (isGrounded && velocity.y < 0)
            velocity.y = -2f; // Garante que o personagem fique no chão

        // Captura a entrada do jogador no eixo X (horizontal) e Z (vertical)
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Obtém a direção da câmera (sem a inclinação no eixo Y)
        Vector3 forward = cameraTransform.forward;
        forward.y = 0f;
        forward.Normalize();

        Vector3 right = cameraTransform.right;
        right.y = 0f;
        right.Normalize();

        // Combina as direções da câmera com as entradas do jogador
        moveDirection = (forward * vertical + right * horizontal).normalized;

        // Detecta o pressionamento do Shift
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            shiftPressTime = Time.time;
        }

        // Detecta se o Shift foi solto
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            float pressDuration = Time.time - shiftPressTime;

            if (pressDuration <= maxShiftPressDuration)
            {
                StartCoroutine(Dash());
            }
        }

        // Verifica se o jogador está pressionando Shift
        if (Input.GetKey(KeyCode.LeftShift) && !isDashing)
        {
            moveSpeed = 12f; // Aumenta a velocidade para corrida
        }
        else if (!isDashing)
        {
            moveSpeed = 6f; // Velocidade normal
        }

        // Movimenta o personagem
        if (moveDirection.magnitude >= 0.1f && !isDashing)
        {
            // Calcula o ângulo de rotação desejado
            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;

            // Rotaciona suavemente o personagem na direção do movimento
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotateSpeed, 0.1f);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            // Aplica a movimentação ao CharacterController
            Vector3 move = moveDirection * moveSpeed * Time.deltaTime;
            characterController.Move(move);
        }

        // Aplica a gravidade
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    private IEnumerator Dash()
    {
        isDashing = true;
        float startTime = Time.time;

        while (Time.time < startTime + dashDuration)
        {
            characterController.Move(moveDirection * dashSpeed * Time.deltaTime);
            yield return null;
        }

        isDashing = false;
    }
}
