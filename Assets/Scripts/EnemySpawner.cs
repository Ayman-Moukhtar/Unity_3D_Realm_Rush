using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Range(0.1f, 120f)]
    [Tooltip("In Seconds")]
    [SerializeField]
    private float _delay = 3f;

    [SerializeField]
    private EnemyController _enemy;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        while (true)
        {
            Instantiate(_enemy, transform.position, Quaternion.identity, transform);
            yield return new WaitForSeconds(_delay);
        }
    }
}
