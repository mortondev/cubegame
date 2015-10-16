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
    public static int ChunkSize = 16;

    private Block[ , , ] _blocks;
    private MeshFilter _meshFilter;
    private MeshCollider _meshCollider;

    // Use this for initialization
    void Start()
    {
        _meshFilter = gameObject.GetComponent<MeshFilter>();
        _meshCollider = gameObject.GetComponent<MeshCollider>();

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
        _meshFilter.mesh.Clear();
        _meshFilter.mesh.vertices = meshData.vertices.ToArray();
        _meshFilter.mesh.triangles = meshData.triangles.ToArray();
    }
}
