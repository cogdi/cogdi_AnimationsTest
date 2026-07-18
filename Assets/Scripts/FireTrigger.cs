using UnityEngine;

public class FireTrigger : MonoBehaviour
{
   [SerializeField] private ParticleSystem fireVFX;
   public float FireProgress { get => fireProgress; }
   private float fireProgress;
   private const float FIRE_PROGRESS_MAX = 100F;

    private void Awake()
    {
        fireProgress = FIRE_PROGRESS_MAX;
    }

    private void Start()
    {
        fireVFX.gameObject.SetActive(true);
    }

    public void Extinguish()
    {
        if (fireProgress > 0f)
        {
            fireProgress -= 34 * Time.deltaTime;
        }
    }

    public void StopBurning()
    {
        fireVFX.gameObject.SetActive(false);
        Destroy(gameObject);
    }
}