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
    public WorldPos WorldPos { get; set; }

    private Block[,,] _blocks;
    private MeshFilter _meshFilter;
    private MeshCollider _meshCollider;
    private MeshRenderer _meshRenderer;
    private bool _update = true;

    public Chunk()
    {
        _blocks = new Block[ChunkSize, ChunkSize, ChunkSize];
    }

    // Use this for initialization
    void Start()
    {
        _meshFilter = gameObject.GetComponent<MeshFilter>();
        _meshCollider = gameObject.GetComponent<MeshCollider>();
        _meshRenderer = gameObject.GetComponent<MeshRenderer>();

        //for (var x = 0; x < ChunkSize; x++)
        //{
        //    for (var y = 0; y < ChunkSize; y++)
        //    {
        //        for (var z = 0; z < ChunkSize; z++)
        //        {
        //            _blocks[x, y, z] = new BlockAir();
        //        }
        //    }
        //}

        //var chunkMeshGenerator = new ChunkMeshGenerator();
        //var meshData = chunkMeshGenerator.CreateChunkMesh(this);

        //RenderMesh(meshData);
    }

    // Update is called once per frame
    void Update()
    {
        if (!_update) return;

        var chunkMeshGenerator = new ChunkMeshGenerator();
        var meshData = chunkMeshGenerator.CreateChunkMesh(this);

        RenderMesh(meshData);
        _update = false;
    }

    private static bool InRange(int index)
    {
        return index >= 0 && index < ChunkSize;
    }

    public Block GetBlock(int x, int y, int z)
    {
        if (!InRange(x) || !InRange(y) || !InRange(z))
            return new BlockAir();

        return _blocks[x, y, z];
    }

    public void SetBlock<T>(int x, int y, int z) where T : Block, new()
    {
        _blocks[x, y, z] = new T();
        _update = true;
    }

    // Sends the calculated mesh information to the mesh and collision components
    void RenderMesh(MeshData meshData)
    {
        _meshFilter.mesh.Clear();
        _meshFilter.mesh.vertices = meshData.vertices.ToArray();
        _meshFilter.mesh.triangles = meshData.triangles.ToArray();
        _meshFilter.mesh.RecalculateNormals();
        
        _meshRenderer.material.globalIlluminationFlags = MaterialGlobalIlluminationFlags.RealtimeEmissive;
        _meshRenderer.useLightProbes = true;
        _meshRenderer.material.color = new Color(0, 1, 0);
    }
}
