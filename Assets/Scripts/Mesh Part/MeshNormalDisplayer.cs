using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshNormalDisplayer : MonoBehaviour
{
    [SerializeField] private bool drawNormals;
    [SerializeField] private float normalLength = 0.5f;

    private void OnDrawGizmosSelected()
    {
        if (drawNormals)
        {
            Mesh mesh = GetComponent<MeshFilter>().sharedMesh;

            if(mesh != null)
            {
                for (int i = 0; i < mesh.normals.Length; i++)
                {
                    //change these to world space so they display normals when move transform
                    Vector3 vertex = transform.TransformPoint(mesh.vertices[i]);
                    Vector3 normal = transform.TransformDirection(mesh.normals[i]);

                    Gizmos.color = Color.blue;
                    Gizmos.DrawLine(vertex, vertex + normal.normalized * normalLength);
                }
            }
        }
    }

}
