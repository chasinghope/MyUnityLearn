using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProcedureMeshes
{
    public struct SquareGrid : IMeshGenerator
    {
        public int VertexCount => 0;

        public int IndexCount => 0;

        public int JobLength => 0;

        public void Execute<S>(int i, S streams) where S : IMeshStreams
        {
            
        }
    }
}

