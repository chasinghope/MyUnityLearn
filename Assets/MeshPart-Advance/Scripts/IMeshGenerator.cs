using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProcedureMeshes
{
    public interface IMeshGenerator
    {
        void Execute<S>(int i, S streams) where S : IMeshStreams;
        int VertexCount { get; }
        int IndexCount { get; }
        int JobLength { get; }
        Bounds Bounds { get; }
        int Resolution { get; set; }

    }
}