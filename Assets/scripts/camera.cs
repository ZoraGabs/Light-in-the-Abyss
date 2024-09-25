using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Referência para o personagem que a câmera seguirá
    public Vector3 offset; // Distância da câmera em relação ao personagem
    public float smoothSpeed = 0.125f; // Velocidade de suavização do movimento
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
        // Define a posição desejada da câmera baseada na posição do personagem e no offset
        Vector3 desiredPosition = target.position + offset;

        // Suaviza o movimento da câmera
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Atualiza a posição da câmera
        transform.position = smoothedPosition;

        // Opcional: mantenha a câmera voltada para o personagem
        // transform.LookAt(target);
    }
}

