using UnityEngine;

public class Flamable : MonoBehaviour
{
   [SerializeField] private ParticleSystem fireVFX;

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