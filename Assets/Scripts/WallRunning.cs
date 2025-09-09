using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRunning : MonoBehaviour
{
    [Header("Wall Running")]
    public LayerMask wall;
    public LayerMask ground;
    public float wallRunForce;
    public float maxWallRunTime;
    

    [Header("Input")]
    private float horizontalInput;
    private float verticalInput;

    [Header("Detection")]
    public float wallCheckDistance;
    public float minJumpHeight;
    private RaycastHit leftWallHit;
    private RaycastHit rightWallHit;
    private bool wallLeft;
    private bool wallRight;
    private bool isWallRunning;

    [Header("References")]
    public Transform orientation;
   
    private Rigidbody rb;
    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        

    }

    // Update is called once per frame
   private void Update()
    {
        CheckForWall();
        StateMachine();

        if (isWallRunning)
        {
            WallRunningMovement();
        }
    }

    void FixedUpdate()
    {
    
    }

    private void CheckForWall()
    {
        wallLeft = Physics.Raycast(transform.position, -orientation.right, out leftWallHit, wallCheckDistance, wall);
        wallRight = Physics.Raycast(transform.position, orientation.right, out rightWallHit, wallCheckDistance, wall);
    }

    private bool AboveGround()
    {
        return !Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, minJumpHeight, ground);
    }

    private void StateMachine()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if ((wallLeft || wallRight) && AboveGround())
        {
            if (!isWallRunning) StartWallRun();
        }
        else
        {
            if (isWallRunning) StopWallRun();
        }
    }

    private void StartWallRun()
    {
        isWallRunning = true;
        rb.useGravity = false; 
    }

    private void WallRunningMovement()
    {
        if (!isWallRunning) return; // don’t run this if not in wallrun

        // cancel upward movement, but allow falling
        if (rb.velocity.y > 0)
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        Vector3 wallNormal = wallRight ? rightWallHit.normal : leftWallHit.normal;
        Vector3 wallForward = Vector3.Cross(wallNormal, Vector3.up);

        // Aseguramos que apunte hacia la dirección del jugador
        if (Vector3.Dot(wallForward, orientation.forward) < 0)
        {
            wallForward = -wallForward;
        }

        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(wallForward * wallRunForce, ForceMode.Force);
        }   

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StopWallRun();
        }
    }


    private void StopWallRun()
    {
        isWallRunning = false;
        rb.useGravity = true;
    }
}
