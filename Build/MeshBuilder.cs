using System.Collections.Generic;

using UnityEngine;

namespace ExoLabs.MeshTools
{
    public sealed class MeshBuilder
    {
        MeshFilter meshFilter;
        MeshRenderer meshRenderer;
        MeshCollider meshCollider;

        bool use32BitIndices = false;
        List<Vector3> vertices = new();
        List<int> triangles = new();
        readonly List<Vector2> uv0 = new();
        readonly List<Vector2> uv1 = new();
        readonly List<Color> colours = new();
        readonly Dictionary<Vector3, int> vertexLookup = new();


        public void SetupComponents(GameObject target)
        {
            meshFilter = target.GetComponent<MeshFilter>();
            if (meshFilter == null)
                meshFilter = target.AddComponent<MeshFilter>();
            meshRenderer = target.GetComponent<MeshRenderer>();
            if (meshRenderer == null)
                meshRenderer = target.AddComponent<MeshRenderer>();
            meshCollider = target.GetComponent<MeshCollider>();
            if (meshCollider == null)
                meshCollider = target.AddComponent<MeshCollider>();
        }
        public void ClearMesh()
        {
            if (meshFilter != null)
                meshFilter.mesh = null;
            if (meshCollider != null)
                meshCollider.sharedMesh = null;
        }
        public void ApplyMaterial(Material material)
        {
            if (meshRenderer != null)
                meshRenderer.sharedMaterial = material;
        }
        public int AddVertex(Vector3 value, bool smooth = true) => smooth ? AddSmoothVertex(value) : AddFlatVertex(value);

        int AddFlatVertex(Vector3 value)
        {
            vertices.Add(value);    // possibly a deliberate duplicate
            int last = vertices.Count - 1;
            if (!vertexLookup.ContainsKey(value)) // avoid dupes on the lookup
                vertexLookup.Add(value, last);
            return last;
        }
        int AddSmoothVertex(Vector3 value)
        {
            if (vertexLookup.TryGetValue(value, out int index))
                return index;
            else
            {
                vertices.Add(value);
                int last = vertices.Count - 1;
                vertexLookup.Add(value, last);
                return last;
            }
        }
        public void AddVertexColour(Color colour) => colours.Add(colour);
        public void AddUV0(Vector2 uv) => uv0.Add(uv);
        public void AddUV1(Vector2 uv) => uv1.Add(uv);

        public void AddUV0(params Vector2[] uvs) => uv0.AddRange(uvs);
        public void AddUV1(params Vector2[] uvs) => uv1.AddRange(uvs);
        public void AddTriangle(int v0, int v1, int v2)
        {
            triangles.Add(v0);
            triangles.Add(v1);
            triangles.Add(v2);
        }
        public void AddQuad(int v0, int v1, int v2, int v3)
        {
            triangles.Add(v0);
            triangles.Add(v1);
            triangles.Add(v2);
            triangles.Add(v2);
            triangles.Add(v3);
            triangles.Add(v0);
        }
        public void AddQuad(Vector3 v0, Vector3 v1, Vector3 v2, Vector3 v3, bool smooth = true)
        {
            var iv0 = AddVertex(v0, smooth);
            var iv1 = AddVertex(v1, smooth);
            var iv2 = AddVertex(v2, smooth);
            var iv3 = AddVertex(v3, smooth);
            AddQuad(iv0, iv1, iv2, iv3);
        }

        public void AddQuad(Vector3 v0, Vector3 v1, Vector3 v2, Vector3 v3,
                        Vector2 uv0, Vector2 uv1, Vector2 uv2, Vector2 uv3,
                        bool smooth = false)
        {
            var iv0 = AddVertex(v0, smooth);
            var iv1 = AddVertex(v1, smooth);
            var iv2 = AddVertex(v2, smooth);
            var iv3 = AddVertex(v3, smooth);

            AddUV0(uv0, uv1, uv2, uv3);
            AddQuad(iv0, iv1, iv2, iv3);
        }
        public void AddTriangle(Vector3 v0, Vector3 v1, Vector3 v2, bool smooth = true)
        {
            var iv0 = AddVertex(v0, smooth);
            var iv1 = AddVertex(v1, smooth);
            var iv2 = AddVertex(v2, smooth);
            AddTriangle(iv0, iv1, iv2);
        }

        public void AddTriangles(IEnumerable<int> triangleIndices) => triangles.AddRange(triangleIndices);

        public void ClearMeshData()
        {
            vertices.Clear();
            triangles.Clear();
            uv0.Clear();
            uv1.Clear();
            colours.Clear();
            vertexLookup.Clear();
        }

        public void BuildFromPointCloud(List<Vector3> pointCloud, string meshName = "Mesh")
        {
            var calc = new GK.ConvexHullCalculator();
            var normals = new List<Vector3>();
            calc.GenerateHull(pointCloud, true, ref vertices, ref triangles, ref normals);
            Mesh mesh = new()
            {
                name = meshName,
                vertices = vertices.ToArray(),
                triangles = triangles.ToArray(),
                normals = normals.ToArray(),
            };
            if (vertices.Count > 65535) use32BitIndices = true;
            if (use32BitIndices) mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
            meshCollider.sharedMesh = mesh;
            meshFilter.sharedMesh = mesh;
        }

        public void BuildMesh(GameObject target, string meshName = "Mesh")
        {
            SetupComponents(target);
            Mesh mesh = new()
            {
                name = meshName,
                vertices = vertices.ToArray(),
                triangles = triangles.ToArray(),
            };
            if (colours.Count > 0) mesh.colors = colours.ToArray();
            if (uv0.Count > 0) mesh.SetUVs(0, uv0);
            if (uv1.Count > 0) mesh.SetUVs(1, uv1);

            mesh.Optimize();
            mesh.RecalculateNormals();
            mesh.RecalculateTangents();
            mesh.RecalculateBounds();

            if (vertices.Count > 65535) use32BitIndices = true;
            if (use32BitIndices) mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
            meshCollider.convex = true;
            meshCollider.sharedMesh = mesh;
            Debug.Log(meshCollider.GeometryHolder.Type);
            Debug.Log(meshCollider.sharedMesh.GetIndexCount(0));
            meshFilter.sharedMesh = meshCollider.sharedMesh;
            Debug.Log(meshFilter.sharedMesh.GetIndexCount(0));
        }

    }
}