using UnityEngine;

public class TowerController : MonoBehaviour
{
    [SerializeField]
    private Transform _gun;

    [SerializeField]
    private Transform _target;

    [SerializeField]
    private int _attackRange = 30;

    // Update is called once per frame
    void Update()
    {
        ProcessShooting();
    }

    private void ProcessShooting()
    {
        if (!_target || Vector3.Distance(transform.position, _target.position) > _attackRange)
        {
            DoShooting(false);
            return;
        }
        _gun.LookAt(_target);
        DoShooting(true);
    }

    private void DoShooting(bool shouldShoot)
    {
        var emission = GetComponentInChildren<ParticleSystem>().emission;
        emission.enabled = shouldShoot;
    }

}
