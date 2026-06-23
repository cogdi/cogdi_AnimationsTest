using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    public static PlayerMotor Instance { get; private set; }
    public float PlayerSpeed { get => currentSpeed; }

    [SerializeField] private CharacterController controller;
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private float acceleration = 0.6f;
    private float currentSpeed = 0f;

    private float gravity = -9.8f;
    private Vector3 velocity;
    private bool isGrounded;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (IsMoving())
        {
            if (currentSpeed <= maxSpeed)
            {
                currentSpeed += acceleration * Time.deltaTime;
            }

            else
            {
                currentSpeed = maxSpeed;
            }
        }

        else
        {
            currentSpeed = 0f;
        }

        Move();
    }

    private void Move()
    {
        Vector2 inputVector = PlayerInput.Instance.GetMovementVectorNormalized();
        Vector3 moveDirection = new Vector3(inputVector.x, 0f, inputVector.y);

        controller.Move(transform.TransformDirection(moveDirection) * (currentSpeed * Time.deltaTime));
        
        velocity.y += gravity * Time.deltaTime;

        if (isGrounded && velocity.y < 0f)
        {
            velocity.y = -2f;
        }

        controller.Move(velocity * Time.deltaTime);

        isGrounded = controller.isGrounded;
    }

    private bool IsMoving()
    {
        return PlayerInput.Instance.GetMovementVectorNormalized().magnitude > 0f;
    }
}
