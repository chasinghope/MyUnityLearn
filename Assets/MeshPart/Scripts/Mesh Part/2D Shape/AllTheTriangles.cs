using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllTheTriangles : AbstractMeshGenerator
{
    public bool reverse;

    [SerializeField] private Vector3[] pointABC;

    protected override void SetMeshNums()
    {
        numVertices = 3;
        numTriangles = 3;
    }

    protected override void SetNormals()
    {

    }

    protected override void SetTangents()
    {

    }

    protected override void SetTriangles()
    {
        if (reverse)
        {
            triangles.Add(0);
            triangles.Add(2);
            triangles.Add(1);
        }
        else
        {
            triangles.Add(0);
            triangles.Add(1);
            triangles.Add(2);
        }
    }

    protected override void SetUVs()
    {

    }

    protected override void SetVertexColours()
    {

    }

    protected override void SetVertices()
    {
        vertices.AddRange(pointABC);
    }
}
