using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    private Dictionary<Vector2Int, Waypoint> _grid = new Dictionary<Vector2Int, Waypoint>();

    void Start()
    {
        LoadBlocks();
    }

    public void LoadBlocks()
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
