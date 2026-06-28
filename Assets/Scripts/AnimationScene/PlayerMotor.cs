using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    public static PlayerMotor Instance { get; private set; }
    public event Action<InteractableObject> OnItemPickedUpFirstPerson;
    public event Action<InteractableObject> OnItemPickedUpThirdPerson;
    public event Action<InteractableObject> OnItemDropped;


    [Header("Movement")]
    public float PlayerSpeed { get => currentSpeed; }

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
    private float thirdPersonInteractionDistance = 9f;
    private float eyeLevel = 1.5f;
    private InteractableObject holdedItem;
    private Vector3 cameraStartPoint;
    private Vector3 cameraForward;
    private InteractableObjectVisual pickableObjectVisual;    

    private PlayerLook playerLookInstance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        PlayerInput.Instance.OnInteractPerformed += PlayerInput_OnInteractPerformed;
        playerLookInstance = PlayerLook.Instance;
    }

    private void PlayerInput_OnInteractPerformed()
    {
        if (!holdedItem)
        {
            if (PlayerLook.Instance.FirstPersonMode)
                PickUpFirstPerson();
            else
                PickUpThirdPerson();
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
        if (holdedItem)
            return;

        if (playerLookInstance.FirstPersonMode)
        {
            cameraStartPoint = playerLookInstance.GetFirstPersonCameraPosition();
            cameraForward = playerLookInstance.GetFirstPersonCameraForward();
        }

        else
        {
            cameraStartPoint = playerLookInstance.GetThirdPersonCameraPosition();
            cameraForward = playerLookInstance.GetThirdPersonCameraForward();
        }


        Ray ray = new Ray(cameraStartPoint, cameraForward);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, thirdPersonInteractionDistance, interactableLayerMask))
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

    public bool IsInteractableObjectLayer(int layer)
    {
        return interactableLayerMask == (interactableLayerMask | 1 << layer);
    }

    private void PickUpFirstPerson()
    {
        Ray ray = new Ray(playerLookInstance.GetFirstPersonCameraPosition(), playerLookInstance.GetFirstPersonCameraForward());
        if (Physics.Raycast(ray, out RaycastHit hitInfo, firstPersonInteractionDistance, interactableLayerMask))
        {
            if (hitInfo.transform.gameObject.TryGetComponent<InteractableObject>(out InteractableObject obj))
            {
                OnItemPickedUpFirstPerson?.Invoke(obj);
                holdedItem = obj;
                Debug.Log("Item picked up");
            }
        }
    }

    private void PickUpThirdPerson()
    {
        if (Physics.Raycast(playerLookInstance.GetThirdPersonCameraPosition(), playerLookInstance.GetThirdPersonCameraForward(), out RaycastHit hitInfo, 9f))
        {
            if (hitInfo.transform.gameObject.TryGetComponent<InteractableObject>(out InteractableObject obj))
            {
                OnItemPickedUpThirdPerson?.Invoke(obj);
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
        return PlayerInput.Instance.GetMovementVectorNormalized().magnitude > 0f;
    }
}
