using UnityEngine;

public class EnemyCollisionHandler : MonoBehaviour
{
    [SerializeField]
    private int _hitPoints = 10;

    [SerializeField]
    private ParticleSystem _hitFx;

    [SerializeField]
    private ParticleSystem _deathFx;

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
            Die();
        }
    }

    private void Die()
    {
        var deathFx = Instantiate(_deathFx, transform.position, Quaternion.identity);
        deathFx.Play();
        Destroy(deathFx.gameObject, deathFx.main.duration);
        Destroy(gameObject);
    }
}
