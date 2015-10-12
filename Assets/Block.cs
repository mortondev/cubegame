using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets
{
    public class Block
    {
        public Block()
        {
            Texture = new Texture2D(1, 1, TextureFormat.ARGB32, false);

            // set the pixel values
            Texture.SetPixel(0, 0,  new Color(1.0f, 1.0f, 1.0f, 0.5f));
            Texture.SetPixel(1, 0, Color.clear);
            Texture.SetPixel(0, 1, Color.white);
            Texture.SetPixel(1, 1, Color.black);

            // Apply all SetPixel calls
            Texture.Apply();
        }
        public Texture2D Texture { get; set; }
    }

    public class BlockMesh
    {
        
    }
}
