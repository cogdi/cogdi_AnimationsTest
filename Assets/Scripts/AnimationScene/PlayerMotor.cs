using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    public static PlayerMotor Instance { get; private set; }
    public event Action<InteractableObject> OnItemPickedUp;
    public event Action<InteractableObject> OnItemDropped;


    [Header("Movement")]
    public float PlayerSpeed { get => currentSpeed; }
    [SerializeField] private PlayerInput playerInputInstance;

    [SerializeField] private CharacterController controller;
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private float acceleration = 0.6f;
    private float currentSpeed = 0f;
    private float gravity = -9.8f;
    private Vector3 velocity;
    private bool isGrounded;


    [Header("Interactables")]
    [SerializeField] private LayerMask interactableLayerMask;
    private float firstPersonInteractionDistance = 2f;
    private float thirdPersonInteractionDistance = 4f;
    private InteractableObject holdedItem;
    private Vector3 cameraStartPoint;
    private Vector3 cameraForward;
    private InteractableObjectVisual pickableObjectVisual;    

    private PlayerLook playerLookInstance;

    private void Awake()
    {
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
        if (!holdedItem)
        {
            PickUpItem();
        }

        else
        {
            DropItem();
        }
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

        CheckInteractableObjects();
        Move();
    }

    private void CheckInteractableObjects()
    {
        cameraStartPoint = playerLookInstance.GetCurrentCameraPosition();
        cameraForward = playerLookInstance.GetCurrentCameraForward();

        if (holdedItem)
            return;

        if (Physics.Raycast(cameraStartPoint, cameraForward, out RaycastHit hitInfo, thirdPersonInteractionDistance, interactableLayerMask))
        {
            pickableObjectVisual = hitInfo.transform.GetComponent<InteractableObjectVisual>();
            pickableObjectVisual.HighlightPickableObject();
            Debug.Log("Item highlited");
        }

        else if (pickableObjectVisual)
        {
            pickableObjectVisual.RemoveHighlight();
            pickableObjectVisual = null;
        }
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

    public bool IsInteractableObjectLayer(int layer)
    {
        return interactableLayerMask == (interactableLayerMask | 1 << layer);
    }

    private void PickUpItem()
    {
        if (Physics.Raycast(cameraStartPoint, cameraForward, out RaycastHit hitInfo, thirdPersonInteractionDistance, interactableLayerMask))
        {
            if (hitInfo.transform.gameObject.TryGetComponent<InteractableObject>(out InteractableObject obj))
            {
                OnItemPickedUp?.Invoke(obj);
                holdedItem = obj;
                Debug.Log("Item picked up");
            }
        }
    }

    private void DropItem()
    {
        OnItemDropped?.Invoke(holdedItem);
        holdedItem = null;
        Debug.Log("Item dropped");
    }

    private bool IsMoving()
    {
        return playerInputInstance.GetMovementVectorNormalized().magnitude > 0f;
    }
}
