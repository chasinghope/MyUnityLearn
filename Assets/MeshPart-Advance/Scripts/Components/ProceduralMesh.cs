using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace ProcedureMeshes
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class ProceduralMesh : MonoBehaviour
    {
        private Mesh mesh;

        [SerializeField, Range(1, 10)]
        private int resolution = 1;

        private void Awake()
        {
            mesh = new Mesh
            {
                name = "Procedural Mesh"
            };
            //GenerateMesh();
            GetComponent<MeshFilter>().mesh = mesh;
        }

        private void OnValidate()
        {
            enabled = true;
        }

        private void Update()
        {
            GenerateMesh();
            enabled = false;
        }

        private void GenerateMesh()
        {
            Debug.Log("GenerateMesh() Called...");
            Mesh.MeshDataArray meshDataArray = Mesh.AllocateWritableMeshData(1);
            Mesh.MeshData meshData = meshDataArray[0];

            MeshJob<SquareGrid, SingleStream>.ScheduleParallel(mesh, meshData, resolution, default).Complete();
            Mesh.ApplyAndDisposeWritableMeshData(meshDataArray, mesh);
        }
    }
}

