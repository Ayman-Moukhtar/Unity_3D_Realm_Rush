using UnityEngine;

public class EnemyCollisionHandler : MonoBehaviour
{
    [SerializeField]
    private int _hitPoints = 10;

    [SerializeField]
    private ParticleSystem _hitFx;

    [SerializeField]
    private AudioClip _enemyHitSfx;

    private AudioSource _audio;

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
    }
    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
    }

    private void ProcessHit()
    {
        _hitPoints -= 1;
        _hitFx.Play();
        if (_hitPoints <= 0)
        {
            GetComponent<EnemyController>().Die();
        }
        _audio.PlayOneShot(_enemyHitSfx);
    }
}
