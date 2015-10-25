using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Blocks;
using UnityEngine;

namespace Assets.Rendering
{
    public class ChunkMeshGenerator
    {
        private static List<Vector3> _lightningBlocks = new List<Vector3>();
        public static List<Vector3> LightningBlocks { get { return _lightningBlocks; } }

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

            chunk.MeshFilter.mesh.Clear();
            chunk.MeshFilter.mesh.vertices = meshData.vertices.ToArray();
            chunk.MeshFilter.mesh.triangles = meshData.triangles.ToArray();
            chunk.MeshFilter.mesh.colors32 = meshData.colors.ToArray();
            chunk.MeshFilter.mesh.RecalculateNormals();  
                     
            chunk.MeshRenderer.material.shader = Shader.Find("Map/Map");

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
      
            // Get neighbours
            Block top = chunk[x, y + 1, z];

            Block frontTop = chunk[x, y + 1, z + 1];
            Block backTop = chunk[x, y + 1, z - 1];

            Block leftTop = chunk[x + 1, y + 1, z];
            Block rightTop = chunk[x - 1, y + 1, z];

            Block frontTopLeft = chunk[x + 1, y + 1, z + 1];
            Block frontTopRight = chunk[x - 1, y + 1, z + 1];

            Block backTopLeft = chunk[x + 1, y + 1, z - 1];
            Block backTopRight = chunk[x - 1, y + 1, z - 1];

            int c1 = top.Light, c2 = top.Light, c3 = top.Light, c4 = top.Light;
            //int c1 = (top.Light + backTop.Light + backTopRight.Light + rightTop.Light) / 4;  // Bottom Left
            //int c2 = (top.Light + frontTop.Light + frontTopRight.Light + rightTop.Light) / 4;// Bottom Right 
            //int c3 = (top.Light + frontTop.Light + frontTopLeft.Light + leftTop.Light) / 4;  // Top Right
            //int c4 = (top.Light + backTop.Light + backTopLeft.Light + leftTop.Light) / 4;    // Top Left

            byte c1Byte = (byte)c1;
            byte c2Byte = (byte)c2;
            byte c3Byte = (byte)c3;
            byte c4Byte = (byte)c4;

            meshData.colors.Add(new Color32(c1Byte, c1Byte, c1Byte, 0));
            meshData.colors.Add(new Color32(c2Byte, c2Byte, c2Byte, 0));
            meshData.colors.Add(new Color32(c3Byte, c3Byte, c3Byte, 0));
            meshData.colors.Add(new Color32(c4Byte, c4Byte, c4Byte, 0));

            return meshData;
        }

        protected virtual MeshData FaceDataDown(Chunk chunk, int x, int y, int z, MeshData meshData)
        {
            meshData.vertices.Add(new Vector3(x - 0.5f, y - 0.5f, z - 0.5f));
            meshData.vertices.Add(new Vector3(x + 0.5f, y - 0.5f, z - 0.5f));
            meshData.vertices.Add(new Vector3(x + 0.5f, y - 0.5f, z + 0.5f));
            meshData.vertices.Add(new Vector3(x - 0.5f, y - 0.5f, z + 0.5f));

            meshData.AddQuadTriangles();

            // Get neighbours
            Block bottom = chunk[x, y - 1, z];

            Block frontBottom = chunk[x, y - 1, z + 1];
            Block backBottom = chunk[x, y - 1, z - 1];

            Block leftBottom = chunk[x + 1, y - 1, z];
            Block rightBottom = chunk[x - 1, y - 1, z];

            Block frontBottomLeft = chunk[x + 1, y - 1, z + 1];
            Block frontBottomRight = chunk[x - 1, y - 1, z + 1];

            Block backBottomLeft = chunk[x + 1, y - 1, z - 1];
            Block backBottomRight = chunk[x - 1, y - 1, z - 1];

            int c1 = bottom.Light, c2 = bottom.Light, c3 = bottom.Light, c4 = bottom.Light;
            //int c1 = (bottom.Light + backBottom.Light + backBottomRight.Light + rightBottom.Light) / 4;   // Bottom Left
            //int c4 = (bottom.Light + frontBottom.Light + frontBottomRight.Light + rightBottom.Light) / 4; // Bottom Right 
            //int c3 = (bottom.Light + frontBottom.Light + frontBottomLeft.Light + leftBottom.Light) / 4;   // Top Right
            //int c2 = (bottom.Light + backBottom.Light + backBottomLeft.Light + leftBottom.Light) / 4;     // Top Left

            byte c1Byte = (byte)c1;
            byte c2Byte = (byte)c2;
            byte c3Byte = (byte)c3;
            byte c4Byte = (byte)c4;

            meshData.colors.Add(new Color32(c1Byte, c1Byte, c1Byte, 0));
            meshData.colors.Add(new Color32(c2Byte, c2Byte, c2Byte, 0));
            meshData.colors.Add(new Color32(c3Byte, c3Byte, c3Byte, 0));
            meshData.colors.Add(new Color32(c4Byte, c4Byte, c4Byte, 0));

            return meshData;
        }

        protected virtual MeshData FaceDataNorth(Chunk chunk, int x, int y, int z, MeshData meshData)
        {
            meshData.vertices.Add(new Vector3(x + 0.5f, y - 0.5f, z + 0.5f));
            meshData.vertices.Add(new Vector3(x + 0.5f, y + 0.5f, z + 0.5f));
            meshData.vertices.Add(new Vector3(x - 0.5f, y + 0.5f, z + 0.5f));
            meshData.vertices.Add(new Vector3(x - 0.5f, y - 0.5f, z + 0.5f));

            meshData.AddQuadTriangles();

            // Get neighbours
            Block front = chunk[x, y, z + 1];

            Block frontTop = chunk[x, y + 1, z + 1];
            Block frontBottom = chunk[x, y - 1, z + 1];

            Block frontLeft = chunk[x + 1, y, z + 1];
            Block frontRight = chunk[x - 1, y, z + 1];

            Block frontTopLeft = chunk[x + 1, y + 1, z + 1];
            Block frontTopRight = chunk[x - 1, y + 1, z + 1];

            Block frontBottomLeft = chunk[x + 1, y - 1, z + 1];
            Block frontBottomRight = chunk[x - 1, y - 1, z + 1];

            int c1 = front.Light, c2 = front.Light, c3 = front.Light, c4 = front.Light;
            //int c1 = (front.Light + frontBottom.Light + frontBottomRight.Light + frontRight.Light) / 4; // Bottom Left
            //int c2 = (front.Light + frontBottom.Light + frontBottomLeft.Light + frontLeft.Light) / 4;   // Bottom Right
            //int c3 = (front.Light + frontTop.Light + frontTopLeft.Light + frontLeft.Light) / 4;         // Top Right
            //int c4 = (front.Light + frontTop.Light + frontTopRight.Light + frontRight.Light) / 4;       // Top Left

            byte c1Byte = (byte)c1;
            byte c2Byte = (byte)c2;
            byte c3Byte = (byte)c3;
            byte c4Byte = (byte)c4;

            meshData.colors.Add(new Color32(c1Byte, c1Byte, c1Byte, 0));
            meshData.colors.Add(new Color32(c2Byte, c2Byte, c2Byte, 0));
            meshData.colors.Add(new Color32(c3Byte, c3Byte, c3Byte, 0));
            meshData.colors.Add(new Color32(c4Byte, c4Byte, c4Byte, 0));

            return meshData;
        }

        protected virtual MeshData FaceDataEast(Chunk chunk, int x, int y, int z, MeshData meshData)
        {
            meshData.vertices.Add(new Vector3(x + 0.5f, y - 0.5f, z - 0.5f));
            meshData.vertices.Add(new Vector3(x + 0.5f, y + 0.5f, z - 0.5f));
            meshData.vertices.Add(new Vector3(x + 0.5f, y + 0.5f, z + 0.5f));
            meshData.vertices.Add(new Vector3(x + 0.5f, y - 0.5f, z + 0.5f));

            meshData.AddQuadTriangles();

            // Get neighbours
            Block left = chunk[x + 1, y, z];

            Block leftTop = chunk[x + 1, y + 1, z];
            Block leftBottom = chunk[x + 1, y - 1, z];

            Block frontLeft = chunk[x + 1, y, z + 1];
            Block backLeft = chunk[x + 1, y, z - 1];

            Block frontTopLeft = chunk[x + 1, y + 1, z + 1];
            Block backTopLeft = chunk[x + 1, y + 1, z - 1];

            Block frontBottomLeft = chunk[x + 1, y - 1, z + 1];
            Block backBottomLeft = chunk[x + 1, y - 1, z - 1];

            int c1 = left.Light, c2 = left.Light, c3 = left.Light, c4 = left.Light;
            //int c1 = (left.Light + leftBottom.Light + backBottomLeft.Light + backLeft.Light) / 4;   // Bottom Left
            //int c2 = (left.Light + leftTop.Light + backTopLeft.Light + backLeft.Light) / 4;         // Top Left
            //int c3 = (left.Light + leftTop.Light + frontTopLeft.Light + frontLeft.Light) / 4;       // Top Right
            //int c4 = (left.Light + leftBottom.Light + frontBottomLeft.Light + frontLeft.Light) / 4; // Bottom Right

            byte c1Byte = (byte)c1;
            byte c2Byte = (byte)c2;
            byte c3Byte = (byte)c3;
            byte c4Byte = (byte)c4;

            meshData.colors.Add(new Color32(c1Byte, c1Byte, c1Byte, 0));
            meshData.colors.Add(new Color32(c2Byte, c2Byte, c2Byte, 0));
            meshData.colors.Add(new Color32(c3Byte, c3Byte, c3Byte, 0));
            meshData.colors.Add(new Color32(c4Byte, c4Byte, c4Byte, 0));
            return meshData;
        }

        protected virtual MeshData FaceDataSouth(Chunk chunk, int x, int y, int z, MeshData meshData)
        {
            meshData.vertices.Add(new Vector3(x - 0.5f, y - 0.5f, z - 0.5f));
            meshData.vertices.Add(new Vector3(x - 0.5f, y + 0.5f, z - 0.5f));
            meshData.vertices.Add(new Vector3(x + 0.5f, y + 0.5f, z - 0.5f));
            meshData.vertices.Add(new Vector3(x + 0.5f, y - 0.5f, z - 0.5f));

            meshData.AddQuadTriangles();

            // Get neighbours
            Block back = chunk[x, y, z - 1];

            Block backTop = chunk[x, y + 1, z - 1];
            Block backBottom = chunk[x, y - 1, z - 1];

            Block backLeft = chunk[x + 1, y, z - 1];
            Block backRight = chunk[x - 1, y, z - 1];

            Block backTopLeft = chunk[x + 1, y + 1, z - 1];
            Block backTopRight = chunk[x - 1, y + 1, z - 1];

            Block backBottomLeft = chunk[x + 1, y - 1, z - 1];
            Block backBottomRight = chunk[x - 1, y - 1, z - 1];

            int c1 = back.Light, c2 = back.Light, c3 = back.Light, c4 = back.Light;
            //int c1 = (back.Light + backBottom.Light + backBottomRight.Light + backRight.Light) / 4; // Bottom Left
            //int c2 = (back.Light + backTop.Light + backTopRight.Light + backRight.Light) / 4;       // Top Left
            //int c3 = (back.Light + backTop.Light + backTopLeft.Light + backLeft.Light) / 4;         // Top Right
            //int c4 = (back.Light + backBottom.Light + backBottomLeft.Light + backLeft.Light) / 4;   // Bottom Right

            byte c1Byte = (byte)c1;
            byte c2Byte = (byte)c2;
            byte c3Byte = (byte)c3;
            byte c4Byte = (byte)c4;

            meshData.colors.Add(new Color32(c1Byte, c1Byte, c1Byte, 0));
            meshData.colors.Add(new Color32(c2Byte, c2Byte, c2Byte, 0));
            meshData.colors.Add(new Color32(c3Byte, c3Byte, c3Byte, 0));
            meshData.colors.Add(new Color32(c4Byte, c4Byte, c4Byte, 0));
            return meshData;
        }

        protected virtual MeshData FaceDataWest(Chunk chunk, int x, int y, int z, MeshData meshData)
        {
            meshData.vertices.Add(new Vector3(x - 0.5f, y - 0.5f, z + 0.5f));
            meshData.vertices.Add(new Vector3(x - 0.5f, y + 0.5f, z + 0.5f));
            meshData.vertices.Add(new Vector3(x - 0.5f, y + 0.5f, z - 0.5f));
            meshData.vertices.Add(new Vector3(x - 0.5f, y - 0.5f, z - 0.5f));

            meshData.AddQuadTriangles();

            // Get neighbours
            Block right = chunk[x - 1, y, z];

            Block rightTop = chunk[x - 1, y + 1, z];
            Block rightBottom = chunk[x - 1, y - 1, z];

            Block frontRight = chunk[x - 1, y, z + 1];
            Block backRight = chunk[x - 1, y, z - 1];

            Block frontTopRight = chunk[x - 1, y + 1, z + 1];
            Block backTopRight = chunk[x - 1, y + 1, z - 1];

            Block frontBottomRight = chunk[x - 1, y - 1, z + 1];
            Block backBottomRight = chunk[x - 1, y - 1, z - 1];

            int c1 = right.Light, c2 = right.Light, c3 = right.Light, c4 = right.Light;
            //int c1 = (right.Light + rightBottom.Light + frontBottomRight.Light + frontRight.Light) / 4; // Bottom Left
            //int c2 = (right.Light + rightTop.Light + frontTopRight.Light + frontRight.Light) / 4;       // Top Left
            //int c3 = (right.Light + rightTop.Light + backTopRight.Light + backRight.Light) / 4;         // Top Right
            //int c4 = (right.Light + rightBottom.Light + backBottomRight.Light + backRight.Light) / 4;   // Bottom Right

            byte c1Byte = (byte)c1;
            byte c2Byte = (byte)c2;
            byte c3Byte = (byte)c3;
            byte c4Byte = (byte)c4;

            meshData.colors.Add(new Color32(c1Byte, c1Byte, c1Byte, 0));
            meshData.colors.Add(new Color32(c2Byte, c2Byte, c2Byte, 0));
            meshData.colors.Add(new Color32(c3Byte, c3Byte, c3Byte, 0));
            meshData.colors.Add(new Color32(c4Byte, c4Byte, c4Byte, 0));

            return meshData;
        }

        // Spread the block's light for a given area
        public static void LightningFloodArea(World world, WorldPos worldPos, int worldVisibleSizeY)
        {
            _lightningBlocks.Clear();

            int xMin = worldPos.X - 5;
            int xMax = worldPos.X + 5;
            int zMin = worldPos.Z - 5;
            int zMax = worldPos.Z + 5;
            int yMin = worldVisibleSizeY - 1;

            for (int x = xMin; x < xMax; x++)
            {
                for (int z = zMin; z < zMax; z++)
                {
                    for (int y = yMin; y >= 0; y--)
                    {
                        Block block = world[x, y, z];
                        if (block != null)
                            LightningFlood(world, x, y, z, block.Light, block);
                    }
                }
            }
        }

        private static void LightningFlood(World world, int x, int y, int z, int light, Block firstStepBlock = null)
        {
            Block block = world[x, y, z];
            if (firstStepBlock != null)
            {
                block = firstStepBlock;
            }
            else
            {
                block = world[x, y, z];
            }

            if (block == null || block.IsSolid())
                return;

            int blockLight = block.Light;

            // Skip decay if it's the first flood iteration
            if (firstStepBlock == null)
            {
                // Light Decay 
                // Note: if you want to create more lightning steps, increase the value bellow,
                // but remember to increase the number of affected neighbours on UpdateHeightMap (ln 267) and LightningFloodArea (ln 319)
                light -= Block.MinLight;

                if (light <= Block.MinLight)
                {
                    return;
                }

                if (blockLight >= light)
                {
                    return;
                }

                block.Light = light;
                _lightningBlocks.Add(new Vector3(x, y, z));
            }

            // Note: there is more optimized ways to do that. But I think that the recursive function
            // is the easiest way to understand the light flood concept
            LightningFlood(world, x, y + 1, z, light);
            LightningFlood(world, x, y - 1, z, blockLight);
            LightningFlood(world, x, y, z + 1, light);
            LightningFlood(world, x, y, z - 1, light);
            LightningFlood(world, x + 1, y, z, light);
            LightningFlood(world, x - 1, y, z, light);
        }
    }
}
