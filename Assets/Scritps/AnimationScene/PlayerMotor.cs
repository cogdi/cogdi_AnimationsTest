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

    [SerializeField] private CharacterController controller;
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private float acceleration = 0.6f;
    private float currentSpeed = 0f;
    private float gravity = -9.8f;
    private Vector3 velocity;
    private bool isGrounded;


    [Header("Interactables")]
    [SerializeField] private LayerMask interactableLayerMask;
    private float interactionDistance = 2f;
    private float eyeLevel = 1.5f;
    private InteractableObject holdedItem;
    //private bool isHoldingItem;
    

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        //PlayerInput.Instance.OnInteractPerformed += PickUpFirstPerson;
        PlayerInput.Instance.OnInteractPerformed += PlayerInput_OnInteractPerformed;
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

    public bool IsInteractableObjectLayer(int layer)
    {
        return interactableLayerMask == (interactableLayerMask | 1 << layer);
    }

    private void PickUpFirstPerson()
    {
        Ray ray = new Ray(PlayerLook.Instance.GetCameraPosition(), PlayerLook.Instance.GetCameraTransformForward());
        if (Physics.Raycast(ray, out RaycastHit hitInfo, interactionDistance, interactableLayerMask))
        {
            if (hitInfo.transform.gameObject.TryGetComponent<InteractableObject>(out InteractableObject cube))
            {
                OnItemPickedUp?.Invoke(cube);
                holdedItem = cube;
                Debug.Log("Item picked up");
            }
        }
    }

    private void PickUpThirdPerson()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, interactionDistance))
        {
            if (hitInfo.transform.gameObject.TryGetComponent<InteractableObject>(out InteractableObject cube))
            {
                OnItemPickedUp?.Invoke(cube);
                holdedItem = cube;
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
