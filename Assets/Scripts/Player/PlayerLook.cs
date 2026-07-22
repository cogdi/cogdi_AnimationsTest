using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public enum CameraMode
    {
        FirstPerson,
        ThirdPerson
    }

    public static PlayerLook Instance { get; private set; }

    [SerializeField] private PlayerInput playerInputInstance;
    [SerializeField] private Camera firstPersonCam;
    [SerializeField] private Camera thirdPersonCam;
    [SerializeField] private Transform cameraOffset;
    [SerializeField, Range(0.1f, 1)] private float firstPersonSensitivity = 0.3f;
    [SerializeField, Range(0.1f, 1)] private float thirdPersonSensitivity = 0.3f;
    [SerializeField] private float cameraUpperClamp = -90f;
    [SerializeField] private float cameraLowerClamp = 60f;
    private float inputX = 0f;
    private float inputY = 0f;
    private float xRotation;
    private CameraMode cameraMode;
    private Dictionary<CameraMode, Camera> cameraDictionary;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        FillCameraDictionary();
        Cursor.lockState = CursorLockMode.Locked;
        cameraMode = CameraMode.FirstPerson;
    }

    private void FillCameraDictionary()
    {
        cameraDictionary = new Dictionary<CameraMode, Camera>();

        cameraDictionary[CameraMode.FirstPerson] = firstPersonCam;
        cameraDictionary[CameraMode.ThirdPerson] = thirdPersonCam;
    }

    private void Start()
    {
        playerInputInstance.OnSwitchCameraPerformed += PlayerInput_OnSwitchCameraTriggered;
    }

    private void PlayerInput_OnSwitchCameraTriggered()
    {
        if (cameraMode == CameraMode.FirstPerson)
        {
            firstPersonCam.gameObject.SetActive(false);
            thirdPersonCam.gameObject.SetActive(true);

            cameraMode = CameraMode.ThirdPerson;
        }

        else
        {
            thirdPersonCam.gameObject.SetActive(false);
            firstPersonCam.gameObject.SetActive(true);

            cameraMode = CameraMode.FirstPerson;
        }
    }

    private void LateUpdate()
    {
        if (cameraMode == CameraMode.FirstPerson)
            FirstPersonLook();    
        else
            ThirdPersonLook();
    }

    private void FirstPersonLook()
    {
        Vector2 look = playerInputInstance.GetLookVector();
        float mouseX = look.x * firstPersonSensitivity;
        float mouseY = look.y * firstPersonSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, cameraUpperClamp, cameraLowerClamp);

        firstPersonCam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        transform.Rotate(0f, mouseX, 0f);
    }

    private void ThirdPersonLook()
    {
        inputX += playerInputInstance.GetLookVector().x;
        inputY += playerInputInstance.GetLookVector().y;

        cameraOffset.transform.rotation = Quaternion.Euler(Mathf.Clamp(-inputY * thirdPersonSensitivity, cameraUpperClamp, cameraLowerClamp), inputX * thirdPersonSensitivity, 0f);

        transform.rotation = Quaternion.Euler(
            0,
            cameraOffset.eulerAngles.y,
            0
        );

        Debug.DrawRay(thirdPersonCam.transform.position, thirdPersonCam.transform.forward * 9f, Color.blue);
    }

    public Vector3 GetCurrentCameraPosition()
    {
        return cameraDictionary[cameraMode].transform.position;
    }

    public Vector3 GetCurrentCameraForward()
    {
        return cameraDictionary[cameraMode].transform.forward;
    }

    public void ParentObjectToCurrentCamera(Transform obj)
    {
        obj.SetParent(cameraDictionary[cameraMode].transform);
    }
}
