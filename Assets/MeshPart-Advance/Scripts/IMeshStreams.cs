using Unity.Mathematics;
using UnityEngine;

namespace ProcedureMeshes
{
    public interface IMeshStreams
    {
        void Setup(Mesh.MeshData meshData, int vertexCount, int indexCount);
        void SetVertex(int index, Vertex vertex);
        void SetTriangle(int index, int3 triangle);
    }
}