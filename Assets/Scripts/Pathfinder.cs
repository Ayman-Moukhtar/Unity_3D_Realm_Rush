using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BreadCrumbsTracker
{
    public Waypoint Point { get; set; }
    public BreadCrumbsTracker LeadingPoint { get; set; }
}

public class Pathfinder : MonoBehaviour
{
    [SerializeField]
    private Waypoint _start, _end;

    private List<Vector2Int> _directions = new List<Vector2Int>
    {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };

    public List<Waypoint> GetPath(EnemyController enemy, Vector2Int? startingPosition = null)
    {
        var queue = new Queue<BreadCrumbsTracker>();
        var explored = new HashSet<BreadCrumbsTracker>();
        var blocks = GetWrappedBlocks();
        var startWrapper = new BreadCrumbsTracker
        {
            Point = startingPosition != null
            ? GetWaypointByPosition((Vector2Int)startingPosition, blocks)
            : _start,
            LeadingPoint = null
        };
        var endWrapper = new BreadCrumbsTracker { Point = _end, LeadingPoint = null };
        BreadCrumbsTracker current = null;
        queue.Enqueue(startWrapper);

        while (queue.Any())
        {
            current = queue.Dequeue();
            explored.Add(current);

            // If reached destination
            if (current.Point == endWrapper.Point)
            {
                break;
            }

            GetUnExploredNeighbors(current, explored, blocks)
                .ForEach(_ =>
                {
                    if (!queue.Contains(_))
                    {
                        queue.Enqueue(_);
                    }

                    _.LeadingPoint = current;
                });
        }

        var path = ConstructPathFromLeadingPoints(startWrapper, current);
        return path;
    }

    private List<Waypoint> ConstructPathFromLeadingPoints(BreadCrumbsTracker start, BreadCrumbsTracker end)
    {
        var tracker = end;
        var path = new List<Waypoint> { tracker.Point };

        while (tracker.LeadingPoint != null)
        {
            path.Insert(0, tracker.Point);
            tracker = tracker.LeadingPoint;
        }

        path.Insert(0, start.Point);
        return path;
    }

    private List<BreadCrumbsTracker> GetUnExploredNeighbors(
        BreadCrumbsTracker waypoint, HashSet<BreadCrumbsTracker> alreadyExplored, Dictionary<Vector2Int, BreadCrumbsTracker> blocks
        )
    {
        return GetNeighbors(waypoint, blocks)
            .Where(_ => !alreadyExplored.Contains(_) && !_.Point.IsBlocked)
            .ToList();
    }

    private List<BreadCrumbsTracker> GetNeighbors(BreadCrumbsTracker waypoint, Dictionary<Vector2Int, BreadCrumbsTracker> blocks)
    {
        var startPosition = waypoint.Point.GetPositionInGrid();
        return _directions
            // Filter in case the neighbor doesn't exist
            // This will happen if the waypoint is on the edge
            .Where(_ => blocks.ContainsKey(_ + startPosition))
            .Select(_ => blocks[_ + startPosition])
            .ToList();
    }

    private Dictionary<Vector2Int, BreadCrumbsTracker> GetWrappedBlocks()
    {
        var grid = new Dictionary<Vector2Int, BreadCrumbsTracker>();

        FindObjectsOfType<Waypoint>()
            .ToList()
            .ForEach(_ =>
            {
                if (grid.ContainsKey(_.GetPositionInGrid()))
                {
                    Debug.LogWarning($"Skipping overlapping block {_}");
                    return;
                }
                grid.Add(_.GetPositionInGrid(), new BreadCrumbsTracker { Point = _, LeadingPoint = null });
            });

        return grid;
    }

    private Waypoint GetWaypointByPosition(Vector2Int position, Dictionary<Vector2Int, BreadCrumbsTracker> blocks)
    {
        var adjustedPosition = new Vector2Int(
            position.x / Waypoint.GridBlockSize,
            position.y / Waypoint.GridBlockSize
            );

        if (blocks.ContainsKey(adjustedPosition))
        {
            return blocks[adjustedPosition].Point;
        }

        return null;
    }
}
