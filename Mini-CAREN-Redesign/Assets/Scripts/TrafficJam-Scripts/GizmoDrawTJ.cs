using UnityEngine;

public class GizmoDrawTJ : MonoBehaviour
{
    [SerializeField] private Color color;
    [SerializeField] private Vector3 cubeDimensions = new Vector3 (1, 1, 1);
    void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawCube(transform.position, cubeDimensions);
    }
}
