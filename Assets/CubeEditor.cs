using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
[RequireComponent(typeof(Waypoint))]
public class CubeEditor : MonoBehaviour
{
    private TextMesh _textMesh;
    private Waypoint _waypoint;

    private void Awake()
    {
        _waypoint = GetComponent<Waypoint>();
    }

    private void Start()
    {
        _textMesh = GetComponentInChildren<TextMesh>();
        AdjustNameAndLabelText();
    }

    private void Update()
    {
        var positionInGrid = _waypoint.GetPositionInGrid();
        transform.position = new Vector3(
            positionInGrid.x,
            0f,
            positionInGrid.y
            );
        AdjustNameAndLabelText();
    }

    private void AdjustNameAndLabelText()
    {
        var positionInGrid = _waypoint.GetPositionInGrid();
        var text = $"{positionInGrid.x / _waypoint.GridSize},{positionInGrid.y / _waypoint.GridSize}";
        _textMesh.text = text;
        gameObject.name = text;
    }
}
