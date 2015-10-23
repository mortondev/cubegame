using System.Linq;
using Assets.Blocks;
using Assets.Rendering;
using UnityEngine;

namespace Assets
{
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(MeshCollider))]
    public class Chunk : MonoBehaviour
    {
        public static int ChunkSize = 16;
        public WorldPos WorldPos { get; set; }

        private readonly Block[,,] _blocks;
        private MeshFilter _meshFilter;
        private MeshCollider _meshCollider;
        private MeshRenderer _meshRenderer;
        private bool _update;

        public Chunk()
        {
            _blocks = new Block[ChunkSize, ChunkSize, ChunkSize];
        }

        void Start()
        {
            _meshFilter = gameObject.GetComponent<MeshFilter>();
            _meshCollider = gameObject.GetComponent<MeshCollider>();
            _meshRenderer = gameObject.GetComponent<MeshRenderer>();
        }

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

        void RenderMesh(MeshData meshData)
        {
            _meshFilter.mesh.Clear();
            _meshFilter.mesh.vertices = meshData.vertices.ToArray();
            _meshFilter.mesh.triangles = meshData.triangles.ToArray();
            _meshFilter.mesh.RecalculateNormals();

            var vertices = _meshFilter.mesh.vertices;
            var colors = new Color32[vertices.Length];

            var i = 0;
            while (i < vertices.Length)
            {
                colors[i] = Color.Lerp(Color.red, Color.green, vertices[i].y);
                i++;
            }

            _meshFilter.mesh.colors32 = colors;// new[] { Color.yellow, Color.red, Color.magenta, };
            
            //_meshRenderer.material.color = new Color(0f, 0.5f, 0f);
            //_meshRenderer.material.shader = Shader.Find("CubeShader");
        }
    }
}
