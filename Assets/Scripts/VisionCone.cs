using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class VisionCone : MonoBehaviour
{
    public float viewDistance = 10f;
    public float viewAngle = 90f;
    public float height = 5f;
    public int segments = 10;

    private Mesh mesh;

    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        CreateVisionCone();
    }

    void CreateVisionCone()
    {
        int verticesCount = (segments + 1) * 2 + 1;
        Vector3[] vertices = new Vector3[verticesCount];
        int[] triangles = new int[segments * 12];

        vertices[0] = Vector3.zero; // apex of the cone

        float angleStep = viewAngle / segments;
        for (int i = 0; i <= segments; i++)
        {
            float angle = -viewAngle / 2 + angleStep * i;
            Vector3 dir = Quaternion.Euler(0, angle, 0) * Vector3.forward;
            vertices[i + 1] = dir * viewDistance;
            vertices[i + 1 + segments + 1] = vertices[i + 1] + Vector3.up * height;
        }

        for (int i = 0; i < segments; i++)
        {
            // Bottom base triangles
            triangles[i * 3] = 0;
            triangles[i * 3 + 1] = i + 1;
            triangles[i * 3 + 2] = i + 2;

            // Side triangles
            triangles[(segments + i) * 6] = i + 1;
            triangles[(segments + i) * 6 + 1] = i + 1 + segments + 1;
            triangles[(segments + i) * 6 + 2] = i + 2 + segments + 1;

            triangles[(segments + i) * 6 + 3] = i + 1;
            triangles[(segments + i) * 6 + 4] = i + 2 + segments + 1;
            triangles[(segments + i) * 6 + 5] = i + 2;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, viewDistance);

        Vector3 leftBoundary = Quaternion.Euler(0, -viewAngle / 2, 0) * transform.forward * viewDistance;
        Vector3 rightBoundary = Quaternion.Euler(0, viewAngle / 2, 0) * transform.forward * viewDistance;

        Gizmos.DrawLine(transform.position, transform.position + leftBoundary);
        Gizmos.DrawLine(transform.position, transform.position + rightBoundary);

        Vector3 leftBoundaryTop = leftBoundary + Vector3.up * height;
        Vector3 rightBoundaryTop = rightBoundary + Vector3.up * height;

        Gizmos.DrawLine(transform.position + leftBoundary, transform.position + leftBoundaryTop);
        Gizmos.DrawLine(transform.position + rightBoundary, transform.position + rightBoundaryTop);
        Gizmos.DrawLine(transform.position + leftBoundaryTop, transform.position + rightBoundaryTop);
    }
}