using UnityEngine;

public class Flamable : MonoBehaviour
{
   [SerializeField] private ParticleSystem fireVFX;
   public ParticleSystem FireVFX { get => fireVFX; }
   public float FireProgress { get; set; }
   private float FIRE_PROGRESS_MAX = 100F;

    private void Awake()
    {
        FireProgress = FIRE_PROGRESS_MAX;
    }

    private void Start()
    {
        fireVFX.gameObject.SetActive(true);
    }

    private void Update()
    {
        Burn();
    }

    private void Burn()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            fireVFX.gameObject.SetActive(false);
        }
    }
}