using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllThePolygons : AbstractMeshGenerator
{

    [SerializeField, Range(3, 100)] private int numSides = 3;
    [SerializeField] private float radius;

    [SerializeField] private float xTiling = 1;
    [SerializeField] private float yTiling = 1;

    [SerializeField] private float xScroll = 1;
    [SerializeField] private float yScroll = 1;

    [SerializeField] private float angle = 0;

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

    protected override void SetTriangles()
    {
        for (int i = 1; i < numSides - 1; i++)
        {
            //each physical triangle starts at the zeroeth vertex
            triangles.Add(0);
            triangles.Add(i + 1);
            triangles.Add(i);
        }
    }

    protected override void SetNormals() 
    {
        for (int i = 0; i < numVertices; i++)
        {
            normals.Add(new Vector3(0, 0, -1));
        }
    }

    protected override void SetTangents()
    {
        Vector3 tangent3 = new Vector3(1, 0, 0);  //because this is how the UVs are oriented at angle = 0
        //Rotate clockwise as alpha increase
        Vector3 rotatedTangent = Quaternion.AngleAxis(angle, -Vector3.forward) * tangent3;
        Vector4 tangent = rotatedTangent;
        tangent.w = -1;    // left hand rule
        for (int i = 0; i < numVertices; i++)
        {
            tangents.Add(tangent);
        }
    }

    protected override void SetUVs()
    {
        for (int i = 0; i < numVertices; i++)
        {
            Vector2 uv = new Vector2(xTiling * vertices[i].x + xScroll, yTiling * vertices[i].y + yScroll);
            uvs.Add( Quaternion.AngleAxis(angle, Vector3.forward) * uv );
        }
    }



    protected override void SetVertexColours() { }

}