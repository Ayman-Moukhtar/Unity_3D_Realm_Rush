using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
public class CubeEditor : MonoBehaviour
{
    [Range(1f, 10f)]
    [SerializeField]
    private float _dragOffset = 10f;

    private TextMesh _textMesh;

    private void Start()
    {
        _textMesh = GetComponentInChildren<TextMesh>();
        AdjustNameAndLabelText();
    }

    private void Update()
    {
        transform.position = new Vector3(
            Mathf.RoundToInt(transform.position.x / _dragOffset) * _dragOffset,
            0f,
            Mathf.RoundToInt(transform.position.z / _dragOffset) * _dragOffset
            );
        AdjustNameAndLabelText();
    }

    private void AdjustNameAndLabelText()
    {
        var text = $"{transform.position.x / _dragOffset},{transform.position.z / _dragOffset}";
        _textMesh.text = text;
        gameObject.name = text;
    }
}
