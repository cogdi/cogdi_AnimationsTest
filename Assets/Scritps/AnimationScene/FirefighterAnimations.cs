using UnityEngine;

public class FirefighterAnimations : MonoBehaviour
{
    private const string SPEED_VAR = "Speed";
    [SerializeField] private Animator animator;
    [Range(0, 5)] [SerializeField] private float speed;

    private void Update()
    {
        animator.SetFloat(SPEED_VAR, speed);
    } 
}
