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
        SnapInPosition();
        AdjustNameAndLabelText();
    }

    private void SnapInPosition()
    {
        var positionInGrid = _waypoint.GetPositionInGrid();
        transform.position = new Vector3(
            positionInGrid.x * _waypoint.GridSize,
            0f,
            positionInGrid.y * _waypoint.GridSize
            );
    }

    private void AdjustNameAndLabelText()
    {
        var positionInGrid = _waypoint.GetPositionInGrid();
        var text = $"{positionInGrid.x},{positionInGrid.y}";
        _textMesh.text = text;
        gameObject.name = text;
    }
}
