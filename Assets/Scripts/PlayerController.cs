using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float movementSpeed = 5f;
    float currentSpeed;
    Rigidbody rb;
    Vector3 direction;
    [SerializeField] float jumpForce = 10f;
    public bool isGrounded = true;
    public bool Dash = true;
    public Transform cameraTransform;
    [SerializeField] float dashForce = 10f;
    [SerializeField] float groundCheckRadius = 0.3f;
    [SerializeField] LayerMask groundMask;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = movementSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        CheckGround();
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        direction = new Vector3(moveHorizontal, 0.0f, moveVertical);
        direction = transform.TransformDirection(direction);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            isGrounded = false;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && Dash == true && isGrounded == false)
        {

            Vector3 dashDirection = cameraTransform.forward.normalized;

            rb.velocity = Vector3.zero;
            rb.AddForce(dashDirection * dashForce, ForceMode.Impulse);
            Dash = false;

        }

        if (rb.velocity.y < -0.1f)
        {
            isGrounded = false;
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(transform.position + direction * currentSpeed * Time.deltaTime);
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = true;
            Dash = true;
        }
    }
    
    private void CheckGround()
    {
        isGrounded = Physics.CheckSphere(transform.position + Vector3.down * 1f, groundCheckRadius, groundMask);
        if (isGrounded) Dash = true;
    }
}
