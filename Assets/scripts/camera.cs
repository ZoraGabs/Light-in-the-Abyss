using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Refer�ncia para o personagem que a c�mera seguir�
    public Vector3 offset; // Dist�ncia da c�mera em rela��o ao personagem
    public float smoothSpeed = 0.125f; // Velocidade de suaviza��o do movimento
    public int width = 1920;  // Desired width of the game window
    public int height = 1080; // Desired height of the game window
    public bool isFullscreen = false; // Fullscreen mode
    void Start()
    {
        // Change the resolution when the game starts
        Screen.SetResolution(width, height, isFullscreen);
    }
    public void ChangeResolution(int newWidth, int newHeight, bool fullscreen)
    {
        Screen.SetResolution(newWidth, newHeight, fullscreen);
    }
    void LateUpdate()
    {
        // Define a posi��o desejada da c�mera baseada na posi��o do personagem e no offset
        Vector3 desiredPosition = target.position + offset;

        // Suaviza o movimento da c�mera
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Atualiza a posi��o da c�mera
        transform.position = smoothedPosition;

        // Opcional: mantenha a c�mera voltada para o personagem
        // transform.LookAt(target);
    }
}

