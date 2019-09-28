using System;
using System.Collections;
using UnityEngine;

public class FireCannon : MonoBehaviour
{
    public enum FireDirection
    {
        Left,
        Right
    }

    [SerializeField] private FireDirection direction;
    [SerializeField] private Transform fireball;
    [SerializeField] private float fireCooldown;
    [SerializeField] private float startDelay;
    [SerializeField] private AudioClip fireSound;

    private AudioSource _audioSourceComponent;

    private bool _startedFire;
    
    private void Start()
    {
        _audioSourceComponent = GetComponent<AudioSource>();
        
        _startedFire = false;
        StartCoroutine(Fire());
    }

    private IEnumerator Fire()
    {
        if (!_startedFire)
        {
            yield return new WaitForSeconds(startDelay);
            _startedFire = true;
        }
        
        switch (direction)
        {
            case FireDirection.Left:
            {
                var fireballInstance = Instantiate(fireball, transform.position, Quaternion.Euler(0, 0, 90));
                fireballInstance.GetComponent<SpriteRenderer>().flipY = true;
                fireballInstance.GetComponent<Fireball>().SetDirection(Vector2.left);
                _audioSourceComponent.PlayOneShot(fireSound);
            }
                break;
            case FireDirection.Right:
            {
                var fireballInstance = Instantiate(fireball, transform.position, Quaternion.Euler(0, 0, 90));
                fireballInstance.GetComponent<Fireball>().SetDirection(Vector2.right);
                _audioSourceComponent.PlayOneShot(fireSound);
            }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        yield return new WaitForSeconds(fireCooldown);
        yield return Fire();
    }
}
