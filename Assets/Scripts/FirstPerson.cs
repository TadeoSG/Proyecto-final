using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField][Range(0.5f, 10f)] 
    float mouseSense = 1f; 
    [SerializeField][Range(-80, -10)]
    int lookUp = -15;
    [SerializeField][Range(15, 80)]
    int lookDown = 80;

    float xRotation = 0f; // acumulador para pitch

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float rotateX = Input.GetAxis("Mouse X") * mouseSense;
        float rotateY = Input.GetAxis("Mouse Y") * mouseSense;

        // Rotar jugador en eje Y (yaw)
        player.transform.Rotate(Vector3.up * rotateX);

        // Acumular rotación vertical
        xRotation -= rotateY;
        xRotation = Mathf.Clamp(xRotation, lookUp, lookDown);

        // Aplicar rotación vertical solo a la cámara
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Toggle cursor
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = (Cursor.lockState == CursorLockMode.Locked) 
                ? CursorLockMode.None 
                : CursorLockMode.Locked;
        }
    }
}
