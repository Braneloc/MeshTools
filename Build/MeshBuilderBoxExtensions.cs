using System;
using UnityEngine;

namespace ExoLabs.MeshTools
{
    public static class MeshBuilderBoxExtensions
    {
        public static void AddExtrudedQuad(this MeshBuilder builder, Vector3 v0, Vector3 v1, Vector3 v2, Vector3 v3,
                              Vector3 extrude,
                              bool smooth = false)
        {
            var ov0 = v0 + extrude;
            var ov1 = v1 + extrude;
            var ov2 = v2 + extrude;
            var ov3 = v3 + extrude;

            var iv0 = builder.AddVertex(ov0, smooth);
            var iv1 = builder.AddVertex(ov1, smooth);
            var iv2 = builder.AddVertex(ov2, smooth);
            var iv3 = builder.AddVertex(ov3, smooth);
            builder.AddQuad(iv0, iv1, iv2, iv3);
        }

        public static void AddBox(this MeshBuilder builder, Vector3 centre, Vector3 size, BoxFaces faces, bool smooth = false)
        {
            Vector3 halfSize = size * 0.5f;

            Vector3 corner000 = centre + new Vector3(-halfSize.x, -halfSize.y, -halfSize.z);
            Vector3 corner100 = centre + new Vector3(halfSize.x, -halfSize.y, -halfSize.z);
            Vector3 corner110 = centre + new Vector3(halfSize.x, halfSize.y, -halfSize.z);
            Vector3 corner010 = centre + new Vector3(-halfSize.x, halfSize.y, -halfSize.z);

            Vector3 corner001 = centre + new Vector3(-halfSize.x, -halfSize.y, halfSize.z);
            Vector3 corner101 = centre + new Vector3(halfSize.x, -halfSize.y, halfSize.z);
            Vector3 corner111 = centre + new Vector3(halfSize.x, halfSize.y, halfSize.z);
            Vector3 corner011 = centre + new Vector3(-halfSize.x, halfSize.y, halfSize.z);

            Vector2 uv00 = new Vector2(0f, 0f);
            Vector2 uv10 = new Vector2(1f, 0f);
            Vector2 uv11 = new Vector2(1f, 1f);
            Vector2 uv01 = new Vector2(0f, 1f);

            if (faces.HasFlag(BoxFaces.Back))
            {
                builder.AddQuad(corner010, corner110, corner100, corner000, smooth); // back
                builder.AddUV0(uv01, uv11, uv10, uv00);
            }

            if (faces.HasFlag(BoxFaces.Forward))
            {
                builder.AddQuad(corner001, corner101, corner111, corner011, smooth); // front
                builder.AddUV0(uv00, uv10, uv11, uv01);
            }

            if (faces.HasFlag(BoxFaces.Left))
            {
                builder.AddQuad(corner000, corner001, corner011, corner010, smooth); // left
                builder.AddUV0(uv10, uv11, uv01, uv00);
            }

            if (faces.HasFlag(BoxFaces.Right))
            {
                builder.AddQuad(corner110, corner111, corner101, corner100, smooth); // right
                builder.AddUV0(uv01, uv11, uv10, uv00);
            }

            if (faces.HasFlag(BoxFaces.Down))
            {
                builder.AddQuad(corner100, corner101, corner001, corner000, smooth); // bottom
                builder.AddUV0(uv10, uv11, uv01, uv00);
            }

            if (faces.HasFlag(BoxFaces.Up))
            {
                builder.AddQuad(corner010, corner011, corner111, corner110, smooth); // top
                builder.AddUV0(uv00, uv01, uv11, uv10);
            }
        }

        public static void AddExtrudedBox(this MeshBuilder builder, Vector3 centre, Vector3 size, BoxFaces faces, float thickness = 0.3f, bool smooth = false)
        {
            Vector3 halfSize = size * 0.5f;

            Vector3 corner000 = centre + new Vector3(-halfSize.x, -halfSize.y, -halfSize.z);
            Vector3 corner100 = centre + new Vector3(halfSize.x, -halfSize.y, -halfSize.z);
            Vector3 corner110 = centre + new Vector3(halfSize.x, halfSize.y, -halfSize.z);
            Vector3 corner010 = centre + new Vector3(-halfSize.x, halfSize.y, -halfSize.z);

            Vector3 corner001 = centre + new Vector3(-halfSize.x, -halfSize.y, halfSize.z);
            Vector3 corner101 = centre + new Vector3(halfSize.x, -halfSize.y, halfSize.z);
            Vector3 corner111 = centre + new Vector3(halfSize.x, halfSize.y, halfSize.z);
            Vector3 corner011 = centre + new Vector3(-halfSize.x, halfSize.y, halfSize.z);

            Vector2 uv00 = new Vector2(0f, 0f);
            Vector2 uv10 = new Vector2(1f, 0f);
            Vector2 uv11 = new Vector2(1f, 1f);
            Vector2 uv01 = new Vector2(0f, 1f);

            if (faces.HasFlag(BoxFaces.Back))
            {
                Vector3 offset = Vector3.back * thickness;
                builder.AddExtrudedQuad(corner010, corner110, corner100, corner000, offset, smooth); // back
                builder.AddUV0(uv01, uv11, uv10, uv00);
            }

            if (faces.HasFlag(BoxFaces.Forward))
            {
                Vector3 offset = Vector3.forward * thickness;
                builder.AddExtrudedQuad(corner001, corner101, corner111, corner011, offset, smooth); // front
                builder.AddUV0(uv00, uv10, uv11, uv01);
            }

            if (faces.HasFlag(BoxFaces.Left))
            {
                Vector3 offset = Vector3.left * thickness;
                builder.AddExtrudedQuad(corner000, corner001, corner011, corner010, offset, smooth); // left
                builder.AddUV0(uv10, uv11, uv01, uv00);
            }

            if (faces.HasFlag(BoxFaces.Right))
            {
                Vector3 offset = Vector3.right * thickness;
                builder.AddExtrudedQuad(corner110, corner111, corner101, corner100, offset, smooth); // right
                builder.AddUV0(uv01, uv11, uv10, uv00);
            }

            if (faces.HasFlag(BoxFaces.Down))
            {
                Vector3 offset = Vector3.down * thickness;
                builder.AddExtrudedQuad(corner100, corner101, corner001, corner000, offset, smooth); // bottom
                builder.AddUV0(uv10, uv11, uv01, uv00);
            }

            if (faces.HasFlag(BoxFaces.Up))
            {
                Vector3 offset = Vector3.up * thickness;
                builder.AddExtrudedQuad(corner010, corner011, corner111, corner110, offset, smooth); // top
                builder.AddUV0(uv00, uv01, uv11, uv10);
            }
        }
    }
    [Flags]
    public enum BoxFaces
    {
        None = 0,
        Left = 1 << 0,
        Right = 1 << 1,
        Down = 1 << 2,
        Up = 1 << 3,
        Back = 1 << 4,
        Forward = 1 << 5,
        All = Left | Right | Down | Up | Back | Forward,
    }
}