using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllTheQuad : AbstractMeshGenerator
{

    [SerializeField] private Vector3[] points = new Vector3[4];
    [SerializeField] private Vector2[] flexibleUVs = new Vector2[4];
    [SerializeField] private Vector3 flexibleNormals = new Vector3(0, 0, -1);
    protected override void SetMeshNums()
    {
        numVertices = 4;
        numTriangles = 6;
    }

    protected override void SetVertices()
    {
        vertices.AddRange(points);
    }

    protected override void SetTangents()
    {
        
    }

    protected override void SetUVs() 
    {
        for (int i = 0; i < flexibleUVs.Length; i++)
        {
            uvs.Add(flexibleUVs[i]);
        }
    }

    protected override void SetNormals() 
    {
        for (int i = 0; i < vertices.Count; i++)
        {
            normals.Add( flexibleNormals );
        }
    }

    protected override void SetTriangles()
    {
        triangles.Add(0);
        triangles.Add(1);
        triangles.Add(3);

        triangles.Add(0);
        triangles.Add(3);
        triangles.Add(2);
    }
    protected override void SetVertexColours(){ }

}
