using System;
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
        private readonly Block[,,] _blocks;

        public static int ChunkSize = 16;

        public World World;
        public WorldPos WorldPos { get; set; }

        public MeshFilter MeshFilter;
        public MeshCollider MeshCollider;
        public MeshRenderer MeshRenderer;

        private bool _update;

        public Chunk()
        {
            _blocks = new Block[ChunkSize, ChunkSize, ChunkSize];
        }

        void Start()
        {
            MeshFilter = gameObject.GetComponent<MeshFilter>();
            MeshCollider = gameObject.GetComponent<MeshCollider>();
            MeshRenderer = gameObject.GetComponent<MeshRenderer>();
        }

        void Update()
        {
            if (!_update) return;

            var chunkMeshGenerator = new ChunkMeshGenerator();
            chunkMeshGenerator.CreateChunkMesh(this);

            _update = false;
        }

        public void SetDirty()
        {
            _update = true;
        }

        private static bool InRange(int index)
        {
            return index >= 0 && index < ChunkSize;
        }

        public Block GetBlock(int x, int y, int z)
        {
            if (!InRange(x) || !InRange(y) || !InRange(z))
                return World.GetBlock(new WorldPos(WorldPos.X + x, WorldPos.X + y, WorldPos.X + z));

            return _blocks[x, y, z];
        }

        public Block this[int x, int y, int z]
        {
            get { return GetBlock(x, y, z); }
        }

        public void SetBlock<T>(int x, int y, int z) where T : Block, new()
        {
            _blocks[x, y, z] = new T { _chunk = this };

            _update = true;
        }
    }
}