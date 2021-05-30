using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllThePolygons : AbstractMeshGenerator
{

    [SerializeField, Range(3, 60)] private int numSides = 3;
    [SerializeField] private float radius;

    protected override void SetMeshNums()
    {
        numVertices = numSides;
        numTriangles = 3 * (numSides -2); 
    }

    protected override void SetVertices()
    {
        //coordinates of a regular polygon
        for (int i = 0; i < numSides; i++)
        {
            float angle = 2 * Mathf.PI * i / numSides;
            vertices.Add(new Vector3(radius * Mathf.Cos(angle), radius * Mathf.Sin(angle), 0));
        }
    }

    protected override void SetTangents()
    {
        for (int i = 1; i < numSides - 1; i++)
        {
            //each physical triangle starts at the zeroeth vertex
            triangles.Add(0);
            triangles.Add(i + 1);
            triangles.Add(i);
        }
    }

    protected override void SetNormals() { }
    protected override void SetTriangles() { }
    protected override void SetUVs() { }
    protected override void SetVertexColours() { }

}