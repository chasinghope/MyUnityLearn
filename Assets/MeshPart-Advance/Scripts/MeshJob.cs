using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace ProcedureMeshes
{
    public struct MeshJob<G, S> : IJobFor
        where G : struct, IMeshGenerator
        where S : struct, IMeshStreams
    {
        G generator;
        [WriteOnly]
        S streams;

        public void Execute(int index)
        {
            generator.Execute(index, streams);
        }

        public static JobHandle ScheduleParallel(Mesh.MeshData meshData, JobHandle dependency)
        {
            MeshJob<G, S> job = new MeshJob<G, S>();
            job.streams.Setup(meshData, job.generator.VertexCount, job.generator.IndexCount);
            return job.ScheduleParallel(job.generator.JobLength, 1, dependency);
        }
    }
}

