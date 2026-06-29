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
    [SerializeField, Range(0, 12)] private float firstPersonVerticalSensitivity = 2.8f;
    [SerializeField, Range(0, 12)] private float firstPersonHorizontalSensitivity = 2.8f;
    [SerializeField, Range(0, 12)] private float thirdPersonSensitivity = 3.6f;
    [SerializeField] private float cameraUpperClamp = 90f;
    [SerializeField] private float cameraLowerClamp = 60f;
    private float inputX = 0f;
    private float inputY = 0f;
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
    }

    private void FillCameraDictionary()
    {
        cameraDictionary = new Dictionary<CameraMode, Camera>();

        cameraDictionary[CameraMode.FirstPerson] = firstPersonCam;
        cameraDictionary[CameraMode.ThirdPerson] = thirdPersonCam;
    }

    private void Start()
    {
        playerInputInstance.OnSwitchCameraTriggered += PlayerInput_OnSwitchCameraTriggered;
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
        inputX += playerInputInstance.GetLookVector().x;
        inputY += playerInputInstance.GetLookVector().y;

        Vector3 lookRotation = new Vector3(-inputY * firstPersonVerticalSensitivity, inputX * firstPersonHorizontalSensitivity, 0f);

        transform.rotation = Quaternion.Euler(0f, lookRotation.y, 0f);
        firstPersonCam.transform.rotation = Quaternion.Euler(Mathf.Clamp(lookRotation.x, cameraLowerClamp, cameraUpperClamp), lookRotation.y, 0f);
    }

    private void ThirdPersonLook()
    {
        inputX += playerInputInstance.GetLookVector().x;
        inputY += playerInputInstance.GetLookVector().y;

        cameraOffset.transform.rotation = Quaternion.Euler(Mathf.Clamp(-inputY * thirdPersonSensitivity, cameraLowerClamp, cameraUpperClamp), inputX * thirdPersonSensitivity, 0f);

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
