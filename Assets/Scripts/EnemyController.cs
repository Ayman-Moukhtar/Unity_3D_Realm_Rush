using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Pathfinder _pathFinder;
    private Coroutine _coroutine;

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
        if (initialDelay)
        {
            yield return new WaitForSeconds(1);
        }

        for (int i = 0; i < path.Count; i++)
        {
            var waypoint = path[i];
            if (waypoint.IsBlocked)
            {
                StopCoroutine(_coroutine);
                FollowPath(GetPositionInGrid());
                break;
            }
            transform.position = waypoint.transform.position;
            yield return new WaitForSeconds(1);
        }
    }
}
