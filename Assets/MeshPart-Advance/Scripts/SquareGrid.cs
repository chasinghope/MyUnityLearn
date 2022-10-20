using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace ProcedureMeshes
{
    public struct SquareGrid : IMeshGenerator
    {
        public int VertexCount => 4 * Resolution * Resolution;

        public int IndexCount => 6 * Resolution * Resolution;

        public int JobLength => 1 * Resolution; //1 * Resolution * Resolution;

        public Bounds Bounds => new Bounds(Vector3.zero, new Vector3(1f, 0f, 1f));

        public int Resolution { get; set; }

        public void Execute<S>(int z, S streams) where S : IMeshStreams
        {
            int vi = 4 * Resolution * z, ti = 2 * Resolution * z;

            //int z = i / Resolution;
            //int x = i - Resolution * z;

            for (int x = 0; x < Resolution; x++, vi+=4, ti+=2)
            {
                //var coordinates = new float4(x, x + 1f, z, z + 1f) / Resolution - 0.5f;

                float2 xCoordinates = new float2(x, x + 1f) / Resolution - 0.5f;
                float2 zCoordinates = new float2(z, z + 1f) / Resolution - 0.5f;

                //vertex...
                Vertex vertex = new Vertex();

                vertex.position.xz = new float2(xCoordinates.x, zCoordinates.x);
                vertex.normal.y = 1f;
                vertex.tangent.xw = new float2(1f, -1f);
                vertex.texCoord0 = float2.zero;
                streams.SetVertex(vi + 0, vertex);

                vertex.position.xz = new float2(xCoordinates.y, zCoordinates.x);
                vertex.normal.y = 1f;
                vertex.tangent.xw = new float2(1f, -1f);
                vertex.texCoord0 = new float2(1f, 0f);
                streams.SetVertex(vi + 1, vertex);

                vertex.position.xz = new float2(xCoordinates.x, zCoordinates.y);
                vertex.normal.y = 1f;
                vertex.tangent.xw = new float2(1f, -1f);
                vertex.texCoord0 = new float2(0f, 1f);
                streams.SetVertex(vi + 2, vertex);

                vertex.position.xz = new float2(xCoordinates.y, zCoordinates.y);
                vertex.normal.y = 1f;
                vertex.tangent.xw = new float2(1f, -1f);
                vertex.texCoord0 = new float2(1f, 1f);
                streams.SetVertex(vi + 3, vertex);

                //triangles...
                streams.SetTriangle(ti + 0, vi + new int3(0, 2, 1));
                streams.SetTriangle(ti + 1, vi + new int3(1, 2, 3));
            }

        }

    }
}

