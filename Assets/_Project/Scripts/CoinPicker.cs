using UnityEngine;

public class CoinPicker : MonoBehaviour
{
    [SerializeField] private AudioClip pickupSound;
    
    private AudioSource _audioSourceComponent;

    private void Start()
    {
        _audioSourceComponent = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Interactables") && other.gameObject.CompareTag("Coin"))
        {
            _audioSourceComponent.PlayOneShot(pickupSound);
            Destroy(other.gameObject);
        }
    }
}
