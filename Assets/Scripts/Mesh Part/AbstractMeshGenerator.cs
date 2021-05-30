using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
public abstract class AbstractMeshGenerator : MonoBehaviour
{
    [SerializeField] protected Material material;
    protected List<Vector3> vertices;
    protected List<int> triangles;

    protected List<Vector3> normals;
    protected List<Vector4> tangents;
    protected List<Vector2> uvs;
    protected List<Color32> vertexColours;

    protected int numVertices;
    protected int numTriangles;

    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    private MeshCollider meshCollider;
    private Mesh mesh;


    protected virtual void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
        meshCollider = GetComponent<MeshCollider>();
        meshRenderer.material = material;

    }


    private void Update()
    {
        //initialise
        InitMesh();
        SetMeshNums();

        //create th mesh
        CreateMesh();
    }

    private bool ValidateMesh()
    {
        //build a string containing erros;
        string errorStr = string.Empty;

        //check there are the correct number of triangles and vertices
        errorStr += vertices.Count == numVertices ? string.Empty : "Should be " + numVertices + " vertices, but there are " + vertices.Count + ".";
        errorStr += triangles.Count == numTriangles ? string.Empty : "Should be " + numTriangles + " triangles, but there are " + triangles.Count + ".";

        errorStr += (normals.Count == numTriangles || normals.Count == 0) ? string.Empty : "Should be " + numVertices + " normals, but there are " + normals.Count + ".";
        errorStr += (tangents.Count == numTriangles || tangents.Count == 0) ? string.Empty : "Should be " + numVertices + " tangents, but there are " + tangents.Count + ".";
        errorStr += (uvs.Count == numTriangles || uvs.Count == 0) ? string.Empty : "Should be " + numVertices + " uvs, but there are " + uvs.Count + ".";
        errorStr += (vertexColours.Count == numTriangles || vertexColours.Count == 0) ? string.Empty : "Should be " + numVertices + " vertexColours, but there are " + vertexColours.Count + ".";

        bool isValid = string.IsNullOrEmpty(errorStr);
        if (!isValid)
        {
            Debug.LogError("Not drawing mesh. " + errorStr);
        }
        return isValid;
    }


    private void InitMesh()
    {
        vertices = new List<Vector3>();
        triangles = new List<int>();

        //optional
        normals = new List<Vector3>();
        tangents = new List<Vector4>();
        uvs = new List<Vector2>();
        vertexColours = new List<Color32>();
    }

    protected abstract void SetMeshNums();


    private void CreateMesh()
    {
        mesh = new Mesh();

        SetVertices();
        SetTriangles();

        SetNormals();
        SetTangents();
        SetUVs();
        SetVertexColours();

        if (ValidateMesh())
        {
            //This should always be done vertices first, triangles second - Unity requires this.
            mesh.SetVertices(vertices);
            mesh.SetTriangles(triangles, 0);


            if(normals.Count == 0)
            {
                mesh.RecalculateNormals();
                normals.AddRange(mesh.normals);
            }

            mesh.SetNormals(normals);
            mesh.SetTangents(tangents);
            mesh.SetUVs(0, uvs);
            mesh.SetColors(vertexColours);

            meshFilter.mesh = mesh;
            meshCollider.sharedMesh = mesh;
        }
        
    }


    protected abstract void SetVertices();
    protected abstract void SetTriangles();
    protected abstract void SetNormals();
    protected abstract void SetTangents();
    protected abstract void SetUVs();
    protected abstract void SetVertexColours();

}
