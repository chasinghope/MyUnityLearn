using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllTheUniqueQuad : AbstractMeshGenerator
{

    [SerializeField] private Vector3[] points = new Vector3[4];
    [SerializeField] private Vector2[] flexibleUVs = new Vector2[4];

    protected override void SetMeshNums()
    {
        numVertices = 6;
        numTriangles = 6;
    }

    protected override void SetNormals()
    {
    }

    protected override void SetTangents()
    {

    }

    protected override void SetTriangles()
    {
        triangles.Add(0);
        triangles.Add(1);
        triangles.Add(2);

        triangles.Add(3);
        triangles.Add(4);
        triangles.Add(5);
    }

    protected override void SetUVs()
    {
        uvs.Add(flexibleUVs[0]);
        uvs.Add(flexibleUVs[3]);
        uvs.Add(flexibleUVs[2]);

        uvs.Add(flexibleUVs[0]);
        uvs.Add(flexibleUVs[1]);
        uvs.Add(flexibleUVs[3]);
    }

    protected override void SetVertexColours()
    {
    }

    protected override void SetVertices()
    {
        vertices.Add(points[0]);
        vertices.Add(points[3]);
        vertices.Add(points[2]);

        vertices.Add(points[0]);
        vertices.Add(points[1]);
        vertices.Add(points[3]);
    }
}