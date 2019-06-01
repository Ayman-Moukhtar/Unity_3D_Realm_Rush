using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{
    [SerializeField] private int _towersLimit = 3;
    [SerializeField] private TowerController _towerPrefab;

    private Queue<TowerController> _existingTowers = new Queue<TowerController>();

    public void AddTower(Waypoint waypoint)
    {
        if (_existingTowers.Count == _towersLimit)
        {
            MoveExistingTower(waypoint);
            return;
        }

        DoAddTower(waypoint);
    }

    private void DoAddTower(Waypoint waypoint)
    {
        var tower = Instantiate(_towerPrefab, waypoint.transform.position, Quaternion.identity, transform);
        tower.Waypoint = waypoint;
        _existingTowers.Enqueue(tower);
        waypoint.IsBlocked = true;
    }

    private void MoveExistingTower(Waypoint waypoint)
    {
        var tower = _existingTowers.Dequeue();

        tower.transform.position = waypoint.transform.position;
        waypoint.IsBlocked = true;
        tower.Waypoint.IsBlocked = false;
        tower.Waypoint = waypoint;

        _existingTowers.Enqueue(tower);
    }
}
