using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class EditorOnlyObject : MonoBehaviour
{
    public Color color;
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawSphere(transform.position, 0.2f);
    }
#endif
}