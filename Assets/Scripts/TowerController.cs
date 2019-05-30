using System.Linq;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    [SerializeField]
    private Transform _gun;

    [SerializeField]
    private int _attackRange = 30;

    // Update is called once per frame
    void Update()
    {
        ProcessShooting();
    }

    private void ProcessShooting()
    {
        var closest = FindObjectsOfType<EnemyCollisionHandler>()
            .Select(_ => new { enemy = _.gameObject, distance = Vector3.Distance(transform.position, _.gameObject.transform.position) })
            .Where(_ => _.distance <= _attackRange)
            .OrderBy(_ => _.distance)
            .FirstOrDefault();

        if (closest == null)
        {
            DoShooting(false);
            return;
        }
        _gun.LookAt(closest.enemy.transform);
        DoShooting(true);
    }

    private void DoShooting(bool shouldShoot)
    {
        var emission = GetComponentInChildren<ParticleSystem>().emission;
        emission.enabled = shouldShoot;
    }

}
