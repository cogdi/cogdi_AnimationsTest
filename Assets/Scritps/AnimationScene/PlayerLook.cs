using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private Camera firstPersonCam;
    [SerializeField] private Camera thirdPersonCam;
    [SerializeField, Range(0, 12)] private float firstPersonVerticalSensitivity = 1.33f;
    [SerializeField, Range(0, 12)] private float firstPersonHorizontalSensitivity = 3.6f;
    [SerializeField, Range(0, 12)] private float thirdPersonSensitivity = 3.6f;
    [SerializeField] private float cameraUpperClamp = 90f;
    [SerializeField] private float cameraLowerClamp = 60f;
    private float inputX = 0f;
    private float inputY = 0f;
    private bool firstPersonMode = true;


    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Start()
    {
        PlayerInput.Instance.OnSwitchCameraTriggered += PlayerInput_OnSwitchCameraTriggered;
    }

    private void PlayerInput_OnSwitchCameraTriggered()
    {
        // thirdPersonCam.gameObject.SetActive(firstPersonMode);
        // thirdPersonCam.gameObject.SetActive(!firstPersonMode);


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
        inputX += PlayerInput.Instance.GetLookVectorNormalized().x;
        inputY += PlayerInput.Instance.GetLookVectorNormalized().y;

        Vector3 lookRotation = new Vector3(-inputY * firstPersonVerticalSensitivity, inputX * firstPersonHorizontalSensitivity, 0f);
        //lookRotation *= firstPersonSensitivity;

        transform.rotation = Quaternion.Euler(0f, lookRotation.y, 0f);
        firstPersonCam.transform.rotation = Quaternion.Euler(Mathf.Clamp(lookRotation.x, cameraLowerClamp, cameraUpperClamp), lookRotation.y, 0f);
    }

    private void ThirdPersonLook()
    {
        inputX += PlayerInput.Instance.GetLookVectorNormalized().x;
        transform.rotation = Quaternion.Euler(0f, inputX * thirdPersonSensitivity, 0f);
    }
}
