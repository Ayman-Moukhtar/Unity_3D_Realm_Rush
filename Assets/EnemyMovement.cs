using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private List<Waypoint> _path;

    void Start()
    {
        StartCoroutine(FollowPath());
    }

    private IEnumerator FollowPath()
    {
        yield return new WaitForSeconds(1);
        foreach (var waypoint in _path)
        {
            transform.position = waypoint.transform.position;
            yield return new WaitForSeconds(1);
        }
    }
}
