using UnityEngine;

public class SidePush : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("Layer de las paredes (no se empuja contra ellas).")]
    public LayerMask wallLayer;

    [Tooltip("Distancia lateral máxima a revisar.")]
    public float sideCheckDistance = 0.6f;

    [Tooltip("Distancia mínima que queremos mantener respecto al objeto.")]
    public float minSeparation = 0.5f;

    [Tooltip("Velocidad de corrección.")]
    public float pushSpeed = 3f;

    [Tooltip("Máximo desplazamiento por frame (para evitar empujones fuertes).")]
    public float maxPushPerFixed = 0.15f;

    [Tooltip("Altura desde la que se lanzan los raycasts laterales.")]
    public float rayHeight = 1f;

    private Rigidbody rb;
    public Transform orientation;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (orientation == null)
        {
            Debug.LogWarning("SidePush: no se asignó Orientation, se usará transform.");
            orientation = transform;
        }
    }

    private void FixedUpdate()
    {
        Vector3 rayOrigin = transform.position + Vector3.up * rayHeight;

        CheckSide(rayOrigin, orientation.right);   // Derecha
        CheckSide(rayOrigin, -orientation.right);  // Izquierda
    }

    private void CheckSide(Vector3 origin, Vector3 dir)
    {
        if (Physics.Raycast(origin, dir, out RaycastHit hit, sideCheckDistance))
        {
            // Ignoramos si es pared
            int hitLayer = hit.collider.gameObject.layer;
            bool hitIsWall = ((1 << hitLayer) & wallLayer) != 0;
            if (hitIsWall) return;

            float dist = hit.distance;
            float penetration = Mathf.Max(0f, minSeparation - dist);

            if (penetration > 0f)
            {
                float moveAmount = Mathf.Min(penetration * pushSpeed, maxPushPerFixed);

                Vector3 pushDir = new Vector3(hit.normal.x, 0f, hit.normal.z).normalized;
                Vector3 pushVector = pushDir * moveAmount;

                rb.MovePosition(rb.position + pushVector);

                // Debug para ver el empuje
                Debug.DrawRay(origin, dir * hit.distance, Color.red);
            }
        }
    }
}
