using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField]
    private Waypoint _start, _end;

    private Dictionary<Vector2Int, Waypoint> _grid = new Dictionary<Vector2Int, Waypoint>();
    private Queue<Waypoint> _queue = new Queue<Waypoint>();
    private List<Vector2Int> _directions = new List<Vector2Int>
    {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };
    private List<Waypoint> _cachedPath;

    public List<Waypoint> GetPath()
    {
        if (_cachedPath != null)
        {
            return _cachedPath;
        }

        LoadBlocks();

        _queue.Enqueue(_start);
        while (_queue.Any())
        {
            var current = _queue.Dequeue();
            current.IsExplored = true;

            // If reached destination
            if (current == _end)
            {
                break;
            }

            GetUnExploredNeighbors(current)
                .ForEach(_ =>
                {
                    if (!_queue.Contains(_))
                    {
                        _queue.Enqueue(_);
                    }

                    _.LeadingWaypoint = current;
                });
        }

        var path = ConstructPathFromEnd(_end);
        _cachedPath = path;
        return path;
    }

    private List<Waypoint> ConstructPathFromEnd(Waypoint end)
    {
        var backTracked = end;
        var path = new List<Waypoint> { backTracked };

        while (backTracked.LeadingWaypoint)
        {
            path.Insert(0, backTracked.LeadingWaypoint);
            backTracked = backTracked.LeadingWaypoint;
        }

        return path;
    }

    private List<Waypoint> GetUnExploredNeighbors(Waypoint waypoint)
    {
        return GetNeighbors(waypoint)
            .Where(_ => !_.IsExplored)
            .ToList();
    }

    private List<Waypoint> GetNeighbors(Waypoint waypoint)
    {
        var startPosition = waypoint.GetPositionInGrid();
        return _directions
            // Filter in case the neighbor doesn't exist
            // This will happen if the waypoint is on the edge
            .Where(_ => _grid.ContainsKey(_ + startPosition))
            .Select(_ => _grid[_ + startPosition])
            .ToList();
    }

    private void LoadBlocks()
    {
        FindObjectsOfType<Waypoint>()
            .ToList()
            .ForEach(_ =>
            {
                if (_grid.ContainsKey(_.GetPositionInGrid()))
                {
                    Debug.LogWarning($"Skipping overlapping block {_}");
                    return;
                }
                _grid.Add(_.GetPositionInGrid(), _);
            });
    }
}
