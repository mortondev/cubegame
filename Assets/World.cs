﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using Assets.TerrainGen;
using UnityEngine;

namespace Assets
{
    public class World : MonoBehaviour
    {
        IDictionary<WorldPos, Chunk> _chunks = new Dictionary<WorldPos, Chunk>(); 

        public void Start()
        {
            for (var x = 0; x < 16; x++)
            {
                for (var z = 0; z < 16; z++)
                {
                    CreateChunk(x * 16, 0, z * 16);
                }
            }
        }

        public void Update()
        {

        }

        public void CreateChunk(int x, int y, int z)
        {
            var worldPos = new WorldPos(x, y, z);
            var chunkObject = new GameObject("Chunk");
            chunkObject.transform.position = new Vector3(x, y, z);
            chunkObject.AddComponent<Chunk>();

            var chunk = chunkObject.GetComponent<Chunk>();
            chunk.WorldPos = worldPos;

            var terrainGenerator = new TerrainGenerator();
            chunk = terrainGenerator.GenerateChunk(chunk);

            _chunks.Add(worldPos, chunk);
        }
    }
}
