﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class TileMap : MonoBehaviour {
    public int size_x = 20;
    public int size_y = 10;
    public float tileSize = 1f; 

	// Use this for initialization
	void Start () {
        BuildTileMap();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void BuildTileMap() {
        int numTiles = size_x * size_y;

        int vsize_x = size_x + 1;
        int vsize_y = size_y + 1;
        int numVertices = vsize_x * vsize_y;

        Vector3[] vertices = new Vector3[numVertices];
        Vector3[] normals = new Vector3[numVertices];
        Vector2[] uv = new Vector2[numVertices];

        int[] triangleVertices = new int[numTiles * 2 * 3]; // numTiles * numTrianglesPerTile * numVerticesPerTriangle


        for(int y=0; y < vsize_y; y++) {
            for(int x=0; x < vsize_x; x++) {
                int verticeIndex = y * vsize_x + x;     // verticeRow * rowSize + verticeInRow
                vertices[verticeIndex] = new Vector3(x * tileSize, y * tileSize);
                normals[verticeIndex] = Vector3.forward;
                uv[verticeIndex] = new Vector2((float)x / vsize_x, (float)y / vsize_y);
            }
        }

        for(int y=0; y < size_y; y++) {
            for(int x=0; x < size_x; x++) {
                int firstTriangleEdgeOfTile = (y * size_x + x) * 2 * 3;     // (tileRow * tileRowSize + tileInRow) * numTrianglesPerTile * numVerticesPerTriangle
                int firstVerticeOfTile = (y * vsize_x + x);
                triangleVertices[firstTriangleEdgeOfTile + 0] = firstVerticeOfTile;                     // Bottom Left
                triangleVertices[firstTriangleEdgeOfTile + 1] = firstVerticeOfTile + vsize_x;           // Top Left (1 row higher)
                triangleVertices[firstTriangleEdgeOfTile + 2] = firstVerticeOfTile + vsize_x + 1;       // Top Right (1 row higher & 1 vertice right)

                triangleVertices[firstTriangleEdgeOfTile + 3] = firstVerticeOfTile;                // Bottom Left
                triangleVertices[firstTriangleEdgeOfTile + 4] = firstVerticeOfTile + vsize_x + 1;  // Top Right (1 row higher & 1 vertice right)
                triangleVertices[firstTriangleEdgeOfTile + 5] = firstVerticeOfTile + 1;            // Bottom Right (1 Vertice to the right)
            }
        }

        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangleVertices;
        mesh.normals = normals;
        mesh.uv = uv;

        MeshFilter mesh_filter = GetComponent<MeshFilter>();
        MeshRenderer mesh_renderer = GetComponent<MeshRenderer>();
        MeshCollider mesh_collider = GetComponent<MeshCollider>();

        mesh_filter.mesh = mesh;
    }
}
