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

        public static JobHandle ScheduleParallel(Mesh mesh, Mesh.MeshData meshData, int resolution, JobHandle dependency)
        {
            MeshJob<G, S> job = new MeshJob<G, S>();
            job.generator.Resolution = resolution;
            mesh.bounds = job.generator.Bounds;
            job.streams.Setup(meshData, job.generator.Bounds, job.generator.VertexCount, job.generator.IndexCount);
            return job.ScheduleParallel(job.generator.JobLength, 1, dependency);
        }
    }

    public delegate JobHandle MeshJobScheduleDelegate(Mesh mesh, Mesh.MeshData meshData, int resolution, JobHandle dependency);
}

