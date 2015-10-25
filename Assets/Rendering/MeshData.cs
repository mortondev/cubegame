using System.Collections.Generic;
using UnityEngine;

namespace Assets.Rendering
{
    public class MeshData
    {
        public IList<Vector3> vertices = new List<Vector3>();
        public IList<int> triangles = new List<int>();
        public IList<Color32> colors = new List<Color32>(); 

        public void AddQuadTriangles()
        {
            triangles.Add(vertices.Count - 4);
            triangles.Add(vertices.Count - 3);
            triangles.Add(vertices.Count - 2);

            triangles.Add(vertices.Count - 4);
            triangles.Add(vertices.Count - 2);
            triangles.Add(vertices.Count - 1);
        }
    }
}