using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    public static PlayerMotor Instance { get; private set; }

    public float PlayerSpeed { get => currentSpeed; }
    [Header("Movement")]
    [SerializeField] private PlayerInput playerInputInstance;

    [SerializeField] private CharacterController controller;
    [SerializeField] private float walkingSpeed; // 2f.
    [SerializeField] private float runningSpeed; // 5f.
    [SerializeField] private float acceleration; // 1f.
    [SerializeField] private float deceleration; // 6f.
    private bool movementEnabled;

    private float desiredSpeed;
    private float currentSpeed = 0f;
    private float gravity = -9.8f;
    private Vector3 velocity;
    private bool isGrounded;

    public bool IsLookingAtObject { get => isLookingAtObject; }

    [Header("Interactables")]
    [SerializeField] private LayerMask interactableLayerMask;
    private float interactionDistance = 4f;
    private Vector3 cameraStartPoint;
    private Vector3 cameraForward;
    private bool isLookingAtObject;
    private IInteractable highlightedObject;

    private PlayerLook playerLookInstance;

    // [Header("Tools")]
    // [SerializeField] private Transform playerHands;


    private void Awake()
    {
        EnableMovement();

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        playerInputInstance.OnInteractPerformed += PlayerInput_OnInteractPerformed;
        playerLookInstance = PlayerLook.Instance;
    }

    private void PlayerInput_OnInteractPerformed()
    {
        if (PlayerHands.Instance.HandsOccupied)
            PlayerHands.Instance.DropItem();

        if (Physics.Raycast(cameraStartPoint, cameraForward, out RaycastHit hitInfo, interactionDistance, interactableLayerMask))
        {
            if (hitInfo.transform.TryGetComponent(out IInteractable obj))
            {
                obj.Interact();
            }
        }
    }

    private void Update()
    {
        SetCameraReferences();

        if (!PlayerHands.Instance.HandsOccupied)
        {
            HighlightInteractableObjects();
            DisplayActionMessages();
        }

        if (movementEnabled)
        {
            HandlePlayerSpeed();
            Move();
        }
    }

    private void HandlePlayerSpeed()
    {
        if (IsMoving())
        {
            desiredSpeed = playerInputInstance.IsRunKeyHolded() ? runningSpeed : walkingSpeed;

            if (currentSpeed <= desiredSpeed)
            {
                currentSpeed += acceleration * Time.deltaTime;
            }

            else
            {
                currentSpeed = desiredSpeed;
            }
        }

        else
        {
            if (currentSpeed > 0f)
                currentSpeed -= deceleration * Time.deltaTime;
            else
                currentSpeed = 0f;
        }
    }

    private void HighlightInteractableObjects()
    {
        if (Physics.Raycast(cameraStartPoint, cameraForward, out RaycastHit hitInfo, interactionDistance, interactableLayerMask))
        {
            if (highlightedObject == null)
            {
                isLookingAtObject = true;
                highlightedObject = hitInfo.transform.GetComponent<IInteractable>();

                highlightedObject.HandleHighlight();
            }
        }
        
        else if (highlightedObject != null)
        {
            isLookingAtObject = false;
            highlightedObject.HandleHighlight();
            highlightedObject = null;
        }
    }

    private void DisplayActionMessages()
    {
        if (highlightedObject != null && IsLookingAtObject)
        {
            if (highlightedObject.GetActionMessage() != null)
            {
                PlayerUI.Instance.DisplayActionMessage(highlightedObject.GetActionMessage());
            }
        }
        
        else PlayerUI.Instance.DiscardCurrentActionMessage();
    }


    private void SetCameraReferences()
    {
        cameraStartPoint = playerLookInstance.GetCurrentCameraPosition();
        cameraForward = playerLookInstance.GetCurrentCameraForward();
    }

    private void Move()
    {
        Vector2 inputVector = playerInputInstance.GetMovementVectorNormalized();
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

    public void DisableMovement()
    {
        controller.enabled = false;
        movementEnabled = false;
    }

    public void EnableMovement()
    {
        controller.enabled = true;
        movementEnabled = true;
    }

    public bool IsInteractableObjectLayer(int layer)
    {
        return interactableLayerMask == (interactableLayerMask | 1 << layer);
    }

    private bool IsMoving()
    {
        return playerInputInstance.GetMovementVectorNormalized().magnitude > 0f;
    }
}
