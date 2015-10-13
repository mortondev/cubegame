using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets
{
    public class World : MonoBehaviour
    {
        public void Start()
        {
            for (var i = 0; i < 10; i++)
            {
                var chunkObject = new GameObject("Chunk");
                chunkObject.transform.position = new Vector3(16 * i, 0, 0);
                chunkObject.AddComponent<Chunk>();
            }
        }

        public void Update()
        {

        }
    }
}
