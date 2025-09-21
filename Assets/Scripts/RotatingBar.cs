using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingBar : MonoBehaviour
{
    [SerializeField] private GameObject barAxis;
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private bool rotateClockwise = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float direction = rotateClockwise ? 1f : -1f;
        barAxis.transform.Rotate(Vector3.up * rotationSpeed * direction * Time.deltaTime);
    }
}
