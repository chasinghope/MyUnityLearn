using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProcGenChunk : AbstractLandscapeMeshGenerator 
{
	private int xStartPos;
	private int zStartPos;
	private int xEndPos;
	private int zEndPos;

	private int zOuterStartPos;
	private int zOuterEndPos;
	private int xOuterStartPos;
	private int xOuterEndPos;

	private float uvScale;

	private List<Vector3> outerVertices = new List<Vector3> (); //(xResolution + 3) * (zResolution + 3)
	private List<int> outerTriangles = new List<int> ();
	private List<Vector3> outerUVs = new List<Vector3> ();

	public void InitInfiniteLandscape(Material mat, int xRes, int zRes, float meshScale, float yScale, int octaves, float lacunarity, float gain, float perlinScale, float uvScale, Vector2 startPosition)
	{
		this.material = mat;
		this.xResolution = xRes;
		this.zResolution = zRes;
		this.meshScale = meshScale;
		this.yScale = yScale;

		this.octaves = octaves;
		this.lacunarity = lacunarity;
		this.gain = gain;
		this.perlinScale = perlinScale;

		xStartPos = (int)startPosition.x;
		zStartPos = (int)startPosition.y;
		xEndPos = xStartPos + xRes;
		zEndPos = zStartPos + zRes;

		zOuterStartPos = zStartPos - 1;
		zOuterEndPos = zEndPos + 1;
		xOuterStartPos = xStartPos - 1;
		xOuterEndPos = xEndPos + 1;

		this.uvScale = uvScale;

		//No islands for now.
		type = FallOffType.None;
	}


	protected override void SetMeshNums ()
	{
		numVertices = (xResolution + 1) * (zResolution + 1);  //number of vertices in x direction multiplied by number in z
		numTriangles = 6 * xResolution * zResolution; //This is 3 ints per geometric triangle * 2 geometric triangles per square * the number of triangles needed in the x direction * in the z direction
	}

	protected override void SetVertices ()
	{
		float xx, y, zz = 0;
		NoiseGenerator noise = new NoiseGenerator (octaves, lacunarity, gain, perlinScale);

		for (int z=zOuterStartPos; z <= zOuterEndPos; z++)
		{
			for (int x = xOuterStartPos; x <= xOuterEndPos; x++) 
			{
				xx = ((float)x / xResolution) * meshScale;
				zz = ((float)z / zResolution) * meshScale; 

				y = yScale * noise.GetFractalNoise (xx, zz);
				y = FallOff ((float)x, y, (float)z);

				Vector3 vertex = new Vector3 (xx, y, zz);
				outerVertices.Add (vertex);

				//only add to vertices list if is within original positions
				if (z >= zStartPos && z <= zEndPos && x>= xStartPos && x<=xEndPos)
				{
					vertices.Add (vertex);
				}
			}
		}
	}

	protected override void SetTriangles ()
	{
		int outerTriCount = 0;
		int triCount = 0;
		for (int z=zOuterStartPos; z < zOuterEndPos; z++)
		{
			for (int x = xOuterStartPos; x < xOuterEndPos; x++) 
			{
				outerTriangles.Add (outerTriCount);
				outerTriangles.Add (outerTriCount + xResolution + 3);
				outerTriangles.Add (outerTriCount + 1);

				outerTriangles.Add (outerTriCount + 1);
				outerTriangles.Add (outerTriCount + xResolution + 3);
				outerTriangles.Add (outerTriCount + xResolution + 4);

				outerTriCount++;

				//only add to vertices list if is within original positions
				if (z >= zStartPos && z < zEndPos && x>= xStartPos && x<xEndPos)
				{
					triangles.Add (triCount);
					triangles.Add (triCount + xResolution + 1);
					triangles.Add (triCount + 1);

					triangles.Add (triCount + 1);
					triangles.Add (triCount + xResolution + 1);
					triangles.Add (triCount + xResolution + 2);

					triCount++;
				}
			}
			if (z >= zStartPos && z < zEndPos)
			{
				triCount++;	
			}
			outerTriCount++;
		}
	}


	protected override void SetNormals ()
	{
		int numGeometricTriangles = outerTriangles.Count / 3;
		Vector3[] norms = new Vector3[outerVertices.Count]; //used to add up the per triangle normals
		int index = 0;
		for (int i=0; i<numGeometricTriangles; i++)
		{
			//the triangle ints that make up a geometric triangle
			int triA = outerTriangles[index];
			int triB = outerTriangles[index+1];
			int triC = outerTriangles[index+2];

			//directions from the index-th vertex that make up the triangle
			Vector3 dirA = outerVertices[triB] - outerVertices[triA];
			Vector3 dirB = outerVertices[triC] - outerVertices[triA];

			//Normal needs to come out of the plane perpendicular to dirA and dirB
			Vector3 normal = Vector3.Cross(dirA, dirB);

			//add the normals for each vertex cumulatively
			norms[triA] += normal;
			norms[triB] += normal;
			norms[triC] += normal;

			index += 3;
		}

		//go through the vertices and normalise the norms
		int outerWidth = xResolution + 2;
		for (int i=0; i<outerVertices.Count; i++)
		{
			//skip outer edges

			//left and right edges
			if (i % (outerWidth + 1) == 0 || i % (outerWidth + 1) == outerWidth)
			{
				continue;
			}

			//first and last rows
			if (i <= outerWidth || i >= outerVertices.Count - outerWidth)
			{
				continue;
			}

			//add the normalised normals
			normals.Add(norms[i].normalized);
		}
	}


	protected override void SetUVs ()
	{
		for (int z=zOuterStartPos; z <= zOuterEndPos; z++)
		{
			for (int x = xOuterStartPos; x <= xOuterEndPos; x++) 
			{
				outerUVs.Add(new Vector2 (x / (uvScale * xResolution), z / (uvScale * zResolution)));

				if (z >= zStartPos && z <= zEndPos && x>= xStartPos && x<=xEndPos)
				{
					uvs.Add(new Vector2 (x / (uvScale * xResolution), z / (uvScale * zResolution)));
				}
			}
		}
	}

	protected override void SetVertexColours ()	{	}

	protected override void SetTangents ()	
	{	
		if (uvs.Count == 0 || normals.Count == 0)
		{
			print ("Set UVs and Normals before adding tangents");
			return;
		}

		int numGeometricTriangles = outerTriangles.Count / 3;
		Vector3[] tans = new Vector3[outerVertices.Count];
		Vector3[] bitans = new Vector3[outerVertices.Count];
		int index = 0;
		for (int i=0; i<numGeometricTriangles; i++)
		{
			//the triangle ints that make up a geometric triangle
			int triA = outerTriangles[index];
			int triB = outerTriangles[index+1];
			int triC = outerTriangles[index+2];

			//the corresponding UVs
			Vector2 uvA = outerUVs[triA];
			Vector2 uvB = outerUVs[triB];
			Vector2 uvC = outerUVs[triC];

			//directions from the index-th vertex that make up the triangle
			Vector3 dirA = outerVertices[triB] - outerVertices[triA];
			Vector3 dirB = outerVertices[triC] - outerVertices[triA];

			//from the matrix equation
			Vector2 uvDiffA = new Vector2(uvB.x - uvA.x, uvC.x - uvA.x);
			Vector2 uvDiffB = new Vector2(uvB.y - uvA.y, uvC.y - uvA.y);

			float invDet = uvDiffA.x * uvDiffB.y - uvDiffA.y * uvDiffB.x;
			if (invDet == 0)
			{
				print ("Invalid determinant!");
				return;
			}
			float determinant = 1f / invDet;
			Vector3 sDir = determinant * (new Vector3 (uvDiffB.y * dirA.x - uvDiffB.x * dirB.x, uvDiffB.y * dirA.y - uvDiffB.x * dirB.y, uvDiffB.y * dirA.z - uvDiffB.x * dirB.z));
			Vector3 tDir = determinant * (new Vector3 (uvDiffA.x * dirB.x - uvDiffA.y * dirA.x, uvDiffA.x * dirB.y - uvDiffA.y * dirA.y, uvDiffA.x * dirB.z - uvDiffA.y * dirA.z));

			//add the tangents for each vertex cumulatively so that all contributions are added
			tans[triA] += sDir;
			tans[triB] += sDir;
			tans[triC] += sDir;

			//and for bitans
			bitans[triA] += tDir;
			bitans[triB] += tDir;
			bitans[triC] += tDir;

			index += 3;
		}

		int outerWidth = xResolution + 2;
		int normalsIndex = 0;
		for (int i=0; i<outerVertices.Count; i++)
		{
			//skip outer edges

			//left and right edges
			if (i % (outerWidth + 1) == 0 || i % (outerWidth + 1) == outerWidth)
			{
				continue;
			}

			//first and last rows
			if (i <= outerWidth || i >= outerVertices.Count - outerWidth)
			{
				continue;
			}

			Vector3 normal = normals [normalsIndex];
			normalsIndex++;

			Vector3 tan = tans [i];

			//Orthonormalise using Gram-Schmidt
			Vector3 tangent3 = (tan - Vector3.Dot(normal, tan) * normal).normalized;
			Vector4 tangent = tangent3;

			//calculate handedness
			tangent.w = Vector3.Dot(Vector3.Cross(normal, tan), bitans[i]) < 0f ? -1f : 1f;
			tangents.Add (tangent);
		}
	}
}
