using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshTangentDisplayer : MonoBehaviour
{
    [SerializeField] private bool drawTangent;
    [SerializeField] private float tangentLength = 0.5f;

    private void OnDrawGizmosSelected()
    {
        if (drawTangent)
        {
            Mesh mesh = GetComponent<MeshFilter>().sharedMesh;

            if( mesh!= null)
            {
                for (int i = 0; i < mesh.vertices.Length; i++)
                {
                    //change these to world space so they display normals when move transform
                    Vector3 vertex = transform.TransformPoint(mesh.vertices[i]);
                    //Vector3 tangent = transform.TransformDirection(mesh.tangents[i].x, mesh.tangents[i].y, mesh.tangents[i].z);
                    Vector3 tangent = transform.TransformDirection(mesh.tangents[i]);

                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(vertex, vertex + tangent.normalized * tangentLength);
                }
            }
        }
    }
}
