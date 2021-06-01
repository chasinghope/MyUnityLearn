using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllTheSharedVertTetrahedrons : AbstractMeshGenerator
{

    [SerializeField] private Vector3[] vs = new Vector3[4];

    protected override void SetMeshNums()
    {
        numVertices = 4;
        numTriangles = 12; //tetrahedron has 4 sides, all triangular. 4 physical triangles * 3 ints for each = 12
    }

    protected override void SetVertices()
    {
        vertices.AddRange(vs);
    }

    protected override void SetTangents()
    {
        //base
        triangles.Add(0);
        triangles.Add(1);
        triangles.Add(2);

        //sides
        triangles.Add(0);
        triangles.Add(3);
        triangles.Add(1);

        triangles.Add(0);
        triangles.Add(2);
        triangles.Add(3);

        triangles.Add(1);
        triangles.Add(3);
        triangles.Add(2);
    }

    protected override void SetNormals() { }
    protected override void SetTriangles() { }
    protected override void SetUVs() { }
    protected override void SetVertexColours() { }

}

