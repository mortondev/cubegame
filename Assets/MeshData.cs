using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets
{
    public class MeshData
    {
        public IList<Vector3> vertices = new List<Vector3>();
        public IList<int> triangles = new List<int>();

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