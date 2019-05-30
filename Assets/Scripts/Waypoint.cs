using UnityEngine;

public class Waypoint : MonoBehaviour
{
    private const int _gridSize = 10;
    public int GridSize => _gridSize;

    public bool IsExplored { get; set; } = false;
    public Waypoint LeadingWaypoint { get; set; }

    private void OnMouseOver()
    {

    }

    #region Public Methods
    public Vector2Int GetPositionInGrid()
    {
        return new Vector2Int(
            Mathf.RoundToInt(transform.position.x / _gridSize),
            Mathf.RoundToInt(transform.position.z / _gridSize)
            );
    }
    #endregion
}
