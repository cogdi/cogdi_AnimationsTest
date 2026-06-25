using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public static PlayerLook Instance { get; private set; }
    public Camera FirstPersonCamera { get => firstPersonCam; }
    public bool FirstPersonMode { get => firstPersonMode; }

    [SerializeField] private Camera firstPersonCam;
    [SerializeField] private Camera thirdPersonCam;
    [SerializeField, Range(0, 12)] private float firstPersonVerticalSensitivity = 2.8f;
    [SerializeField, Range(0, 12)] private float firstPersonHorizontalSensitivity = 2.8f;
    [SerializeField, Range(0, 12)] private float thirdPersonSensitivity = 3.6f;
    [SerializeField] private float cameraUpperClamp = 90f;
    [SerializeField] private float cameraLowerClamp = 60f;
    private float inputX = 0f;
    private float inputY = 0f;
    private bool firstPersonMode = true;


    private void Awake()
    {
        Instance = this;

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Start()
    {
        PlayerInput.Instance.OnSwitchCameraTriggered += PlayerInput_OnSwitchCameraTriggered;
    }

    private void PlayerInput_OnSwitchCameraTriggered()
    {
        if (firstPersonMode)
        {
            firstPersonCam.gameObject.SetActive(false);
            thirdPersonCam.gameObject.SetActive(true);
        }

        else
        {
            firstPersonCam.gameObject.SetActive(true);
            thirdPersonCam.gameObject.SetActive(false);
        }

        firstPersonMode = !firstPersonMode;
    }

    private void LateUpdate()
    {
        if (firstPersonMode)
            FirstPersonLook();    
        else
            ThirdPersonLook();
    }

    private void FirstPersonLook()
    {
        inputX += PlayerInput.Instance.GetLookVector().x;
        inputY += PlayerInput.Instance.GetLookVector().y;

        Vector3 lookRotation = new Vector3(-inputY * firstPersonVerticalSensitivity, inputX * firstPersonHorizontalSensitivity, 0f);

        transform.rotation = Quaternion.Euler(0f, lookRotation.y, 0f);
        firstPersonCam.transform.rotation = Quaternion.Euler(Mathf.Clamp(lookRotation.x, cameraLowerClamp, cameraUpperClamp), lookRotation.y, 0f);
    }

    private void ThirdPersonLook()
    {
        inputX += PlayerInput.Instance.GetLookVector().x;
        transform.rotation = Quaternion.Euler(0f, inputX * thirdPersonSensitivity, 0f);
    }

    public Vector3 GetCameraPosition()
    {
        return firstPersonCam.transform.position;
    }

    public Vector3 GetCameraTransformForward()
    {
        return firstPersonCam.transform.forward;
    }
}
