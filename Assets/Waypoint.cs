using UnityEngine;

public class Waypoint : MonoBehaviour
{
    private const int _gridSize = 10;
    public int GridSize => _gridSize;


    #region Public Methods
    public Vector2Int GetPositionInGrid()
    {
        return new Vector2Int(
            Mathf.RoundToInt(transform.position.x / _gridSize) * _gridSize,
            Mathf.RoundToInt(transform.position.z / _gridSize) * _gridSize
            );
    }
    #endregion
}
