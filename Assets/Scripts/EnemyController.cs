using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Pathfinder _pathFinder;
    private Coroutine _coroutine;

    [SerializeField]
    private ParticleSystem _deathFx;

    [SerializeField]
    private ParticleSystem _selfDestructParticle;

    [SerializeField]
    private float _movementDelay = .5f;

    [SerializeField]
    private AudioClip _enemyDeathSfx;

    private AudioSource _audio;

    private void Start()
    {
        _pathFinder = FindObjectOfType<Pathfinder>();
        FollowPath();
    }

    private void FollowPath(Vector2Int? startingPosition = null)
    {
        _coroutine = StartCoroutine(
            DoFollowPath(_pathFinder.GetPath(startingPosition), startingPosition == null)
            );
    }

    private Vector2Int GetPositionInGrid()
    {
        return new Vector2Int(
            Mathf.RoundToInt(transform.position.x),
            Mathf.RoundToInt(transform.position.z)
            );
    }

    private IEnumerator DoFollowPath(List<Waypoint> path, bool initialDelay)
    {
        var didPathEnd = true;
        if (initialDelay)
        {
            yield return new WaitForSeconds(_movementDelay);
        }

        for (int i = 0; i < path.Count; i++)
        {
            var waypoint = path[i];
            if (waypoint.IsBlocked)
            {
                didPathEnd = false;
                StopCoroutine(_coroutine);
                FollowPath(GetPositionInGrid());
                break;
            }
            transform.position = waypoint.transform.position;
            if (i == path.Count - 1)
            {
                SelfDestruct();
                break;
            }
            yield return new WaitForSeconds(_movementDelay);
        }
    }

    private void SelfDestruct()
    {
        var selfDestructFx = Instantiate(_selfDestructParticle, transform.position, Quaternion.identity);
        selfDestructFx.Play();
        Destroy(selfDestructFx.gameObject, selfDestructFx.main.duration);
        Destroy(gameObject);
    }

    public void Die(float delay = 0f)
    {
        var deathFx = Instantiate(_deathFx, transform.position, Quaternion.identity);
        deathFx.Play();
        AudioSource.PlayClipAtPoint(_enemyDeathSfx, Camera.main.transform.position);
        Destroy(deathFx.gameObject, deathFx.main.duration);
        Destroy(gameObject, delay);
    }
}
