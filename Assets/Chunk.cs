using UnityEngine;
using System.Collections;
using System.Linq;
using Assets;
using Assets.Blocks;
using Assets.Rendering;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class Chunk : MonoBehaviour
{
    private Block[ , , ] _blocks;
    public static int ChunkSize = 16;

    MeshFilter filter;
    MeshCollider coll;

    // Use this for initialization
    void Start()
    {
        filter = gameObject.GetComponent<MeshFilter>();
        coll = gameObject.GetComponent<MeshCollider>();

        _blocks = new Block[ChunkSize, ChunkSize, ChunkSize];

        for (var x = 0; x < ChunkSize; x++)
        {
            for (var y = 0; y < ChunkSize; y++)
            {
                for (var z = 0; z < ChunkSize; z++)
                {
                    _blocks[x, y, z] = new BlockAir();
                }
            }
        }

        _blocks[4, 2, 3] = new Block();
        _blocks[1, 4, 10] = new Block();
        _blocks[6, 4, 3] = new Block();

        var chunkMeshGenerator = new ChunkMeshGenerator();
        var meshData = chunkMeshGenerator.CreateChunkMesh(this);

        RenderMesh(meshData);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private static bool InRange(int index)
    {
        return index >= 0 && index < ChunkSize;
    }

    public Block GetBlock(int x, int y, int z)
    {
        if(!InRange(x) || !InRange(y) || !InRange(z))
            return new BlockAir();

        return _blocks[x, y, z];
    }

    // Sends the calculated mesh information to the mesh and collision components
    void RenderMesh(MeshData meshData)
    {
        filter.mesh.Clear();
        filter.mesh.vertices = meshData.vertices.ToArray();
        filter.mesh.triangles = meshData.triangles.ToArray();
    }
}
