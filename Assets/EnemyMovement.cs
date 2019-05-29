using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private List<Waypoint> _path;

    public void FollowPath(List<Waypoint> path)
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
