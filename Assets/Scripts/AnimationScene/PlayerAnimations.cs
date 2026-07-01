using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private const string SPEED = "Speed";
    private const string IS_RUNNING = "IsRunning";
    [SerializeField] private Animator animator;
    private PlayerMotor playerMotorInstance;
    private float playerSpeed;

    [SerializeField] private PlayerInput playerInputInstance;

    private void Start()
    {
        playerMotorInstance = PlayerMotor.Instance;
    }

    private void Update()
    {
        HandlePlayerSpeed();
        HandleRunning();
    }

    private void HandlePlayerSpeed()
    {
        playerSpeed = PlayerMotor.Instance.PlayerSpeed;
        animator.SetFloat(SPEED, playerSpeed);
    }

    private void HandleRunning()
    {
        animator.SetBool(IS_RUNNING, playerInputInstance.IsRunKeyHolded());
    }
}
