using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Pathfinder _pathFinder;

    private void Start()
    {
        _pathFinder = FindObjectOfType<Pathfinder>();
        FollowPath(_pathFinder.GetPath());
    }

    private void FollowPath(List<Waypoint> path)
    {
        StartCoroutine(DoFollowPath(path));
    }

    private IEnumerator DoFollowPath(List<Waypoint> path)
    {
        yield return new WaitForSeconds(1);
        foreach (var waypoint in path)
        {
            transform.position = waypoint.transform.position;
            yield return new WaitForSeconds(1);
        }
    }
}
