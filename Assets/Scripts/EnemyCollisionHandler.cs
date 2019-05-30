using UnityEngine;

public class EnemyCollisionHandler : MonoBehaviour
{
    [SerializeField]
    private int _hitPoints = 10;

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
    }

    private void ProcessHit()
    {
        _hitPoints -= 1;
        if (_hitPoints <= 0)
        {
            Die();
        }
    }

    private void Die() => Destroy(gameObject);
}
