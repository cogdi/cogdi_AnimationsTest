using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private const string SPEED_VAR = "Speed";
    [SerializeField] private Animator animator;
    private PlayerMotor playerMotorInstance;
    private float playerSpeed;

    private void Start()
    {
        playerMotorInstance = PlayerMotor.Instance;
    }

    private void Update()
    {
        playerSpeed = PlayerMotor.Instance.PlayerSpeed;
        animator.SetFloat(SPEED_VAR, playerSpeed);
    } 
}
