using UnityEngine;
using System.Collections;
using Assets;

public class Chunk : MonoBehaviour
{
    private Block[ , , ] _blocks;
    public static int ChunkSize = 16;

	// Use this for initialization
	void Start () {

        _blocks = new Block[ChunkSize, ChunkSize, ChunkSize];

        for (var x = 0; x < ChunkSize; x++)
        {
            for (var y = 0; y < ChunkSize; y++)
            {
                for (var z = 0; z < ChunkSize; z++)
                {
                    _blocks[x, y, z] = new Block();
                }
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
