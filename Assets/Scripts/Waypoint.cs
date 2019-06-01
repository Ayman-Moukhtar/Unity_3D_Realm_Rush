using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public static int GridBlockSize => 10;

    public bool IsBlocked { get; set; }

    private void OnMouseOver()
    {
        if (IsBlocked)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0)) // Left Click
        {
            FindObjectOfType<TowerFactory>().AddTower(this);
        }
    }

    #region Public Methods
    public Vector2Int GetPositionInGrid()
    {
        return new Vector2Int(
            Mathf.RoundToInt(transform.position.x / GridBlockSize),
            Mathf.RoundToInt(transform.position.z / GridBlockSize)
            );
    }
    #endregion
}
