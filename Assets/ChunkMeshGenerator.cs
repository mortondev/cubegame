using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets
{
    public class ChunkMeshGenerator
    {
        public MeshData CreateChunkMesh(Chunk chunk)
        {
            var meshData = new MeshData();
            for (var x = 0; x < Chunk.ChunkSize; x++)
            {
                for (var y = 0; y < Chunk.ChunkSize; y++)
                {
                    for (var z = 0; z < Chunk.ChunkSize; z++)
                    {
                        if (chunk.GetBlock(x, y, z).IsSolid())
                        {
                            meshData = AddFaces(chunk, x, y, z, meshData);
                        }
                        
                    }
                }
            }

            return meshData;
        }

        protected virtual MeshData AddFaces(Chunk chunk, int x, int y, int z, MeshData meshData)
        {
            if (!chunk.GetBlock(x, y + 1, z).IsSolid())
            {
                meshData = FaceDataUp(chunk, x, y, z, meshData);
            }

            if (!chunk.GetBlock(x, y - 1, z).IsSolid())
            {
                meshData = FaceDataDown(chunk, x, y, z, meshData);
            }

            if (!chunk.GetBlock(x, y, z + 1).IsSolid())
            {
                meshData = FaceDataNorth(chunk, x, y, z, meshData);
            }

            if (!chunk.GetBlock(x, y, z - 1).IsSolid())
            {
                meshData = FaceDataSouth(chunk, x, y, z, meshData);
            }

            if (!chunk.GetBlock(x + 1, y, z).IsSolid())
            {
                meshData = FaceDataEast(chunk, x, y, z, meshData);
            }

            if (!chunk.GetBlock(x - 1, y, z).IsSolid())
            {
                meshData = FaceDataWest(chunk, x, y, z, meshData);
            }
            return meshData;
        }

        protected virtual MeshData FaceDataUp(Chunk chunk, int x, int y, int z, MeshData meshData)
        {
            meshData.vertices.Add(new Vector3(x - 0.5f, y + 0.5f, z + 0.5f));
            meshData.vertices.Add(new Vector3(x + 0.5f, y + 0.5f, z + 0.5f));
            meshData.vertices.Add(new Vector3(x + 0.5f, y + 0.5f, z - 0.5f));
            meshData.vertices.Add(new Vector3(x - 0.5f, y + 0.5f, z - 0.5f));

            meshData.AddQuadTriangles();

            return meshData;
        }

        protected virtual MeshData FaceDataDown(Chunk chunk, int x, int y, int z, MeshData meshData)
        {
            meshData.vertices.Add(new Vector3(x - 0.5f, y - 0.5f, z - 0.5f));
            meshData.vertices.Add(new Vector3(x + 0.5f, y - 0.5f, z - 0.5f));
            meshData.vertices.Add(new Vector3(x + 0.5f, y - 0.5f, z + 0.5f));
            meshData.vertices.Add(new Vector3(x - 0.5f, y - 0.5f, z + 0.5f));

            meshData.AddQuadTriangles();
            return meshData;
        }

        protected virtual MeshData FaceDataNorth(Chunk chunk, int x, int y, int z, MeshData meshData)
        {
            meshData.vertices.Add(new Vector3(x + 0.5f, y - 0.5f, z + 0.5f));
            meshData.vertices.Add(new Vector3(x + 0.5f, y + 0.5f, z + 0.5f));
            meshData.vertices.Add(new Vector3(x - 0.5f, y + 0.5f, z + 0.5f));
            meshData.vertices.Add(new Vector3(x - 0.5f, y - 0.5f, z + 0.5f));

            meshData.AddQuadTriangles();
            return meshData;
        }

        protected virtual MeshData FaceDataEast(Chunk chunk, int x, int y, int z, MeshData meshData)
        {
            meshData.vertices.Add(new Vector3(x + 0.5f, y - 0.5f, z - 0.5f));
            meshData.vertices.Add(new Vector3(x + 0.5f, y + 0.5f, z - 0.5f));
            meshData.vertices.Add(new Vector3(x + 0.5f, y + 0.5f, z + 0.5f));
            meshData.vertices.Add(new Vector3(x + 0.5f, y - 0.5f, z + 0.5f));

            meshData.AddQuadTriangles();
            return meshData;
        }

        protected virtual MeshData FaceDataSouth(Chunk chunk, int x, int y, int z, MeshData meshData)
        {
            meshData.vertices.Add(new Vector3(x - 0.5f, y - 0.5f, z - 0.5f));
            meshData.vertices.Add(new Vector3(x - 0.5f, y + 0.5f, z - 0.5f));
            meshData.vertices.Add(new Vector3(x + 0.5f, y + 0.5f, z - 0.5f));
            meshData.vertices.Add(new Vector3(x + 0.5f, y - 0.5f, z - 0.5f));

            meshData.AddQuadTriangles();
            return meshData;
        }

        protected virtual MeshData FaceDataWest(Chunk chunk, int x, int y, int z, MeshData meshData)
        {
            meshData.vertices.Add(new Vector3(x - 0.5f, y - 0.5f, z + 0.5f));
            meshData.vertices.Add(new Vector3(x - 0.5f, y + 0.5f, z + 0.5f));
            meshData.vertices.Add(new Vector3(x - 0.5f, y + 0.5f, z - 0.5f));
            meshData.vertices.Add(new Vector3(x - 0.5f, y - 0.5f, z - 0.5f));

            meshData.AddQuadTriangles();
            return meshData;
        }
    }
}
