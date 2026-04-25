using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Move : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 6;
    public float sprintSpeed = 10;

    [Header("Stamina")] 
    public float maxStamina = 5;
    public float staminaDrain = 1;
    public float staminaRegen = 0.8f;
    public float staminaRegenDelay = 1.5f;
    
    private float currentStamina;
    private float regenTimer;
    private bool isSprinting;
    
    [Header("Jump")]
    public float gravity = -9.81f;
    public float jumpHeight = 1.5f;
    
    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    
    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            //keep grounded
            velocity.y = -2f; 
        }
        
        //movement
        var x = Input.GetAxisRaw("Horizontal");
        var z = Input.GetAxisRaw("Vertical");

        var movingForward = z > 0;
        var sprintKey = Input.GetKey(KeyCode.LeftShift);
        
        //sprint logic
        if (sprintKey && movingForward && currentStamina > 0)
        {
            isSprinting = true;
            currentStamina -= staminaDrain * Time.deltaTime;
            regenTimer = 0;
        }
        else
        {
            isSprinting = false;
            regenTimer += Time.deltaTime;
            if (regenTimer >= staminaRegenDelay)
            {
                currentStamina += staminaRegen * Time.deltaTime;
            }
        }
        
        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
        
        var speed = isSprinting ? sprintSpeed : walkSpeed;
        
        var move = transform.right * x + transform.forward * z;
        move.Normalize();
        controller.Move(move * speed * Time.deltaTime);
        
        //jump
        if (Input.GetButton("Jump") && isGrounded)
        {
            //v  = sqrt(-2gh)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        
        //gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
