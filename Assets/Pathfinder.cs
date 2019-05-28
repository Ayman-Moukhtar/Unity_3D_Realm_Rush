using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField]
    private Waypoint _start, _end;

    private Dictionary<Vector2Int, Waypoint> _grid = new Dictionary<Vector2Int, Waypoint>();
    private List<Vector2Int> _directions = new List<Vector2Int>
    {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };

    void Start()
    {
        LoadBlocks();
        ColorStartAndEnd();
    }

    private void FindPath()
    {
        var queue = new Queue<Waypoint>(new[] { _start });
        while (queue.Any())
        {
            var current = queue.Dequeue();
            var neighbors = GetNeighbors(queue.Dequeue());
        }
    }

    private List<Waypoint> GetNeighbors(Waypoint waypoint)
    {
        var startPosition = waypoint.GetPositionInGrid();
        var neighbors = new List<Waypoint>();
        _directions.ForEach(_ =>
        {
            var neighborCoordinates = _ + startPosition;
            if (_grid.ContainsKey(neighborCoordinates))
            {
                neighbors.Add(_grid[neighborCoordinates]);
            }
        });

        return neighbors;
    }

    private void ColorStartAndEnd()
    {
        _start.SetTopColor(Color.green);
        _end.SetTopColor(Color.red);
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
